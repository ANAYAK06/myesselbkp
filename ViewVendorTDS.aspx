<%@ Page Title="View Vendor TDS" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewVendorTDS.aspx.cs" Inherits="ViewVendorTDS" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function searchvalidate() {
            //debugger;
            var objs = new Array("<%=ddlCCcode.ClientID %>", "<%=ddlcategory.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById("<%=ddlcategory.ClientID %>").value == "SubDca") {
                if (document.getElementById("<%=ddsdcaitcode.ClientID %>").value == "Select") {
                    alert("Please Select SubDca");
                    return false;
                }
            }
            else if (document.getElementById("<%=ddlcategory.ClientID %>").value == "ItCode") {
                if (document.getElementById("<%=ddsdcaitcode.ClientID %>").value == "Select") {
                    alert("Please Select IT Code");
                    return false;
                }
            }
            var objs = new Array("<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
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
                                    <table class="estbl" width="750px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="5" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Font-Underline="true" Text="View Vendor TDS"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center" valign="middle">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Smaller" Text="Cost Center"></asp:Label>
                                                <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="300px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="3" valign="middle">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="Smaller" Text="Category"></asp:Label>
                                                <asp:DropDownList ID="ddlcategory" CssClass="esddown" Width="300px" runat="server"
                                                    ToolTip="Category" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="Select">Select Category</asp:ListItem>
                                                    <asp:ListItem Value="SelectAll">Select All</asp:ListItem>
                                                    <asp:ListItem Value="SubDca">By Sub Dca</asp:ListItem>
                                                    <asp:ListItem Value="ItCode">By It Code</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trcategory" runat="server">
                                            <td align="center" colspan="6" valign="middle">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="Smaller" Text="SDCA/IT Code"></asp:Label>
                                                <asp:DropDownList ID="ddsdcaitcode" CssClass="esddown" Width="300px" runat="server"
                                                    ToolTip="Type">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="lblfrombank" runat="server" Font-Bold="true" Font-Size="Smaller" Text="From Date"></asp:Label>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtfrom">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="true" Font-Size="Smaller" Text="To Date"></asp:Label>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:TextBox ID="txtto" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="To Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtto">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td width="130px" align="center" valign="middle">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View Report" OnClick="btnAssign_Click" OnClientClick="javascript:return searchvalidate()" />
                                                <asp:Button ID="oldpage" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Old Report" OnClick="oldpage_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <asp:GridView ID="gvvendortds" runat="server" Width="750px" AutoGenerateColumns="False"
                                        Font-Size="X-Small" CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="Maroon"
                                        FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                        OnRowDataBound="gvvendortds_RowDataBound">
                                        <%--OnDataBound = "OnDataBound" OnRowCreated = "OnRowCreated"--%>
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Date of Payment/Credit" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="invoiceno" HeaderText="Invoice No" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="name" HeaderText="Vendor Name" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="pan_no" HeaderText="PAN" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                            <asp:BoundField DataField="it_code" HeaderText="IT Code" />
                                            <asp:BoundField DataField="Basicvalue" HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="TdsAmount" HeaderText="Total TDS" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="TdsAmountBal" HeaderText="TDS Balance" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:#,##,##,###.00}" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="gvtdsit" runat="server" Width="750px" AutoGenerateColumns="False"
                                        Font-Size="X-Small" CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="Maroon"
                                        FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                        OnRowDataBound="gvtdsit_RowDataBound">
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="it_code" HeaderText="IT Code" />
                                            <asp:BoundField DataField="it_name" HeaderText="IT Name" />
                                             <asp:BoundField DataField="Basicvalue" HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="TdsAmountBal" HeaderText="TDS Balance/IT" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:#,##,##,###.00}" />
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
