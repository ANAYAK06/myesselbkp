<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyClient.aspx.cs" Inherits="VerifyClient" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=txtClientName.ClientID %>", "<%=txttin.ClientID %>", "<%=txtpan.ClientID %>", "<%=txttan.ClientID %>", "<%=txtPersonname.ClientID %>", "<%=txtPsnphoneno.ClientID %>", "<%=txtAddress.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (!ChceckRBL("<%=rbtngstu.ClientID %>")) {
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
            document.getElementById("<%=btnAddClient.ClientID %>").style.display = 'none';
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
                <table style="width: 700px">
                    <tr>
                        <th>
                            <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify Clients"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" EmptyDataText="There is no new Clients to Approve"
                                GridLines="None" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="ID" FooterStyle-BackColor="DarkGray"
                                OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <%--<asp:CommandField HeaderText="Edit" ShowEditButton="true" CancelText="Cancel" ItemStyle-HorizontalAlign="Center" />--%>
                                    <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="20px" ShowSelectButton="true"
                                        SelectImageUrl="~/images/iconset-b-edit.gif" />
                                    <asp:BoundField DataField="client_id" HeaderText="ClientID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="client_name" HeaderText="Client Name" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="contact_person" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="contact_personphoneno" HeaderText="Contact No" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" />
                                    <%--<asp:TemplateField HeaderText="ClientID" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblclientid" runat="server" Text='<%# Bind("client_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtclientname" runat="server" Width="80px" Text='<%# Bind("client_name") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtclientname"
                                                runat="server" ErrorMessage="Enter Client Name"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblclientname" runat="server" Text='<%# Bind("client_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtpname" runat="server" Width="80px"  Text='<%# Bind("contact_person") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtpname"
                                                runat="server" ErrorMessage="Enter Contact Person"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcontactperson" runat="server" Text='<%# Bind("contact_person") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtno" runat="server" Width="80px" Text='<%# Bind("contact_personphoneno") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtno"
                                                runat="server" ErrorMessage="Enter Contact Phone Number"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcontactNo" runat="server" Text='<%# Bind("contact_personphoneno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIN NO" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txttin" runat="server" Width="80px"  Text='<%# Bind("tinno") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txttin"
                                                runat="server" ErrorMessage="Enter TIN Number"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltinno" runat="server" Text='<%# Bind("tinno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAN NO" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtpan" runat="server" Width="80px" Text='<%# Bind("panno") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtpan"
                                                runat="server" ErrorMessage="Enter PAN Number"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpanno" runat="server" Text='<%# Bind("panno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TAN NO" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txttan" runat="server" Width="80px" Text='<%# Bind("tanno") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txttan"
                                                runat="server" ErrorMessage="Enter TAN Number"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltanno" runat="server" Text='<%# Bind("tanno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtdate" runat="server" Width="80px" Text='<%# Bind("date") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtdate"
                                                ErrorMessage="Date Required"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                PopupButtonID="txtdate">
                                            </cc1:CalendarExtender>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtaddress" runat="server" Width="130px" Text='<%# Bind("address") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtaddress"
                                                ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" DeleteText="Delete"
                                        ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table style="width: 700px" id="tblverifyclient" runat="server">
                    <tr valign="top">
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
                                    <table class="estbl" width="600px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Verify Client" CssClass="eslbl"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="Category" runat="server">
                                            <td colspan="2">
                                                <asp:Label ID="Label2" runat="server" Text="Client Name" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtClientName" runat="server" CssClass="estbox" ToolTip="Client Name" MaxLength="50"></asp:TextBox><span
                                                    class="starSpan">*</span><span id="spanAvailability"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1">
                                                <asp:Label ID="Label3" runat="server" Text="TIN NO" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttin" runat="server" ToolTip="TIN No" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="PAN NO" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtpan" runat="server" ToolTip="PAN No" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label8" runat="server" Text="TAN NO" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttan" runat="server" ToolTip="TAN No" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="Label1" runat="server" Text="Contact Person Name" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left" colspan="1" valign="top">
                                                <asp:TextBox ID="txtPersonname" runat="server" ToolTip="Contact Person Name" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1">
                                                <asp:Label ID="Label6" runat="server" Text="Person Phone No" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPsnphoneno" runat="server" ToolTip="Person Phone No" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Address" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtAddress" runat="server" ToolTip="Address" TextMode="MultiLine" CssClass="estbox"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="600px" style="background-color: #DCDCDC">
                                        <tr align="center">
                                            <td colspan="2">
                                                <asp:Label ID="Label15" runat="server" Font-Size="10px" Width="600px" CssClass="eslbl"
                                                    Text="GST Yes/No"></asp:Label>
                                                <asp:RadioButtonList ID="rbtngstu" runat="server" Width="100px" AutoPostBack="true"
                                                    ClientIDMode="AutoID" OnSelectedIndexChanged="rbtngstu_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Style="font-size: x-small" ToolTip="GST Yes or No">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
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
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnAddClient" runat="server" Text="Verify Client" CssClass="esbtn" OnClick="btnAddClient_Click" OnClientClick="javascript:return validate()" />
                                                <%--OnClientClick="javascript:return validate()" OnClick="btnAddClient_Click"--%>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
