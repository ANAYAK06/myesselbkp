<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AmendSPPO.aspx.cs"
    Inherits="AmendSPPO" Title="Amended SPPO" EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            //debugger;
            var objs = new Array("<%=ddlvendor.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlpono.ClientID %>", "<%=txtpodate.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var GridViewold = document.getElementById("<%=gvDetails.ClientID %>");
            if (GridViewold != null) {
                for (var rowCount = 1; rowCount < GridViewold.rows.length - 1; rowCount++) {
                    if (GridViewold.rows(rowCount).cells(6).children[0].isDisabled == false) {
                        if (GridViewold.rows(rowCount).cells(6).children[0].value != "Select") {
                            if (GridViewold.rows(rowCount).cells(7).children[0].value == "" || GridViewold.rows(rowCount).cells(7).children[0].value == "0") {
                                window.alert("Amended Qty Required");
                                return false;
                            }
                        }
                    }
                }
            }
            var GridView = document.getElementById("<%=gvDetailsnew.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (rowCount >= 1) {
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
                        if (GridView.rows(rowCount).cells(2).children(0).value == "") {
                            window.alert("Please Add Items");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(3).children(0).value == "") {
                            window.alert("Please Insert Unit");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(4).children(0).value == "") {
                            window.alert("Please Insert Quantity");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(5).children(0).value == "") {
                            window.alert("Please Insert Rate");
                            return false;
                        }
                    }
                }
            }
            if (GridViewold == null && GridView == null) {
                window.alert("Invalid Amendment");
                return false;
            }
            var GridView2 = document.getElementById("<%=grdterms.ClientID %>");
            if (GridView2 != null) {
                var count = 0;
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(0).children[0].isDisabled == true) {
                        count = 1;
                    }
                    if (GridView2.rows(rowCount).cells(2).children[0].value == "") {
                        count = 0;
                        window.alert("Please Add Terms and Conditions");
                        return false;
                    }
                    if (GridView2.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                        count = 0;
                        window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                        return false;
                    }
                    if (GridView2.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                        window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                        return false;
                    }
                    if (GridView2.rows(rowCount).cells(0).children(0).checked == false) {
                        count = 0;
                        window.alert("Please verify Terms and Conditions");
                        return false;
                    }
                    if (GridView2.rows(rowCount).cells(0).children[0].isDisabled == false) {
                        count = 0;
                    }

                }
                if (count == 1) {
                    window.alert("Please Add Terms and Conditions for amendment");
                    return false;
                }
            }
            var txtpass1 = document.getElementById("<%=txttotalamt.ClientID %>").value;
            if (txtpass1 == "" || txtpass1 == "0") {
                window.alert("Invalid Amendment");
                return false;
            }
            else {
                return true;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
        function validate1() {
            //debugger;
            var objs = new Array("<%=txpovalue.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var GridView = document.getElementById("<%=GridView2.ClientID %>");
            //debugger;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (rowCount == 1) {
                        if (GridView.rows(rowCount).cells(2).children[0].value == "") {
                            window.alert("Please Insert Item Description");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            window.alert("Comma (,) are not allowed in Add Items");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Items");
                            return false;
                        }
                    }
                    if (rowCount > 1) {
                        if (GridView.rows(rowCount).cells(2).children[0].value == "") {
                            window.alert("Please Insert Item Details");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
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
            var GridView1 = document.getElementById("<%=grdpodesc.ClientID %>");
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
            document.getElementById("<%=btnupdate.ClientID %>").style.display = 'none';
            return true;
        }
        function chksplcharacters() {
            //debugger;
            var Grid = document.getElementById("<%=gvDetailsnew.ClientID %>");
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
        function chksplcharactersamend() {
            //debugger;
            var Grid = document.getElementById("<%=GridView2.ClientID %>");
            if (Grid != null) {
                for (var rowCount = 1; rowCount < Grid.rows.length - 1; rowCount++) {
                    if (Grid.rows(rowCount).cells(2).children[0].value != "") {
                        if (Grid.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            window.alert("Comma (,) are not allowed in Item Description");
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
                    if (gvdterms.rows(rowCount).cells(2).children[0].value != "") {
                        if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                            gvdterms.rows(rowCount).cells(2).children[0].value = "";
                            window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                            return false;
                        }
                    }
                    if (gvdterms.rows(rowCount).cells(2).children[0].value != "") {
                        if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                            gvdterms.rows(rowCount).cells(2).children[0].value = "";
                            window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                            return false;
                        }
                    }
                }
            }
        }
        function chksplcharactersdescamend() {
            //debugger;
            var gvdterms = document.getElementById("<%=grdpodesc.ClientID %>");
            if (gvdterms != null) {
                for (var rowCount = 1; rowCount < gvdterms.rows.length - 1; rowCount++) {
                    if (gvdterms.rows(rowCount).cells(2).children[0].value != "") {
                        if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                            gvdterms.rows(rowCount).cells(2).children[0].value = "";
                            window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                            return false;
                        }
                    }
                    if (gvdterms.rows(rowCount).cells(2).children[0].value != "") {
                        if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                            gvdterms.rows(rowCount).cells(2).children[0].value = "";
                            window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                            return false;
                        }
                    }
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
    <table width="100%">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td align="center" style="width: 850px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table style="vertical-align: middle; width: 850px">
                            <tr valign="top" align="center">
                                <td align="center">
                                    <table style="vertical-align: middle; width: 850px">
                                        <tr>
                                            <td style="height: 20px"></td>
                                        </tr>
                                        <tr>
                                            <th align="center">
                                                <asp:Label CssClass="eslbl" ID="lblvendor" runat="server" Text="Amend Service Provider PO"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="trAmendedposnno" runat="server" align="center" style="border: solid 2px gray; width: 800px">
                                            <td>
                                                <table style="border: solid 2px gray; width: 800px">
                                                    <tr>
                                                        <td colspan="4" style="height: 30px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Vendor ID"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlvendor" Width="200px" CssClass="esddown" AutoPostBack="true"
                                                                ToolTip="Vendor" runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged1">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="Cascadingvenid" runat="server" TargetControlID="ddlvendor"
                                                                ServicePath="cascadingDCA.asmx" PromptText="Select VendorId" Category="venid"
                                                                LoadingText="Please Wait" ServiceMethod="ven_id">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblcccode" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlcccode" Width="200px" runat="server" CssClass="esddown"
                                                                AutoPostBack="true" ToolTip="CC Code" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                                <asp:ListItem Value="Select CC Code"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="pono"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlpono" runat="server" CssClass="esddown" ToolTip=" PONO"
                                                                Width="200px" OnSelectedIndexChanged="ddlpono_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="Select PONO"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:HiddenField ID="h1" runat="server" />
                                                            <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtpodate" runat="server" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                onkeypress="return false;" CssClass="estbox" ToolTip="Date" Width="200px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtpodate" OnClientDateSelectionChanged="checkDate"
                                                                TargetControlID="txtpodate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="Actual PO Value"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="lblActpovalue" CssClass="estbox" runat="server" Font-Bold="true"
                                                                ForeColor="black" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                Text="0" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Actual PO Balance"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="lblActpobalance" CssClass="estbox" runat="server" Font-Bold="true"
                                                                ForeColor="black" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                Text="0" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                Width="100%" GridLines="None" ShowFooter="true" OnRowDataBound="gvDetails_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectitems" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtitemdesc" onkeyup="this.value = this.value.replace(/^[0-9 ,]+$/, '')"
                                                                                runat="server" Width="400px" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Item Description Required"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtitemdesc"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtunit" onkeyup="this.value = this.value.replace(/^[0-9 ,]+$/, '')"
                                                                                runat="server" Width="50px" onblur="chksplcharacters()" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Unit Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtunit"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="POQuantity" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtquantity" runat="server" Width="50px" onkeypress="return numericValidationu(this);" /><br />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtrate" runat="server" Width="50px" onkeypress="return numericValidation(this);"
                                                                                onkeyup="Calculate();" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Rate Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtrate"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                        ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddltype" Font-Size="8" Width="60px" ToolTip="Type" runat="server"
                                                                                onchange="checkvaliddata(this)">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="AmendPOQty" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtamendquantity" runat="server" Width="50px" Text="0" onkeypress="return numericValidationu(this);"
                                                                                onkeyup="Check();Calculate();" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Amend Qty Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtamendquantity"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtamount" runat="server" Width="75px" Text="0" onKeyPress="javascript: return false;"
                                                                                onKeyDown="javascript: return false;" /><br />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                                                    <asp:BoundField DataField="Id" Visible="false" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td colspan="4" align="left">
                                                            <button name="button" onclick="AddItems()" id="btnadditem" runat="server" type="button">
                                                                Add New Items</button>
                                                            <button name="button" id="btnremoveitem" runat="server" onclick="RemoveItems()" type="button">
                                                                Remove New Items</button>
                                                            <asp:Button ID="btnremoveitems" runat="server" Style="display: none;" Text="" OnClick="btnremoveitems_Click" />
                                                            <asp:Button ID="btnadditems" runat="server" Style="display: none;" Text="" OnClick="btnAdditems_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr id="tradditems" runat="server">
                                                        <td colspan="4">
                                                            <asp:GridView ID="gvDetailsnew" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                Width="100%" GridLines="None" ShowFooter="true" OnRowDeleting="gvDetailsnew_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectnew" runat="server" />
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
                                                                            <asp:TextBox ID="txtitemdescnew" runat="server" onkeyup="this.value = this.value.replace(/^[,/]+$/, '')"
                                                                                Width="450px" onblur="chksplcharacters()" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Item Description Required"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtitemdescnew"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtunitnew" runat="server" onblur="chksplcharacters()" onkeyup="this.value = this.value.replace(/^[0-9 ,]+$/, '')"
                                                                                Width="50px" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Unit Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtunitnew"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtquantitynew" runat="server" Width="50px" onkeypress="return numericValidationu(this);"
                                                                                onkeyup="Calculatenew();" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Qty Req" Display="Dynamic"
                                                                                ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtquantitynew"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Our Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtourrate" runat="server" Width="50px" onkeypress="return numericValidationourrate(this);" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Rate Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtourrate"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PRW Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtprwrate" runat="server" Width="50px" onkeypress="return numericValidationprw(this);" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Rate Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtprwrate"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtratenew" runat="server" Width="50px" onkeypress="return numericValidation(this);"
                                                                                onkeyup="Calculatenew();" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Rate Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtratenew"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtamountnew" runat="server" Width="100px" onKeyPress="javascript: return false;"
                                                                                onKeyDown="javascript: return false;" /><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Amount Req"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtamountnew"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="btnAddnew" runat="server" ValidationGroup="valGroup12" OnClick="btnAddnew_Click"
                                                                                ImageUrl="~/images/imgadd1.gif" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr align="right" id="tramounts" runat="server">
                                                        <td colspan="1" align="center" style="width: 150px">
                                                            <asp:HiddenField ID="hfreturnAmt" runat="server" Value="0" />
                                                            <asp:Label ID="lblreturnAmt" runat="server" CssClass="eslbl" Width="75px" Font-Size="12px"
                                                                Text="Amendment(-) Value:"></asp:Label>
                                                            <asp:TextBox ID="txtreturnAmt" CssClass="estbox" runat="server" Font-Bold="true"
                                                                ForeColor="White" BackColor="Red" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                Text="0" Width="75px"></asp:TextBox>
                                                        </td>
                                                        <td colspan="1" align="center" style="width: 125px">
                                                            <asp:HiddenField ID="hfamendamt" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hfamendamt1" runat="server" Value="0" />
                                                            <asp:Label ID="lblamendamt" runat="server" CssClass="eslbl" Width="75px" Font-Size="12px"
                                                                Text="Amendment(+) Value:"></asp:Label>
                                                            <asp:TextBox ID="txtamendamt" CssClass="estbox" runat="server" Font-Bold="true" ForeColor="White"
                                                                BackColor="Green" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                Text="0" Width="75px"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" align="center" style="width: 125px">
                                                            <asp:HiddenField ID="hftotalpo" runat="server" />
                                                            <asp:Label ID="Label10" runat="server" CssClass="eslbl" Width="75px" Font-Size="12px"
                                                                Text="Total PO Value:"></asp:Label>
                                                            <asp:TextBox ID="txttotalamt" CssClass="estbox" runat="server" Font-Bold="true" onKeyPress="javascript: return false;"
                                                                onKeyDown="javascript: return false;" Text="0" Width="75px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <asp:GridView ID="grdterms" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                            PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                            Width="100%" GridLines="None" ShowFooter="true" OnRowDeleting="grdterms_RowDeleting"
                                                            OnRowDataBound="grdterms_RowDataBound">
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
                                                                <asp:TemplateField HeaderText="Terms & Conditions" ItemStyle-Width="850px" ItemStyle-Wrap="true"
                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtterms" runat="server" TextMode="MultiLine" onkeypress="return isNumberKey(event)"
                                                                            Width="850px" onblur="chksplcharactersdesc()" /><br />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Terms and Conditions Required"
                                                                            Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup11" ControlToValidate="txtterms"
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnAddterm" runat="server" ValidationGroup="valGroup11" ImageUrl="~/images/imgadd1.gif"
                                                                            OnClick="btnAddterm_Click" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </tr>
                                                    <tr id="trbtns" runat="server">
                                                        <td align="center" colspan="4">
                                                            <asp:Button ID="btnsubmit" runat="server" CssClass="esbtn" Text="Submit" OnClick="btnsubmit_Click"
                                                                OnClientClick="javascript:return validate()" />
                                                            <asp:Button ID="btnreset" CssClass="esbtn" runat="server" Text="Reset" OnClick="btnreset_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <asp:HiddenField ID="hfamendvalue" runat="server" Value="0" />
                                        <asp:HiddenField ID="hfsumpobal" runat="server" Value="0" />
                                        <tr id="tramendedpo" runat="server">
                                            <td style="width: 800px">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="800px"
                                                    GridLines="None" EmptyDataText="No Data Avaliable" HorizontalAlign="Center" CssClass="grid-content"
                                                    BorderColor="Black" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                    FooterStyle-BackColor="DarkGray" DataKeyNames="id,po_value,cc_type" OnRowDeleting="GridView1_RowDeleting"
                                                    OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="" ShowSelectButton="true" ItemStyle-Width="15px"
                                                            SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField HeaderText="" Visible="false" DataField="id" />
                                                        <asp:BoundField HeaderText="PO No" ItemStyle-BorderColor="Black" ItemStyle-Width="50px"
                                                            ReadOnly="true" DataField="pono" />
                                                        <asp:BoundField HeaderText="PO Date" ReadOnly="true" DataField="Amended_date" />
                                                        <asp:BoundField HeaderText="Old PO Value" ReadOnly="true" DataField="po_value" />
                                                        <asp:BoundField HeaderText="Amended Requested Value" ReadOnly="true" DataField="Amended_amount" />
                                                        <%--<asp:BoundField HeaderText="New PO Value" ReadOnly="true" DataField="POTotal" />--%>
                                                        <asp:BoundField HeaderText="Description" ReadOnly="true" DataField="remarks" SortExpression="remarks"
                                                            ItemStyle-CssClass="Shorter" />
                                                        <asp:BoundField HeaderText="Vendor Name" ReadOnly="true" DataField="vendor_name" />
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblamendsppo" runat="server" style="margin: 1em; border-collapse: collapse;"
                            width="800px">
                            <tr>
                                <th colspan="4" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small; background: #E3E4FA;"
                                    align="center">Verify Amend SPPO
                                </th>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center" colspan="1">
                                    <asp:Label ID="Label4" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="Vendor Name"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="3">
                                    <asp:Label ID="lbvname" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label5" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="PO NO"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                    <asp:Label ID="lbpono" Enabled="true" runat="server"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label6" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="PO Date"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                    <asp:TextBox ID="txpodate" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label7" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="OLD PO Value"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                    <asp:TextBox ID="txoldpo" Width="90px" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label8" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="Amended Requested Value"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                    <asp:TextBox ID="lbtotal" Width="90px" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label17" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="Approved Amended Value"></asp:Label>
                                </td>
                                <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                                    align="center">
                                    <asp:TextBox ID="txpovalue" Width="90px" ToolTip="PO Value" onkeyup="Total(this.value); isInteger(this.value);"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label24" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                        Text="New PO Value"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                    <asp:TextBox ID="txtotal" Width="90px" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="4">
                                    <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="False" BorderColor="White" BackColor="White" CssClass="grid-content"
                                        HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        Width="100%" GridLines="None" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound"
                                        FooterStyle-HorizontalAlign="Right" DataKeyNames="Item_status,itemid">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectamditems" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="250px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtdescamend" runat="server" Text='<%#Bind("Description") %>' onkeyup="this.value = this.value.replace(/^[,/]+$/, '')"
                                                        onblur="chksplcharactersamend()" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-BackColor="White" />--%>
                                            <asp:BoundField DataField="unit" HeaderText="Unit" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Oldquantity" HeaderText="CurrentQty" HeaderStyle-BackColor="White" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="quantity" HeaderText="AmendedQty" HeaderStyle-BackColor="White" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ClientRate" HeaderText="Our Rate" HeaderStyle-BackColor="White" HeaderStyle-Width="25px"  ItemStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="PRWRate" HeaderText=" PRW Appr Rate" HeaderStyle-BackColor="White" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="rate" HeaderText="Rate" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="PO_Type" HeaderText="Type" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Center"  />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="1" align="center" style="width: 275px">
                                    <asp:Label ID="Label9" runat="server" CssClass="eslbl" Width="100px" Font-Size="10px"
                                        Text="Amendment(-) Value:"></asp:Label>
                                    <asp:TextBox ID="txtamendpreviouspovalue" CssClass="estbox" runat="server" Font-Bold="true"
                                        ForeColor="White" BackColor="Red" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                        Text="0" Width="75px"></asp:TextBox>
                                </td>
                                <td colspan="1" align="center" style="width: 275px">
                                    <asp:Label ID="Label11" runat="server" CssClass="eslbl" Width="100px" Font-Size="10px"
                                        Text="Amendment(+) Value:"></asp:Label>
                                    <asp:TextBox ID="txtamdamount" CssClass="estbox" runat="server" Font-Bold="true"
                                        ForeColor="White" BackColor="Green" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                        Text="0" Width="75px"></asp:TextBox>
                                </td>
                                <td colspan="2" align="center" style="width: 275px">
                                    <asp:Label ID="Label12" runat="server" CssClass="eslbl" Width="100px" Font-Size="10px"
                                        Text="Total PO Value:"></asp:Label>
                                    <asp:TextBox ID="txtamdpoval" CssClass="estbox" runat="server" Font-Bold="true" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" Text="0" Width="75px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                    <asp:GridView CssClass="" ID="grdpodescold" Width="100%" runat="server" AutoGenerateColumns="false"
                                        HeaderStyle-BackColor="LightGray" DataKeyNames="">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="20px"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="splitdata" ItemStyle-Font-Bold="true" HeaderText="Terms and Conditions Old" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                    <asp:GridView CssClass="" ID="grdpodesc" Width="100%" runat="server" AutoGenerateColumns="false"
                                        HeaderStyle-BackColor="LightGray" ShowFooter="true" OnRowDeleting="grdpodesc_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectterms" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="20px"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terms & Conditions New" ItemStyle-Width="850px" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txttermsamd" runat="server" Text='<%#Bind("splitdata") %>' onkeypress="return isNumberKeyamend(event)"
                                                        onblur="chksplcharactersdescamend()" Width="850px" /><br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Terms and Conditions Required"
                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup11" ControlToValidate="txttermsamd"
                                                        runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnAddterm" runat="server" ValidationGroup="valGroup11" ImageUrl="~/images/imgadd1.gif"
                                                        OnClick="btnAddterma_Click" />

                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="4">
                                    <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                        PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        DataKeyNames="" GridLines="none" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                        <HeaderStyle CssClass="headerstyle" />
                                        <Columns>
                                            <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Desc" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Role" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Name" ItemStyle-Wrap="false" />
                                            <%--<asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="UserID" ItemStyle-Wrap="false" />--%>
                                        </Columns>
                                        <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                        <PagerStyle CssClass="grid pagerbar" />
                                        <HeaderStyle CssClass="grid-header" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="4" align="center" style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;">
                                    <asp:Button CssClass="esbtn" Style="font-size: small;" ID="btnupdate" runat="server"
                                        Text="Update" OnClientClick="javascript:return validate1()" OnClick="btnupdate_Click" />
                                    &nbsp;&nbsp;
                                    <%--OnClientClick="this.disabled = true; this.value = 'Please Wait...';" UseSubmitBehavior="false"--%>
                                    <asp:Button ID="Button1" runat="server" CssClass="esbtn" Style="font-size: small;"
                                        Text="Back" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function isNumberKeyamend(evt) {
            //debugger;
            grd = document.getElementById("<%=grdpodesc.ClientID %>");
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
        function isInteger(s) {
            var i;
            s = s.toString();
            for (i = 0; i < s.length; i++) {
                var c = s.charAt(i);
                if (isNaN(c)) {
                    document.getElementById("<%=txpovalue.ClientID %>").value = "";
                    alert("Given value is not a number");
                    return false;
                }
            }
            return true;
        }


        function IsNumeric1(evt) {
            GView = document.getElementById("<%=GridView1.ClientID %>");
            for (var rowCount = 1; rowCount < GView.rows.length; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
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
    </script>
    <script language="javascript" type="text/javascript">

        function Total() {
            grd = document.getElementById("<%=GridView1.ClientID %>");
            var OV = document.getElementById("<%=txoldpo.ClientID %>").value;
            var AV = document.getElementById("<%=txpovalue.ClientID %>").value;


            document.getElementById("<%=txtotal.ClientID %>").value = parseInt(OV) + parseInt(AV);
        }
        function Check() {
            //debugger;
            grid = document.getElementById("<%=gvDetails.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(7).children[0].value != "" && grid.rows(i).cells(7).children[0].value != "0") {
                    if (grid.rows(i).cells(6).children[0].value == "Select" && grid.rows(i).cells(6).children[0].isDisabled == false) {
                        window.alert("Please Select Type before adding quantity");
                        grid.rows(i).cells(7).children[0].value = "0";
                        grid.rows(i).cells(8).children[0].value = "0";
                        return false;
                    }
                    if (grid.rows(i).cells(6).children[0].value == "Subtract") {
                        if (parseFloat(grid.rows(i).cells(4).children[0].value) < parseFloat(grid.rows(i).cells(7).children[0].value)) {
                            window.alert("Invalid Amended PO Quantity");
                            grid.rows(i).cells(7).children[0].value = "0";
                            grid.rows(i).cells(8).children[0].value = "0";
                            return false;
                        }
                    }
                }
                if (grid.rows(i).cells(7).children[0].value == "" || grid.rows(i).cells(7).children[0].value == "0") {
                    grid.rows(i).cells(8).children[0].value = "0";
                }
            }
        }
    </script>
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
            grid = document.getElementById("<%=gvDetailsnew.ClientID %>");
            var hfnetamt = document.getElementById("<%=hftotalpo.ClientID %>").value.replace(/,/g, "");
            var totalreturn = 0;
            var totalamend = 0;
            var sumtotal = 0;
            var amt = 0;
            var ActuallReturn = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells(7).children[0].value) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(7).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells(7).children[0].value)) * (Number(grd.rows(rowCount).cells(5).children[0].value));
                            grd.rows(rowCount).cells(8).children[0].value = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);

                            if (grd.rows(rowCount).cells(6).children[0].isDisabled == true) {
                                totalamend += Number(grd.rows(rowCount).cells(8).children[0].value);
                                sumtotal += Number(grd.rows(rowCount).cells(8).children[0].value);
                            }
                            if (grd.rows(rowCount).cells(6).children[0].isDisabled == false) {
                                if (grd.rows(rowCount).cells(6).children[0].value == "Add") {
                                    totalamend += Number(grd.rows(rowCount).cells(8).children[0].value);
                                    sumtotal += Number(grd.rows(rowCount).cells(8).children[0].value);
                                    //document.getElementById("<%=hfamendamt.ClientID %>").value = parseFloat(totalamend);
                                }
                                if (grd.rows(rowCount).cells(6).children[0].value == "Subtract") {
                                    totalreturn += Number(grd.rows(rowCount).cells(8).children[0].value);
                                    sumtotal += Number(grd.rows(rowCount).cells(8).children[0].value);
                                    //document.getElementById("<%=hfreturnAmt.ClientID %>").value = parseFloat(totalreturn);
                                }
                                else if (grd.rows(rowCount).cells(6).children[0].value == "Select") {
                                    totalamend += 0;
                                    totalreturn += 0;
                                    sumtotal += 0;
                                    //document.getElementById("<%=hfamendamt.ClientID %>").value = parseFloat(totalamend);
                                }
                            }
                        }
                    }
                    else {
                        totalamend += Number(grd.rows(rowCount).cells(8).children[0].value);
                        totalreturn += Number(grd.rows(rowCount).cells(8).children[0].value);
                        document.getElementById("<%=hfamendamt.ClientID %>").value = parseFloat(totalamend);
                        document.getElementById("<%=hfreturnAmt.ClientID %>").value = parseFloat(totalreturn);
                    }

                }
                document.getElementById("<%=hfamendamt.ClientID %>").value = parseFloat(totalamend);
                document.getElementById("<%=hfreturnAmt.ClientID %>").value = parseFloat(totalreturn);
                //debugger;
                if (document.getElementById("<%=hfamendamt.ClientID %>").value != "0") {
                    document.getElementById("<%=txtamendamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfamendamt.ClientID %>").value) + parseFloat(document.getElementById("<%=hfamendamt1.ClientID %>").value);
                }
                else {
                    document.getElementById("<%=txtamendamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfamendamt1.ClientID %>").value);
                }
                if (document.getElementById("<%=hfreturnAmt.ClientID %>").value != "0") {
                    document.getElementById("<%=txtreturnAmt.ClientID %>").value = parseFloat(document.getElementById("<%=hfreturnAmt.ClientID %>").value);
                }
                else {
                    document.getElementById("<%=txtreturnAmt.ClientID %>").value = "0";
                }
                //document.getElementById("<%=hfreturnAmt.ClientID %>").value = totalreturn;
                //document.getElementById("<%=txtreturnAmt.ClientID %>").value = totalreturn;

                var result = parseFloat(document.getElementById("<%=txtamendamt.ClientID %>").value) - parseFloat(totalreturn);
                if (!isNaN(result)) {
                    document.getElementById("<%=hftotalpo.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(result);
                    if (parseFloat(document.getElementById("<%=txtamendamt.ClientID %>").value) > parseFloat(totalreturn)) {
                        document.getElementById("<%=txttotalamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(result);
                        document.getElementById("<%=txttotalamt.ClientID %>").style.backgroundColor = 'green';
                        document.getElementById("<%=txttotalamt.ClientID %>").style.color = "white";
                    }

                    if (parseFloat(document.getElementById("<%=txtamendamt.ClientID %>").value) < parseFloat(totalreturn)) {
                        document.getElementById("<%=txttotalamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(result);
                        document.getElementById("<%=txttotalamt.ClientID %>").style.backgroundColor = 'red';
                        document.getElementById("<%=txttotalamt.ClientID %>").style.color = "white";
                    }
                    else {
                        document.getElementById("<%=txttotalamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(result);
                        document.getElementById("<%=txttotalamt.ClientID %>").style.backgroundColor = 'green';
                        document.getElementById("<%=txttotalamt.ClientID %>").style.color = "white";
                    }
                }
            }
            if (grd == null && grid == null) {
                document.getElementById("<%=txttotalamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(0);
                document.getElementById("<%=hftotalpo.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(0);
                document.getElementById("<%=hfamendamt.ClientID %>").value = 0;
                document.getElementById("<%=txtamendamt.ClientID %>").value = 0;
                document.getElementById("<%=hfamendamt1.ClientID %>").value = 0;
                document.getElementById("<%=hfreturnAmt.ClientID %>").value = 0;
                document.getElementById("<%=txtreturnAmt.ClientID %>").value = 0;
            }
        }
    </script>
    <script type="text/javascript">
        function checkvaliddata(val) {
            //debugger;
            grid = document.getElementById("<%=gvDetails.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                var dropdownoldValue = grid.rows[i].cells[6].childNodes[0].value;
                if (rowIndex == i) {
                    if (currentDropDownValue == "Select") {
                        if (grid.rows(i).cells(6).children[0].value == "Select") {
                            grid.rows(i).cells(7).children[0].value = "0";
                            grid.rows(i).cells(8).children[0].value = "0";
                            Check();
                            Calculate();
                            return false;
                        }
                    }
                }
            }
            Check();
            Calculate();
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
        function AddItems() {
            //debugger;
            document.getElementById("<%=tradditems.ClientID %>").style.display = "block";
            document.getElementById("<%=btnadditems.ClientID %>").click();
            return true;
        }
        function RemoveItems() {
            document.getElementById("<%=btnremoveitems.ClientID %>").click();
        }
        function Calculatenew() {
            //debugger;
            grd = document.getElementById("<%=gvDetailsnew.ClientID %>");
            //var hfreturnAmt = document.getElementById("<%=hfreturnAmt.ClientID %>").value.replace(/,/g, "");
            var total = 0;
            var amt = 0;
            var subtt = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells(7).children[0].value) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(4).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells(4).children[0].value)) * (Number(grd.rows(rowCount).cells(7).children[0].value));
                            grd.rows(rowCount).cells(8).children[0].value = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                            total += Number(grd.rows(rowCount).cells(8).children[0].value);
                            subtt = total;
                            document.getElementById("<%=hfamendamt1.ClientID %>").value = total;
                        }
                         if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            if((Number(grd.rows(rowCount).cells(7).children[0].value)) > (Number(grd.rows(rowCount).cells(6).children[0].value)))
                                grd.rows(rowCount).cells(7).children[0].style.color = 'red';
                            else
                                grd.rows(rowCount).cells(7).children[0].style.color = 'black';
                        }
                    }
                    else {
                        total += Number(grd.rows(rowCount).cells(6).children[0].value);
                        document.getElementById("<%=hfamendamt1.ClientID %>").value = total;
                    }

                }

                if (document.getElementById("<%=hfamendamt1.ClientID %>").value != "0") {
                    document.getElementById("<%=txtamendamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfamendamt1.ClientID %>").value) + parseFloat(document.getElementById("<%=hfamendamt.ClientID %>").value);
                    document.getElementById("<%=txttotalamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(document.getElementById("<%=hfreturnAmt.ClientID %>").value) + parseFloat(document.getElementById("<%=txtamendamt.ClientID %>").value);
                    document.getElementById("<%=txttotalamt.ClientID %>").style.backgroundColor = 'green';
                    document.getElementById("<%=txttotalamt.ClientID %>").style.color = "white";
                }
                else {
                    document.getElementById("<%=txtamendamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfamendamt1.ClientID %>").value);
                    document.getElementById("<%=txttotalamt.ClientID %>").value = parseFloat(document.getElementById("<%=hfsumpobal.ClientID %>").value) + parseFloat(document.getElementById("<%=hfreturnAmt.ClientID %>").value) + parseFloat(document.getElementById("<%=txtamendamt.ClientID %>").value);
                    document.getElementById("<%=txttotalamt.ClientID %>").style.backgroundColor = 'green';
                    document.getElementById("<%=txttotalamt.ClientID %>").style.color = "white";
                }
            }
            Calculate();
        }
    </script>
</asp:Content>
