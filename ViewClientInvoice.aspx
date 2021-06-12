<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewClientInvoice.aspx.cs" EnableEventValidation="false" Inherits="ViewClientInvoice" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            //debugger;            
            var objs = new Array("<%=ddlclientid.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var clientid = document.getElementById("<%=ddlclientid.ClientID %>");
            if (clientid.value != "Select All") {
                var objs = new Array("<%=ddlsubclientid.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlpo.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var cccode = document.getElementById("<%=ddlcccode.ClientID %>");
            if (clientid.value == "Select All" && cccode.value == "select All") {
                var objs = new Array("<%=ddlyear.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">

        function OpenNewPage(invno, cccode) {
            window.open("ViewClientInvoiceDetails.aspx?Invoiceno=" + invno + "&ccode=" + cccode, "NewWindow", "toolbar=no,menubar=no,top=100,left=100,titlebar=no");
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
                    <tr valign="top">
                        <td align="center">
                            <asp:UpdatePanel ID="up" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
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
                                    <table class="estbl" width="600px">
                                        <tr style="border: 1px solid #000">
                                            <th colspan="4" align="center">
                                                <asp:Label ID="itform" CssClass="esfmhead" runat="server" Text="View Client Invoices"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table class="innertab" align="center">
                                                    <tr>
                                                        <td style="font-weight: bold">
                                                            Report Type:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Report Type"
                                                                AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                                CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                                <asp:ListItem Value="Before Payment">Pending</asp:ListItem>
                                                                <asp:ListItem Value="After Payment">Received</asp:ListItem>
                                                                <asp:ListItem Value="All Records">All Records</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trclientdetails" runat="server">
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Client ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclientid" AutoPostBack="true" onchange="SetDynamicKey('dp2',this.value);"
                                                    Width="100px" runat="server" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged"
                                                    ToolTip="ClientID" CssClass="esddown">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:Label ID="lblclientid" runat="server" Text=""></asp:Label>
                                                <%--<cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp2" runat="server"
                                            TargetControlID="lblclientid" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                            ServiceMethod="GetClientName">
                                        </cc1:DynamicPopulateExtender>--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Subclient ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsubclientid" AutoPostBack="true" runat="server" ToolTip="Sub ClientID"
                                                    Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:Label ID="lblsubclientid" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trcccodedetails" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="CC Code:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" AutoPostBack="true"
                                                    Width="180px" CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="PO NO:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpo" runat="server" ToolTip="PO No" AutoPostBack="true" Width="110px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trdatedetails" runat="server">
                                            <td>
                                                <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
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
                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlyear" runat="server" ToolTip="Year" CssClass="esddown" />
                                            </td>
                                        </tr>
                                        <tr id="trbtndetails" runat="server">
                                            <td colspan="4" align="center">
                                                <asp:Button ID="btnview" CssClass="esbtn" runat="server" Text="View" OnClientClick="return validate();"
                                                    OnClick="btnview_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnreset" runat="server" CssClass="esbtn" Text="Reset" OnClick="btnreset_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr id="Grid" runat="server">
                                                    <td class="grid-content" colspan="4">
                                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                PagerStyle-CssClass="grid pagerbar" ShowFooter="true" GridLines="Both" EmptyDataText="There is no records"
                                                                OnRowDataBound="GridView1_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" ItemStyle-Wrap="false"
                                                                        ItemStyle-BorderColor="black" />
                                                                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                                                                    <asp:BoundField DataField="RA_NO" HeaderText="RA NO" />
                                                                    <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" ItemStyle-Wrap="false"
                                                                        HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
                                                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Wrap="false" HtmlEncode="false"
                                                                        DataFormatString="{0:MM/dd/yyyy}" />
                                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                                                    <asp:BoundField DataField="BasicValue" HeaderText="Basic Value" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="NetServiceTax" HeaderText="Service Tax/Excise Duty" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="NetEDcess" HeaderText="EDcess" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="NetHEDcess" HeaderText="HEDcess" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="NetSalesTax" HeaderText="Sales Tax" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="Taxamt" HeaderText="TaxAmt" HtmlEncode="False" DataFormatString="{0:f0}"
                                                                        ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="TDS" HeaderText="TDS" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="Retention" HeaderText="Retention" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="Advance" HeaderText="Advance" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="Hold" HeaderText="Hold" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="WCT" HeaderText="WCT" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="ClientDed" HeaderText="CleintDeduction" HtmlEncode="False"
                                                                        DataFormatString="{0:f0}" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="AnyOther" HeaderText="AnyOther" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField DataField="Credit" HeaderText="Net Amount" ItemStyle-HorizontalAlign="Right" />
                                                                    <%--<asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <%#Eval("paymenttype") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("paymenttype")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                            </asp:GridView>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="pagerbar">
                                                    <td class="pagerbar-cell" align="right" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr id="SummaryGrid" runat="server" width="550px" align="right">
                                                    <td width="550px" align="left" colspan="4">
                                                        <asp:GridView ID="gvdeduction" Width="280px" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                            GridLines="None" AutoGenerateColumns="false" HorizontalAlign="Right" ShowFooter="true"
                                                            OnRowDataBound="gvdeduction_RowDataBound">                                                           
                                                            <FooterStyle Font-Bold="true" ForeColor="Black" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="Debit Summary" HeaderText="Debit Summary" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="GridView3" Width="280px" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                            GridLines="None" AutoGenerateColumns="false" HorizontalAlign="Right" ShowFooter="true"
                                                            OnRowDataBound="GridView3_RowDataBound">
                                                            <FooterStyle Font-Bold="true" ForeColor="Black" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="Credit Summary" HeaderText="Credit Summary" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="Total Net Amount" HeaderText="Total Net Amount" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>
                                                      <%--  <asp:GridView ID="GridView2" Width="280px" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                            GridLines="None" AutoGenerateColumns="false" HorizontalAlign="Right" ShowFooter="false">
                                                            <FooterStyle Font-Bold="true" ForeColor="Black" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="Description" HeaderText="Credit Summary" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="Total Net Amount" HeaderText="Total Net Amount" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>--%>
                                                    </td>
                                                </tr>
                                                <tr id="trexcel" runat="server">
                                                    <td align="left" colspan="4">
                                                        <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                                                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                                            OnClick="btnExcel_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
