<%@ Page Title="Site Store Closed" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="SiteStoreClosed.aspx.cs" Inherits="SiteStoreClosed" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .header
        {
            font-weight: bold;
            position: absolute;
            background-color: White;
        }
    </style>
    <script type="text/javascript">
        function closepopup() {
            document.getElementById("<%=btncomclose.ClientID %>").click();
            $find('mdlstoreclose').hide();
        }
    </script>
    <script type="text/javascript">
        function validate() {
            var date = document.getElementById("<%=txtdate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdate.ClientID %>");
            var remarks = document.getElementById("<%=txtdesc.ClientID %>").value;
            var remarksctrl = document.getElementById("<%=txtdesc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            if (role != "StoreKeeper") {
                var status = document.getElementById("<%=ddltype.ClientID %>").value;
                var statusctrl = document.getElementById("<%=ddltype.ClientID %>");
                if (status == "Select Status") {
                    window.alert("Please Select Status");
                    statusctrl.focus();
                    return false;
                }
            }
            if (date == "") {
                window.alert("Please Enter Date");
                datectrl.focus();
                return false;
            }

            if (remarks == "") {
                window.alert("Please Enter Remarks");
                remarksctrl.focus();
                return false;
            }
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Site Store Close<a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="100%">
                                <tr>
                                    <td align="center" colspan="5">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr id="trbtns" runat="server">
                                                                    <td valign="top">
                                                                        <div id="search_filter_data">
                                                                            <table border="0" class="fields" width="750px">
                                                                                <tr>
                                                                                    <td class="label search_filters search_fields" style="width: 250px">
                                                                                        <table class="search_table">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <span class="filter_item">
                                                                                                            <asp:Button ID="btntempcls" Width="250px" CssClass="button_examples" runat="server"
                                                                                                                Text="Temporary Close store" OnClick="btntempcls_Click" />
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" style="width: 250px">
                                                                                        <table class="search_table">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <span class="filter_item">
                                                                                                            <asp:Button ID="btnreopen" Width="250px" CssClass="button_examples" runat="server"
                                                                                                                Text="Reopen Store" OnClick="btnreopen_Click" />
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" style="width: 250px">
                                                                                        <table class="search_table">
                                                                                            <tr>
                                                                                                <td class="item item-selection" valign="middle">
                                                                                                    <asp:Button ID="btncomclose" Width="250px" CssClass="button_examples" runat="server"
                                                                                                        Text="Close Store Permanently" OnClick="btncomclose_Click" />
                                                                                                </td>
                                                                                                <td width="1%" nowrap="true">
                                                                                                    <div class="filter-a">
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" class="item search_filters item-group" valign="top">
                                                                                        <div class="group-expand">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" colspan="5">
                                                                        <div class="box-a list-a">
                                                                            <div class="inner">
                                                                                <table id="" class="" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="grid-content" colspan="4">
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                                                                    <asp:HiddenField ID="hfrole" runat="server" />
                                                                                                    <asp:GridView ID="Gvdetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                                        EnableViewState="false" CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="White" ShowFooter="false"
                                                                                                        FooterStyle-Font-Bold="true" OnRowEditing="Gvdetails_RowEditing" DataKeyNames="Id,close_type,CC_Code,Date"
                                                                                                        OnRowDataBound="Gvdetails_RowDataBound">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                                                                                EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                            <asp:BoundField DataField="Id" Visible="false" />
                                                                                                            <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="close_type" HeaderText="Type" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="Sk_Desc" HeaderText="StoreKeeper Remarks" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="PM_Desc" HeaderText="Project Manager Remarks" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="CSK_Desc" HeaderText="Central Store Keeper Remarks" ItemStyle-HorizontalAlign="Center" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                    <div align="center">
                                                                                                        <asp:Label ID="labledisplay" runat="server" Font-Bold="true" Font-Size="20px" ForeColor="Maroon"></asp:Label>
                                                                                                        <asp:Label ID="lblcccode" runat="server" Font-Bold="true" Font-Size="20px" ForeColor="Maroon"></asp:Label></div>
                                                                                                    <asp:GridView ID="Gvstoreclosed" runat="server" Width="100%" Height="100px" AutoGenerateColumns="False"
                                                                                                        EnableViewState="false" CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="White" ShowFooter="true"
                                                                                                        FooterStyle-Font-Bold="true" OnRowDataBound="Gvstoreclosed_RowDataBound">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="Item_Code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                                                                                            <%--Cell[0] --%>
                                                                                                            <asp:BoundField DataField="Item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <%--Cell[1] --%>
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <%--Cell[2] --%>
                                                                                                            <asp:BoundField DataField="BasicPrice" HeaderText="BasicPrice" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <%--Cell[3] --%>
                                                                                                            <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                                                                                            <%--Cell[4] --%>
                                                                                                            <asp:BoundField DataField="rcv_frm_central" HeaderText="Recived From CentralStore"
                                                                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                                                                                ShowHeader="true" />
                                                                                                            <%--Cell[5] --%>
                                                                                                            <asp:BoundField DataField="rcv_frm_othercc" HeaderText="Recieved From OtherCC" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderStyle-Wrap="true" ItemStyle-Wrap="true" ShowHeader="true" />
                                                                                                            <%--Cell[6] --%>
                                                                                                            <asp:BoundField DataField="purchased_at_cc" HeaderText="Purchase At CC" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ShowHeader="true" />
                                                                                                            <%--Cell[7] --%>
                                                                                                            <asp:BoundField DataField="" HeaderText="Total Recieved at CC" ItemStyle-HorizontalAlign="Center"
                                                                                                                ShowHeader="true" />
                                                                                                            <%--Cell[8] --%>
                                                                                                            <asp:BoundField DataField="Trans_to_central" HeaderText="Transfer To CentralStore"
                                                                                                                ItemStyle-HorizontalAlign="Center" ShowHeader="true" />
                                                                                                            <%--Cell[9] --%>
                                                                                                            <asp:BoundField DataField="Trans_to_othercc" HeaderText="Transfer To OtherCC" ItemStyle-HorizontalAlign="Center"
                                                                                                                ShowHeader="true" />
                                                                                                            <%--Cell[10] --%>
                                                                                                            <asp:BoundField DataField="Issue_for_cons" HeaderText=" Issued For CC Consumption"
                                                                                                                ItemStyle-HorizontalAlign="Center" ShowHeader="true" />
                                                                                                            <%--Cell[11] --%>
                                                                                                            <asp:BoundField DataField="lost_and_damage" HeaderText="Lost & Damages" ItemStyle-HorizontalAlign="Center"
                                                                                                                ShowHeader="true" />
                                                                                                            <%--Cell[12] --%>
                                                                                                            <asp:BoundField DataField="" HeaderText="Total Recieved at CC" ItemStyle-HorizontalAlign="Center"
                                                                                                                ShowHeader="true" />
                                                                                                            <%--Cell[13] --%>
                                                                                                            <asp:BoundField DataField="" HeaderText="Total Out From CC" ItemStyle-HorizontalAlign="Center"
                                                                                                                ShowHeader="true" />
                                                                                                            <%--Cell[14] --%>
                                                                                                            <asp:BoundField DataField="" HeaderText="Balance Stock at CC" ItemStyle-HorizontalAlign="Center"
                                                                                                                ItemStyle-Wrap="false" />
                                                                                                            <%--Cell[15] --%>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5" class="item search_filters item-group" valign="top">
                                                                        <div class="group-expand">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table width="100%" id="tbltempclosed" runat="server">
                                                            <tr style="width: 750px">
                                                                <td>
                                                                    <table>
                                                                        <tr align="left">
                                                                            <td valign="top" colspan="5">
                                                                                <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                                                                <asp:DropDownList ID="ddltype" runat="server" Width="150px">
                                                                                    <%--<asp:ListItem Value="Select Status" Text="Select Status"></asp:ListItem>
                                                                                    <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                                                                                    <asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>--%>
                                                                                </asp:DropDownList>
                                                                                &nbsp;&nbsp;
                                                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                                                <asp:TextBox ID="txtdate" Font-Size="Small" runat="server" Style="width: 130px; vertical-align: middle"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                    PopupButtonID="txtdate">
                                                                                </cc1:CalendarExtender>
                                                                                &nbsp;&nbsp;
                                                                                <asp:Label ID="lbldescr" runat="server" Text="Description"></asp:Label>
                                                                                <asp:TextBox ID="txtdesc" TextMode="MultiLine" Width="340px" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center" style="height: 10px">
                                                                            <td colspan="5" class="item search_filters item-group" valign="top">
                                                                                <div class="group-expand">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="width: 750px;" align="center">
                                                                            <td colspan="5">
                                                                                <asp:Button ID="btnsubmit" runat="server" CssClass="button" OnClick="btnsubmit_Click"
                                                                                    OnClientClick="javascript:return validate()" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" id="tbltransferstock" runat="server">
                                                            <tr style="width: 750px" align="center">
                                                                <td colspan="5">
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td>
                                                                                <asp:Button ID="btntransferstock" runat="server" Text="Transfer Stock" CssClass="button"
                                                                                    OnClick="btntransferstock_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div style="background-color: Transparent; color: Black">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                DisplayAfter="0">
                                                <ProgressTemplate>
                                                    <div style="top: 0px; height: 100%; background-color: White; opacity: 2.75; filter: alpha(opacity=75);
                                                        vertical-align: middle; left: 0px; z-index: 999999; width: 100%; position: absolute;
                                                        text-align: center; vertical-align: middle">
                                                        <table width="100%" height="100%">
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    <img id="Img1" src="~/images/processing1.gif" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
