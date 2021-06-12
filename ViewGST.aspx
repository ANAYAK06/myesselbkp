<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewGST.aspx.cs" Inherits="ViewGST" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            //debugger;

            var objs = new Array("<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>", "<%=ddlGstno.ClientID %>");
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
                            <tr>
                                <td>
                                    <table class="estbl" width="750px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="5" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Font-Underline="true" Text="View GST Report"></asp:Label>
                                            </th>
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
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Smaller" Text="GST Nos"></asp:Label>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:DropDownList ID="ddlGstno" CssClass="esddown" Width="300px" runat="server" ToolTip="GST Nos">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" valign="middle" colspan="2">
                                                <asp:Button ID="btnView" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Invoice Date" OnClientClick="javascript:return validate()" OnClick="btnView_Click" />
                                                <asp:Button ID="btnview1" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Invoice Entry Date" OnClientClick="javascript:return validate()" OnClick="btnView1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="False"
                                        OnRowCreated="GridView1_RowCreated" Font-Size="X-Small" OnRowDataBound="GridView1_RowDataBound">
                                        <RowStyle HorizontalAlign="Right" CssClass="mGrid td" />
                                        <AlternatingRowStyle CssClass="mGrid alt" />
                                        <Columns>
                                            <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}"
                                                ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="ModifiedDate" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}"
                                                FooterText="Total in Rs:" />
                                            <asp:BoundField DataField="ClientInvoiceNo" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                                            <asp:BoundField DataField="ClientGST" HeaderText="Client GST Number" />
                                            <asp:BoundField DataField="ClientBasic" HeaderText="Basic Value" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="ClientIGST" HeaderText="IGST" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="ClientCGST" HeaderText="CGST" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="ClientSGST" HeaderText="SGST" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="VendorInvoiceNo" HeaderText="Invoice No" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" />
                                            <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" />
                                            <asp:BoundField DataField="VendorGSTNo" HeaderText="Vendor GST Number" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" />
                                            <asp:BoundField DataField="VendorBasicValue" HeaderText="Basic Value" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="VendorIGST" HeaderText="IGST" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="VendorCGST" HeaderText="CGST" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="VendorSGST" HeaderText="SGST" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="GovtIGST" HeaderText="IGST" HeaderStyle-Wrap="false" ItemStyle-Width="30px"
                                                DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="GovtCGST" HeaderText="CGST" HeaderStyle-Wrap="false" ItemStyle-Width="30px"
                                                DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="GovtSGST" HeaderText="SGST" HeaderStyle-Wrap="false" ItemStyle-Width="30px"
                                                DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Total" HeaderText="Total GST" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="PenalInterest" HeaderText="" HeaderStyle-Wrap="false"
                                                ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField HeaderText="" HeaderStyle-Wrap="false" ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
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
                <%--OnClick="btnExcel_Click"--%>
            </td>
        </tr>
    </table>
</asp:Content>
