<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AmendDCABudget.aspx.cs"
    Inherits="AmendDCABudget" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {

            //              var type = document.getElementById("<%=ddlcctype.ClientID %>").value;
            //              var ccode = document.getElementById("<%=ddlCCcode.ClientID %>").value;

            //              if (type == "Select") {
            //                  window.alert("Select Type");
            //                  document.getElementById("<%=ddlcctype.ClientID %>").focus();
            //                  return false;
            //              }
            //              if (ccode == "") {
            //                  window.alert("Select Cost Center");
            //                  document.getElementById("<%=ddlCCcode.ClientID %>").focus();
            //                  return false;
            //              }
            //              else if (type != "Performing") {
            //                  var year = document.getElementById("<%=ddlyear.ClientID %>").value;
            //                  if (year == "") {

            //                      window.alert("Select Year");
            //                      document.getElementById("<%=ddlyear.ClientID %>").focus();
            //                      return false;
            //                  }
            //              }
            var objs = new Array("<%=ddlcctype.ClientID %>", "<%=ddlCCcode.ClientID %>", "<%=ddlyear.ClientID %>")
            if (!CheckInputs(objs)) {
                return false;
            }

        }
        function IsNumeric2(evt) {
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
        function Checkbudget(value) {
            var Add = 0;
            var subtract = 0;
            var bal1 = 0;

            var bal = document.getElementById("<%=h3.ClientID %>").value;
            var balance = document.getElementById("<%=h2.ClientID %>").value;

            var row = parseFloat(bal) + 1;
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var sumgrid2 = document.getElementById("<%=GridView2.ClientID %>");
            if (sumgrid.rows(row).cells(5).children(0).selectedIndex == 2) {
                if (parseFloat(sumgrid.rows[row].cells[4].innerHTML) < parseFloat(value)) {
                    alert("Insufficient Balance");
                    sumgrid.rows(row).cells(6).children(0).value = "";
                    return false;

                }
            }
            else if (sumgrid.rows(row).cells(7).children(0).value == "InActive" && sumgrid.rows(row).cells(5).children(0).selectedIndex == 1) {

                alert("You could not add due to this DCA is blocked");
                sumgrid.rows(row).cells(5).children(0).value = "Select";
                return false;


            }
            else if (sumgrid.rows(row).cells(5).children(0).selectedIndex == 1) {
                if (sumgrid2 != null) {
                    for (var i = 1; i < sumgrid2.rows.length - 1; i++) {
                        if (sumgrid2.rows[i].cells[2].innerHTML != "") {
                            Add += Number(sumgrid2.rows[i].cells[2].innerHTML);
                        }
                        else if (!isNaN(sumgrid2.rows[i].cells[3].innerHTML)) {
                            subtract += Number(sumgrid2.rows[i].cells[3].innerHTML);
                        }
                    }
                }
                //bal = parseFloat(balance) - (Add - subtract);
                if (balance < parseFloat(value)) {
                    alert("Insufficient Balance");
                    sumgrid.rows(row).cells(6).children(0).value = "";
                    return false;
                }

            }

            else {
                alert("Select amend type");
                sumgrid.rows(row).cells(5).children(0).focus();
                sumgrid.rows(row).cells(6).children(0).value = "";
                return false;
            }
        }
    </script>
    <script language="javascript">
        function footervalidation() {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var dcacode = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).selectedIndex;
            var Amount = sumgrid.rows[sumgrid.rows.length - 1].cells(6).children(0).value;
            if (dcacode == 0) {
                alert("Select DCA");
                return false;
            }
            else if (Amount == "") {
                alert("Enter DCA Budget");
                return false;

            }
        }
    </script>
    <script language="javascript">
        function checkdcabudget(value) {
            var sumgrid = document.getElementById("<%=GridView2.ClientID %>");
            sumgrid1 = document.getElementById("<%=GridView1.ClientID %>");
            var balance = document.getElementById("<%=h2.ClientID %>").value;
            var Add = 0;
            var subtract = 0;
            var bal = 0;
            if (sumgrid != null) {
                for (var i = 1; i < sumgrid.rows.length - 1; i++) {
                    if (sumgrid.rows[i].cells[2].innerHTML != "") {
                        Add += Number(sumgrid.rows[i].cells[2].innerHTML);
                    }
                    else if (!isNaN(sumgrid.rows[i].cells[3].innerHTML)) {
                        subtract += Number(sumgrid.rows[i].cells[3].innerHTML);
                    }
                }
            }
            // bal = parseFloat(balance) - (Add - subtract);
            if (balance < parseFloat(value)) {
                alert("Insufficient Balance");
                sumgrid1.rows[sumgrid1.rows.length - 1].cells(6).children(0).value = "";
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function balcheck(add, subtract) {

        }
    </script>
    <script language="javascript">
        function gridvalidate() {
            var GridView = document.getElementById("<%=GridView2.ClientID %>");
            var balance = document.getElementById("<%=h2.ClientID %>").value;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }

            if (parseFloat(balance) < 0) {
                alert("Insufficient CC Balance");

                return false;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript">
        function ValidaDCA() {
            var bal = document.getElementById("<%=h3.ClientID %>").value;
            var row = parseFloat(bal) + 1;
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            if (sumgrid.rows(row).cells(7).children(0).value == "InActive" && sumgrid.rows(row).cells(5).children(0).selectedIndex == 1) {

                alert("You could not add due to this DCA is blocked");
                sumgrid.rows(row).cells(5).children(0).value = "Select";
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
                                            <th valign="top" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Amend DCA Budget"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="estbl" width="400px">
                                                    <tr id="tr" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlcctype" AutoPostBack="true" runat="server" ToolTip="Cost Center"
                                                                Width="200px" CssClass="esddown" OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Performing</asp:ListItem>
                                                                <asp:ListItem>Non-Performing</asp:ListItem>
                                                                <asp:ListItem>Capital</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trtype" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="200px" CssClass="esddown"
                                                                OnSelectedIndexChanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Service</asp:ListItem>
                                                                <asp:ListItem>Trading</asp:ListItem>
                                                                <asp:ListItem>Manufacturing</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trcccode" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="200px"
                                                                AutoPostBack="true" CssClass="esddown" OnSelectedIndexChanged="ddlCCcode_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblccbud" class="ajaxspan" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="tryear" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="200px" runat="server" ToolTip="Year"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View" OnClick="btnAssign_Click" OnClientClick="return validate()" />
                                                <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                                    Text="Reset" OnClick="btnCancel1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="header" runat="server" width="100%" class="accordionHeader">
                                        <tr>
                                            <td width="100px" align="center">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("cc_code")%>'></asp:Label>
                                            </td>
                                            <td width="70px" align="center">
                                                Budget Assigned
                                            </td>
                                            <td width="70px" align="center">
                                                Balance
                                            </td>
                                            <td width="50px" align="center">
                                                <asp:Label ID="Label9" runat="server" Text="Year"></asp:Label>
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
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("balance")%>'></asp:Label>
                                            </td>
                                            <td width="100px" align="center">
                                                <asp:Label ID="Label8" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="griddetails" runat="server">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="100%" GridLines="None" ShowFooter="True" DataKeyNames="dca_code" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dca Code" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="id" runat="server" Text='<%# Bind("mapdca_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddldetailhead" CssClass="esddown" Width="105px" ToolTip="DCA"
                                                                    runat="server" onchange="SetDynamicKey('dp3',this.value);">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dca Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
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
                                                        <asp:TemplateField HeaderText="Budget Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%# Eval("budget_dca_yearly")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budget Balance" ItemStyle-HorizontalAlign="Right"
                                                            HeaderStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%# Eval("dca_yearly_bal")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltype" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ValidationGroup="Update" ID="ddltype" onchange="ValidaDCA();" runat="server">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem>Add</asp:ListItem>
                                                                    <asp:ListItem>Substract</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amended Budget">
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtbudget" Width="75px" onkeyup="Checkbudget(this.value);" onkeypress="javascript:return isNumberKey(event);"
                                                                    runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtbud" Width="75px" onkeyup="checkdcabudget(this.value);" onkeypress="javascript:return isNumberKey(event);"
                                                                    runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="1px">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hstatus" runat="server" Value='<%#Eval("Status")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Link" HeaderText="Edit" ShowEditButton="True" ShowCancelButton="true" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnadd" runat="server" CssClass="button" Style="font-size: small"
                                                                    Text="Add" OnClientClick="return footervalidation()" OnClick="btnadd_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="h1" runat="server" />
                                                <asp:HiddenField ID="h2" runat="server" />
                                                <asp:HiddenField ID="h3" runat="server" />
                                                <asp:Literal ID="Literal1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="tramendlistheader" runat="server">
                                            <td align="center">
                                                <asp:Label ID="dcalabel" runat="server" Text=" Amended DCA List" Font-Bold="true"
                                                    Font-Size="Medium"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                Width="100%" GridLines="None" ShowFooter="True" DataKeyNames="id" OnRowDeleting="GridView2_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DCA Code" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Eval("mapdca_code")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DCA Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <%# Eval("dca_name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcredit" runat="server" Text='<%# Eval("credit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subtract" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldebit" runat="server" Text='<%# Eval("debit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                        ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                </Columns>
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                            </asp:GridView>
                                        </tr>
                                        <tr align="center" id="Tr1" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="button" Style="font-size: small"
                                                    Text="Amend DCA Budget" OnClientClick="return gridvalidate()" OnClick="btnsubmit_Click" />
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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }





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
</asp:Content>
