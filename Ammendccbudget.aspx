<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Ammendccbudget.aspx.cs"
    Inherits="Ammendccbudget" Title="Amend CC Budget" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            //            var type = document.getElementById("<%=ddlcctype.ClientID %>");
            //            var year = document.getElementById("<%=ddlyear.ClientID %>");

            //            if (type.selectedIndex == 0) {
            //                window.alert("Select Type Of CostCenter")
            //                type.focus();
            //                return false;
            //            }
            //            else if (((type.selectedIndex == 2) || (type.selectedIndex == 3)) && year.selectedIndex == 0) {
            //                window.alert("Select Year");
            //                year.focus();
            //                return false;
            //            }
            var objs = new Array("<%=ddlcctype.ClientID %>", "<%=ddlyear.ClientID %>", "<%=ddltype.ClientID %>")
            if (!CheckInputs(objs)) {
                return false;
            }

        }
        function balcheck() {
            if (document.getElementById("<%=ddlamendtype.ClientID %>").selectedIndex == 2) {
                var amt = document.getElementById("<%=txtamount.ClientID %>").value;
                if (parseInt(document.getElementById("<%=lbltbbalance.ClientID %>").innerHTML) < parseInt(amt)) {
                    window.alert("Insufficient balance");
                    document.getElementById("<%=txtamount.ClientID %>").value = "";
                    return false;
                }
            }
        }

        function validation() {
            var objs = new Array("<%=ddlamendtype.ClientID %>", "<%=txtamount.ClientID %>")
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnammendccbudget.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td align="center">
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
                            <tr align="center">
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center" colspan="2">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Amend Cost Center Budget"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlcctype" runat="server" ToolTip="Cost Center" Width="200px"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Performing</asp:ListItem>
                                                    <asp:ListItem>Non-Performing</asp:ListItem>
                                                    <asp:ListItem>Capital</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trtype" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="200px" CssClass="esddown"
                                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Service</asp:ListItem>
                                                    <asp:ListItem>Trading</asp:ListItem>
                                                    <asp:ListItem>Manufacturing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trvisible" runat="server">
                                            <td id="tdlblyear" style="width: 150px" runat="server">
                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                            </td>
                                            <td align="left" id="tdddlyear" runat="server">
                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="200px" runat="server" ToolTip="Year">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlyear"
                                                    ServicePath="cascadingDCA.asmx" LoadingText="Please Wait" Category="Yy" ServiceMethod="checkyear"
                                                    PromptText="Select Year">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnview" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View" OnClick="btnview_Click" OnClientClick="return validate();" />
                                                <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                                    Text="Reset" OnClick="btnCancel1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="700px" id="tblgridammendcc" runat="server">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="gridammendcc" GridLines="None" runat="server" Width="700px" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="cc_code" OnRowCancelingEdit="gridammendcc_RowCancelingEdit" OnRowEditing="gridammendcc_RowEditing"
                                                    OnRowUpdating="gridammendcc_RowUpdating" EmptyDataText="THERE IS NO DATA" OnRowDataBound="gridammendcc_RowDataBound"
                                                    OnSelectedIndexChanged="gridammendcc_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" ShowSelectButton="true" HeaderText="Edit" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:TemplateField HeaderText="CC Code" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Eval("cc_code")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CC Name" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Eval("cc_name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budget Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            HeaderStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%# Eval("budget_amount")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbalance" runat="server" Text=' <%# Eval("balance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltype" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ValidationGroup="Update" ID="ddltype" runat="server">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem>Add</asp:ListItem>
                                                                    <asp:ListItem>Substract</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtamount1" Width="50px" onkeypress="javascript:IsNumeric(this.value);"
                                                                    runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="700px" id="tbl" runat="server">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:Label ID="lblcccode" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="CC Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblccname" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr2" runat="server">
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Budget Amount"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblbudgetamount" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr3" runat="server">
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Balance"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbltbbalance" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trcc" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="Amended type"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlamendtype" runat="server" ToolTip="Amended type">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Add</asp:ListItem>
                                                    <asp:ListItem>Substract</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trrefno" runat="server">
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Amount"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtamount" Width="200px" ToolTip="Amount" CssClass="estbox" runat="server"
                                                    onkeypress="balcheck();return isNumberKey(event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnammendccbudget" runat="server" CssClass="esbtn" Text="Verified"
                                                    OnClick="btnammendccbudget_Click1" OnClientClick="return validation();"></asp:Button>&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="esbtn" OnClick="btnCancel_Click">
                                                </asp:Button>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblverify1" runat="server" CssClass="eslbl" Visible="false"></asp:Label>
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



        function IsNumeric(evt) {
            GridView = document.getElementById("<%=gridammendcc.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
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
