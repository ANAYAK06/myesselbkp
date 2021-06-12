<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Veifyamendeddcas.aspx.cs"
    Inherits="Veifyamendeddcas" Title="Verify Amended Dca Budget" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function gridvalidate() {
            var GridView = document.getElementById("<%=GridView2.ClientID %>");

            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(6).children(0).value != "Active" && GridView.rows(rowCount).cells(3).innerHTML != 0) {
                        window.alert(GridView.rows(rowCount).cells(1).innerHTML + " is blocked,Please Reject from Budget Amendment");
                        return false;

                    }
                }
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">
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

            document.getElementById("<%=btnAssign.ClientID %>").style.display = 'none';
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
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Verify Amend DCA Budget"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="estbl" width="400px">
                                                    <tr id="tr" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                                        </td>
                                                        <td align="left" width="200px">
                                                            <asp:DropDownList ID="ddlcctype" AutoPostBack="true" runat="server" ToolTip="Cost Center"
                                                                Width="200px" CssClass="esddown" onchange="SetContextKey('dp2',this.value);"
                                                                OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
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
                                                        <td align="left" width="200px">
                                                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="200px" CssClass="esddown"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Service</asp:ListItem>
                                                                <asp:ListItem>Trading</asp:ListItem>
                                                                <asp:ListItem>Manufacturing</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="pcc" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                                        </td>
                                                        <td align="left" width="200px">
                                                            <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="200px"
                                                                CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlCCcode_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblccbud" class="ajaxspan" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="tryear" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td align="left" width="200px">
                                                            <asp:DropDownList ID="ddlyear" AutoPostBack="true" CssClass="esddown" Width="200px"
                                                                runat="server" ToolTip="Year">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View" OnClick="btnAssign_Click" OnClientClick="return validate();" />
                                                <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                                    Text="Reset" OnClick="btnCancel1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center">
                                    <table>
                                        <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                            PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                            Width="90%" GridLines="None" ShowFooter="True" DataKeyNames="id" OnRowDeleting="GridView2_RowDeleting"
                                            OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="mapdca_code" ItemStyle-Width="50px" HeaderText="DCA Code"
                                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="dca_name" ItemStyle-Width="250px" HeaderText="DCA Name"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="credit" HeaderText="Add" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="debit" HeaderText="Subtract" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                                <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                    ItemStyle-Width="15px" ItemStyle-HorizontalAlign="Center" DeleteImageUrl="~/images/Delete.jpg" />
                                                <asp:TemplateField ItemStyle-Width="1px">
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
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr align="center" id="Tr1" runat="server">
                                <td align="center" width="700px">
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="button" Style="font-size: small"
                                        Text="Accept" OnClientClick="return gridvalidate()" OnClick="btnsubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
