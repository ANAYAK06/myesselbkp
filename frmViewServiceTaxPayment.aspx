<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmViewServiceTaxPayment.aspx.cs"
    EnableEventValidation="false" Inherits="Admin_frmViewServiceTaxPayment" Title="Service Tax - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function searchvalidate() {

            var objs = new Array("<%=ddltype.ClientID %>", "<%=ddlno.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var str1 = document.getElementById("<%=txtfrom.ClientID %>").value;
            var str2 = document.getElementById("<%=txtto.ClientID %>").value;
            var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var dt1 = str1.substring(0, 2);
            var dt2 = str2.substring(0, 2);
            var yr1 = str1.substring(7, 11);
            var yr2 = str2.substring(7, 11);
            for (var i = 0; i < args.length; i++) {
                var month = str2.substring(3, 6);
                var month1 = str1.substring(3, 6);
                if (args[i] == month) {
                    var month = parseInt(i + 1);
                    var date2 = yr2 + "-" + month + "-" + dt2;

                }
                if (args[i] == month1) {
                    var month1 = parseInt(i + 1);
                    var date1 = yr1 + "-" + month1 + "-" + dt1;
                }

            }
            var one_day = 1000 * 60 * 60 * 24;
            var x = date1.split("-");
            var y = date2.split("-");

            var date4 = new Date(x[0], (x[1] - 1), x[2]);
            var date3 = new Date(y[0], (y[1] - 1), y[2]);

            var month1 = x[1] - 1;
            var month2 = y[1] - 1;

            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (parseInt(_Diff) < 0) {
                alert("Invalid date");
                document.getElementById("<%=txtto.ClientID %>").focus();
                document.getElementById("<%=txtto.ClientID %>").value = "";
                return false;
            }
            return true;
        }
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    }
                    else {
                        evt.returnValue = false;
                    }
                }
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
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
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
                        <table>
                            <tr>
                                <td>
                                    <table class="estbl" width="700px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="5" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Service Tax/Excise Duty Payment"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="center" valign="middle">
                                                <asp:RadioButton ID="rbtnpaiddate" runat="server" Text="Paid Date" GroupName="dt"
                                                    Font-Bold="true" Font-Names="Courier New" Font-Size="Small" Checked="true" ForeColor="Black" />&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rbtninvmkdate" runat="server" Text="Invoice Entry Date" GroupName="dt"
                                                    Font-Bold="true" Font-Names="Courier New" Font-Size="Small" ForeColor="Black" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:DropDownList ID="ddltype" CssClass="esddown" Width="200px" runat="server" ToolTip="Type"
                                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="Select Month">Select Type</asp:ListItem>
                                                    <asp:ListItem Value="ServiceTax">ServiceTax</asp:ListItem>
                                                    <asp:ListItem Value="Excise">Excise</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlno" CssClass="esddown" Width="200px" runat="server" ToolTip="Type">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="From Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtfrom">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="To Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtto" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="To Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtto">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td width="130px">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View Report" OnClick="btnAssign_Click" OnClientClick="javascript:return searchvalidate()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                           
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="False"
                                        OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Font-Size="X-Small"
                                        CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="White"
                                        FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                        <RowStyle HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:BoundField DataField="Date" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}"
                                                FooterText="Total in Rs:" />
                                            <asp:BoundField DataField="Servicetax" HeaderText="NetServicetax" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Edcess" HeaderText="NetEdcess" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Hedcess" HeaderText="NetHedcess" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="TotalServiceTax" HeaderText="ServiceTax" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Vendor" HeaderText="TO Vendor" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Govt" HeaderText="Service Tax to Govt" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="PTax" HeaderText="Service tax Penel Interest" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal1" runat="server" Text='<%#Bind("balance") %>' ForeColor='<%# Convert.ToDecimal(Eval("balance"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
