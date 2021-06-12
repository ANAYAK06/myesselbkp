<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="InvoiceCancel.aspx.cs"
    Inherits="InvoiceCancel" Title="Invoice Cancel" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function Total() {

            var originalValue = 0;


            var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
            var tax = document.getElementById("<%=txttax.ClientID %>").value;
            var ed = document.getElementById("<%=txted.ClientID %>").value;
            var hed = document.getElementById("<%=txthed.ClientID %>").value;
            var hf = document.getElementById("<%=hf1.ClientID %>").value;
            var hftype = document.getElementById("<%=hf2.ClientID %>").value;
            if (hf == "Service") {
                if (basic == "") {
                    basic = 0;
                }
                if (tax == "") {
                    tax = 0;
                }
                if (ed == "") {
                    ed = 0;
                }
                if (hed == "") {
                    hed = 0;
                }
                if (hftype == "VAT/Material Supply") {
                    var ex = document.getElementById("<%=txtex.ClientID %>").value;
                    var fre = document.getElementById("<%=txtfre.ClientID %>").value;
                    var ins = document.getElementById("<%=txtins.ClientID %>").value;
                    if (fre == "") {
                        fre = 0;
                    }
                    if (ins == "") {
                        ins = 0;
                    }

                    if (ex == "") {
                        ex = 0;
                    }
                    originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(fre) + parseFloat(ins) + parseFloat(ex)));
                }
                else
                    originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)));
                var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
            }
            else {
                var ex = document.getElementById("<%=txtex.ClientID %>").value;
                var fre = document.getElementById("<%=txtfre.ClientID %>").value;
                var ins = document.getElementById("<%=txtins.ClientID %>").value;
                if (basic == "") {
                    basic = 0;
                }
                if (tax == "") {
                    tax = 0;
                }
                if (ed == "") {
                    ed = 0;
                }
                if (hed == "") {
                    hed = 0;
                }
                if (fre == "") {
                    fre = 0;
                }
                if (ins == "") {
                    ins = 0;
                }

                if (ex == "") {
                    ex = 0;
                }
                originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(fre) + parseFloat(ins) + parseFloat(ex)));
                var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
            }

        }                   
                                                 
                                         
    </script>
    <script language="javascript">
        function Validate() {
            var hftype = document.getElementById("<%=hf2.ClientID %>").value;
            if (hftype == "VAT/Material Supply") {
                var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=txtinvoice.ClientID %>", "<%=txtpono.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>", "<%=txttax.ClientID %>"
                        , "<%=txtfre.ClientID %>", "<%=txtins.ClientID %>", "<%=txttotal.ClientID %>"
                    );

            }
            else {
                var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=txtinvoice.ClientID %>", "<%=txtpono.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>", "<%=txttax.ClientID %>"
                        , "<%=txtex.ClientID %>", "<%=txtfre.ClientID %>", "<%=txtins.ClientID %>", "<%=txted.ClientID %>", "<%=txthed.ClientID %>", "<%=txttotal.ClientID %>"
                    );
            }
            if (!CheckInputs(objs)) {
                return false;
            }
            var str1 = document.getElementById("<%=txtindt.ClientID %>").value;

            var str2 = document.getElementById("<%=txtindtmk.ClientID %>").value;
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
                alert("Invalid Invoice Making Date");
                document.getElementById("<%=txtindtmk.ClientID %>").focus();
                return false;
            }

            document.getElementById("<%=btnedit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="wrap">
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
                                        <table class="view" cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <div id="body_form">
                                                        <div>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 90%" valign="top" align="left">
                                                                        <h1>
                                                                            Invoice Updation<a class="help" href="" title="Raise Indent"> <small>Help</small></a></h1>
                                                                    </td>
                                                                </tr>
                                                                <tr id="tredit" runat="server">
                                                                    <td>
                                                                        <table class="estbl" runat="server" id="Invoice" width="100%">
                                                                            <tr id="trdebit" runat="server" style="height: 40px;">
                                                                                <td>
                                                                                    CC-Code:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="150px"
                                                                                        CssClass="esddown">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    Client ID:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlclientid" o runat="server" ToolTip="ClientID" Width="105px"
                                                                                        CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged"
                                                                                        AutoPostBack="true">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    Subclient ID:
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList ID="ddlsubclientid" onchange="SetDynamicKey('dp9',this.value);"
                                                                                        runat="server" ToolTip="ClientID" Width="105px" CssClass="esddown">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Invoice No:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:HiddenField ID="hf1" runat="server" />
                                                                                    <asp:HiddenField ID="hf2" runat="server" />
                                                                                    <asp:TextBox ID="txtinvoice" CssClass="estbox" runat="server" ToolTip="Invoice No"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    Po No:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtpono" CssClass="estbox" runat="server" ToolTip="Po No" Width="100px"></asp:TextBox><span
                                                                                        class="starSpan">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    Invoice Date:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtindt" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindt"
                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                        PopupButtonID="txtindt">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                 <td>
                                                                                    Inv Making Date:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtindtmk" CssClass="estbox" runat="server" ToolTip="Invoice Making Date"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtindtmk"
                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                        PopupButtonID="txtindtmk">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    RA No:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtra" CssClass="estbox" runat="server" ToolTip="RA No:" Width="100px"></asp:TextBox><span
                                                                                        class="starSpan">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    Basic Value:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtbasic" CssClass="estbox" onkeyup="Total();" runat="server" ToolTip="Basic Value"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txttax" CssClass="estbox" onkeyup="Total();" runat="server" ToolTip="Tax"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="ex" runat="server">
                                                                                <td>
                                                                                    <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtex" CssClass="estbox" runat="server" ToolTip="Excise duty" onkeyup="Total();"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    EDCess:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txted" CssClass="estbox" runat="server" onkeyup="Total();" ToolTip="EDCess"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    HEDCess:
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txthed" CssClass="estbox" onkeyup="Total();" runat="server" ToolTip="HEDCess"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style9">
                                                                                    <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Freight"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtfre" CssClass="estbox" runat="server" ToolTip="Freight" onkeyup="Total();"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Insurance"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtins" CssClass="estbox" runat="server" ToolTip="Insurance" onkeyup="Total();"
                                                                                        Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    Total:
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txttotal" CssClass="estbox" runat="server" ToolTip="Total" Width="100px"></asp:TextBox><span
                                                                                        class="starSpan">*</span><span id="spanAvailability" class="esspan"></span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="Srtxregno" runat="server">
                                                                                <td colspan="8">
                                                                                    <table class="innertab">
                                                                                        <tr>
                                                                                            <td align="center" style="width: 130px;" id="Td1" runat="server">
                                                                                                ServiceTax No :
                                                                                            </td>
                                                                                            <td align="center" style="width: 200px;">
                                                                                                <asp:DropDownList ID="ddlservicetax" runat="server" ToolTip="ServiceTax No" Width="200px"
                                                                                                    CssClass="esddown">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="Exno" runat="server">
                                                                                <td align="center" style="width: 130px;" id="lblex" runat="server">
                                                                                    <asp:Label ID="lblexno" runat="server" Text=" Excise No :"></asp:Label>
                                                                                </td>
                                                                                <td align="center" style="width: 200px;" colspan="3">
                                                                                    <asp:DropDownList ID="ddlExcno" runat="server" ToolTip="Excise No" Width="200px"
                                                                                        CssClass="esddown">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="center" style="width: 130px;" id="lblvat" runat="server">
                                                                                    <asp:Label ID="lblvatno" runat="server" Text="  Vat No:"></asp:Label>
                                                                                </td>
                                                                                <td align="center" style="width: 200px;" colspan="3">
                                                                                    <asp:DropDownList ID="ddlvatno" runat="server" Width="200px" ToolTip="Vat No" CssClass="esddown">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <%--<td>
                                                                                </td>
                                                                                <td>
                                                                                </td>--%>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <asp:Button ID="btnedit" Height="20px" runat="server" Text="Update" CssClass="button-a"
                                                                                        OnClick="btnedit_Click" OnClientClick="return Validate();" />
                                                                                </td>
                                                                                <td colspan="4">
                                                                                    <asp:Button ID="btndelete" Height="20px" runat="server" Text="Delete" CssClass="button-a"
                                                                                        OnClick="btndelete_Click" OnClientClick="javascript:var r= confirm('Do you want to delete the Invoice'); if(r){ return true; } else {return false; }" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="height: 500px; width: 800px; overflow: auto;">
                                                                        <asp:GridView ID="gridinvoice" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                            PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                DataKeyNames="InvoiceNo" Width="950px" OnSelectedIndexChanged="gridinvoice_SelectedIndexChanged">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                    SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC" ItemStyle-Width="60px" />
                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" ItemStyle-Width="140px" />
                                                                                <asp:BoundField DataField="PO_NO" HeaderText="Po No" />
                                                                                <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" ItemStyle-Width="100px" />
                                                                                <asp:BoundField DataField="RA_NO" HeaderText="Running A\C No" />
                                                                                    <asp:BoundField DataField="BasicValue" HeaderText="Basic Value" ItemStyle-HorizontalAlign="Right" />
                                                                                    <asp:BoundField DataField="ServiceTax" HeaderText="Service Tax" ItemStyle-HorizontalAlign="Right" />
                                                                                    <asp:BoundField DataField="Exciseduty" HeaderText="Exciseduty" ItemStyle-HorizontalAlign="Right" />
                                                                                    <asp:BoundField DataField="EDcess" HeaderText="ED Cess" ItemStyle-HorizontalAlign="Right" />
                                                                                    <asp:BoundField DataField="HEDcess" HeaderText="HED Cess" ItemStyle-HorizontalAlign="Right" />
                                                                                    <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                                                            </Columns>
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                            <HeaderStyle CssClass="grid-header" />
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                        </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
