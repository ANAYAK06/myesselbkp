<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="frmTransferCash.aspx.cs" Inherits="frmTransferCash" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddltransfertype.ClientID %>", "<%=ddllist.ClientID %>", "<%=txtdt.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">
        function checkDate(sender, args) {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }
            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";
            var mmm = month[today.getMonth()];
            today = dd + '-' + mmm + '-' + yyyy;
            var str1 = document.getElementById("<%=txtdt.ClientID %>").value;
            var str2 = today;
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
            //debugger;
            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (date4 > date3) {
                alert("Invalid Future Date Selection");
                document.getElementById("<%=txtdt.ClientID %>").value = "";
                return false;
            }
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
    <asp:Panel runat="server" ID="viewreportpanel">
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
                            <table width="653px" style="border: 1px solid #000">
                                <tr>
                                    <th valign="top" style="background-color: #8B8A8A;" align="center">
                                        <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="estbl" width="653px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Transfer To"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddltransfertype" runat="server" CssClass="esddown" ToolTip="Payment Category"
                                                        OnSelectedIndexChanged="ddltransfertype_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>CentralDayBook</asp:ListItem>
                                                        <asp:ListItem>Bank</asp:ListItem>
                                                        <asp:ListItem>CostCenter</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="trlist" runat="server">
                                                <td>
                                                    <asp:Label ID="lbllist" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddllist" runat="server" CssClass="esddown" ToolTip="Type">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldate" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtdt" runat="server" ToolTip="Date" onKeyDown="preventBackspace();"
                                                        onpaste="return false;" onkeypress="return false;" CssClass="estbox"></asp:TextBox><span
                                                            class="starSpan" style="cursor: not-allowed;">*</span>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdt"
                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true" OnClientDateSelectionChanged="checkDate"
                                                        PopupButtonID="txtdt">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldesc" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtdesc" runat="server" CssClass="estbox" Height="64px" TextMode="MultiLine"
                                                        ToolTip="Decription" Width="184px"></asp:TextBox>
                                                    <span class="starSpan">*</span>
                                                </td>
                                            </tr>
                                            <tr id="amt">
                                                <td>
                                                    <span id="lblamoutn" class="eslbl"></span>
                                                    <asp:Label ID="lblamoutn1" runat="server" CssClass="eslbl" Text="Amount"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtamt" runat="server" CssClass="estbox" onkeypress="IsNumeric(event)"
                                                        ToolTip="Amount" Width="182px"></asp:TextBox>
                                                    <span class="starSpan">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Button ID="btn" runat="server" Text="" CssClass="esbtn" Style="display: none" />&nbsp;
                                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                        OnClientClick="javascript:return validate();" />&nbsp;
                                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Unassigned Balance: "></asp:Label>
                                                    <asp:Label ID="lblbal" runat="server" CssClass="eslbl"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lbltot" CssClass="eslbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lbldebitbal" CssClass="eslbl" runat="server"></asp:Label>
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
    </asp:Panel>
</asp:Content>
