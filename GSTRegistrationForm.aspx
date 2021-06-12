<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="GSTRegistrationForm.aspx.cs" Inherits="GSTRegistrationForm" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function checkDate(sender, args) {
            //debugger;
            var str1 = document.getElementById("<%=txtregdate.ClientID %>").value;
            var str2 = "01-Jun-2017";
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
            if (date3 > date4) {
                alert("Please Select on OR After[01-Jul-2017] Date ");
                document.getElementById("<%=txtregdate.ClientID %>").value = "";
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
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=txttradename.ClientID %>", "<%=txtlegalname.ClientID %>", "<%=txtgstnumber.ClientID %>", "<%=txtregdate.ClientID %>", "<%=txtnature.ClientID %>", "<%=txtRegAdress.ClientID %>",
             "<%=txtjurisdiction.ClientID %>", "<%=txtward.ClientID %>", "<%=txtdstatecode.ClientID%>", "<%=ddlstate.ClientID%>");
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
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr align="center">
                        <td>
                            <table class="estbl" width="370px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                    </th>
                                </tr>
                                <tr id="trgrid" runat="server" width="470px">
                                    <td align="center">
                                        <asp:GridView ID="gridupdate" runat="server" Width="470px" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                            PagerStyle-CssClass="grid pagerbar" BorderColor="Black" EmptyDataText="No Records to verify"
                                            RowStyle-CssClass=" grid-row char grid-row-odd" GridLines="Both" DataKeyNames="Id"
                                            OnSelectedIndexChanged="gridupdate_SelectedIndexChanged">
                                            <%----%>
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                    SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                <asp:BoundField DataField="id" Visible="false" />
                                                <asp:BoundField DataField="LegalName" HeaderText="Legal Name" />
                                                <asp:BoundField DataField="GST_no" HeaderText="GST No" />
                                                <asp:BoundField DataField="state" HeaderText="State" ItemStyle-Width="100px" />
                                                <asp:BoundField DataField="regd_Date" HeaderText="Registration Date" />
                                            </Columns>
                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                            <PagerStyle CssClass="grid pagerbar" />
                                            <HeaderStyle CssClass="grid-header" />
                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr valign="top" id="trtable" runat="server">
                                    <td align="center">
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="estbl" width="470px">
                                                    <tr id="trtradename" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label9" runat="server" Text="Trade Name" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txttradename" runat="server" Width="200px" ToolTip="Trade Name"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trlegalname" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label11" runat="server" Text="Legal Name" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtlegalname" runat="server" Width="200px" Height="20px" ToolTip="Legal Name"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trgstin" runat="server">
                                                        <td style="width: 250px" align="left">
                                                            <asp:Label ID="Label4" runat="server" Text="GSTIN" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtgstnumber" runat="server" Width="200px" ToolTip="GST Number"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trregdate" runat="server">
                                                        <td style="width: 250px" align="left">
                                                            <asp:Label ID="Label12" runat="server" Text="Registration Date" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtregdate" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                onkeypress="return false;" runat="server" ToolTip="Registration Date" Width="200px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtregdate"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtregdate">
                                                            </cc1:CalendarExtender>
                                                            <%--OnClientDateSelectionChanged="checkDate"--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="trnature" runat="server">
                                                        <td style="width: 250px" align="left">
                                                            <asp:Label ID="Label2" runat="server" Text="Nature of Business" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtnature" runat="server" Width="200px" ToolTip="Nature of Business"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trregadress" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label5" runat="server" Text="Registration Address" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtRegAdress" runat="server" Width="200px" TextMode="MultiLine"
                                                                ToolTip="Registration Address" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trstatejuridiction" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label6" runat="server" Text="State of Jurisdiction" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtjurisdiction" runat="server" Width="200px" ToolTip="State of Jurisdiction"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trcircle" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label7" runat="server" Text="Ward/Circle/Sector" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtward" runat="server" Width="200px" Height="20px" ToolTip="Ward/Circle/Sector"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trstatecode" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label8" runat="server" Text="State Code" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtdstatecode" runat="server" Width="200px" ToolTip="State Code"
                                                                CssClass="esddown" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trstate" runat="server">
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label3" runat="server" Text="State" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlstate" Width="200px" ToolTip="State" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="btn" runat="server">
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnAdd" runat="server" Text="" CssClass="esbtn" OnClick="btnAdd_Click"
                                                                OnClientClick="javascript:return validate();" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="" CssClass="esbtn" />
                                                            <%--OnClick="btnCancel_Click"--%>
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
            </td>
        </tr>
    </table>
</asp:Content>
