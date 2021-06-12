<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="CloseTransferOut.aspx.cs" Inherits="CloseTransferOut" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isrepeted() {
            gridview = document.getElementById("<%=grdtransferout.ClientID %>");
            var sel = "|";
            var cel = "|";
            for (var i = 1; i < gridview.rows.length - 1; i++) {
                var idx = gridview.rows[i].cells[1].innerHTML;
                var idy = gridview.rows[i].cells[7].children[0].selectedIndex;
                if (idy != 0) {
                    if (sel.indexOf("|" + idx + "|" && "|" + idy + "|") == -1) {
                        sel = sel + idx + "|";
                        cel = cel + idy + "|";
                    }
                    else {
                        alert(gridview.rows[i].cells[1].innerHTML + " Already Selected")
                        gridview.rows[i].cells[7].children[0].focus();
                        return false;
                    }
                }
            }
            return true;
        }

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {

                //      var rqty=GridView.rows(rowCount).cells(10).children[0].value;
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                    //    theEvent.preventDefault();
                }
            }

        }
        function validate() {
            var date = document.getElementById("<%=txtdate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdate.ClientID %>");
            var remarks = document.getElementById("<%=txtremarks.ClientID %>").value;
            var remarksctrl = document.getElementById("<%=txtremarks.ClientID %>");
            var GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            var tdate = document.getElementById("<%=ddlDays.ClientID %>").value;
            var tdatectrl = document.getElementById("<%=ddlDays.ClientID %>");

            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(7).children(0).selectedIndex == 0) {
                        window.alert("Please Select Item Status");
                        GridView.rows(rowCount).cells(7).children(0).focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(9).children[0].value == "") {
                        window.alert("Please Enter Quantity");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        return false;
                    }

                    else if (parseInt(GridView.rows(rowCount).cells(8).innerHTML) < parseInt(GridView.rows(rowCount).cells(9).children[0].value)) {
                        window.alert("You are not able to transfer more than the available quantity");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        GridView.rows(rowCount).cells(9).children[0].value = "";
                        return false;
                    }

                    else if (parseInt(GridView.rows(rowCount).cells(9).children[0].value) == 0) {
                        window.alert("You are not able to transfer more than the available quantity");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        GridView.rows(rowCount).cells(9).children[0].value = "";
                        return false;
                    }
                }
            }

            if (date == "") {
                window.alert("Please Enter Date");
                datectrl.focus();
                return false;
            }
            if (tdate == "Select No of Days") {
                window.alert("Please Select Transit Days");
                tdatectrl.focus();
                return false;
            }
            if (remarks == "") {
                window.alert("Please Enter Remarks");
                remarksctrl.focus();
                return false;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">
        function validateld() {
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(7).children[0].value == "" || GridView.rows(rowCount).cells(7).children[0].value == 0) {
                        window.alert("Enter Quantity");
                        GridView.rows(rowCount).cells(7).children[0].value = "";
                        GridView.rows(rowCount).cells(7).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(8).children[0].selectedIndex == 0) {
                        window.alert("Select Item Type");
                        GridView.rows(rowCount).cells(8).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(9).children[0].selectedIndex == 0) {
                        window.alert("Select Item Status");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(10).children[0].value == "") {
                        window.alert("Enter Remarks");
                        GridView.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }

                }

            }

            var date = document.getElementById("<%=txtdateld.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdateld.ClientID %>");
            if (date == "Enter Date..") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }

            document.getElementById("<%=btnsubmitld.ClientID %>").style.display = 'none';
            return true;

        }
    </script>
    <script language="javascript" type="text/javascript">
        function validateissue() {
            GridView = document.getElementById("<%=GridView2.ClientID %>");
            var date = document.getElementById("<%=txtdateissue.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdateissue.ClientID %>");
            var desc = document.getElementById("<%=txtdesc.ClientID %>").value;
            var descctrl = document.getElementById("<%=txtdesc.ClientID %>");

            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                    window.alert("Please verify");
                    return false;
                }
                else if (GridView.rows(rowCount).cells(8).children[0].value == "" || GridView.rows(rowCount).cells(8).children[0].value == 0) {
                    window.alert("Please Enter Quantity");
                    GridView.rows(rowCount).cells(8).children[0].focus();
                    GridView.rows(rowCount).cells(8).children[0].value = "";
                    return false;
                }
                else if (parseInt(GridView.rows(rowCount).cells(9).children[0].value) < parseInt(GridView.rows(rowCount).cells(8).children[0].value)) {
                    window.alert("Inavlid");
                    GridView.rows(rowCount).cells(8).children[0].focus();
                    GridView.rows(rowCount).cells(8).children[0].value = "";
                    return false;
                }

            }

            if (date == "") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }
            if (desc == "") {
                window.alert("Enter Description");
                descctrl.focus();
                return false;
            }

            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
        }
        function IsNumeric3(evt) {
            GridView = document.getElementById("<%=GridView2.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="750px">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Transfer Out For Store Close
                                <a class="help" href="" title=""><small>Help</small> </a>
                            </h1>
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
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <div id="body_form">
                                                    <div>
                                                        <div id="server_logs">
                                                        </div>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div id="search_filter_data">
                                                                            <table class="search_table" id="tblSearch" runat="server">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <asp:GridView ID="Gvdetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                                EnableViewState="false" CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                                                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="White" ShowFooter="false"
                                                                                                FooterStyle-Font-Bold="true" DataKeyNames="code,Type" OnRowDataBound="Gvdetails_RowDataBound"
                                                                                                OnSelectedIndexChanged="Gvdetails_SelectedIndexChanged">
                                                                                                <Columns>
                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                        SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                    <asp:BoundField DataField="code" HeaderText="codes" ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="qty" HeaderText="qty" ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="view_form_options" width="100%">
                                                                        <table width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table id="tbltransferout" runat="server">
                                                            <tr id="trsecondgrid" runat="server">
                                                                <td valign="top">
                                                                    <div class="box-a list-a">
                                                                        <div class="inner">
                                                                            <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td class="grid-content">
                                                                                            <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0"
                                                                                                style="background: none;">
                                                                                                <asp:GridView ID="grdtransferout" BorderColor="White" Width="100%" runat="server"
                                                                                                    AutoGenerateColumns="False" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                    PagerStyle-CssClass="grid pagerbar" DataKeyNames="id" ShowFooter="true" OnRowDataBound="grdtransferout_RowDataBound">
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="id" Visible="false" />
                                                                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                        <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                        <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                        <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                        <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                        <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                        <asp:TemplateField HeaderText="Item Status">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:DropDownList ID="ddlStatus" CssClass="char" runat="server" onblur="isrepeted();">
                                                                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                                                                    <asp:ListItem>Stock</asp:ListItem>
                                                                                                                    <asp:ListItem>New Stock</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="Available Qty" HeaderText="Avaliable Qty" />
                                                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtqty" runat="server" onkeypress='IsNumeric1(event)' Width="50px"
                                                                                                                    Text='<%# Eval("Issued Qty")  %>'></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("Available qty")%>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <asp:HiddenField ID="hfcheck" runat="server" />
                                                                                    <asp:HiddenField ID="hfrole" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr id="trfields" runat="server">
                                                                <td>
                                                                    <table border="0" class="fields" width="100%" id="tbldesc" runat="server">
                                                                        <tr>
                                                                            <td style="width: 100%">
                                                                                <table class="search_table" align="center">
                                                                                    <tr>
                                                                                        <td class="item item-selection" align="left" width="">
                                                                                            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                                                                        </td>
                                                                                        <td class="item item-selection" id="tddayslabel" runat="server" align="left" width="">
                                                                                            <asp:Label ID="lblremark" runat="server" Text="Transit Days"></asp:Label>
                                                                                        </td>
                                                                                        <td class="item item-selection" align="left" width="">
                                                                                            <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="top" align="left">
                                                                                            <asp:TextBox ID="txtdate" runat="server" CssClass="char" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                PopupButtonID="TextBox1">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td class="item item-selection" align="left" width="" runat="server" id="tddays"
                                                                                            valign="top">
                                                                                            <asp:DropDownList ID="ddlDays" CssClass="char" ToolTip="Select No of Days" Width="150px"
                                                                                                runat="server">
                                                                                                <asp:ListItem Value="Select No of Days">Select No of Days</asp:ListItem>
                                                                                                <asp:ListItem>1</asp:ListItem>
                                                                                                <asp:ListItem>2</asp:ListItem>
                                                                                                <asp:ListItem>3</asp:ListItem>
                                                                                                <asp:ListItem>4</asp:ListItem>
                                                                                                <asp:ListItem>5</asp:ListItem>
                                                                                                <asp:ListItem>6</asp:ListItem>
                                                                                                <asp:ListItem>7</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="item item-selection" valign="top" align="left">
                                                                                            <asp:TextBox ID="txtremarks" TextMode="MultiLine" Font-Size="Small" runat="server"
                                                                                                MaxLength="200" Width="450px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table" width="100%">
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" class="item search_filters item-group" valign="top">
                                                                                <div class="group-expand">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="pagerbar" id="trbutton" runat="server">
                                                                <td class="pager-cell-button" align="center">
                                                                    <asp:Button ID="btnsubmit" Width="80px" Height="20px" runat="server" Text="Submit"
                                                                        CssClass="button" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="tbllostanddamages" runat="server">
                                                            <tr>
                                                                <td class="grid-content">
                                                                    <table id="Table1" class="grid" width="100%" cellspacing="0" cellpadding="0" style="background: none;">
                                                                        <asp:GridView ID="GridView1" Width="100%" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                            DataKeyNames="id" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="id" Visible="false" />
                                                                                <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtqty" runat="server" Width="50px" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddltype" runat="server" Width="60px">
                                                                                            <asp:ListItem>Select</asp:ListItem>
                                                                                            <asp:ListItem>New Stock</asp:ListItem>
                                                                                            <asp:ListItem>Stock</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlreporttype" runat="server" Width="60px" SelectedValue='<%# Bind("type") %>'>
                                                                                            <asp:ListItem>Select</asp:ListItem>
                                                                                            <asp:ListItem>Lost</asp:ListItem>
                                                                                            <asp:ListItem>Damaged</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtremarksld" runat="server" Width="200px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="item item-selection" valign="middle" style="padding-left: 20px">
                                                                    <asp:TextBox ID="txtdateld" Font-Size="Small" ToolTip="Date" runat="server" Style="width: 130px;
                                                                        height: 20px; vertical-align: middle"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Enter Date.."
                                                                        WatermarkCssClass="watermarked" TargetControlID="txtdateld" runat="server">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdateld"
                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                        PopupButtonID="txtdate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr class="pagerbar" id="trbtn" runat="server">
                                                                <td class="pager-cell-button" align="center">
                                                                    <asp:Button ID="btnsubmitld" Width="120px" Height="20px" runat="server" Text="Submit"
                                                                        CssClass="button" OnClick="btnsubmitld_Click" OnClientClick="javascript:return validateld()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="tbldailyissue" runat="server">
                                                            <tr>
                                                                <td class="grid-content">
                                                                    <table id="Table2" class="grid" width="100%" align="center" style="background: none;">
                                                                        <asp:GridView ID="GridView2" BorderColor="White" Width="700px" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                            DataKeyNames="id" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound">
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="id" Visible="false" />
                                                                                <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtqty" runat="server" Width="50px" Text='<%#Eval("Quantity")%>'
                                                                                            onkeypress='IsNumeric3(event)'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("Available qty")%>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr1" runat="server">
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td width="150px" style="padding-left: 50px">
                                                                                <table class="search_table" width="100%">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="">
                                                                                            <asp:Label ID="Label3" runat="server" Text="Issued Date"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:TextBox ID="txtdateissue" runat="server" Font-Size="Small" Style="width: 130px;
                                                                                                height: 20px; vertical-align: middle"></asp:TextBox>
                                                                                            <%-- <img alt="" src="images/stock_calendar.png" style="width: 13px; height: 15px; vertical-align: middle;"
                                                                                                    id="Img2" />--%>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                                                FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtdate" TargetControlID="txtdateissue">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="200px">
                                                                                <table align="center" class="search_table" width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="item item-selection" colspan="2" width="">
                                                                                            <asp:Label ID="lbldate" runat="server" Text="Description"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="item item-selection" valign="middle">
                                                                                            <asp:TextBox ID="txtdesc" runat="server" CssClass="filter_item" MaxLength="50" TextMode="MultiLine"
                                                                                                ToolTip="Description" Width="450px"></asp:TextBox>
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
                                                                    <asp:Button ID="btnSave" Height="18px" runat="server" Text="Save" CssClass="button"
                                                                        OnClick="btnSave_Click" OnClientClick="javascript:return validateissue()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
