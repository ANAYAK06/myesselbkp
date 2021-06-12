<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAddVendor.aspx.cs"
    Inherits="Admin_frmAddVendor" EnableEventValidation="false" Title="Add Vendor - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function check() {
            var type = document.getElementById("<%=ddlVType.ClientID %>").value;
            var tvatpan = document.getElementById("<%=txtvatpan.ClientID %>");
            var lvatpan = document.getElementById("<%=lblvatpan.ClientID %>");
            var ttintax = document.getElementById("<%=txttintax.ClientID %>");
            var ltintax = document.getElementById("<%=lbltintax.ClientID %>");
            var tcstpf = document.getElementById("<%=txtcstpf.ClientID %>");
            var lcstpf = document.getElementById("<%=lblcstpf.ClientID %>");
            if (type == 'Service Provider') {
                tvatpan.title = lvatpan.innerHTML = "PAN No";
                ttintax.title = ltintax.innerHTML = "ServiceTax No";
                tcstpf.title = lcstpf.innerHTML = "PF Reg No";
            }
            else {
                tvatpan.title = lvatpan.innerHTML = "VAT No";
                ttintax.title = ltintax.innerHTML = "TIN No";
                tcstpf.title = lcstpf.innerHTML = "CST No";
            }
        }
        function validate() {
            var objs = new Array("<%=ddlVType.ClientID %>", "<%=txtvatpan.ClientID %>", "<%=txttintax.ClientID %>", "<%=txtcstpf.ClientID %>",
                                "<%=txtVName.ClientID %>", "<%=txtAddress.ClientID %>");
            //            return CheckInputs(objs);
            if (!CheckInputs(objs)) {
                return false;
            }
            if (!ChceckRBL("<%=rbtngst.ClientID %>")) {
                return false;
            }
            //debugger;
            if (SelectedIndex("<%=rbtngst.ClientID %>") == 0) {
                GridView3 = document.getElementById("<%=gvother.ClientID %>");
                if (GridView3 != null) {
                    for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                        if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                            window.alert("Select State");
                            GridView3.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(3).children[0].value == "") {
                            window.alert("GST No Required");
                            GridView3.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }
                    }
                }
            }
            document.getElementById("<%=btnAddVendor.ClientID %>").style.display = 'none';
            return true;

        }
        function validate1() {

            var objs = new Array("<%=txtvendorname.ClientID %>", "<%=txtvatpanupdate.ClientID %>", "<%=txttintaxupdate.ClientID %>", "<%=txtcstpfupdate.ClientID %>",
                                "<%=txtVNameupdate.ClientID %>", "<%=txtAddressupdate.ClientID %>");
            //            return CheckInputs(objs);
            if (!CheckInputs(objs)) {
                return false;
            }
            //debugger;
            if (SelectedIndex("<%=rbtngstu.ClientID %>") == 0) {
                GridView3 = document.getElementById("<%=gvotheru.ClientID %>");
                if (GridView3 != null) {
                    for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                        if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                            window.alert("Select State");
                            GridView3.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(3).children[0].value == "") {
                            window.alert("GST No Required");
                            GridView3.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }
                    }
                }

            }
            document.getElementById("<%=btnUpdate.ClientID %>").style.display = 'none';
            return true;

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
                        <table style="width: 700px">
                            <tr valign="top">
                                <td align="center">
                                    <asp:RadioButton ID="rbtnadd" runat="server" Text="Add Vendor" GroupName="Software"
                                        AutoPostBack="true" OnCheckedChanged="rbtnadd_CheckedChanged" Font-Bold="true"
                                        Font-Names="Courier New" Font-Size="Small" Checked="true" ForeColor="Black" />
                                    <asp:RadioButton ID="rbtnupdate" runat="server" Text="Update Vendor" GroupName="Software"
                                        AutoPostBack="true" OnCheckedChanged="rbtnupdate_CheckedChanged" Font-Bold="true"
                                        Font-Names="Courier New" Font-Size="Small" ForeColor="Black" />
                                </td>
                            </tr>
                            <tr valign="top" id="tbladdvendor" runat="server">
                                <td align="center">
                                    <table class="estbl" width="600px">
                                        <tr>
                                            <th colspan="2" align="center">
                                                <asp:Label CssClass="eslbl" ID="lblvendor" runat="server" Text="Add Vendor"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px">
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Vendor Type"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlVType" runat="server" ToolTip="Vendor Type" Width="300px"
                                                    onchange="check();" CssClass="esddown">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlVType"
                                                    ServicePath="EsselServices.asmx" LoadingText="Please Wait" ServiceMethod="VendorType"
                                                    Category="VendorType" PromptText="Select Vendor Type">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lblvtype" CssClass="eslbl" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblvatpan" runat="server" CssClass="eslbl" Text="VAT No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtvatpan" CssClass="estbox" runat="server" Width="300px" ToolTip="VAT No"
                                                    MaxLength="50"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbltintax" runat="server" CssClass="eslbl" Text="TIN No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txttintax" CssClass="estbox" runat="server" Width="300px" ToolTip="TIN No"
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblcstpf" runat="server" CssClass="eslbl" Text="CST No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtcstpf" CssClass="estbox" runat="server" Width="300px" ToolTip="CST No"
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Vendor Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtVName" CssClass="estbox" runat="server" Width="300px" ToolTip="Vendor Name"
                                                    MaxLength="50"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Phone No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtpno" CssClass="estbox" runat="server" Width="300px" ToolTip="Phone No"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="Mobile No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtmno" CssClass="estbox" runat="server" Width="300px" ToolTip="Mobile No"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Bank Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtbankname" CssClass="estbox" runat="server" Width="300px" ToolTip="Bank Name"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Bank Account No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtacno" CssClass="estbox" runat="server" Width="300px" ToolTip="Bank Account No"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="IFSC Code"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtifsc" CssClass="estbox" runat="server" Width="300px" ToolTip="IFSC Code"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Address"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="estbox" Width="300px" ToolTip="Address"
                                                    TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="600px" style="background-color: #DCDCDC">
                                        <tr align="center">
                                            <td colspan="2">
                                                <asp:Label ID="lblother" runat="server" Font-Size="10px" Width="600px" CssClass="eslbl"
                                                    Text="GST Yes/No"></asp:Label>
                                                <asp:RadioButtonList ID="rbtngst" runat="server" Width="100px" AutoPostBack="true"
                                                    ClientIDMode="AutoID" OnSelectedIndexChanged="rbtngst_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                    Style="font-size: x-small" ToolTip="GST Yes or No">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                    <%--onclick="javascript:return getotherIndex(this)"--%>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trothergrid" runat="server" style="display: none">
                                            <td colspan="2">
                                                <asp:GridView runat="server" ID="gvother" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="600px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvother_RowDeleting" OnRowDataBound="gvother_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkgst" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlstates" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="State" runat="server">
                                                                    <%--onchange="checkotherdca(this)"--%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST No" ItemStyle-Width="125px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtregno" runat="server" CssClass="filter_item" Width="125px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnaddgst" runat="server" Text="Add GST No's" OnClick="btnaddgst_Click" />
                                                                <%--OnClientClick="javascript:return verifyotherdca();"--%>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="50px" DeleteText="Remove" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAddVendor" runat="server" CssClass="esbtn" Text="Add Vendor" OnClientClick="javascript:return validate();"
                                                    OnClick="btnAddVendor_Click1" />
                                                <%-- <asp:Button ID="btnUpdate" runat="server" CssClass="esbtn" Text="Update" OnClientClick="javascript:return validate();"
                                                    Visible="false" OnClick="update_Click" />--%>
                                                <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Text="Reset" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblAlert" CssClass="eslblalert" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="tblverifyvendor" runat="server" width="700px">
                            <tr>
                                <td>
                                    <table id="Table1" align="center" runat="server">
                                        <tr valign="top">
                                            <td align="center">
                                                <table class="estbl" width="600px" align="center">
                                                    <tr>
                                                        <th colspan="2" align="center">
                                                            <asp:Label CssClass="eslbl" ID="Label8" runat="server" Text="Update Vendor"></asp:Label>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Vendor Name"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblvtypeupdate" CssClass="eslbl" runat="server" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtvendorname" Width="300px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ServiceMethod="Searchvendors" MinimumPrefixLength="2" CompletionInterval="100"
                                                                EnableCaching="false" CompletionSetCount="10" TargetControlID="txtvendorname"
                                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                            </cc1:AutoCompleteExtender>
                                                            <asp:Button ID="btngo" runat="server" Text="go" OnClick="btngo_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvatpanupdate" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtvatpanupdate" CssClass="estbox" runat="server" Width="300px"
                                                                ToolTip="VAT No" MaxLength="50"></asp:TextBox><span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbltintaxupdate" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txttintaxupdate" CssClass="estbox" runat="server" Width="300px"
                                                                ToolTip="TIN No" MaxLength="50"></asp:TextBox>
                                                            <span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcstpfupdate" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtcstpfupdate" CssClass="estbox" runat="server" Width="300px" ToolTip="CST No"
                                                                MaxLength="50"></asp:TextBox>
                                                            <span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="Vendor Name"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtVNameupdate" CssClass="estbox" runat="server" Enabled="false" Width="300px" ToolTip="Vendor Name"
                                                                MaxLength="50"></asp:TextBox><span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="Phone No"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtpnoupdate" CssClass="estbox" runat="server" Width="300px" ToolTip="Phone No"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="Mobile No"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtmnoupdate" CssClass="estbox" runat="server" Width="300px" ToolTip="Mobile No"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="Bank Name"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtbanknameupdate" CssClass="estbox" runat="server" Width="300px"
                                                                ToolTip="Bank Name" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="Bank Account No"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtacnoupdate" CssClass="estbox" runat="server" Width="300px" ToolTip="Bank Account No"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="IFSC Code"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtifscupdate" CssClass="estbox" runat="server" Width="300px" ToolTip="IFSC Code"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Address"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAddressupdate" runat="server" CssClass="estbox" Width="300px"
                                                                ToolTip="Address" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table id="tblstateu" runat="server" width="600px" style="background-color: #DCDCDC">
                                                    <tr align="center">
                                                        <td colspan="2">
                                                            <asp:Label ID="Label15" runat="server" Font-Size="10px" Width="600px" CssClass="eslbl"
                                                                Text="GST Yes/No"></asp:Label>
                                                            <asp:RadioButtonList ID="rbtngstu" runat="server" Width="100px" AutoPostBack="true"
                                                                ClientIDMode="AutoID" OnSelectedIndexChanged="rbtngstu_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal" Style="font-size: x-small" ToolTip="GST Yes or No">
                                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                <asp:ListItem Value="No">No</asp:ListItem>
                                                                <%--onclick="javascript:return getotherIndex(this)"--%>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trothergridu" runat="server" style="display: none">
                                                        <td colspan="2">
                                                            <asp:GridView runat="server" ID="gvotheru" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                DataKeyNames="" GridLines="Both" ShowFooter="true" Width="600px" ShowHeaderWhenEmpty="true"
                                                                OnRowDeleting="gvotheru_RowDeleting" OnRowDataBound="gvotheru_RowDataBound">
                                                                <HeaderStyle CssClass="headerstyle" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkgstu" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="State" ItemStyle-Width="200px">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlstatesu" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                                ToolTip="State" runat="server">
                                                                                <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GST No" ItemStyle-Width="125px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtregnou" runat="server" CssClass="filter_item" Width="125px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnaddgstu" runat="server" Text="Add GST No's" OnClick="btnaddgstu_Click" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="50px" DeleteText="Remove" />
                                                                </Columns>
                                                                <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                <HeaderStyle CssClass="grid-header" />
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnUpdate" runat="server" CssClass="esbtn" OnClientClick="javascript:return validate1()"
                                                                OnClick="update_Click" Text="Update" /><%--  OnClick="update_Click"--%>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="Button1" CssClass="esbtn" runat="server" Text="Reset" OnClick="btnCancel1_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
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
    <script type="text/javascript" language="javascript">        //FOR OTHER GRID Starts
        function getotherIndex(r) {
            //debugger;
            var rbs = document.getElementById("<%=rbtngst.ClientID%>");
            var radio = rbs.getElementsByTagName("input");
            var label = rbs.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    var value = radio[i].value;
                    if (value == "Yes") {

                    }
                    else if (value == "No") {

                    }
                }

            }
        }     
        
    </script>
</asp:Content>
