<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyAdvancePayment.aspx.cs" Inherits="VerifyAdvancePayment" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style9
        {
            width: 70px;
        }
        .style10
        {
            width: 126px;
        }
        .style11
        {
            width: 175px;
        }
    </style>
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
    <script language="javascript">

        function validate() {

            var hfcctype = document.getElementById("<%=hfcctype.ClientID %>").value;

            if (hfcctype != "Service") {
                var objs = new Array("<%=txted.ClientID %>", "<%=txthed.ClientID %>", "<%=txtNetED.ClientID %>", "<%=txtNetHED.ClientID %>"
                        , "<%=txtnetex.ClientID %>", "<%=txtex.ClientID %>", "<%=txtnetadvsaltax.ClientID %>", "<%=txtAdvsaltax.ClientID %>", "<%=txtadvbasic.ClientID %>", "<%=TxtAdvtds.ClientID %>", "<%=TxtAdvwct.ClientID %>", "<%=TxtAdvother.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            }
            else {
                var objs = new Array("<%=txted.ClientID %>", "<%=txthed.ClientID %>", "<%=txtNetED.ClientID %>", "<%=txtNetHED.ClientID %>"
                        , "<%=txtnetadvsevtax.ClientID %>", "<%=txtAdvStax.ClientID %>", "<%=txtnetadvsaltax.ClientID %>", "<%=txtAdvsaltax.ClientID %>", "<%=txtadvbasic.ClientID %>", "<%=TxtAdvtds.ClientID %>", "<%=TxtAdvwct.ClientID %>", "<%=TxtAdvother.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");

            }
            if (!CheckInputs(objs)) {
                return false;
            }
            var AdvStax = document.getElementById("<%=txtAdvStax.ClientID %>").value;
            var Advsaltax = document.getElementById("<%=txtAdvsaltax.ClientID %>").value;
            var AdvExtax = document.getElementById("<%=txtex.ClientID %>").value;

            if (hfcctype != "Service") {
                if (AdvExtax != "" && AdvExtax != "0.00" && AdvExtax != "0") {
                    var excise = document.getElementById("<%=ddlservicetax.ClientID %>").value;
                    if (excise == "Select") {
                        alert("Excise No Required");
                        document.getElementById("<%=ddlservicetax.ClientID %>").focus();
                        return false;
                    }
                }
            }
            else {
                if (AdvStax != "" && AdvStax != "0.00" && AdvExtax != "0") {
                    var service = document.getElementById("<%=ddlservicetax.ClientID %>").value;
                    if (service == "Select") {
                        alert("ServiceTax No Required");
                        document.getElementById("<%=ddlservicetax.ClientID %>").focus();
                        return false;
                    }
                }
            }
            if (Advsaltax != "" && Advsaltax != "0.00" && AdvExtax != "0") {
                var vat = document.getElementById("<%=ddlvatno.ClientID %>").value;
                if (vat == "Select") {
                    alert("VAT No Required");
                    document.getElementById("<%=ddlvatno.ClientID %>").focus();
                    return false;
                }
            }
            
        }

        function TotalAdv() {
            var originalValue = 0;
            var originalValue1 = 0;

            var Advbasic = document.getElementById("<%=txtadvbasic.ClientID %>").value;
            var AdvStax = document.getElementById("<%=txtAdvStax.ClientID %>").value;
            var Advex = document.getElementById("<%=txtex.ClientID %>").value;
            var Adved = document.getElementById("<%=txted.ClientID %>").value;
            var Advhed = document.getElementById("<%=txthed.ClientID %>").value;
            var Advsaltax = document.getElementById("<%=txtAdvsaltax.ClientID %>").value;
            var Advtds = document.getElementById("<%=TxtAdvtds.ClientID %>").value;
            var Advwct = document.getElementById("<%=TxtAdvwct.ClientID %>").value;
            var advhold = document.getElementById("<%=Txtadvhold.ClientID %>").value;
            var Advother = document.getElementById("<%=TxtAdvother.ClientID %>").value;
            var netex = document.getElementById("<%=txtnetex.ClientID %>").value;
            var NetED = document.getElementById("<%=txtNetED.ClientID %>").value;
            var NetHED = document.getElementById("<%=txtNetHED.ClientID %>").value;
            var netadvsaltax = document.getElementById("<%=txtnetadvsaltax.ClientID %>").value;
            var netadvsevtax = document.getElementById("<%=txtnetadvsevtax.ClientID %>").value;


            if (Advbasic == "") {
                Advbasic = 0;
            }
            if (AdvStax == "") {
                AdvStax = 0;
            }
            if (Advex == "") {
                Advex = 0;
            }
            if (Adved == "") {
                Adved = 0;
            }
            if (Advhed == "") {
                Advhed = 0;
            }
            if (Advsaltax == "") {
                Advsaltax = 0;
            }
            if (Advtds == "") {
                Advtds = 0;
            }
            if (Advwct == "") {
                Advwct = 0;
            }
            if (advhold == "") {
                advhold = 0;
            }
            if (Advother == "") {
                Advother = 0;
            }

            if (netadvsaltax == "") {
                netadvsaltax = 0;
            }
            if (NetED == "") {
                NetED = 0;
            }
            if (NetHED == "") {
                NetHED = 0;
            }
            if (netadvsevtax == "") {
                netadvsevtax = 0;
            }
            if (netex == "") {
                netex = 0;
            }

            var Staxbal = 0;
            var edbal = 0;
            var hedbal = 0;
            var salbal = 0;
            var exbal = 0;

            if ((AdvStax != netadvsevtax) && netadvsevtax != "") {
                Staxbal = netadvsevtax - AdvStax;
            }
            if ((Adved != NetED) && NetED != "") {
                edbal = NetED - Adved;
            }
            if ((Advhed != NetHED) && NetHED != "") {
                hedbal = NetHED - Advhed;
            }
            if ((Advex != netex) && netex != "") {
                exbal = netex - Advex;
            }
            if ((Advsaltax != netadvsaltax) && netadvsaltax != "") {
                salbal = netadvsaltax - Advsaltax;
            }
            originalValue = eval(parseFloat(Advbasic) + parseFloat(AdvStax) + parseFloat(Advex) + parseFloat(Adved) + parseFloat(Advhed) + parseFloat(Advsaltax));
            originalValue1 = eval(((parseFloat(Advbasic) + parseFloat(AdvStax) + parseFloat(Advex) + parseFloat(Adved) + parseFloat(Advhed) + parseFloat(Advsaltax)) - (parseFloat(Advtds) + parseFloat(Advwct) + parseFloat(advhold) + parseFloat(Advother))) + (parseFloat(Staxbal) + parseFloat(edbal) + parseFloat(hedbal) + +parseFloat(exbal) + +parseFloat(salbal)));

            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
            var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);

            document.getElementById('<%= TxtAdvtotal.ClientID%>').value = roundValue;
            document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;

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
                        <table class="estbl" width="660px">
                            <tr>
                                <td>
                                    <h2 align="left">
                                        Advance Credit Form</h2>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="grid-content">
                                    <table id="Table1" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                        style="background: none;" width="100%">
                                        <asp:GridView ID="gridadvcredit" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="Transaction_No"
                                            EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                            PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%" OnSelectedIndexChanged="gridadvcredit_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                    ShowSelectButton="true" />
                                                <asp:BoundField DataField="Transaction_No" HeaderText="Transaction No" />
                                                <asp:BoundField DataField="po_no" HeaderText="PO NO" />
                                                <asp:BoundField DataField="CC_code" HeaderText="CC CODE" />
                                                <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                                <asp:BoundField DataField="Amount" DataFormatString="{0:0.00}" HeaderText="Credit" />
                                            </Columns>
                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                            <PagerStyle CssClass="grid pagerbar" />
                                            <HeaderStyle CssClass="grid-header" />
                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                        </asp:GridView>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdetail" runat="server">
                                <td>
                                    <table class="estbl" width="660px">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblclient" runat="server" CssClass="eslbl" Text="Client ID:" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtclientid" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="Client id"
                                                    Width="100px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblclientname" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblsubclient" runat="server" CssClass="eslbl" Text="Subclient ID:"
                                                    Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtsubclient" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="Subclient id"
                                                    Width="100px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblsubclientname" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblcccode" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcccode" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="CC Code"
                                                    Width="100px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblccname" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblpono" runat="server" CssClass="eslbl" Text="PO NO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hfcctype" runat="server" />
                                                <asp:TextBox ID="txtpono" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="PO NO"
                                                    Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Trtaxnums" runat="server">
                                            <td>
                                                <asp:Label ID="lblservtaxnum" runat="server" CssClass="eslbl" Text="ServiceTax No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlservicetax" runat="server" ToolTip="ServiceTax No" Width="150px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>                                             
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvattaxnum" runat="server" CssClass="eslbl" Text="VAT NO:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvatno" runat="server" Width="150px" ToolTip="Vat No" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="Advance" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" Width="150px" runat="server" CssClass="eslbl" Text="Basic Advance Amount"></asp:Label>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="txtadvbasic" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Basic Value" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Service Tax"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAdvStax" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Tax" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="ex" runat="server">
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtex" runat="server" CssClass="estbox" onkeyup="TotalAdv();" ToolTip="Excise duty"
                                                    Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text=" EDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txted" runat="server" CssClass="estbox" onkeyup="TotalAdv();" ToolTip="EDCess"
                                                    Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="advtotal" runat="server">
                                            <td class="style9">
                                                <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="HEDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txthed" runat="server" CssClass="estbox" onkeyup="TotalAdv();" ToolTip="HEDCess"
                                                    Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="Sales Tax/VAT" Width="150px"></asp:Label>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="txtAdvsaltax" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Tax" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="Total Advance Amount"
                                                    Width="200px"></asp:Label>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:TextBox ID="TxtAdvtotal" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Total" Width="150px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="center" colspan="6">
                                                Deductions
                                            </th>
                                        </tr>
                                        <tr id="tradvtds" runat="server">
                                            <td>
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="TDS:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAdvtds" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="TDS" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="WCT:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAdvwct" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="WCT" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradvhold" runat="server">
                                            <td>
                                                <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="Hold:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txtadvhold" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Hold" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Text="Any Other:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAdvother" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Any Other" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="NET" runat="server">
                                            <th align="center" colspan="6">
                                                Net Receipt Against Taxes
                                            </th>
                                        </tr>
                                        <tr id="netex" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetex" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Net Excise duty" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="EDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetED" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Net Receipt EDCess" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradvexc" runat="server">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="HEDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetHED" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Net Receipt HEDCess" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label21" runat="server" CssClass="eslbl" Text="Service Tax"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetadvsevtax" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Net Receipt Tax" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="eslbl" Text="Sales Tax:"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtnetadvsaltax" runat="server" CssClass="estbox" onkeyup="TotalAdv();"
                                                    ToolTip="Net Receipt sales Tax" Width="100px" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="bank" runat="server">
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" AutoPostBack="true"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpayment" AutoPostBack="true" runat="server" ToolTip="Mode Of Pay"
                                                    CssClass="esddown" Width="70">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                <img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="660px">
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                    Text="Submit" OnClientClick="javascript:return validate()" OnClick="btnsubmit_Click" />
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
