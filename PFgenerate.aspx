<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="PFgenerate.aspx.cs"
    Inherits="PFgenerate" Title="PF Generation" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function PFfootervalidation() {
            sumgrid = document.getElementById("<%=gvdata.ClientID %>");
            var CCCode = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).selectedIndex;
            var dcacode = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).selectedIndex;
            var Amount = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
            var Date = document.getElementById("<%=txtdate.ClientID %>").value;



            if (Date == "") {
                alert("Enter Due Date");
                return false;

            }
            else if (CCCode == 0) {
                alert("Select CC Code");
                return false;
            }
            else if (dcacode == 0) {
                alert("Select Sub DCA");
                return false;
            }
            else if (Amount == "") {
                alert("Enter Amount");
                return false;

            }
        }
    </script>
    <script language="javascript">
        function validate() {
            var GridView = document.getElementById("<%=gvdata.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                    window.alert("Please verify");
                    return false;
                }
                else if (GridView != null && GridView.rows(rowCount).cells(1).children[0].innerHTML == "") {
                    window.alert("There is no records to Verify");
                    return false;
                }
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;

        }
    </script>
    <script language="javascript">
        function Cal() {
            var sumgrid = document.getElementById("<%=gvdata.ClientID %>");
            document.getElementById("<%=txttotal.ClientID %>").innerHTML = parseFloat(sumgrid.rows[sumgrid.rows.length - 1].cells[3].children[0].value) + parseFloat(document.getElementById("<%=hf1.ClientID %>").value);

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
            var str1 = document.getElementById("<%=txtdate.ClientID %>").value;
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
                document.getElementById("<%=txtdate.ClientID %>").value = "";
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
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr valign="top">
                        <td align="center">
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
                                    <table>
                                        <tr>
                                            <td>
                                                <table id="grid" class="estbl" width="500px">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="PF Generation"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center" id="namedate" runat="server">
                                                        <td>
                                                            <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                            <asp:TextBox ID="txtdate" ToolTip="Apply Date" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                onkeypress="return false;" CssClass="estbox" runat="server"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdate"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                OnClientDateSelectionChanged="checkDate" PopupButtonID="txtdate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="500px" border="1px Solid">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvdata" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                Width="100%" GridLines="None" DataKeyNames="id" EmptyDataText="There no data"
                                                                ShowFooter="True" OnRowDeleting="gvdata_RowDeleting" OnRowDataBound="gvdata_RowDataBound">
                                                                <EmptyDataTemplate>
                                                                    No data available
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                    <asp:TemplateField HeaderText="CC Code" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcc" runat="server" Text='<%# Bind("CC_Code") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="150px"
                                                                                CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);">
                                                                            </asp:DropDownList>
                                                                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                                ServicePath="cascadingDCA.asmx" Category="dd1" PromptText="Select Cost Center"
                                                                                LoadingText="Please Wait" ServiceMethod="newcostcode">
                                                                            </cc1:CascadingDropDown>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SDCA Code" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsdca" runat="server" Text='<%# Bind("Sub_DCA") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddldetailhead" CssClass="esddown" runat="server" Width="75px">
                                                                                <asp:ListItem Value="0" Text="Select SubDca"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="DCA-01 .5"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="DCA-01 .8"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="DCA-02 .5"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="DCA-02 .7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldebit" runat="server" Text='<%# Bind("Debit") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtamount" runat="server" onkeyup="Cal();"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnadd" runat="server" CssClass="button" OnClientClick="return PFfootervalidation()"
                                                                                Style="font-size: small" Text="Add" OnClick="btnadd_Click" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                                        ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                                </Columns>
                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                <HeaderStyle CssClass="grid-header" />
                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px;">
                                                            <asp:Label ID="Label1" runat="server" Text="Total "></asp:Label>
                                                            <asp:Label ID="txttotal" CssClass="eslbl" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hf1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr align="center" id="Tr1" runat="server">
                                                        <td align="center">
                                                            <asp:Button ID="btnsubmit" runat="server" CssClass="button" Style="font-size: small"
                                                                Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return validate();" />
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
            </td>
        </tr>
    </table>
</asp:Content>
