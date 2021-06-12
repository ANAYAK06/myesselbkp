<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="CompanyStatus.aspx.cs" Inherits="CompanyStatus" Title="Consolidate Cash Flow Summary" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/bubble-tooltip.css" rel="stylesheet" type="text/css" />

    <script src="Java_Script/Prototype.js" type="text/javascript"></script>

    <script src="Java_Script/Tooltip.js" type="text/javascript"></script>

    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function validate() {

            var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlyear.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        var t1;
        function showDetails(e, Invoice, tds, retention, hold, anyother, Advance, Due, type) {
            //		var url =  'CustomerInfo.aspx';
            //		var qstr = 'CustID=' + custId+ "&ms=" + new Date().getTime();
            var req = new Ajax.Request(

			{
			    method: 'get'
			});
            if (t1) {
                if (type == "2") {
                    t1.Show(e, "<br>Previous Recieveable Invoice Recieved in Current Year:" + Invoice + "<br> Previous Recieveable TDS Recieved in Current Year:" + tds + "<br> Previous Recieveable Retention Recieved in Current Year:" + retention + "<br> Previous Recieveable Advance Recieved in Current Year:" + Advance + "<br> Previous Recieveable Hold Recieved in Current Year:" + hold + "<br> Previous Recieveable AnyOther Recieved in Current Year:" + anyother + "<br><br> Net Due:" + Due + "<br>");
                }
                else if (type == "1") {
                    t1.Show(e, "<br><br>Previous Payables Paid in Current Year:" + Invoice + "<br><br>Net Due Against Previous Payables:" + tds + "");

                }
            }
        }
        var t2;
        function showpayableDetails(e, prevpayables, Netdue) {
            //		var url =  'CustomerInfo.aspx';
            //		var qstr = 'CustID=' + custId+ "&ms=" + new Date().getTime();
            var req = new Ajax.Request(

			{
			    method: 'get'
			});
            if (t2) t2.Show(e, "<br><br>Previous Payables Paid in Current Year:" + prevpayables + "<br><br>Net Due Against Previous Payables:" + Netdue + "");
        }
        function showTooltip1(res) {
            var p = "prasad";
            var s = "sudheer";
            var n = "naveen";
            var k = "kishore";

            var t = res.responseText;
            //debugger;
            var x = eval('(' + t + ')');
            var i = x.Orders.Items.length;
            var str = "<table width=100% bordercolor='skyblue' border=1 cellpadding='2' cellspacing='0'><tbody align='left'>";
            str += "<tr bgcolor='skyblue'><td><b>Date</b></td>";
            str += "<td><b>City</b></td>";
            str += "<td><b>Name</b></td>";
            str += "<td><b>Quantity</b></td>";

            for (var c = 0; c < i; c++) {
                str += "<tr>";
                str += "<td>" + p + "</td>";
                str += "<td>" + s + "</td>";
                str += "<td>" + n + "</td>";
                str += "<td>" + k + "</td>";
                str += "</tr>";
            }

            str += "</tbody></table>";
            t2.SetHTML(str);
        }


        function hideTooltip(e) {
            if (t1) t1.Hide(e);


        }
        function hideTooltip1(e) {
            if (t2) t2.Hide(e);


        }
        function init() {
            t1 = new ToolTip("a", true, 100);
            t2 = new ToolTip("b", true, 40);
            //showDetails();
        }
        Event.observe(window, 'load', init, false);
	
    </script>

    <script language="javascript">
        function overhead(percentage) {
            window.alert("The OverHead Percentage is :" + percentage);
            return false;
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
                        <td align="center" colspan="2">
                            <table width="500px" class="estbl eslbl">
                                <tr>
                                    <th align="center">
                                        View Consolidate Cash Flow Summary
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <%--   <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>--%>
                                        <table id="paytype" runat="server" class="innertab">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <%-- <td>
                                                                        <asp:Label ID="Label1" runat="server" Text="Status Type"></asp:Label>
                                                                    </td>
                                                                    <td colspan="1" align="left">
                                                                        <asp:DropDownList ID="ddlstatustype" CssClass="esddown" onchange="status()" runat="server"
                                                                            Width="150px">
                                                                            <asp:ListItem Value="Company Status">Overall Company Status</asp:ListItem>
                                                                            <asp:ListItem Value="CC Status">Overall CC Status</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblccode" runat="server" Text="CC Code"></asp:Label>
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="ddlcccode" CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);"
                                                                    runat="server" Width="200px" ToolTip="Cost Center">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlcccode"
                                                                    ServicePath="cascadingDCA.asmx" Category="cc" LoadingText="Loading CC" ServiceMethod="newcostcode"
                                                                    PromptText="Select Cost Center">
                                                                </cc1:CascadingDropDown>
                                                                <%--    <asp:DropDownList ID="ddlcccode" CssClass="filter_item" runat="server">
                                                                        </asp:DropDownList>--%>
                                                                <%--      <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                            ServicePath="cascadingDCA.asmx" Category="dd2" LoadingText="Please Wait" ServiceMethod="costcode"
                                                                            PromptText="Select Cost Center">
                                                                        </cc1:CascadingDropDown>--%>
                                                                <%--<br />
                                                                        <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                                            TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                            ServiceMethod="GetCCName">
                                                                        </cc1:DynamicPopulateExtender>--%>
                                                            </td>
                                                            <%--<td align="left">
                                                                        <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Monthly"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="Month">
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
                                                                    </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="yearly"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                </asp:DropDownList>
                                                                <%--<asp:CascadingDropDown ID="CascadingDropDown6" runat="server" Category="year" TargetControlID="ddlyear"
                                                                ServiceMethod="yearly" ServicePath="~/Admin/cascadingDCA.asmx" PromptText="Select year">
                                                            </asp:CascadingDropDown>--%>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                    </td>
                                                    </tr>--%>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--</ContentTemplate>--%>
                                        <%-- <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbtntype" EventName="SelectedIndexChanged" />
                                </Triggers>--%>
                                        <%-- </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />&nbsp
                                        <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trexcel" runat="server">
                        <td align="right">
                           
                        </td>
                        <td align="right" colspan="2">
                             Convert To Excel:
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                            <%-- <asp:ImageButton ID="btnWord" runat="server" ImageUrl="~/images/pdf.png" OnClick="btnWord_Click" />
--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Font-Size="Small"
                                    ShowFooter="true" CssClass="gridviewstyle">
                                    <%-- <HeaderStyle BackColor="Red" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="LightGray" />--%>
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <AlternatingRowStyle BackColor="LightGray" />
                                    <Columns>
                                    
                                        <asp:BoundField DataField="dca_name" HeaderText="DCA Name" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="50px" />
                                        <asp:BoundField DataField="dca" HeaderText="DCACode" HeaderStyle-Width="100px" />
                                        <asp:TemplateField HeaderText="Cash">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpcash" runat="server" ForeColor='<%# Convert.ToDecimal(Eval("pcash"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                    Text='<%# Eval("pcash") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="pcash" HeaderText="Cash" />--%>
                                        <asp:TemplateField HeaderText="Cheque">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpcheque" runat="server" ForeColor='<%# Convert.ToDecimal(Eval("pcheque"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                    Text='<%# Eval("pcheque") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="pcheque" HeaderText="Cheque" />--%>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaidtotal" runat="server" Text='<%#Bind("ppaidTotal") %>' ForeColor='<%# Convert.ToDecimal(Eval("ppaidTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cash">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcash" runat="server" Text='<%#Bind("pCashpending") %>' ForeColor='<%# Convert.ToDecimal(Eval("pCashpending"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque">
                                            <ItemTemplate>
                                                <asp:Label ID="lblch" runat="server" Text='<%#Bind("pChequepending1") %>' ForeColor='<%# Convert.ToDecimal(Eval("pChequepending1"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendingtotal" runat="server" Text='<%#Bind("PendingTotal") %>'
                                                    ForeColor='<%# Convert.ToDecimal(Eval("PendingTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cash">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcashTotal" runat="server" Text='<%#Bind("pCashTotal") %>' ForeColor='<%# Convert.ToDecimal(Eval("pCashTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheque" runat="server" Text='<%#Bind("pChequeTotal") %>' ForeColor='<%# Convert.ToDecimal(Eval("pChequeTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gross Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Text='<%#Bind("pTotal") %>' ForeColor='<%# Convert.ToDecimal(Eval("pTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal1" runat="server" Text='<%#Bind("PrevPaid") %>' ForeColor='<%# Convert.ToDecimal(Eval("PrevPaid"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal2" runat="server" Text='<%#Bind("PrevPayable") %>' ForeColor='<%# Convert.ToDecimal(Eval("PrevPayable"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblprevpayable" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal3" runat="server" Text='<%#Bind("LastTotal") %>' ForeColor='<%# Convert.ToDecimal(Eval("LastTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal4" runat="server" Text=''></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 1%" valign="top">
                            <asp:Button ID="Button1" runat="server" Text="Over Head" />
                        <%--    <asp:Button ID="viewinternalcashflow" runat="server" Text="Show Internal Cash Flow Details" />--%>
                        </td>
                        <td align="left"  style="width: 80%" colspan="2">
                            <table class="estbl" width="700px" id="tblexpences" runat="server">
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Non performing CC Expences"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblnonccexpences" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Performing CC Expences"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblpeformexpences" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td width="160px">
                                        <asp:Label ID="Label8" runat="server" Text="OVERHEAD PERCENTAGE"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="lbloverheadpercentage" runat="server" Text=""></asp:Label>
                                        <asp:TextBox ID="TextBox3" Width="80px" runat="server" CssClass="char" ToolTip="Group Code"></asp:TextBox>
                                        <asp:ListBox ID="ddloverhead" runat="server" Height="80" Width="250" class="selection selection_search readonlyfield">
                                        </asp:ListBox>
                                        <cc1:DropDownExtender runat="server" ID="DropDownExtender3" TargetControlID="TextBox3"
                                            DropDownControlID="ddloverhead" />
                                    </td>
                                    <td width="240px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td width="160px">
                                        <asp:Label ID="Label9" runat="server" Text="OVER HEAD EXPENCES"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="240px" align="right">
                                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td width="160px">
                                        <asp:Label ID="Label2" runat="server" Text="EXPENSES"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="240px" align="right">
                                        <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="EXPENSES"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td width="160px">
                                        <asp:Label ID="Label15" runat="server" Text="BASIC INVOICE VALUE"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="240px" align="right">
                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160px">
                                        <asp:Label ID="Label17" runat="server" Text="DETAILS"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <%--     <asp:DropDownList ID="ddlReceivables" Width="80px" runat="server">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtgcode" Width="80px" runat="server" CssClass="char" ToolTip="Group Code"></asp:TextBox>
                                        <asp:ListBox ID="ddlReceivables" runat="server" Height="100" Width="150" class="selection selection_search readonlyfield">
                                        </asp:ListBox>
                                        <cc1:DropDownExtender runat="server" ID="DDE" TargetControlID="txtgcode" DropDownControlID="ddlReceivables" />
                                    </td>
                                    <td width="240px" align="right">
                                        <div id="bubble_tooltip" style="left: 0px; top: 93px;">
                                            <div class="bubble_top">
                                            </div>
                                            <div class="bubble_middle">
                                                <span id="bubble_tooltip_content"></span>
                                            </div>
                                            <div class="bubble_bottom">
                                            </div>
                                        </div>
                                        <%--  <asp:DropDownList ID="ddlPrevReceivables" Width="80px" runat="server">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="TextBox1" Width="80px" runat="server" CssClass="char" ToolTip="Group Code"></asp:TextBox>
                                        <asp:ListBox ID="ddlPrevReceivables" runat="server" Height="100" Width="150" onchange="extender();"
                                            class="selection selection_search readonlyfield"></asp:ListBox>
                                        <cc1:DropDownExtender runat="server" ID="DropDownExtender1" TargetControlID="TextBox1"
                                            DropDownControlID="ddlPrevReceivables" />
                                    </td>
                                    <td>
                                        <%--   <asp:DropDownList ID="ddltotal" Width="80px" runat="server">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="TextBox2" Width="80px" runat="server" CssClass="char" ToolTip="Group Code"></asp:TextBox>
                                        <asp:ListBox ID="ddltotal" runat="server" Height="100" Width="150" class="selection selection_search readonlyfield">
                                        </asp:ListBox>
                                        <cc1:DropDownExtender runat="server" ID="DropDownExtender2" TargetControlID="TextBox2"
                                            DropDownControlID="ddltotal" />
                                    </td>
                                </tr>
                                <%--   <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="NET RECEPT AGAINST CURRENT YEAR INVOICE +ADVANCES SETLLED"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="BASIC VALUE OF INVICES YET TO BE RECEIVED+RETENTIN OF ALREADYPAYMENT  RECEIVED INVOICES +ANY HOLD AGAINST PAYMENT ALREADY RECEIVED INVOICEC+TDS AGAINST ALREDY RECIVED INVOICES"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="NET RECEPT AGAINST UP TO PREVIOUS YEAR INVOICE +ADVANCES SETLLED"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="BASIC VALUE OF INVICES YET TO BE RECEIVED+RETENTIN OF ALREADYPAYMENT  RECEIVED INVOICES +ANY HOLD AGAINST PAYMENT ALREADY RECEIVED INVOICEC+TDS AGAINST ALREDY RECIVED INVOICES"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td width="160px">
                                        <asp:Label ID="Label19" runat="server" Text="GROSS STATUS"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="240px" align="right">
                                        <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160px">
                                        <asp:Label ID="Label12" runat="server" Text="NET STATUS"></asp:Label>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="240px" align="right">
                                        <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="a" style="font-family: Tahoma; font-size: small; background-color: white;
                    width: 384px; height: 199px; border: solid 1px gray; text-align: center; filter: alpha(Opacity=85);
                    opacity: 0.85">
                    Translucent tooltip !
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
