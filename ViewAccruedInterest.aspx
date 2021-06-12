<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewAccruedInterest.aspx.cs" Inherits="ViewAccruedInterest" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlyear.ClientID %>");
            if (!CheckInputs(objs)) {
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
                                        <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table width="700px">
                            <tr>
                                <td align="center">
                                    <table width="600px" class="estbl">
                                        <tr>
                                            <th align="center" colspan="6">
                                                View Accrued Interest
                                            </th>
                                        </tr>
                                        <%--<tr>
                                    <td colspan="4" align="center">
                                        <asp:DropDownList ID="ddlfiltertype" CssClass="esddown" ToolTip="Filter Type" runat="server"
                                            Width="200px">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Daily Basis">Daily Basis</asp:ListItem>
                                            <asp:ListItem Value="Monthly Basis">Monthly Basis</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label1" runat="server" Text="Cost Center" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="1" align="left">
                                                <asp:DropDownList ID="ddlcccode" CssClass="esddown" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" Category="cc" LoadingText="Loading CC" PromptText="Select Cost Center"
                                                    ServiceMethod="DepCC">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Month" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown">
                                                    <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Year" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                    Text="View" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />&nbsp
                                                <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                                    <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                        OnClick="btnExcel_Click" />
                                </td>
                            </tr>
                            <tr id="grid" runat="server">
                                <td valign="top">
                                    <div class="box-a list-a">
                                        <div class="inner">
                                            <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr class="pagerbar">
                                                        <td class="pagerbar-cell" align="right">
                                                            <table class="pager-table">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="pager-cell" style="width: 90%" valign="middle">
                                                                            <div class="pager">
                                                                                <div align="right">
                                                                                    <asp:Label ID="Label3" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                                    <asp:DropDownList ID="ddlpagecount" runat="server" OnSelectedIndexChanged="ddlpagecount_SelectedIndexChanged"
                                                                                        AutoPostBack="true">
                                                                                        <asp:ListItem Selected="True">10</asp:ListItem>
                                                                                        <asp:ListItem>20</asp:ListItem>
                                                                                        <asp:ListItem>50</asp:ListItem>
                                                                                        <asp:ListItem>100</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr width="900px" id="print" runat="server">
                                                        <td class="grid-content" align="center">
                                                            <table id="_terp_list_grid" class="grid" width="900px" cellspacing="0" cellpadding="0">
                                                                <asp:GridView ID="GridView1" Width="100%" GridLines="None" runat="server" AutoGenerateColumns="false"
                                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                    ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" AllowPaging="false"
                                                                    EmptyDataText="There Is No Records" 
                                                                    onpageindexchanging="GridView1_PageIndexChanging">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="date" ItemStyle-Wrap="false" ItemStyle-Width="100px" HeaderText="DATE"
                                                                            HeaderStyle-Width="100px" />
                                                                        <asp:BoundField DataField="netrecieved" HeaderText="NET RECEIVED AMOUNT " ItemStyle-HorizontalAlign="right"  HeaderStyle-Width="150px" />
                                                                        <asp:BoundField DataField="cumrec" HeaderText="CUMULATIVE RECEIVED" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="150px" />
                                                                        <asp:BoundField DataField="netpaid" HeaderText="NET PAID AMOUNT " ItemStyle-HorizontalAlign="right" HeaderStyle-Width="200px" />
                                                                        <asp:BoundField DataField="cumpaid" HeaderText="CUMULATIVE NET PAID AMOUNT" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="200px" />
                                                                        <asp:BoundField DataField="paidamount" HeaderText="PAID AMOUNT " ItemStyle-HorizontalAlign="right" HeaderStyle-Width="150px" />
                                                                        <asp:BoundField DataField="Cupaidamount" HeaderText="CUMULATIVE PAID AMOUNT " ItemStyle-HorizontalAlign="right" HeaderStyle-Width="200px" />
                                                                        <asp:BoundField DataField="cashstatus" HeaderText="NET CASH STATUS" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="200px"  />
                                                                        <asp:BoundField DataField="incf" HeaderText="INTEREST ON NEGETIVE CASH FLOW" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="150px" />
                                                                        <asp:BoundField DataField="NewAccumulatedInterst" HeaderText="ACCUMULATED INTEREST" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="200px" />
                                                                    </Columns>
                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                    <PagerTemplate>
                                                                        <asp:ImageButton ID="btnFirst" runat="server" Height="15px" ImageUrl="~/images/pager_first.png"
                                                                            CommandArgument="First" CommandName="Page" OnCommand="btnFirst_Command" />&nbsp;
                                                                        <asp:ImageButton ID="btnPrev" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_left.png"
                                                                            CommandArgument="Prev" OnCommand="btnPrev_Command" />
                                                                        <asp:Label ID="lblpages" runat="server" Text="" Height="15px" CssClass="item item-char"></asp:Label>
                                                                        of
                                                                        <asp:Label ID="lblCurrent" runat="server" Text="Label" Height="15px" CssClass="item item-char"></asp:Label>
                                                                        <asp:ImageButton ID="btnNext" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_right.png"
                                                                            CommandArgument="Next" OnCommand="btnNext_Command" />&nbsp;
                                                                        <asp:ImageButton ID="btnLast" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_last.png"
                                                                            CommandArgument="Last" OnCommand="btnLast_Command" />
                                                                    </PagerTemplate>
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
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
