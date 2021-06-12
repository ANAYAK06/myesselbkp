<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmbankbranch.aspx.cs"
    Inherits="Admin_frmbankbranch" EnableEventValidation="false" Title="Bank Branch - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function display(disp) {
            var update = document.getElementById('ctl00_ContentPlaceHolder1_update');
            var btnaddcc = document.getElementById('ctl00_ContentPlaceHolder1_btnadd');
            if (disp) {
                document.getElementById('ctl00_ContentPlaceHolder1_update').style.visibility = "visible";
                document.getElementById('ctl00_ContentPlaceHolder1_btnadd').style.visibility = "hidden";
                document.getElementById('ctl00_ContentPlaceHolder1_btnUpdCancel').style.visibility = "visible";
                document.getElementById('ctl00_ContentPlaceHolder1_btnreset').style.visibility = "hidden";
                document.getElementById('ctl00_ContentPlaceHolder1_txtbankname').style.visibility = "hidden";
                document.getElementById('ctl00_ContentPlaceHolder1_txtacholder').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtaccno').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtacopening').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtminbal').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtlocation').value = "";
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_update').style.visibility = "hidden";
                // document.getElementById('').style.visibility="visible";
                document.getElementById('ctl00_ContentPlaceHolder1_btnUpdCancel').style.visibility = "hidden";
                document.getElementById('ctl00_ContentPlaceHolder1_btnreset').style.visibility = "visible";
                document.getElementById('ctl00_ContentPlaceHolder1_btnadd').style.visibility = "visible";
                document.getElementById('ctl00_ContentPlaceHolder1_txtbankname').style.visibility = "visible";
                document.getElementById('ctl00_ContentPlaceHolder1_txtacholder').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtaccno').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtacopening').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtminbal').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtlocation').value = "";

            }
            return false;

        }
    </script>
    <script type="text/javascript">
        function validate() {
            var role = '<%=role %>';
            if (role == "Chairman Cum Managing Director") {
                var objs = new Array("<%=txtacholder.ClientID %>", "<%=txtaccno.ClientID %>", "<%=txtacopening.ClientID %>", "<%=txtminbal.ClientID %>", "<%=txtlocation.ClientID %>");
            }
            else {
                var objs = new Array("<%=txtacholder.ClientID %>", "<%=txtaccno.ClientID %>", "<%=txtacopening.ClientID %>", "<%=txtlocation.ClientID %>");
            }
            if (!CheckInputs(objs)) {
                return false;
            }
            var bal = document.getElementById("<%=hfbalance.ClientID %>").value;
            var status = document.getElementById("<%=hfstatus.ClientID %>").value;
            if (document.getElementById('<%=txtminbal.ClientID %>') != null) {
                var minbal = document.getElementById('<%=txtminbal.ClientID %>').value;
                var minbal1 = document.getElementById("<%=txtminbal.ClientID %>");
                if (parseFloat(bal) < parseFloat(minbal) && status == "3") {
                    window.alert("present available balance is lower than minimum balance");
                    document.getElementById("<%=txtminbal.ClientID %>").value = "";
                    minbal1.focus();
                    return false;
                }
            }
        }

        function validateadd() {
            var objs = new Array("<%=txtbankname.ClientID %>", "<%=txtacholder.ClientID %>", "<%=txtaccno.ClientID %>", "<%=txtacopening.ClientID %>", "<%=txtlocation.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnadd.ClientID %>").style.display = 'none';
            return true;
        }
        function fillbankname(bankname) {
            document.getElementById("<%=txtbankname.ClientID %>").value = bankname;
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
            var str1 = document.getElementById("<%=txtacopening.ClientID %>").value;
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
                document.getElementById("<%=txtacopening.ClientID %>").value = "";               
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
                                    <table class="estbl" width="500px" id="tbldetails" runat="server">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label7" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr style="height: 20px" id="trtxtbankname" runat="server">
                                            <td style="width: 100px">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblbankname" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtbankname" runat="server" CssClass="char" ToolTip="Bank Name"></asp:TextBox>
                                                <%--  <asp:ListBox ID="lstbankname" runat="server" Height="100" Width="130" onchange="fillbankname(this.value);"
                                                    class="selection selection_search readonlyfield"></asp:ListBox>
                                                <cc1:DropDownExtender runat="server" ID="ddlbanks" TargetControlID="txtbankname"
                                                    DropDownControlID="lstbankname" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="A/C Holder Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtacholder" runat="server" ToolTip="A/C Holder Name" CssClass="estbox"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="A/C No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtaccno" runat="server" ToolTip="A/C No" CssClass="estbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="A/C Opening Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtacopening" runat="server" Width="130px" ToolTip="A/C Opening Date"
                                                    CssClass="estbox" onkeydown="return DateReadonly();"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="imgPopup" ImageUrl="~/images/cal.gif" />
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtacopening"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="imgPopup" OnClientDateSelectionChanged="checkDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblminbalance" runat="server" CssClass="eslbl" Text="Minimum Balance"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtminbal" runat="server" ToolTip="Minimum Balance" CssClass="estbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Location"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtlocation" runat="server" Width="100%" ToolTip="Location" CssClass="estbox"
                                                    TextMode="MultiLine" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Button ID="btnadd" runat="server" CssClass="esbtn" OnClientClick="javascript:return validateadd()"
                                                    Text="ADD" OnClick="btnadd_Click" />
                                                <asp:Button ID="update" runat="server" Text="Update" OnClientClick="javascript:return validate()"
                                                    CssClass="esbtn" OnClick="update_Click" />
                                                <asp:Button ID="btnreset" runat="server" CssClass="esbtn" Text="Reset" OnClick="btnreset_Click" />&nbsp;&nbsp;&nbsp;
                                                <%--<asp:Label ID="labelbalance" runat="server" CssClass="eslbl" visible="false" ></asp:Label>--%>
                                                <asp:HiddenField ID="hfbalance" runat="server" />
                                                <asp:HiddenField ID="hfstatus" runat="server" />
                                                <asp:HiddenField ID="hfbankid" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="trnotice" runat="server">
                                            <td colspan="4">
                                                <asp:Label ID="lblnotification" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td class="grid-content" align="center">
                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" align="center"
                                                    cellpadding="0" style="background: none;">
                                                    <asp:GridView ID="GridView1" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                        AllowPaging="false" DataKeyNames="bank_id" BorderColor="White" OnRowDataBound="GridView1_RowDataBound"
                                                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" EmptyDataText="There is no records"
                                                        OnRowDeleting="GridView1_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                            <asp:BoundField DataField="bank_name" Visible="true" ItemStyle-Width="100px" HeaderText="Bank Name" />
                                                            <asp:BoundField DataField="bank_location" ItemStyle-Width="50px" HeaderText="Bank Location"
                                                                ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="acc_no" ItemStyle-HorizontalAlign="Center" HeaderText="Account Number"
                                                                ItemStyle-Width="100px" />
                                                            <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Center" HeaderText="Account Opening Date"
                                                                ItemStyle-Width="200px" />
                                                            <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                                ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                        </Columns>
                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                    </asp:GridView>
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
