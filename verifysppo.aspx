<%@ Page Language="C#" MasterPageFile="~/Essel.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="verifysppo.aspx.cs" Inherits="verifysppo" Title="VerifyPO" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();
        }
        function showpopup() {
            $find('mdlindent').show();
        }

    </script>
    <style type="text/css">
        .buttonSubmit {
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
    <script language="javascript" type="text/javascript">
        function print() {

            var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();


        }
    </script>
    <script language="javascript">
        function validate() {
//            var GridView = document.getElementById("<%=grdbill.ClientID %>");
//            if (GridView != null) {
//                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
//                    if (rowCount >= 1) {
//                        if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
//                            window.alert("Please verify Items");
//                            return false;
//                        }
//                    }
//                }
//            }
//            var GridView1 = document.getElementById("<%=grdpodesc.ClientID %>");
            //            if (GridView1 != null) {

            //                for (var rowCount = 1; rowCount < GridView1.rows.length; rowCount++) {
            //                    if (rowCount >= 1) {
            //                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
            //                            window.alert("Please verify Terms and Conditions");
            //                            return false;
            //                        }
            //                    }
            //                }

            //            }
            var GridView = document.getElementById("<%=grdbill.ClientID %>");
            debugger;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
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
            document.getElementById("<%=buttonprint.ClientID %>").style.display = 'none';
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
                <table>
                    <tr>
                        <th align="center">
                            <asp:Label CssClass="eslbl" ID="lblvendor" runat="server" Text="Verify PO"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" ShowFooter="true"
                                HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                FooterStyle-BackColor="DarkGray" DataKeyNames="pono" OnRowDeleting="GridView1_RowDeleting"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                    <asp:TemplateField HeaderText="PO No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpono" Enabled="true" runat="server" Text='<%# Bind("pono") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="VendorId" ReadOnly="true" DataField="vendor_id" />
                                    <asp:BoundField HeaderText="Vendor Name" ReadOnly="true" DataField="vendor_name" />
                                    <asp:BoundField HeaderText="CC Code" ReadOnly="true" DataField="cc_code" />
                                    <asp:BoundField HeaderText="DCA Code" ReadOnly="true" DataField="dca_code" />
                                    <asp:BoundField HeaderText="SDca Code" ReadOnly="true" DataField="subdca_code" />
                                    <asp:TemplateField HeaderText="PO Date" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="podate" runat="server" Text='<%# Bind("po_date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtpodate" Width="80px" runat="server" Text='<%# Bind("po_date") %>'></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtpodate"
                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                PopupButtonID="txtpodate">
                                            </cc1:CalendarExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PO Value " ReadOnly="true" ItemStyle-HorizontalAlign="Right"
                                        DataField="po_value" />
                                    <asp:TemplateField HeaderText="New PO value" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpovalue" runat="server" Text='<%# Bind("po_value") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtpovalue" Width="80px" onkeypress='IsNumeric1(event)' runat="server"
                                                Text='<%# Bind("po_value") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="id" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
                                        <td bgcolor="#FFFFFF">&nbsp;
                                        </td>
                                        <td height="180" valign="top" class="popcontent">
                                            <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px; height: 400px;">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                                            <ProgressTemplate>
                                                                <asp:Panel ID="pnl" runat="server" CssClass="popup-div-background">
                                                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <table style="vertical-align: middle;" align="center">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr valign="bottom" id="tblpo" runat="server">
                                                                            <td align="center" colspan="2">
                                                                                <table width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table id="Table3" width="100%" runat="server" class="pestbl" style="">
                                                                                                <tr style="border: 1px solid #000">
                                                                                                    <td width="50%" align="left" style="border: 1px solid #000" colspan="2">
                                                                                                        <asp:Label ID="lblvenname" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                        <asp:Label ID="lblvenaddress" runat="server" Text="" Width="99%" CssClass="pestbox"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="left" width="100%" style="border: 1px solid #000" colspan="2">
                                                                                                        <asp:Label ID="Label4" Width="25%" CssClass="peslbl1" runat="server" Text="PO No:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtpono" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="PO NO"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                        <asp:Label ID="Label5" Width="25%" CssClass="peslbl1" runat="server" Text="PO Date:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtpodate" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="PO Date"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                        <%--    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtpodate"
                                                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                            PopupButtonID="txtpodate">
                                                                                                        </cc1:CalendarExtender>--%>
                                                                                                        <br />
                                                                                                        <asp:Label ID="Label6" Width="25%" CssClass="peslbl1" runat="server" Text="CC Code:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtcccode" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="CC Code"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                        <asp:Label ID="Label7" Width="25%" CssClass="peslbl1" runat="server" Text="DCA Code:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtdcacode" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="DCA Code"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                        <asp:Label ID="Label8" Width="25%" CssClass="peslbl1" runat="server" Text="Vendor Code:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtvencode" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="Vendor Code"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                        <br />
                                                                                                        <asp:Label ID="Label1" Width="25%" CssClass="peslbl1" runat="server" Text="PO Completion Date:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtpocompdate" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="PO Completion Date"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <table id="Table4" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                <tr style="border: 1px solid #000">
                                                                                                    <td colspan="4" style="border: 1px solid #000">
                                                                                                        <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                            OnRowDataBound="grdbill_RowDataBound" HeaderStyle-BackColor="LightGray" DataKeyNames="ID">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="20px"
                                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Description">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:TextBox ID="txtdesc" runat="server" Text='<%#Bind("Description") %>' onkeyup="this.value = this.value.replace(/^[,/]+$/, '')" onblur="chksplcharacters()" TextMode="MultiLine"
                                                                                                                            Width="100%"></asp:TextBox>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <%--<asp:BoundField DataField="Description" HeaderText="Description" />--%>
                                                                                                                <asp:BoundField DataField="unit" HeaderText="Unit" />
                                                                                                                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                                                                                                                <asp:BoundField DataField="ClientRate" HeaderText="Our Rate" />
                                                                                                                <asp:BoundField DataField="PRWRate" HeaderText=" PRW Approved Rate" />
                                                                                                                <asp:BoundField DataField="rate" HeaderText="Rate" />
                                                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                <asp:BoundField DataField="Id" Visible="false" />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border: 1px solid #000" runat="server" id="trpovalue">
                                                                                                    <td style="border: 1px solid #000" align="right">
                                                                                                        <asp:Label ID="lblt" Width="450px" CssClass="peslbl1" Text="Total PO Value" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="border: 1px solid #000" align="center">
                                                                                                        <asp:Label ID="lblpovalue" Width="15%" CssClass="peslbl1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border: 1px solid #000">
                                                                                                    <td colspan="4" style="border: 1px solid #000">
                                                                                                        <asp:GridView CssClass="" ID="grdpodesc" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                            HeaderStyle-BackColor="LightGray" ShowFooter="true" OnRowDeleting="grdterms_RowDeleting">
                                                                                                            <Columns>
                                                                                                                <%-- <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
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
                                                                                                                <asp:BoundField DataField="splitdata" ItemStyle-Font-Bold="true" HeaderText="Terms and Conditions" />--%>
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
                                                                                                                        <asp:TextBox ID="txtterms" runat="server" Text='<%#Bind("splitdata") %>' onkeypress="return isNumberKey(event)" onblur="chksplcharactersdesc()" Width="850px" /><br />
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
                                                                                                <tr id="Trgvusers" runat="server">
                                                                                                    <td colspan="6">
                                                                                                        <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                            PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                            DataKeyNames="" GridLines="none" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                                                                                            <HeaderStyle CssClass="headerstyle" />
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Desc" ItemStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                                                                                <%--  <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="UserID" ItemStyle-Wrap="false" />--%>
                                                                                                            </Columns>
                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                                                            <HeaderStyle CssClass="grid-header" />
                                                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr valign="bottom" id="tblpoprint" style="display: none;" runat="server">
                                                                            <td align="center" colspan="2">
                                                                                <table width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table id="Table2" width="100%" runat="server" class="pestbl" style="">
                                                                                                <tr style="border: 1px solid #000">
                                                                                                    <td width="50%" align="left" style="border: 1px solid #000" colspan="2">
                                                                                                        <asp:Label ID="lblvennameprint" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                        <asp:Label ID="lblvenaddressprint" runat="server" Text="" Width="99%" CssClass="pestbox"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="left" width="100%" style="border: 1px solid #000" colspan="2">
                                                                                                        <asp:Label ID="Label9" Width="25%" CssClass="peslbl1" runat="server" Text="PO No:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtponoprint" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="PO NO"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                        <asp:Label ID="Label10" Width="25%" CssClass="peslbl1" runat="server" Text="PO Date:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtpodateprint" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="PO Date"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                        <br />
                                                                                                        <asp:Label ID="Label11" Width="25%" CssClass="peslbl1" runat="server" Text="CC Code:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtcccodeprint" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="CC Code"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                        <asp:Label ID="Label12" Width="25%" CssClass="peslbl1" runat="server" Text="DCA Code:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtdcacodeprint" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="DCA Code"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                        <asp:Label ID="Label13" Width="25%" CssClass="peslbl1" runat="server" Text="Vendor Code:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtvencodeprint" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="Vendor Code"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                        <br />
                                                                                                        <asp:Label ID="Label14" Width="25%" CssClass="peslbl1" runat="server" Text="PO Completion Date:-"></asp:Label>
                                                                                                        <asp:TextBox ID="txtpocompdateprint" Width="60%" CssClass="pestbox" Enabled="false" ToolTip="PO Completion Date"
                                                                                                            Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <table id="Table5" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                <tr style="border: 1px solid #000">
                                                                                                    <td colspan="4" style="border: 1px solid #000">
                                                                                                        <asp:GridView CssClass="" ID="grdbillprint" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                            HeaderStyle-BackColor="LightGray" DataKeyNames="ID">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="20px"
                                                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Description">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:TextBox ID="txtdesc" runat="server" Text='<%#Bind("Description") %>' onkeyup="this.value = this.value.replace(/^[,/]+$/, '')" onblur="chksplcharacters()" TextMode="MultiLine"
                                                                                                                            Width="100%"></asp:TextBox>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:BoundField DataField="unit" HeaderText="Unit" />
                                                                                                                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                                                                                                                <asp:BoundField DataField="ClientRate" HeaderText="Our Rate" />
                                                                                                                <asp:BoundField DataField="PRWRate" HeaderText=" PWR Rate" />
                                                                                                                <asp:BoundField DataField="rate" HeaderText="Rate" />
                                                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                <asp:BoundField DataField="Id" Visible="false" />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border: 1px solid #000" runat="server" id="trpovalueprint">
                                                                                                    <td style="border: 1px solid #000" align="right">
                                                                                                        <asp:Label ID="lbltprint" Width="450px" CssClass="peslbl1" Text="Total PO Value" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="border: 1px solid #000" align="center">
                                                                                                        <asp:Label ID="lblpovalueprint" Width="15%" CssClass="peslbl1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border: 1px solid #000">
                                                                                                    <td colspan="4" style="border: 1px solid #000">
                                                                                                        <asp:GridView CssClass="" ID="grdpodescprint" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                            HeaderStyle-BackColor="LightGray" ShowFooter="true" >
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
                                                                                                                        <asp:TextBox ID="txtterms" runat="server" Text='<%#Bind("splitdata") %>' onkeypress="return isNumberKey(event)" onblur="chksplcharactersdesc()" Width="850px" /><br />
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
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="Tr1" runat="server">
                                                                                                    <td colspan="6">
                                                                                                        <asp:GridView runat="server" ID="gvusersprint" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                            PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                            DataKeyNames="" GridLines="none" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusersprint_RowDataBound">
                                                                                                            <HeaderStyle CssClass="headerstyle" />
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Desc" ItemStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                                                                                <%--  <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="UserID" ItemStyle-Wrap="false" />--%>
                                                                                                            </Columns>
                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                                                            <HeaderStyle CssClass="grid-header" />
                                                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td id="print" runat="server" align="center" colspan="2">
                                                                    <asp:Button ID="buttonprint" runat="server" Text="Approve" OnClick="buttonprint_Click" OnClientClick="javascript:return validate()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </td>
                                        <td bgcolor="#FFFFFF">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript">


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



        function filldata1() {
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
                if (GridView.rows(rowCount).cells(6).children[0].value == "") {
                    window.alert("Enter Date");
                    (GridView.rows(rowCount).cells(6).children[0].focus());
                    return false;
                }
                else if (GridView.rows(rowCount).cells(8).children[0].value == "") {
                    window.alert("Enter New PO Value");
                    (GridView.rows(rowCount).cells(8).children[0].focus());
                    return false;
                }
                else if (GridView.rows(rowCount).cells(9).children[0].value == "") {
                    window.alert("Enter Remarks");
                    (GridView.rows(rowCount).cells(9).children[0].focus());
                    return false;
                }
            }
        }
        function chksplcharactersdesc() {
            //debugger;
            var gvdterms = document.getElementById("<%=grdpodesc.ClientID %>");
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
        function chksplcharacters() {
            //debugger;
            var Grid = document.getElementById("<%=grdbill.ClientID %>");
            if (Grid != null) {
                for (var rowCount = 1; rowCount < Grid.rows.length - 1; rowCount++) {
                    if (Grid.rows(rowCount).cells(2).children[0].value != "") {
                        if (Grid.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            //Grid.rows(rowCount).cells(2).children[0].value = "";
                            window.alert("Comma (,) are not allowed in Item Description");
                            return false;
                        }
                    }

                }
            }
        }
    </script>
</asp:Content>
