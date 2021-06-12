<%@ Page Title="Reconcilation Of Stock" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="ViewStockReconcilationReport.aspx.cs" Inherits="ViewStockReconcilationReport" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=Gvselectall] tr').each(function () {
                var toolTip = $(this).attr("title");
                $(this).find("td").each(function () {
                    $(this).simpletip({
                        content: toolTip
                    });
                });
                $(this).removeAttr("title");
            });
        });
    </script>--%>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function searchvalidate() {
            var objs = new Array("<%=ddltype.ClientID %>", "<%=ddlcccode.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .tooltip
        {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 3;
            display: none;
            background-color: #FB66AA;
            color: White;
            padding: 5px;
            font-size: 10pt;
            font-family: Arial;
        }
        td
        {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function OpenNewPage(strRowIndex, Type, RecivedFromCentralStore, cccode) {
            window.open("ViewStockReconcilationReportPrint.aspx?id=" + strRowIndex + "&type=" + Type + "&For=" + RecivedFromCentralStore + "&cccode=" + cccode, "NewWindow", "toolbar=no,menubar=no,top=100,left=100,titlebar=no");
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
                        <table align="center">
                            <tr align="center">
                                <td colspan="5">
                                    <table class="estbl" width="800px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="5" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="RECONCILIATION OF STOCK"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 300px">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Select Type"></asp:Label>
                                            </td>
                                            <td colspan="3" align="center" style="width: 500px">
                                                <asp:DropDownList ID="ddltype" CssClass="char" runat="server" Width="300px" ToolTip="Select Type">
                                                    <asp:ListItem Value="Select Type">Select Type</asp:ListItem>
                                                    <asp:ListItem Value="0">Select All Consumables & Semi Assets</asp:ListItem>
                                                    <asp:ListItem Value="1">Assets</asp:ListItem>
                                                    <asp:ListItem Value="2">Semi Assets</asp:ListItem>
                                                    <asp:ListItem Value="3">Consumables</asp:ListItem>
                                                    <asp:ListItem Value="4">Bought Out Items</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 300px">
                                                <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Select Cost Center"></asp:Label>
                                            </td>
                                            <td colspan="3" style="width: 500px">
                                                <asp:DropDownList ID="ddlcccode" CssClass="filter_item" Width="300px" runat="server"
                                                    ToolTip="Select Cost Center">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="stockrecon"
                                                    PromptText="Select Cost Center">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" align="center" style="width: 800px">
                                                <asp:Button ID="btnview" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View Report" OnClientClick="javascript:return searchvalidate()" OnClick="btnview_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr id="trreport" runat="server">
                                <td align="center" colspan="5">
                                    <asp:GridView ID="Gvselectall" runat="server" Width="100%" AutoGenerateColumns="False"
                                        CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass="grid-row char grid-row-odd"
                                        PagerStyle-CssClass="GridViewPagerStyle" FooterStyle-BackColor="White" ShowFooter="true"
                                        FooterStyle-Font-Bold="true" OnRowDataBound="Gvselectall_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ShowHeader="false" ItemStyle-HorizontalAlign="Left" />
                                            <%--Cell[0] --%>
                                            <asp:BoundField DataField="Item_name" HeaderText="Item Name" ShowHeader="false" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[1] --%>
                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ShowHeader="false"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[2] --%>
                                            <asp:BoundField DataField="BasicPrice" HeaderText="BasicPrice" ShowHeader="false"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[3] --%>
                                            <asp:BoundField DataField="Units" HeaderText="Units" ShowHeader="false" ItemStyle-HorizontalAlign="Center"
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
                                            <asp:BoundField DataField="" HeaderText="Total Out From CC" ItemStyle-HorizontalAlign="Center"
                                                ShowHeader="true" />
                                            <%--Cell[13] --%>
                                            <asp:BoundField DataField="" HeaderText="Balance Stock at CC" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" ShowHeader="false" />
                                            <%--Cell[14] --%>
                                            <asp:BoundField DataField="" HeaderText="Amount Of Consumed at CC" DataFormatString="{0:n}"
                                                ItemStyle-Wrap="false" ShowHeader="false" ItemStyle-HorizontalAlign="Right" />
                                            <%--Cell[15] --%>
                                            <asp:BoundField DataField="" HeaderText="Balance Stock Amt at CC" ItemStyle-HorizontalAlign="Right"
                                                ItemStyle-Wrap="false" ShowHeader="false" />
                                            <%--Cell[16] --%>
                                            <asp:BoundField DataField="" HeaderText="Amount Of Damage" ItemStyle-HorizontalAlign="Right"
                                                ItemStyle-Wrap="false" ShowHeader="false" />
                                            <%--Cell[17] --%>
                                            <asp:BoundField DataField="" HeaderText="Comments On Balance" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" ShowHeader="false" />
                                            <%--Cell[18] --%>
                                            <asp:BoundField DataField="" HeaderText="CC Store Status" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" ShowHeader="false" />
                                            <%--Cell[19] --%>
                                        </Columns>
                                    </asp:GridView>
                                     <table runat="server" id="Table1">
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label10" Font-Bold="true" BackColor="SkyBlue" ForeColor="Black" Font-Size="Medium"
                                                    runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="Gvsemiassets" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Font-Size="X-Small" CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass="grid-row char grid-row-odd"
                                        PagerStyle-CssClass="GridViewPagerStyle" FooterStyle-BackColor="White" ShowFooter="true"
                                        FooterStyle-Font-Bold="true" OnRowDataBound="Gvsemiassets_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                            <%--Cell[0] --%>
                                            <asp:BoundField DataField="Item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[1] --%>
                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[2] --%>
                                            <asp:BoundField DataField="Subdca" HeaderText="Sub-Dca" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[3] --%>
                                            <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <%--Cell[4] --%>
                                            <asp:BoundField DataField="rcv_frm_central" HeaderText="Recived From CentralStore"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                            <%--Cell[5] --%>
                                            <asp:BoundField DataField="rcv_frm_othercc" HeaderText="Recieved From OtherCC" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                            <%--Cell[6] --%>
                                            <asp:BoundField DataField="purchased_at_cc" HeaderText="Purchase At CC" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <%--Cell[7] --%>
                                            <asp:BoundField DataField="" HeaderText="Total Recieved at CC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[8] --%>
                                            <asp:BoundField DataField="Trans_to_central" HeaderText="Transfer To CentralStore"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[9] --%>
                                            <asp:BoundField DataField="Trans_to_othercc" HeaderText="Transfer To OtherCC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[10] --%>
                                            <asp:BoundField DataField="" HeaderText="Total Out From CC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[11] --%>
                                            <asp:BoundField DataField="" HeaderText="Balance Stock At CC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[12] --%>
                                            <asp:BoundField DataField="lostdamaged_rep_accep" HeaderText="LostDamaged Accepted"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[13] --%>
                                            <asp:BoundField DataField="BasicPrice" HeaderText="Basic Price" ItemStyle-HorizontalAlign="Right" />
                                            <%--Cell[14] --%>
                                            <asp:BoundField DataField="" HeaderText="Amount Against TotalRecieved" ItemStyle-HorizontalAlign="Right" />
                                            <%--Cell[15] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Against Total Returned/Transferd From CC"
                                                ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                            <%--Cell[16] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Of Balance Stock At CC" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                            <%--Cell[17] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Of Lost&Damage" ItemStyle-HorizontalAlign="Right"
                                                ItemStyle-Wrap="false" />
                                            <%--Cell[18] --%>
                                            <asp:BoundField DataField="" HeaderText="Comments On Balance" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                            <%--Cell[19] --%>
                                            <asp:BoundField DataField="" HeaderText="CC Store Status" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" />
                                            <%--Cell[20] --%>
                                        </Columns>
                                    </asp:GridView>
                                    <table runat="server" id="divGvsemiassets">
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label2" runat="server" Text="STATUS REMARK"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server">
                                                <table id="header" runat="server" class="estbl" width="90%">
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label6" runat="server" Text="STOCK BALANCE"></asp:Label>
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Label ID="lblstockbalance" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label9" runat="server" Text="DAMAGED"></asp:Label>
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Label ID="lbldamaged" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label3" runat="server" Text="NET BALANCE STOCK"></asp:Label>
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Label ID="lblnetbalancestock" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="Gvconsumable" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Font-Size="X-Small" CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass="grid-row char grid-row-odd"
                                        PagerStyle-CssClass="GridViewPagerStyle" FooterStyle-BackColor="White" GridLines="Both"
                                        ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="Gvconsumable_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                            <%--Cell[0] --%>
                                            <asp:BoundField DataField="Item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[1] --%>
                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[2] --%>
                                            <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <%--Cell[3] --%>
                                            <asp:BoundField DataField="rcv_frm_central" HeaderText="Recived From CentralStore"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                            <%--Cell[4] --%>
                                            <asp:BoundField DataField="rcv_frm_othercc" HeaderText="Recieved From OtherCC" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                            <%--Cell[5] --%>
                                            <asp:BoundField DataField="purchased_at_cc" HeaderText="Purchase At CC" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <%--Cell[6] --%>
                                            <asp:BoundField DataField="" HeaderText="Total Recieved at CC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[7] --%>
                                            <asp:BoundField DataField="Trans_to_central" HeaderText="Transfer To CentralStore"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[8] --%>
                                            <asp:BoundField DataField="Trans_to_othercc" HeaderText="Transfer To OtherCC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[9] --%>
                                            <asp:BoundField DataField="Issue_for_cons" HeaderText=" Issued For CC Consumption"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[10] --%>
                                            <asp:BoundField DataField="" HeaderText="Total Out From CC" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[11] --%>
                                            <asp:BoundField DataField="" HeaderText="Balance Stock at CC" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" />
                                            <%--Cell[12] --%>
                                            <asp:BoundField DataField="lostdamaged_rep_accep" HeaderText="LostDamaged Accepted"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[13] --%>
                                            <asp:BoundField DataField="BasicPrice" HeaderText="Basic Price" ItemStyle-Wrap="true"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <%--Cell[14] --%>
                                            <asp:BoundField DataField="" HeaderText="Amount Against TotalRecieved" ItemStyle-HorizontalAlign="Right" />
                                            <%--Cell[15] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Of Consumed Items At CC" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                            <%--Cell[16] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Against Total Transfered To Other CC"
                                                ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                                            <%--Cell[17] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Of Balance Stock At CC" ItemStyle-HorizontalAlign="Right"
                                                ItemStyle-Wrap="false" />
                                            <%--Cell[18] --%>
                                            <asp:BoundField DataField="" HeaderText="Amt Of Lost And Damage" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                            <%--Cell[19] --%>
                                            <asp:BoundField DataField="" HeaderText="Comments On Balance" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                            <%--Cell[20] --%>
                                            <asp:BoundField DataField="" HeaderText="CC Store Status" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" />
                                            <%--Cell[21] --%>
                                        </Columns>
                                    </asp:GridView>
                                    <table runat="server" id="tblGvconsumable">
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label7" runat="server" Text="RECONCILIATION SUMMARY"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="Tr1" runat="server">
                                            <td id="Td1" runat="server">
                                                <table id="Table2" runat="server" class="estbl" width="90%">
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label8" runat="server" Text="TOTAL RECEIVED AT CC"></asp:Label>
                                                        </td>
                                                        <td width="100px" align="right">
                                                            <asp:Label ID="lbltotalrecieved" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label11" runat="server" Text="CONSUMED AT CC"></asp:Label>
                                                        </td>
                                                        <td width="100px" align="right">
                                                            <asp:Label ID="lblconsumedatcc" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label13" runat="server" Text="TRANSFERED"></asp:Label>
                                                        </td>
                                                        <td width="100px" align="right">
                                                            <asp:Label ID="lbltransfered" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label15" runat="server" Text="BALANCE STOCK AT CC"></asp:Label>
                                                        </td>
                                                        <td width="100px" align="right">
                                                            <asp:Label ID="lblbalancestock" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200px">
                                                            <asp:Label ID="Label17" runat="server" Text="LOST DAMAGED"></asp:Label>
                                                        </td>
                                                        <td width="100px" align="right">
                                                            <asp:Label ID="lbllostdamaged" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="Gvassets" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Font-Size="Small" CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                                        AlternatingRowStyle-CssClass="GridViewAlternatingRowStyle" RowStyle-CssClass="GridViewRowStyle"
                                        PagerStyle-CssClass="GridViewPagerStyle" FooterStyle-BackColor="White" GridLines="Both"
                                        OnRowDataBound="Gvassets_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Item_Code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                            <%--Cell[0] --%>
                                            <asp:BoundField DataField="Item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[1] --%>
                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[2] --%>
                                            <asp:BoundField DataField="BasicPrice" HeaderText="Basic Price" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <%--Cell[3] --%>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                            <%--Cell[4] --%>
                                            <asp:BoundField DataField="RecievedDate" HeaderText="Recieved Date" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                            <%--Cell[5] --%>
                                            <asp:BoundField DataField="TransferDate" HeaderText="Returned From CC And Confirmed Date Of Transfer"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" />
                                            <%--Cell[6] --%>
                                            <asp:BoundField DataField="" HeaderText="Balance At Site" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[7] --%>
                                            <asp:BoundField DataField="" HeaderText="Comments On Status Today" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[8] --%>
                                            <asp:BoundField DataField="" HeaderText="CC Store Status CLOSED/ACTIVE" ItemStyle-HorizontalAlign="Center" />
                                            <%--Cell[9] --%>
                                        </Columns>
                                    </asp:GridView>
                                    <table runat="server" id="tblassets">
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblassets" Font-Bold="true" BackColor="Red" ForeColor="Black" Font-Size="Medium"
                                                    runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
