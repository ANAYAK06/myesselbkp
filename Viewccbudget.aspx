<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Viewccbudget.aspx.cs"
    Inherits="Viewccbudget" Title="View Cost Center Budget" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

        function validate() {
            var type = document.getElementById("<%=ddlcctype.ClientID %>");
            var subtype = document.getElementById("<%=ddltype.ClientID %>");
            var year = document.getElementById("<%=ddlyear.ClientID %>");

            if (type.selectedIndex == 0) {
                window.alert("Select Type Of CostCenter")
                type.focus();
                return false;
            }
            else if (type.selectedIndex == 1 && subtype.selectedIndex == 0) {
                window.alert("Select Sub Type")
                subtype.focus();
                return false;
            }
            else if (((type.selectedIndex == 2) || (type.selectedIndex == 3)) && year.selectedIndex == 0) {
                window.alert("Select Year");
                year.focus();
                return false;
            }


        }
        function validation() {

            document.getElementById("<%=btnverifyccbudget.ClientID %>").style.display = 'none';
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
                        <table>
                            <tr align="center">
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center" colspan="2">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Verify Cost Center Budget"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="cctypevisible" runat="server">
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
                                        <tr id="trvisible" runat="server">
                                            <td id="tdlblyear" style="width: 150px" runat="server">
                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                            </td>
                                            <td align="left" id="tdddlyear" runat="server">
                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="200px" runat="server" ToolTip="Year">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlyear"
                                                    ServicePath="cascadingDCA.asmx" LoadingText="Please Wait" Category="Yy" ServiceMethod="checkyear1"
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
                                    <table width="800px">
                                        <tr style="border: solid 1px">
                                            <td>
                                                <asp:GridView ID="gridviewccbudget" GridLines="None" runat="server" Width="100%"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="cc_code"
                                                    OnRowDataBound="gridviewccbudget_RowDataBound" EmptyDataText="THERE IS NO DATA"
                                                    OnSelectedIndexChanged="gridviewccbudget_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:TemplateField HeaderText="CC Code" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <%# Eval("cc_code")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CC Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Eval("cc_name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budget Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%# Eval("budget_amount")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%# Eval("Balance")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtbal" runat="server" onkeypress="javascript:IsNumeric(this.value);"
                                                                    Text='<%# Bind("Balance") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="status" ReadOnly="true" HeaderText="Status" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="h1" runat="server" Value=' <%# Eval("cstatus")%>' />
                                                            </ItemTemplate>
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
                                                <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:Label ID="lblcccode" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="CC Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblccname" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trcc" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="Budget Amount"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtbudgetamt" Width="200px" ToolTip="Budget Amount" CssClass="estbox"
                                                    onkeypress="return isNumberKey(event);" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trrefno" runat="server">
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Balance"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblbalance" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnverifyccbudget" runat="server" CssClass="esbtn" Text="Verified"
                                                    OnClick="btnverifyccbudget_Click" OnClientClick="javascript:validation();" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="esbtn" OnClick="btnCancel_Click" />&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblverify" runat="server" CssClass="eslbl" Visible="false"></asp:Label>
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
            GridView = document.getElementById("<%=gridviewccbudget.ClientID %>");
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
