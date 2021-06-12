<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AssignDCABudget.aspx.cs"
    Inherits="AssignDCABudget" Title="Assigned DCA Budget" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddlcctype.ClientID %>")
            if (!CheckInputs(objs)) {
                return false;
            }
            else if (document.getElementById("<%=ddlCCcode.ClientID %>").selectedIndex == 0 && document.getElementById("<%=ddlCCcode.ClientID %>").disabled == false) {
                alert("Select Cost Center");
                return false;
            }

            else if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex != 1 && document.getElementById("<%=ddlyear.ClientID %>").selectedIndex == 0 && document.getElementById("<%=ddlCCcode.ClientID %>").disabled == false) {
                alert("Select Year");
                return false;
            }
            else if (document.getElementById("<%=ddlCCcode.ClientID %>").disabled == true) {
                alert("There is no Verified Cost Centers");
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
                        <table cellpadding="0" cellspacing="0" class="grid" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Assign DCA Budget"></asp:Label>
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
                                                            <asp:DropDownList ID="ddlcctype" runat="server" ToolTip="CC Type" Width="200px" CssClass="esddown"
                                                                OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged" AutoPostBack="true">
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
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
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
                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp1" runat="server"
                                                                TargetControlID="lblccbud" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                ServiceMethod="GetCCName">
                                                            </cc1:DynamicPopulateExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="tryear" runat="server">
                                                        <td style="width: 150px" align="right">
                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="200px" runat="server" ToolTip="Year">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" OnClientClick="javascript:return validate();"
                                                    CssClass="esbtn" Style="font-size: small" Text="View" OnClick="btnAssign_Click" />
                                                <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                                    Text="Reset" OnClick="btnCancel1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr id="trgrid" runat="server">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <table id="tblpo" runat="server" class="pestbl">
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
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("year")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView ID="GridView1" runat="server" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="vertical-align: middle"
                                                                EmptyDataText="There is no records" ShowFooter="true" DataKeyNames="dca_code"
                                                                AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="mapdca_code" HeaderText="DCA Code" />
                                                                    <asp:BoundField DataField="dca_name" HeaderText="DCA Name" />
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtamount" onkeyup="checkbudget1();" onkeypress="javascript:IsNumeric(this.value);"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle ForeColor="#000066" />
                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#006699" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" />
                                                            </asp:GridView>
                                                            <asp:HiddenField ID="h1" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center" id="Tr1" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btngridsubmit" OnClientClick="return checkbudget();" runat="server"
                                                    CssClass="esbtn" Style="font-size: small" Text="Assign Budget" OnClick="btngridsubmit_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
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
        function checkbudget() {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var bal = document.getElementById("<%=h1.ClientID %>").value;
            var amt = 0;
            for (var i = 1; i < sumgrid.rows.length - 1; i++) {
                if (!isNaN(sumgrid.rows[i].cells[2].children[0].value)) {
                    amt += Number(sumgrid.rows[i].cells[2].children[0].value);
                }
            }
            sumgrid.rows[sumgrid.rows.length - 1].cells[2].innerHTML = amt;
            if (bal < amt) {
                alert("Insufficient Balance");
                return false;
            }

            document.getElementById("<%=btngridsubmit.ClientID %>").style.display = 'none';
            return true;

        }
        function checkbudget1() {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var bal = document.getElementById("<%=h1.ClientID %>").value;
            var amt = 0;
            for (var i = 1; i < sumgrid.rows.length - 1; i++) {
                if (!isNaN(sumgrid.rows[i].cells[2].children[0].value)) {
                    amt += Number(sumgrid.rows[i].cells[2].children[0].value);
                }
            }
            sumgrid.rows[sumgrid.rows.length - 1].cells[2].innerHTML = amt;
            if (bal < amt) {
                alert("Insufficient Balance");
                return false;
            }


        }

    </script>
    <script type="text/javascript">


        function ccdatecheck(vendoradd) {
            var checekyear = document.getElementById("<%=ddlyear.ClientID %>").value;
            var checkcc = document.getElementById("<%=ddlCCcode.ClientID %>").value;
            PageMethods.checking(checkcc, checekyear, OnSucceede);
        }

        function OnSucceede(result, userContext, methodName) {
            if (methodName == "checking") {
                var grdtbl = document.getElementById("<%=tblpo.ClientID %>");
                var grdbtn = document.getElementById("<%=Tr1.ClientID %>");
                var view = document.getElementById("<%=btn.ClientID %>");
                if (result != "") {

                    grdtbl.style.display = 'none';
                    grdbtn.style.display = 'none';
                    view.style.display = 'none';
                    return false;
                }
                else {
                    grdtbl.style.display = 'block';
                    grdbtn.style.display = 'block';
                    view.style.display = 'block';
                }

            }
        }

        function IsNumeric(evt) {
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
</asp:Content>
