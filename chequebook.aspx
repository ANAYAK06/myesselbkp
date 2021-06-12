<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="chequebook.aspx.cs" EnableEventValidation="false" Inherits="chequebook" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddlbankname.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtform.ClientID %>", "<%=txtTo.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (Number(document.getElementById("<%=txtform.ClientID %>").value) >= Number(document.getElementById("<%=txtTo.ClientID %>").value)) {
                window.alert("Please Enter valid cheque nos");
                document.getElementById("<%=txtTo.ClientID %>").value = "";
                document.getElementById("<%=txtform.ClientID %>").value = "";
                return false;

            }
            var from = document.getElementById("<%=txtform.ClientID %>").value;
            var to = document.getElementById("<%=txtTo.ClientID %>").value;
            if (from.length != 6 || to.length != 6) {
                window.alert("Enter six digits in from and to textboxes");
                document.getElementById("<%=txtform.ClientID %>").value = "";
                document.getElementById("<%=txtTo.ClientID %>").value = "";
                return false;

            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;

        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

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
                        <table style="width: 700px">
                            <tr valign="top">
                                <td align="center">
                                    <table class="estbl" width="600px" id="tblcheque" runat="server">
                                        <tr style="border: 1px solid #000">
                                            <th colspan="3">
                                                <asp:Label ID="itform" CssClass="esfmhead" runat="server" Text="Cheque Book Entries "></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblbankname" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlbankname" runat="server" ToolTip="Bank" CssClass="esddown"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlbankname"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Date"></asp:Label>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" onKeyDown="preventBackspace();"
                                                    onpaste="return false;" onkeypress="return false;" ToolTip="Date"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Cheque No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="From"></asp:Label>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtform" onkeypress="return isNumberKey(event)" MaxLength="6" ToolTip="From"
                                                    CssClass="estbox" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="To"></asp:Label>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtTo" onkeypress="return isNumberKey(event)" MaxLength="6" ToolTip="To"
                                                    CssClass="estbox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldesc" CssClass="eslbl" runat="server" Text="Description"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtdesc" ToolTip="Description" CssClass="estbox" Width="100%" runat="server"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="esbtn" Text="Submit" OnClientClick="javascript:return validate()"
                                                    OnClick="btnsubmit_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tblgrid" runat="server" style="width: 100%">
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify Bank Cheque Books"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" EmptyDataText="There is no Bank Cheque's to Aprrove"
                                                    GridLines="None" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="chequeid" FooterStyle-BackColor="DarkGray"
                                                    OnRowUpdating="GridView1_RowUpdating" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ShowEditButton="true" CancelText="Cancel" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="" Visible="false" DataField="chequeid" />
                                                        <asp:TemplateField HeaderText="Bank Name" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="txtbankname" runat="server" ToolTip="Bank" CssClass="esddown"
                                                                    Width="100px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbankname"
                                                                    ErrorMessage="Select Bank"></asp:RequiredFieldValidator>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="txtbankname"
                                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                                    PromptText="Select Bank">
                                                                </cc1:CascadingDropDown>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtissuedate" runat="server" Width="80px" Text='<%# Bind("issuedate") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtissuedate"
                                                                    ErrorMessage="Date Required"></asp:RequiredFieldValidator>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtissuedate"
                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                    PopupButtonID="txtissuedate">
                                                                </cc1:CalendarExtender>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissuedate" runat="server" Text='<%# Bind("issuedate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From" ItemStyle-HorizontalAlign="Center">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="textFrom" runat="server" Width="80px" onkeypress="return isNumberKey(event)"
                                                                    MaxLength="6" Text='<%# Bind("from") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="textFrom"
                                                                    runat="server" ErrorMessage="Enter From Value"></asp:RequiredFieldValidator>
                                                                <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="fromValidate"
                                                                    ControlToValidate="textFrom" ErrorMessage="cheque no must be 6 digits.">
                                                                </asp:CustomValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelFrom" runat="server" Text='<%# Bind("from") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To" ItemStyle-HorizontalAlign="Center">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="textTo" runat="server" Width="80px" onkeypress="return isNumberKey(event)"
                                                                    MaxLength="6" Text='<%# Bind("To") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="textTo"
                                                                    runat="server" ErrorMessage="Enter To Value"></asp:RequiredFieldValidator>
                                                                <asp:CustomValidator ID="CustomValidator2" runat="server" OnServerValidate="fromValidate"
                                                                    ControlToValidate="textTo" ErrorMessage="cheque no must be 6 digits.">
                                                                </asp:CustomValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelTo" runat="server" Text='<%# Bind("To") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtdescription" runat="server" Width="130px" onkeypress="return isNumberKey(event)"
                                                                    Text='<%# Bind("description") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdescription"
                                                                    ErrorMessage="Enter Description"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("bankname")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" DeleteText="Delete"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                </asp:GridView>
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
</asp:Content>
