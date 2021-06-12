<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="TasksTime.aspx.cs" Inherits="TasksTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddlno.ClientID%>", "<%=txtdate.ClientID%>", "<%=txttime.ClientID%>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>

    <script language="Javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr align="center">
            <td align="center">
                <table class="estbl" width="250px">
                    <tr>
                        <th valign="top" align="center" colspan="2">
                            <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Tasks Time"></asp:Label>
                        </th>
                    </tr>
                    <tr align="center">
                        <td align="center" colspan="2">
                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Type"
                                AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                <asp:ListItem>CR</asp:ListItem>
                                <asp:ListItem>ISSUE</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="No"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlno" runat="server" Width="140px" ToolTip="NO" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlno_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trdesc" runat="server">
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdesc" Width="490px" Height="40px" runat="server" TextMode="MultiLine"
                                ToolTip="Description"></asp:TextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="SupportUser"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddluser" runat="server" Width="140px" ToolTip="SupportUser">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdate" runat="server" ToolTip="Date"> </asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdate"
                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                PopupButtonID="txtdate">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Hours"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttime" runat="server" onkeypress="return isNumberKey(event)" ToolTip="Hours"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnsubmit" runat="server" CssClass="esbtn" Text="Submit" OnClientClick="javascript:return validate();"
                                OnClick="btnsubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
