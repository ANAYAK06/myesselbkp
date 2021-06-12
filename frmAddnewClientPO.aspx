<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="frmAddnewClientPO.aspx.cs" Inherits="frmAddnewClientPO" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 498px;
        }
        
        .MyCalendar .ajax__calendar_container
        {
            border: 4px solid #646464;
            background-color: lemonchiffon;
            color: red;
            position: relative;
            left: 0px;
            right: 0px;
        }
    </style>
    <script type="text/javascript">
        function validate() {          

            var str1 = document.getElementById("<%=txtStartdate.ClientID %>").value;
            var str2 = document.getElementById("<%=txtEnddate.ClientID %>").value;

            var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var dt1 = str1.substring(0, 2);
            var dt2 = str2.substring(0, 2);

            var yr1 = str1.substring(7, 11);
            var yr2 = str2.substring(7, 11);
            for (var i = 0; i < args.length; i++) {
                var month = str2.substring(3, 6);
                var month1 = str1.substring(3, 6);
                if (args[i] == month) {
                    var month = parseFloat(i + 1);
                    var date2 = yr2 + "-" + month + "-" + dt2;

                }
                if (args[i] == month1) {
                    var month1 = parseFloat(i + 1);
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
            if (parseFloat(_Diff) < 0) {
                alert("Invalid To date");
                document.getElementById("<%=txtEnddate.ClientID %>").focus();
                document.getElementById("<%=txtEnddate.ClientID %>").value = "";
                return false;
            }
            if (document.getElementById("<%=ddlmob.ClientID %>").value == 1) {
                if (document.getElementById("<%=txtadvsett.ClientID %>").value == "") {
                    alert("Advance Settlement Required");
                    document.getElementById("<%=txtadvsett.ClientID %>").focus();
                    return false;
                }
            }           
            var objs = new Array("<%=txtStartdate.ClientID %>", "<%=txtPO.ClientID %>", "<%=txtBasic.ClientID %>", "<%=txtTax.ClientID %>", "<%=ddlclientid.ClientID %>",
                                "<%=ddlsubclientid.ClientID %>", "<%=txtEnddate.ClientID %>", "<%=ddlmob.ClientID %>", "<%=txtRabill.ClientID %>", "<%=txtpaybills.ClientID %>",
                                "<%=txtgst.ClientID %>");
            return CheckInputs(objs);
            
        }
    </script>
    <script type="text/javascript">
        function validstrtdate()
         {
             var str1 = document.getElementById("<%=date.ClientID %>").innerText;
            var str2 = document.getElementById("<%=txtStartdate.ClientID %>").value;
            var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var dt1 = str1.substring(0, 2);
            var dt2 = str2.substring(0, 2);

            var yr1 = str1.substring(7, 11);
            var yr2 = str2.substring(7, 11);
            for (var i = 0; i < args.length; i++) {
                var month = str2.substring(3, 6);
                var month1 = str1.substring(3, 6);
                if (args[i] == month) {
                    var month = parseFloat(i + 1);
                    var date2 = yr2 + "-" + month + "-" + dt2;
                }
                if (args[i] == month1) {
                    var month1 = parseFloat(i + 1);
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
            if (parseFloat(_Diff) < 0) {
                alert("Invalid Start date");
                document.getElementById("<%=txtStartdate.ClientID %>").focus();
                document.getElementById("<%=txtStartdate.ClientID %>").value = "";
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function validateamend() {
            var objs = new Array("<%=ddlpo.ClientID %>", "<%=txtAmdDate.ClientID %>", "<%=txtamPOvalue.ClientID %>", "<%=txtAmdserv.ClientID %>", "<%=txtAmgst.ClientID %>");
            return CheckInputs(objs);
        }
    </script>
    <script type="text/javascript">
        function checkNumeric(event) {
            var kCode = event.keyCode || event.charCode; // for cross browser check

            //FF and Safari use e.charCode, while IE use e.keyCode that returns the ASCII value 
            if ((kCode > 57 || kCode < 48) && (kCode != 46 && kCode != 45)) {
                //code for IE
                if (window.ActiveXObject) {
                    event.keyCode = 0
                    return false;
                }
                else {
                    event.charCode = 0
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        function Total() {
            var originalValue = 0;

            var basic = document.getElementById("<%=txtBasic.ClientID %>").value;
            var tax = document.getElementById("<%=txtTax.ClientID %>").value;          
            var gst = document.getElementById("<%=txtgst.ClientID %>").value;
            if (basic == "") {
                basic = 0;
            }
            if (tax == "") {
                tax = 0;
            }
            if (gst == "") {
                gst = 0;
            }
            originalValue = eval(parseFloat(basic) + parseFloat(tax) + parseFloat(gst));
            document.getElementById('<%= txtTotal.ClientID%>').value = originalValue;
        }
        function Totalamend() {
            var originalValue = 0;
            var originalValue1 = 0;

            var Amendbasic = document.getElementById("<%=txtamPOvalue.ClientID %>").value;
            var Amendtax = document.getElementById("<%=txtAmdserv.ClientID %>").value;
            var Amendsal = document.getElementById("<%=txtAmgst.ClientID %>").value;
            var prepovalue = document.getElementById("<%=txtprPOvalue.ClientID %>").value;
            if (Amendbasic == "") {
                Amendbasic = 0;
            }
            if (Amendtax == "") {
                Amendtax = 0;
            }
            if (Amendsal == "") {
                Amendsal = 0;
            }
            originalValue = eval(parseFloat(Amendbasic) + parseFloat(Amendtax) + parseFloat(Amendsal));
            originalValue1 = eval(parseFloat(Amendbasic) + parseFloat(Amendtax) + parseFloat(Amendsal) + parseFloat(prepovalue));

            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
            var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtrevPOValue.ClientID%>').value = roundValue1;
            document.getElementById('<%= txttotalamend.ClientID%>').value = roundValue;
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
                        <td align="center" class="style1">
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
                                    <table class="estbl" width="400px">
                                        <tr>
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label26" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                            </td>
                                            <td style="width: 250px" align="right">
                                                <asp:DropDownList ID="ddlCC" runat="server" CssClass="esddown" Width="200px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlCC_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span id="s1" runat="server" class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="dd" LoadingText="Please Wait"
                                                    ServiceMethod="performingccnew" ServicePath="cascadingDCA.asmx" TargetControlID="ddlCC">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="CCdetails" class="estbl" width="750px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label8" runat="server" CssClass="esfmhead" Text="View Cost Center Details"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="CC Code:-"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <asp:Label ID="ccode" CssClass="eslbltext" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="CC Name:-"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <asp:Label ID="ccname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="kk" runat="server">
                                            <td>
                                                <asp:Label ID="Label27" runat="server" CssClass="eslbl" Text="CC Type"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <asp:Label ID="lblcctype" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label29" CssClass="eslbl" runat="server" Text="CC Sub Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblccsubtype" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trcc" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="EPPL Final OfferNo"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="epplfinalofferno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="Final OfferDate"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="finalofferdate" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trrefno" runat="server">
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Client Acceptance Reference No"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <asp:Label ID="refno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="date" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="4">
                                                <asp:Label ID="Label3" runat="server" CssClass="esfmhead" Text="Project Incharge"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label33" runat="server" CssClass="eslbl" Text="ProjectName:-"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblpname" CssClass="eslbltext" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Incharge Name:-"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="piname" CssClass="eslbltext" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Contact No:-"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="cno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="Address:-"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="add" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="4">
                                                <asp:Label ID="Label19" runat="server" CssClass="esfmhead" Text="CC Admin Incharge"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label21" runat="server" CssClass="eslbl" Text="Incharge Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="incname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label22" runat="server" CssClass="eslbl" Text="Incharge Phone No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="inphno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label23" runat="server" CssClass="eslbl" Text="Address"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="addres" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" CssClass="eslbl" Text="Phone No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="phno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="750px" runat="server" id="rbtbl">
                                        <tr>
                                            <td align="center">
                                                <asp:RadioButtonList ID="rbtnPOtype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                    AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                    CellSpacing="0" OnSelectedIndexChanged="rbtnPOtype_SelectedIndexChanged" Font-Bold="True">
                                                    <asp:ListItem Value="0">New PO</asp:ListItem>
                                                    <asp:ListItem Value="1">Amend PO</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tblnewpo" class="estbl" width="750px">
                                        <tr>
                                            <th valign="top" colspan="6" align="left">
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Purchase Order Information"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="Client ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclientid" AutoPostBack="true" onchange="SetDynamicKey('dp2',this.value);"
                                                    Width="120px" runat="server" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged"
                                                    ToolTip="ClientID" CssClass="esddown">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <br />
                                                <asp:Label ID="lblclientid" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="Subclient ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsubclientid" AutoPostBack="true" runat="server" ToolTip="ClientID"
                                                    Width="120px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <br />
                                                <asp:Label ID="lblsubclientid" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="PO No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPO" runat="server" Width="120px" CssClass="estbox" ToolTip="PO No"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="eslbl" Text="PO Start Date"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtStartdate" runat="server" onkeydown="return DateReadonly();"
                                                    CssClass="estbox" Width="120px" ToolTip="Strat Date" onchange="validstrtdate()"></asp:TextBox><span class="starSpan">*</span>&nbsp;
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label25" runat="server" CssClass="eslbl" Text="Completion Date"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEnddate" runat="server" onkeydown="return DateReadonly();" Width="120px"
                                                    CssClass="estbox" ToolTip="Compeltion Date"></asp:TextBox><span class="starSpan">*</span>&nbsp;
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEnddate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="PO Value"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtBasic" runat="server" CssClass="estbox" Width="120px" ToolTip="PO Value"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Lblsertax" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTax" runat="server" CssClass="estbox" Width="120px" ToolTip="Service/Excise Tax"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblgst" runat="server" CssClass="eslbl" Text="GST"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtgst" runat="server" Width="120px" CssClass="estbox" ToolTip="GST"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Total " CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTotal" runat="server" Width="120px" onkeydown="return DateReadonly();"
                                                    ToolTip="Total" CssClass="estbox"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th valign="top" colspan="6" align="left">
                                                <asp:Label ID="Label5" runat="server" Text="Payment Term" CssClass="eslbl"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="Label28" runat="server" Text="Mobilisation Advance " CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="4" align="left">
                                                <asp:DropDownList ID="ddlmob" runat="server" CssClass="esddown" ToolTip="mob. Advance"
                                                    Font-Bold="True" Height="18px" Width="100px" OnSelectedIndexChanged="ddlmob_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label30" runat="server" Text="RA Bill " CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtRabill" runat="server" ToolTip="RA Bill" Width="120px" CssClass="estbox"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label31" runat="server" Text="Payment due of RA Bills " CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtpaybills" runat="server" ToolTip="Payment due of RA Bills" Width="120px"
                                                    CssClass="estbox"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label32" runat="server" Text="Advance Settlement " CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtadvsett" runat="server" ToolTip="Advance Settlement" Width="120px"
                                                    CssClass="estbox"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:Button ID="btnAddPO" runat="server" CssClass="esbtn" Text="Add PO" OnClick="btnAddPO_Click"
                                                    OnClientClick="return validate();" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="esbtn" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="tblAmendpo" class="estbl" width="750px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="6" align="left">
                                                <asp:Label ID="Label34" runat="server" Text="PO Amended Information" CssClass="eslbl"></asp:Label>                                             
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 117px">
                                                <asp:Label ID="Label35" runat="server" Text="PO No" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="bottom" align="left">
                                                <asp:DropDownList ID="ddlpo" runat="server" ToolTip="PO NO" Width="150px" CssClass="esddown"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlpo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span id="spanAvailability" class="esspan"></span>
                                            </td>
                                            <td style="width: 117px">
                                                <asp:Label ID="Label41" runat="server" Text="Amend PO No" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="Txtamndpo" runat="server" Width="120px" CssClass="estbox" ToolTip="Amend po"
                                                    onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span id="span1" class="esspan"></span>
                                            </td>
                                            <td style="width: 110px">
                                                <asp:Label ID="Label36" runat="server" Text=" Completion Date" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:TextBox ID="txtAmdDate" runat="server" Width="110px" onkeydown="return DateReadonly();"
                                                    CssClass="estbox" ToolTip="PO Completion date"></asp:TextBox><img onclick="scwShow(document.getElementById('<%=txtAmdDate.ClientID %>'),this);"
                                                        alt="" src="images/cal.gif" style="left: 3px; position: relative; width: 15px;
                                                        height: 15px;" id="cldrDob1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 117px">
                                                <asp:Label ID="Label37" runat="server" Text="Present PO Value" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="middle" align="center" style="width: 178px">
                                                <asp:TextBox ID="txtprPOvalue" runat="server" Width="120px" CssClass="estbox" onkeydown="return DateReadonly();"
                                                    onkeyup="Totalamend();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label38" runat="server" Text="Amend PO Value" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamPOvalue" runat="server" Width="120px" CssClass="estbox" onkeyup="Totalamend();"
                                                    ToolTip="Amend PO Value" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblamsertax" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmdserv" runat="server" Width="120px" CssClass="estbox" onkeyup="Totalamend();"
                                                    ToolTip="Amend tax"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 117px">
                                                <asp:Label ID="Label42" runat="server" Text="GST" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="middle" align="center" style="width: 178px">
                                                <asp:TextBox ID="txtAmgst" runat="server" Width="120px" ToolTip="GST" CssClass="estbox"
                                                    onkeyup="Totalamend();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label40" runat="server" Text="Total Amend Value" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td style="width: 178px" align="left">
                                                <asp:TextBox ID="txttotalamend" runat="server" onkeydown="return DateReadonly();"
                                                    CssClass="estbox" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label39" runat="server" Text="Revised PO Value" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td style="width: 178px" align="left">
                                                <asp:TextBox ID="txtrevPOValue" runat="server" onkeydown="return DateReadonly();"
                                                    CssClass="estbox" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="8">
                                                <asp:Button ID="btnPOAmended" runat="server" Text="Amend" CssClass="esbtn" OnClick="btnPOAmended_Click"
                                                    OnClientClick="return validateamend();" />
                                                <asp:Button ID="Btnreset" runat="server" Text="Reset" CssClass="esbtn" OnClick="Btnreset_Click" />
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
