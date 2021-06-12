<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AssetsTransfer.aspx.cs"
    Inherits="AssetsTransfer" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function uncheckOthers(id) {
            var elm = document.getElementsByTagName('input');
            for (var i = 0; i < elm.length; i++) {
                if (elm.item(i).type == "checkbox" && elm.item(i) != id)
                    elm.item(i).checked = false;
            }
        }

        function validate() {
            var objs = new Array("<%=ddlindentno.ClientID %>", "<%=ddlcccode.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

        }
        function validation() {
            var objs = new Array("<%=txtdate.ClientID %>", "<%=ddlDays.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;

            }

            GridView1 = document.getElementById("<%=grdaddedassets.ClientID %>");
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }

            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;

        }
        
    </script>
    <script type="text/javascript" language="javascript">
        function checkcodes() {
            maingrid = document.getElementById("<%=GridView1.ClientID %>");
            GridView = document.getElementById("<%=grdaddassets.ClientID %>");
            GridView1 = document.getElementById("<%=grdaddedassets.ClientID %>");
            var isValid = false;
            var j = 0;
            if (GridView != null) {

                var i = 0;
                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
                    var inputs = GridView.rows(rowCount).getElementsByTagName('input');
                    if (inputs != null) {
                        if (inputs[0].type == "checkbox") {
                            if (inputs[0].checked) {
                                isValid = true;
                                j = j + 1;

                            }
                        }
                    }
                    if (GridView.rows(rowCount).cells(0).children(0).checked == true) {
                        i = i + 1;

                        if (i > 1) {
                            window.alert("You could not select multiple items");

                            return false;
                        }
                        var itemcode = GridView.rows(rowCount).cells(1).innerHTML;
                        for (var rowCount1 = 1; rowCount1 < GridView1.rows.length; rowCount1++) {
                            var itemcodes = GridView1.rows(rowCount1).cells(1).innerHTML;

                            if (itemcode == itemcodes) {
                                GridView.rows(rowCount).cells(0).children(0).checked = false;
                                window.alert("Item Code Already Added");
                                return false;
                            }



                        }


                    }

                }
                if (parseInt(j) == 0) {
                    alert("Please select atleast one checkbox to Add");
                    return false;
                }
            }
            if (maingrid != null) {
                for (var rowCount = 1; rowCount < maingrid.rows.length; rowCount++) {
                    var i = 0;
                    if (maingrid.rows(rowCount).cells(0).children(0).checked == true) {
                        var quantity = maingrid.rows(rowCount).cells(2).innerHTML;
                        var items = maingrid.rows(rowCount).cells(1).innerHTML;
                        for (var rowCount1 = 1; rowCount1 < GridView1.rows.length; rowCount1++) {
                            var itemcodes = GridView1.rows(rowCount1).cells(1).innerHTML.substring(0, 8);
                            if (items == itemcodes) {
                                i = i + 1;
                                if (quantity <= i) {
                                    window.alert("Invalid");
                                    return false;
                                }

                            }
                        }
                    }
                }
            }



        }

        function chkremove() {
            GridView1 = document.getElementById("<%=grdaddedassets.ClientID %>");
            var isValid = false;
            var j = 0;
            if (GridView1 != null) {

                var i = 0;
                for (var rowCount = 1; rowCount < GridView1.rows.length; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == true) {
                        i = i + 1;

                        if (i > 1) {
                            window.alert("You could not select multiple items");

                            return false;
                        }


                    }

                    var inputs = GridView1.rows(rowCount).getElementsByTagName('input');
                    if (inputs != null) {
                        if (inputs[0].type == "checkbox") {
                            if (inputs[0].checked) {
                                isValid = true;
                                j = j + 1;

                            }
                        }
                    }
                }
                if (parseInt(j) == 0) {
                    alert("Please select atleast one checkbox to Remove");
                    return false;
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
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td width="10px">
            </td>
            <td>
                <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upd" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table width="700px">
                            <tr width="60%" height="50px" align="left">
                                <td align="right" width="30px">
                                    <asp:Label ID="Label1" Width="50px" runat="server" Text="Indent No"></asp:Label>
                                </td>
                                <td align="left" width="150px">
                                    <asp:DropDownList ID="ddlindentno" Width="150px" ToolTip="Indent No" CssClass="filter_item"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlindentno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlindentno"
                                        ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="indent2A"
                                        PromptText="Select Indent No">
                                    </cc1:CascadingDropDown>
                                </td>
                                <td align="right" id="tdcccode" runat="server">
                                    <asp:Label ID="lblcccode" runat="server" Text="CC Code"></asp:Label>
                                </td>
                                <td id="tdcccode1" runat="server" align="left">
                                    <asp:DropDownList ID="ddlcccode" Width="200px" ToolTip="Cost Center" CssClass="filter_item"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr width="100%" height="50px">
                                <td width="10px">
                                </td>
                                <td style="width: 580px" align="left" colspan="3">
                                    <asp:Button ID="btnview" runat="server" CssClass="button" Text="Accept" OnClientClick="javascript:return validate();"
                                        OnClick="btnview_Click" />&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnignore" runat="server" CssClass="button" Text="Ignore" OnClick="btnignore_Click" />
                                </td>
                            </tr>
                            <tr width="100%" id="tdmaingrid" runat="server">
                                <td colspan="4" style="width: 600px">
                                    <asp:GridView ID="GridView1" GridLines="None" Width="100%" runat="server" AutoGenerateColumns="false"
                                        CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr width="300px">
                                <td colspan="1" id="trsubgrids1" runat="server">
                                    <asp:GridView ID="grdaddassets" Width="250px" GridLines="None" runat="server" AutoGenerateColumns="false"
                                        CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                        DataKeyNames="item_code" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        RowStyle-CssClass=" grid-row char grid-row-odd" EmptyDataText="Already Items Added">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgrd2" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Item Code" DataField="item_code" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td align="center" id="addbtn" runat="server" width="100px" colspan="2">
                                    <asp:Button ID="btnadd" Width="60px" runat="server" CssClass="button" Text="Add"
                                        OnClientClick="javascript:return checkcodes(); " OnClick="btnadd_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnremove" Width="60px" runat="server" CssClass="button" Text="Remove"
                                        OnClientClick="javascript:return chkremove(); " OnClick="btnremove_Click" />
                                </td>
                                <td colspan="1" width="300px" id="trsubgrids2" runat="server">
                                    <asp:GridView ID="grdaddedassets" Width="250px" GridLines="None" runat="server" AutoGenerateColumns="false"
                                        CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        DataKeyNames="id">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="false" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgrd3" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Item Code" DataField="item_code" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr id="trdescription" runat="server">
                                <td valign="middle">
                                    <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                        onkeypress="return false;" Font-Size="Small" runat="server" ToolTip="Date" Style="width: 130px;
                                        height: 20px; vertical-align: middle"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtdate" OnClientDateSelectionChanged="checkDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td valign="middle">
                                    <asp:DropDownList ID="ddlDays" ToolTip="No of Transit Days" runat="server" CssClass="esddown">
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
                                <td align="left" colspan="2">
                                    <asp:Label ID="lbldesc" runat="server" Text="Description"></asp:Label>
                                    <asp:TextBox ID="txtdesc" runat="server" CssClass="filter_item" ToolTip="Description"
                                        TextMode="MultiLine" Width="350px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trbtnvisible" runat="server" height="50px">
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnSave" Height="18px" runat="server" Text="Submit" OnClientClick="javascript:return validation();"
                                        CssClass="button" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
