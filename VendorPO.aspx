<%@ Page Language="C#" MasterPageFile="~/Essel.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="VendorPO.aspx.cs" Inherits="VendorPO" Title="Vendor DO/PO - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();


        }
        function showpopup() {
            $find('mdlindent').show();

        }
        
    </script>
    <style type="text/css">
        .buttonSubmit
        {
            background-color: #4C99CC;
            border-bottom: medium none;
            border-left: medium none;
            border-right: medium none;
            border-top: medium none;
            color: white;
            cursor: pointer;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            height: 18px;
            text-decoration: none;
        }
    </style>
    <script language="javascript">
        function print() {
            //debugger;          
            var Type = document.getElementById("<%=hfpotype.ClientID %>").value;
            if (Type == "DO") {
                var grid_obj = document.getElementById("<%=tbldo.ClientID %>");
            }
            else {
                var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            }
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            $find('mdlindent').hide();

        }       
    </script>
    <script language="javascript">

        function validatesearch() {


            var cccode = document.getElementById("<%=ddlcccode.ClientID %>").value;
            var ccode = document.getElementById("<%=ddlcccode.ClientID %>");


            if (cccode == "") {
                window.alert("Please Select Cost Center");
                ccode.focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">


        function vendorcheck(vendoradd) {

            PageMethods.vendor(vendoradd, OnSucceede);
        }

        // Callback function invoked on successful completion of the page method.
        function OnSucceede(result, userContext, methodName) {
            var name = document.getElementById('ctl00_ContentPlaceHolder1_lblname');
            var Address = document.getElementById('ctl00_ContentPlaceHolder1_lbladdress');
            if (result != "") {//rbtndca

                var values = result.split('|');
                name.innerHTML = values[0];
                Address.innerHTML = values[1];

            }
            else {


            }

        }
    </script>
    <script language="javascript">
        function validate() {
            if (document.getElementById("<%=ddlvendor.ClientID %>").selectedIndex == 0) {
                window.alert("Please select vendor");
                return false;
            }
            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;

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
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var iRowIndex;
        function MouseEvents(objRef, evt, desc) {

            if (evt.type == "mouseover") {
                objRef.style.cursor = 'pointer'
                objRef.style.backgroundColor = "#EEE";
                ShowDiv(desc, -70);

            }
            else {
                objRef.style.backgroundColor = "#FFF";
                iRowIndex = objRef.rowIndex;
                HideDiv();
            }
        }
        function ShowDiv(desc, pos) {
            document.getElementById("<%=divDetail.ClientID %>").style.display = 'block';
            document.getElementById("<%=divDetail.ClientID %>").innerHTML = desc;
            document.getElementById("<%=divDetail.ClientID %>").style.marginTop = pos.toString() + "px";
        }
        function HideDiv() {
            document.getElementById("<%=divDetail.ClientID %>").style.display = 'none';
        }
        function highlight(objRef, evt) {
            if (evt.type == "mouseover") {
                objRef.style.display = 'block';
                document.getElementById("<%=grdbillpo.ClientID %>").rows[iRowIndex].style.backgroundColor = "#641E16";
            }
            else {
                if (evt.type == "mouseout") {
                    document.getElementById("<%=grdbillpo.ClientID %>").rows[iRowIndex].style.backgroundColor = "#FFF";
                    objRef.style.display = 'none';
                }
            }
        }
    </script>
    <style type="text/css">
        body
        {
            font-family: Arial, Tahoma;
            font-size: 15px;
        }
        .grid
        {
            width: 100%;
            font: inherit;
            background-color: #641E16;
            border: solid 1px #525252;
        }
        .grid td
        {
            font: inherit;
            padding: 3px 5px;
            border: solid 1px #C1C1C1;
            color: #333;
            text-align: left;
        }
        .grid th
        {
            padding: 3px;
            color: #FFF;
            background: #424242;
            border-left: solid 1px #525252;
            font: inherit;
            text-align: center;
            text-transform: uppercase;
        }
        h5
        {
            color: #7B241C;
            text-decoration: underline;
        }
        .divDetail
        {
            float: right;
            font: inherit;
            font-size: 13px;
            padding: 2px 5px;
            width: 300px;
            border: solid 2px #A93226;
            -moz-border-radius: 0 7px 7px 0;
            -webkit-border-radius: 0 7px 7px 0;
            border-radius: 0 7px 7px 0;
            display: none;
            color: #78281F;
            background-color: #D5DBDB;
        }
        .divDetail p
        {
            font: inherit;
        }
        .divDetail a
        {
            font: inherit;
            float: right;
            background-color: #A93226;
            color: #2E4053;
            text-decoration: none;
            border: solid 1px #2F5BB7;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            padding: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View DO/PO <a class="help"
                                    href="/view_diagram/process?res_model=hr.employee&amp;res_id=False&amp;title=Employees"
                                    title="Corporate Intelligence..."><small>Help</small> </a>
                            </h1>
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
                                                                    <table border="0" class="fields" width="100%">
                                                                        <tr>
                                                                            <td nowrap="nowrap" class="item search_filters item-filtersgroup" valign="top">
                                                                                <div class="filters-group">
                                                                                    <div style="display: none;">
                                                                                        <div class="filter-a">
                                                                                        </div>
                                                                                    </div>
                                                                                    <table border="0" class="fields" width="100%">
                                                                                        <tbody>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle" width="" colspan="1">
                                                                                                <asp:Label ID="lblmonth" runat="server" Text="Month"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle">
                                                                                                <span class="filter_item">
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
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:DropDownList ID="ddlyear" CssClass="filter_item" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td width="1%" nowrap="true">
                                                                                            <div class="filter-a">
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                            <asp:Label ID="lblcccode" runat="server" Text="CC Code"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:DropDownList ID="ddlcccode" CssClass="filter_item" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                                                ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="codename"
                                                                                                PromptText="Select Cost Center">
                                                                                            </cc1:CascadingDropDown>
                                                                                        </td>
                                                                                    </tr>
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
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="view_form_options" width="100%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click"
                                                                                    OnClientClick="javascript:return validatesearch()" />&nbsp;&nbsp; &nbsp;&nbsp;
                                                                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click" />
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
                                                                            <div class="inner">
                                                                                <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                                <table class="pager-table">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="pager-cell">
                                                                                                                <%-- <h2>
                                                                                                                    New PO :-</h2>--%>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                                <%--                                                                                             <asp:Button ID="_terp_list_new" ToolTip="To Raise New Indent" runat="server" Text="New" PostBackUrl="~/Raise Indent.aspx" />
                                                                                                                --%>
                                                                                                                <%--<button id="_terp_list_new" title="To Raise New Indent" onclick="javascript:redirect();">
                                                                                                                    New</button>--%>
                                                                                                                <%-- <asp:Button ID="btnnew" Width="50px" PostBackUrl="~/RaisePO.aspx" Height="" runat="server"
                                                                                                            Text="New" CssClass="newbtn" />--%>
                                                                                                            </td>
                                                                                                            <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                                                <div class="pager">
                                                                                                                    <div align="right">
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
                                                                                            <td class="grid-content">
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0"
                                                                                                    style="background: none;">
                                                                                                    <asp:GridView ID="grdviewpo" GridLines="None" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        AllowPaging="true" PageSize="20" OnPageIndexChanging="grdviewpo_PageIndexChanging"
                                                                                                        OnDataBound="grdviewpo_DataBound" EmptyDataText="There Is No Records" DataKeyNames="indent_no,po_no,Type,Approved_Users"
                                                                                                        OnRowEditing="grdviewpo_RowEditing" OnRowDataBound="grdviewpo_RowDataBound" OnRowDeleting="grdviewpo_RowDeleting">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                            <asp:TemplateField HeaderText="PO No">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblpo" runat="server" Text='<%#Eval("po_no") %>'></asp:Label>
                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="po_date" HeaderText="DO Date" />
                                                                                                            <asp:BoundField DataField="Indent_no" HeaderText="Indent No" />
                                                                                                            <asp:BoundField DataField="Ref_no" HeaderText="Ref No" />
                                                                                                            <asp:BoundField DataField="Ref_Date" HeaderText="Ref Date" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                                                                            <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                                                                                            <asp:BoundField DataField="amount" HeaderText="DO Value" DataFormatString="{0:N2}" />
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("status")%>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Reject">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="lnkreject" CommandName="Delete" runat="server"><img src="images/Delete.jpg" alt='Reject' /></asp:LinkButton>
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
                                                                                                    <asp:HiddenField ID="hfpotype" runat="server" />
                                                                                                    <cc1:ModalPopupExtender ID="poppo" BehaviorID="mdlindent" runat="server" TargetControlID="btnModalPopUp"
                                                                                                        PopupControlID="pnlraisepo" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                                    <asp:Panel ID="pnlraisepo" runat="server" Style="display: none;">
                                                                                                        <table id="Table1" width="800px" border="0" align="center" runat="server" cellpadding="0"
                                                                                                            cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td width="13" valign="bottom">
                                                                                                                    <img src="images/leftc.jpg">
                                                                                                                </td>
                                                                                                                <td class="pop_head" align="left">
                                                                                                                    <div class="popclose">
                                                                                                                        <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                                    </div>
                                                                                                                    <asp:Label ID="lblviewpo" runat="server" Text=""></asp:Label>
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
                                                                                                                        height: 600px;">
                                                                                                                        <asp:UpdatePanel ID="upslots" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upslots" runat="server">
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
                                                                                                                                            <table>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="2" valign="middle" id="tdvencode" style="width: 50%" runat="server">
                                                                                                                                                        <asp:DropDownList ID="ddlvendor" CssClass="filter_item" ToolTip="Vendor" runat="server"
                                                                                                                                                            onchange="javascript:SetDynamicKey('dp9',this.value);">
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                        <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="ven" TargetControlID="ddlvendor"
                                                                                                                                                            ServiceMethod="vendor" ServicePath="cascadingDCA.asmx">
                                                                                                                                                        </cc1:CascadingDropDown>
                                                                                                                                                        <asp:Label ID="Label3" class="ajaxspan" runat="server"></asp:Label>
                                                                                                                                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp9" runat="server"
                                                                                                                                                            TargetControlID="lblname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                                                                                            ServiceMethod="GetVendorNameAddress">
                                                                                                                                                        </cc1:DynamicPopulateExtender>
                                                                                                                                                        <asp:HiddenField ID="hfvendor" runat="server" />
                                                                                                                                                    </td>
                                                                                                                                                    <td colspan="2">
                                                                                                                                                        <%--<asp:GridView ID="grdpopedit" runat="server" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                            AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                            PagerStyle-CssClass="grid pagerbar" AutoGenerateColumns="false" OnRowDataBound="grdpopedit_RowDataBound">
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
                                                                                                                                                                <asp:BoundField DataField="Quantity" HeaderText="Raised Quantity" />
                                                                                                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                    <FooterTemplate>
                                                                                                                                                                        <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                                    </FooterTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField>
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:HiddenField ID="h2" runat="server" Value="" />
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                            </Columns>
                                                                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                                        </asp:GridView>--%>
                                                                                                                                                        <tr valign="bottom" id="tb12" runat="server">
                                                                                                                                                            <td align="center">
                                                                                                                                                                <table id="tbldo" runat="server" width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <table id="Table2" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/essellogo1.jpg" Height="40px"
                                                                                                                                                                                            Width="89px" />
                                                                                                                                                                                        &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                                                                                                                                        <asp:Label ID="Label1" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="ESSEL PROJECTS PVT LTD." Font-Size="XX-Large" Font-Names="Rockwell"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label13" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                                                                                            Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label15" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                                                                                            Text="Tel/Fax:0771-4268469/4075401." Font-Size="Smaller"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="lblpo" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="DELIVERY ORDER"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table3" width="100%" runat="server" class="pestbl" style="">
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td width="50%" align="left" style="border: 1px solid #000">
                                                                                                                                                                                        <asp:Label ID="lblname" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="lbladdress" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" width="100%" style="border: 1px solid #000">
                                                                                                                                                                                        <asp:Label ID="Label4" Width="25%" CssClass="peslbl1" runat="server" Text="DO No:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtpono" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="DO NO"
                                                                                                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                                                                                                        <asp:Label ID="Label5" Width="25%" CssClass="peslbl1" runat="server" Text="DO Date:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtpodate" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="DO Date"
                                                                                                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label6" Width="25%" CssClass="peslbl1" runat="server" Text="Ref No:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtrefno" Width="60%" CssClass="pestbox" ToolTip="Ref No" Style="border: None;
                                                                                                                                                                                            border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                                                                                                        <asp:Label ID="Label7" Width="25%" CssClass="peslbl1" runat="server" Text="Ref Date:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtrefdate" Width="60%" CssClass="pestbox" ToolTip="Ref Date" Style="border: None;
                                                                                                                                                                                            border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                                                                                        <br />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table4" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2">
                                                                                                                                                                                        <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                                                                                            <Columns>
                                                                                                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                                                                                                <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                                                                                                <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" HeaderText="Units" />
                                                                                                                                                                                                <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                            </Columns>
                                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td>
                                                                                                                                                                                        <asp:Label ID="lblremarks" runat="server" Text="Remarks" CssClass="peslbl1"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left">
                                                                                                                                                                                        <asp:TextBox ID="txtremarks" MaxLength="150" ToolTip="Remarks" CssClass="peslbl1"
                                                                                                                                                                                            runat="server" Width="600px" Text="" Style="border: None; border-bottom: 1px solid #000"></asp:TextBox>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table5" width="100%" runat="server" class="pestbl" style="border-collapse: collapse">
                                                                                                                                                                                <tr style="border-collapse: collapse">
                                                                                                                                                                                    <td colspan="2">
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label8" CssClass="peslbl1" runat="server" Text="">You are requested to supply/deliver above item/s to our work site / Central store  at</asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtrecievedcc" Width="25%" Enabled="false" CssClass="pestbox" Style="border: None;
                                                                                                                                                                                            border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                                                                                        <asp:Label ID="Label10" CssClass="peslbl1" runat="server" Text="">by</asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtrecieveddate" Width="25%" CssClass="pestbox" Style="border: None;
                                                                                                                                                                                            border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                                                                                        <asp:Label ID="Label12" CssClass="peslbl1" Enabled="false" runat="server" Text="">on credit basis which should at par with the specifications, make etc. Further it is cleared that if   the item(s) supplied by you is/are found inferior/defective, the same will be return to you or deduct the amount of such items from your invoice without any notice.</asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label14" CssClass="peslbl1" runat="server" Enabled="false" Text=""> Validity of this DO :-&nbsp&nbsp This delivery order valid only if the above material delivered at below mentioned address/ location  on or before above specified date.</asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table6" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label9" runat="server" CssClass="peslblfooter" Width="170px" Text="">For Essel Projects Pvt Ltd</asp:Label>
                                                                                                                                                                                        &nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="lblinv" runat="server" CssClass="peslblfooter" Width="223px" Text="INVOICE ADDRESS :"
                                                                                                                                                                                            Font-Underline="True"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label16" runat="server" CssClass="peslblfooter" Text="" Font-Underline="True">DELIVERY SITE ADDRESS:</asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label18" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvoiceAdd" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblSaddress" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label19" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvoiceAdd2" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblSaddress2" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label21" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvgst" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblCperson" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label23" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvMobileNum" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblMobileNum" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label11" CssClass="peslblfooter" runat="server" Text=""> Authorized Signatory</asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                                                                                                                                                                                            Text=""></asp:Label>)
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td align="left" colspan="3">
                                                                                                                                                                                        <asp:Label ID="Label30" runat="server" CssClass="peslblfooter" Text="" Font-Size="XX-Small">*    It is an electronically generated DO
                                                                                                                                                                                        </asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                                <table id="tblpo" runat="server" width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <table id="Table7" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/essellogo1.jpg" Height="40px"
                                                                                                                                                                                            Width="89px" />
                                                                                                                                                                                        &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                                                                                                                                        <asp:Label ID="Label20" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="ESSEL PROJECTS PVT LTD." Font-Size="XX-Large" Font-Names="Rockwell"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label22" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                                                                                            Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label24" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                                                                                            Text="Tel/Fax:0771-4268469/4075401." Font-Size="Smaller"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label25" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="PURCHASE ORDER"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table3po" width="100%" runat="server" class="pestbl" style="">
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td width="50%" align="left" style="border: 1px solid #000">
                                                                                                                                                                                        <%--<asp:TextBox ID="txtaddress" runat="server" Width="100%" TextMode="MultiLine" Style="border: None;"></asp:TextBox>--%>
                                                                                                                                                                                        <asp:Label ID="lblnamepo" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="lbladdresspo" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" width="100%" style="border: 1px solid #000">
                                                                                                                                                                                        <asp:Label ID="Label4po" Width="25%" CssClass="peslbl1" runat="server" Text="PO No:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtponopo" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="PO NO"
                                                                                                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                                                                                                        <asp:Label ID="Label5po" Width="25%" CssClass="peslbl1" runat="server" Text="PO Date:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtpodatepo" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="PO Date"
                                                                                                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label6po" Width="25%" CssClass="peslbl1" runat="server" Text="Ref No:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtrefnopo" Width="60%" CssClass="pestbox" ToolTip="Ref No" Style="border: None;
                                                                                                                                                                                            border-bottom: 1px solid #000" runat="server" Enabled="false"></asp:TextBox><br />
                                                                                                                                                                                        <asp:Label ID="Label7po" Width="25%" CssClass="peslbl1" runat="server" Text="Ref Date:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtrefdatepo" Width="60%" CssClass="pestbox" ToolTip="Ref Date"
                                                                                                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server" Enabled="false"></asp:TextBox>
                                                                                                                                                                                        <br />
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table4po" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2">
                                                                                                                                                                                        <div id="divGrid" style="width: auto; float: left;">
                                                                                                                                                                                            <asp:GridView CssClass="" ID="grdbillpo" Width="750px" runat="server" AutoGenerateColumns="false"
                                                                                                                                                                                                OnRowDataBound="grdbillpo_RowDataBound" ShowFooter="true" DataKeyNames="item_code">
                                                                                                                                                                                                <Columns>
                                                                                                                                                                                                    <asp:TemplateField HeaderStyle-BackColor="White">
                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                            <asp:CheckBox ID="chkSelectpo" runat="server" />
                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                    <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                    <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderText="Item Code"
                                                                                                                                                                                                        HeaderStyle-BackColor="White" />
                                                                                                                                                                                                    <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                                                                                                    <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                                                                                                    <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                    <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Units" />
                                                                                                                                                                                                    <asp:BoundField DataField="basic_price" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        HeaderText="Standard Price" />
                                                                                                                                                                                                    <asp:BoundField DataField="quoted_price" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        HeaderText="Quoted Price" />
                                                                                                                                                                                                    <asp:BoundField DataField="New_basicprice" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        HeaderText="Purchase Price" />
                                                                                                                                                                                                    <asp:BoundField DataField="Amt" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                </Columns>
                                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                                            <asp:GridView CssClass="" ID="grdbillpoprint" Width="750px" runat="server" AutoGenerateColumns="false"
                                                                                                                                                                                                ShowFooter="false">
                                                                                                                                                                                                <Columns>
                                                                                                                                                                                                    <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                    <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderText="Item Code"
                                                                                                                                                                                                        HeaderStyle-BackColor="White" />
                                                                                                                                                                                                    <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                                                                                                    <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                                                                                                    <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                    <asp:BoundField DataField="New_basicprice" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        HeaderText="Purchase Price" />
                                                                                                                                                                                                    <asp:BoundField DataField="Amt" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                </Columns>
                                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                                        </div>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="2">
                                                                                                                                                                                        <div runat="server" id="divDetail" class="divDetail" onmouseover="highlight(this, event)"
                                                                                                                                                                                            onmouseout="highlight(this, event)">
                                                                                                                                                                                        </div>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="2">
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
                                                                                                                                                                                                <asp:TemplateField HeaderText="Terms & Conditions" ItemStyle-Width="100%" ItemStyle-HorizontalAlign="Center"
                                                                                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:TextBox ID="txtterms" runat="server" Text='<%#Bind("splitdata") %>' onkeypress="return isNumberKey(event)"
                                                                                                                                                                                                            onblur="chksplcharactersdesc()" Width="600px" /><br />
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
                                                                                                                                                                                        <asp:GridView ID="grdtermsprint" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                                                            PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                                                            Width="100%" GridLines="None">
                                                                                                                                                                                            <Columns>
                                                                                                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:BoundField DataField="splitdata" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                            </Columns>
                                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                            <table id="Table6po" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label9po" runat="server" CssClass="peslblfooter" Width="170px" Text="">For Essel Projects Pvt Ltd</asp:Label>
                                                                                                                                                                                        &nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="lblinvpo" runat="server" CssClass="peslblfooter" Width="223px" Text="INVOICE ADDRESS :"
                                                                                                                                                                                            Font-Underline="True"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label16po" runat="server" CssClass="peslblfooter" Text="" Font-Underline="True">DELIVERY SITE ADDRESS:</asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label18po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvoiceAddpo" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblSaddresspo" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label19po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvoiceAdd2po" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblSaddress2po" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label21po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvgstpo" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblCpersonpo" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label23po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblinvMobileNumpo" CssClass="peslblfooter" runat="server" Text=""
                                                                                                                                                                                            Width="200px"> </asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblMobileNumpo" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <asp:Label ID="Label11po" CssClass="peslblfooter" runat="server" Text=""> Authorized Signatory</asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagernamepo" runat="server" Style="vertical-align: middle"
                                                                                                                                                                                            Text=""></asp:Label>)
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td align="left" colspan="3">
                                                                                                                                                                                        <asp:Label ID="Label30po" runat="server" CssClass="peslblfooter" Text="" Font-Size="XX-Small">*    It is an electronically generated DO
                                                                                                                                                                                        </asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                            </table>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td id="print" runat="server" align="center">
                                                                                                                                                                <input class="buttonprint" type="button" onclick="print();" value="Print" title="Print Report">
                                                                                                                                                                <%--for tblpo--%>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td height="10px">
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr id="Trgvusers" runat="server" style="border: 1px #000000 solid;">
                                                                                                                                        <td>
                                                                                                                                            <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="" GridLines="none" Width="740px" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                                                                                                                                <HeaderStyle CssClass="headerstyle" />
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Status" ItemStyle-Wrap="false" />
                                                                                                                                                    <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                                                                                                                    <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                                                                                                                </Columns>
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                                                            </asp:GridView>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr id="trdobtns" runat="server">
                                                                                                                                        <td align="center" id="btn" runat="server">
                                                                                                                                            <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                            <asp:Button ID="btnmdlupd" runat="server" Text="Update" CssClass="button" OnClientClick="javascript:return validate()"
                                                                                                                                                OnClick="btnmdlupd_Click" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr id="trpobtns" runat="server">
                                                                                                                                        <td align="center" id="btnpo" runat="server">
                                                                                                                                            <asp:Label ID="Label17" runat="server" CssClass="red"></asp:Label>
                                                                                                                                            <asp:Button ID="Button1" runat="server" Text="Update" CssClass="button" OnClientClick="javascript:return validatepo()"
                                                                                                                                                OnClick="btnmdlupdpo_Click" />
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
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <script language="javascript" type="text/javascript">
            function validatepo() {
                //debugger;
                var GridView = document.getElementById("<%=grdbillpo.ClientID %>");

                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify itemcode" + GridView.rows(rowCount).cells(2).innerHTML);
                            return false;
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
    </table>
</asp:Content>
