<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="WorkInProgress.aspx.cs" Inherits="WorkInProgress" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddlclientid.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlpo.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtamt.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btninsert.ClientID %>").style.display = 'none';
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
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
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
                            <tr id="tblfrm" runat="server">
                                <td>
                                    <table class="estbl" width="660px">
                                        <tr align="center">
                                            <th colspan="4">
                                                Work In Progress
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Client ID"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclientid" AutoPostBack="true" runat="server" ToolTip="ClientID"
                                                    Width="120px" CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Subclient ID"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsubclientid" runat="server" AutoPostBack="true" ToolTip="ClientID"
                                                    Width="120px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="CC Code:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcccode" runat="server" AutoPostBack="true" ToolTip="Cost Center"
                                                    Width="180px" CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="PO NO:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpo" runat="server" ToolTip="PO No" Width="120px" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Date" Width="120px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Description"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" TextMode="MultiLine" runat="server" ToolTip="Amount"
                                                    Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="660px">
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btninsert" CssClass="esbtn" runat="server" OnClientClick="javascript:return validate()"
                                                    Text="Submit" OnClick="btninsert_Click" />
                                                <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tblgrid" runat="server" align="center" style="width: 100px">
                                <td>
                                    <table>
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label7" CssClass="esfmhead" runat="server" Text="Verify Work In Progress"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" EmptyDataText="There Is No Records"
                                                    GridLines="None" HeaderStyle-CssClass="grid-header" DataKeyNames="id" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" FooterStyle-BackColor="DarkGray"
                                                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowEditing="GridView1_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="15px" EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="client_id" HeaderText="Clientid" />
                                                        <asp:BoundField DataField="subclient_id" HeaderText="SubClientid" />
                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="pono" HeaderText="PO NO" />
                                                        <asp:BoundField DataField="date" HeaderText="Date" />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="150px" />
                                                        <asp:BoundField DataField="description" HeaderText="Description" />
                                                        <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkreject" CommandName="Delete" runat="server"><img src="images/Delete.jpg" alt='Reject' /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
</asp:Content>
