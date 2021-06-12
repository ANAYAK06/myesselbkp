<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="VerifyDcaBudget.aspx.cs"
    Inherits="VerifyDcaBudget" Title="Verify DCA Budget" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validaterej(DCACode) {

            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                    window.alert("Please Select Checkbox");
                    return false;
                }
                else {
                    GridView.rows(rowCount).cells(0).children(0).checked = false;
                    return true;
                }
            }
        }

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=GridView1.ClientID %>");
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
    <script language="javascript">
        function validation() {
            //debugger;
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            var role = document.getElementById('<%= hf1.ClientID%>').value;
            if (role != "Project Manager") {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(3).children(0).value == "" || GridView.rows(rowCount).cells(3).children(0).value == 0) {
                        window.alert("Enter DCA Budget or Delete the DCA");
                        (GridView.rows(rowCount).cells(3).children(0).value = "");
                        (GridView.rows(rowCount).cells(3).children(0).focus());
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(7).children(0).value != "Active") {
                        window.alert(GridView.rows(rowCount).cells(1).children(0).innerHTML + " is blocked,please delete from budget");
                        return false;

                    }
                }
                checkbudget();
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                return true;
            }
            else {
                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(3).children(0).value == "" || GridView.rows(rowCount).cells(3).children(0).value == 0) {
                        window.alert("Enter DCA Budget or Delete the DCA");
                        (GridView.rows(rowCount).cells(3).children(0).value = "");
                        (GridView.rows(rowCount).cells(3).children(0).focus());
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(7).children(0).value != "Active") {
                        window.alert(GridView.rows(rowCount).cells(1).children(0).innerHTML + " is blocked,please delete from budget");
                        return false;

                    }
                }
                checkbudget();
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                return true;
            }
        }
    </script>
    <script language="javascript">
        function filldata() {
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            GridView.rows[GridView.rows.length - 1].cells(4).children(0).style.display = 'none';
            GridView.rows[GridView.rows.length - 1].cells(1).children(0).style.display = 'none';
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {

                if (GridView.rows(rowCount).cells(3).children(0).value == "" || GridView.rows(rowCount).cells(3).children(0).value == 0) {
                    window.alert("Enter DCA Budget or Delete the DCA");
                    (GridView.rows(rowCount).cells(3).children(0).value = "");
                    (GridView.rows(rowCount).cells(3).children(0).focus());
                    return false;
                }

            }
            checkbudget();
        }
    </script>
    <script language="javascript">
        function checkbudget() {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var bal = document.getElementById("<%=h1.ClientID %>").value;
            var amt = 0;
            for (var i = 1; i < sumgrid.rows.length - 1; i++) {
                if (!isNaN(sumgrid.rows[i].cells[3].children[0].value)) {
                    amt += Number(sumgrid.rows[i].cells[3].children[0].value);

                }

            }
            sumgrid.rows[sumgrid.rows.length - 1].cells[3].innerHTML = amt;
            if (bal < amt) {
                alert("Insufficient Balance");

                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function checkbudget1(value) {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var bal = document.getElementById("<%=h2.ClientID %>").value;
            if (bal < parseInt(value)) {
                alert("Insufficient Balance");
                sumgrid.rows[sumgrid.rows.length - 1].cells[3].children[0].value = "";
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        //        function validate() {
        //            var objs = new Array("<%=ddlcctype.ClientID %>")
        //            if (!CheckInputs(objs)) {
        //                return false;
        //            }
        //            else if (document.getElementById("<%=ddlCCcode.ClientID %>").selectedIndex == 0 && document.getElementById("<%=ddlCCcode.ClientID %>").disabled == false) {
        //                alert("Select Cost Center");
        //                return false;
        //            }

        //            else if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex != 1 && document.getElementById("<%=ddlCCcode.ClientID %>").disabled == false && document.getElementById("<%=ddlyear.ClientID %>").selectedIndex == 0) {
        //                alert("Select Year");
        //                return false;
        //            }

        //            else if (document.getElementById("<%=ddlCCcode.ClientID %>").disabled == true) {
        //                alert("There is no Verified Cost Centers");
        //                return false;
        //            }
        //        }

        function validate() {

            var type = document.getElementById("<%=ddlcctype.ClientID %>").value;

            var ccode = document.getElementById("<%=ddlCCcode.ClientID %>").value;

            if (type == "Select") {
                window.alert("Select CC Type");
                document.getElementById("<%=ddlcctype.ClientID %>").focus();
                return false;
            }
            if (type == "Performing") {
                var subtype = document.getElementById("<%=ddltype.ClientID %>").value;
                if (subtype == "Select") {
                    window.alert("Select Sub Type");
                    document.getElementById("<%=ddltype.ClientID %>").focus();
                    return false;
                }
            }
            if (ccode == "" || ccode == "Select Cost Center") {
                window.alert("Select Cost Center");
                document.getElementById("<%=ddlCCcode.ClientID %>").focus();
                return false;
            }
            else if (type != "Performing") {
                var year = document.getElementById("<%=ddlyear.ClientID %>").value;
                if (year == "Select Year" || year == "") {

                    window.alert("Select Year");
                    document.getElementById("<%=ddlyear.ClientID %>").focus();
                    return false;
                }
            }

        }
    </script>
    <script language="javascript">
        function footervalidation() {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var dcacode = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).selectedIndex;
            var Amount = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
            var bal = document.getElementById("<%=h2.ClientID %>").value;
            if (bal > 0) {
                if (dcacode == 0) {
                    alert("Select DCA");
                    return false;
                }
                else if (Amount == "") {
                    alert("Enter Amount");
                    return false;

                }
            }
            else {
                alert("Insufficient Balance");
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
                        <table id="_terp_list_grid" cellpadding="0" cellspacing="0" class="grid" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center" colspan="2">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Verify DCA Budget"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="tr" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlcctype" runat="server" ToolTip="Cost Center" Width="175px"
                                                    CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Performing</asp:ListItem>
                                                    <asp:ListItem>Non-Performing</asp:ListItem>
                                                    <asp:ListItem>Capital</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trtype" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label16" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="175px" CssClass="esddown"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Service</asp:ListItem>
                                                    <asp:ListItem>Trading</asp:ListItem>
                                                    <asp:ListItem>Manufacturing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trccode" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="175px"
                                                    CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlCCcode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblccbud" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" runat="server" TargetControlID="lblccbud"
                                                    ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx" ServiceMethod="GetCCName">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                        </tr>
                                        <tr id="year" runat="server">
                                            <td style="width: 150px">
                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="175px" runat="server" ToolTip="Year">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" OnClientClick="return validate()"
                                                    Style="font-size: small" Text="View" OnClick="btnAssign_Click" />
                                                <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                                    Text="Reset" OnClick="btnCancel1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="header" runat="server">
                                <td>
                                    <table width="100%" class="accordionHeader">
                                        <tr>
                                            <td width="100px" align="center">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("cc_code")%>'></asp:Label>
                                            </td>
                                            <td width="70px" align="center">
                                                CC Budget
                                            </td>
                                            <td width="70px" align="center">
                                                Assigned to DCA
                                            </td>
                                            <td width="70px" align="center">
                                                Balance
                                            </td>
                                            <td width="50px" align="center">
                                                <asp:Label ID="Label10" runat="server" Text="Year"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100px" align="center">
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("cc_name")%>'></asp:Label>
                                            </td>
                                            <td width="70px" align="center">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("budget_amount")%>'></asp:Label>
                                            </td>
                                            <td width="110px" align="center">
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("Consumed")%>'></asp:Label>
                                            </td>
                                            <td width="110px" align="center">
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("balance")%>'></asp:Label>
                                            </td>
                                            <td width="100px" align="center">
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("year")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="grid" runat="server">
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                        PagerStyle-CssClass="grid pagerbar" PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        Width="100%" ShowFooter="true" GridLines="None" DataKeyNames="dcayearly_id" OnRowDataBound="GridView1_RowDataBound"
                                        OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting1" OnRowCancelingEdit="GridView1_RowCancelingEdit1"
                                        OnRowEditing="GridView1_RowEditing1" OnRowUpdating="GridView1_RowUpdating1">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dca Code" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="id" runat="server" Text='<%# Bind("mapdca_code") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddldetailhead" CssClass="esddown" Width="100px" ToolTip="DCA"
                                                        runat="server" onchange="SetDynamicKey('dp3',this.value);">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dca Name" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="idl" runat="server" Text='<%# Bind("dca_name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="Label5" class="ajaxspan" runat="server"></asp:Label>
                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                        TargetControlID="Label5" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                        ServiceMethod="GetDCAName">
                                                    </cc1:DynamicPopulateExtender>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budget Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbankname" runat="server" Text='<%# Bind("budget_dca_yearly") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtbudget" runat="server" onkeypress="javascript:IsNumeric1(this.value);"
                                                        onkeyup="checkbudget();filldata(this.value);" onblur="" Text='<%# Bind("budget_dca_yearly") %>'></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtbud" runat="server" onkeyup="checkbudget1(this.value);"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="linkAddCust" CssClass="button" OnClientClick="return footervalidation();"
                                                        CommandName="ADD" runat="server">Add</asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="true" CancelText="Cancel" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h3" runat="server" Value='<%#Eval("budget_dca_yearly")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hstatus" runat="server" Value='<%#Eval("Status")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                        <PagerStyle CssClass="grid pagerbar" />
                                        <HeaderStyle CssClass="grid-header" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="h1" runat="server" />
                                    <asp:HiddenField ID="h2" runat="server" />
                                </td>
                            </tr>
                            <tr align="center" id="trbtnsubmit" runat="server">
                                <td align="center">
                                    <asp:Button ID="btnsubmit" OnClientClick="return validation()" runat="server" CssClass="button"
                                        Style="font-size: small" Text="Verify Budget" OnClick="btnsubmit_Click" />
                                        <asp:HiddenField ID="hf1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
