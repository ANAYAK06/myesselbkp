<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="TrackRecords.aspx.cs"
    Inherits="TrackRecords" Title="Untitled Page" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
            border-style: solid;
            border-width: 1px;
            background-color: #004080;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma");
                event.returnValue = false;
            }
        }

        function ClearTextboxes() {
            document.getElementById("<%=txtSearch.ClientID %>").value = '';
        }
        function validate() {
            var ddlnum = document.getElementById("<%=ddlsearchopt.ClientID %>").value;
            var txtserach = document.getElementById("<%=txtSearch.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            if (ddlnum == "Select") {
                window.alert("Please Select an Type of Number");
                return false;
            }
            if (txtserach == "" || txtserach == "Search here..") {
                window.alert("Please Enter the Number");
                return false;
            }
        }
           
    </script>
    <script type="text/javascript">
        function ValidateInput(obj) {

            if (obj.value != "") {
                obj.value = obj.value.toUpperCase();
            }

        }

    </script>
    <script type="text/javascript" language="javascript">
        function IsNumeric1(evt) {
            var ddlnum = document.getElementById("<%=ddlsearchopt.ClientID %>").value;
            var txtserach = document.getElementById("<%=txtSearch.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            if (ddlnum == "Reference Number" || ddlnum == "MRR Number") {
                var charCode = (evt.which) ? evt.which : evt.keyCode
                if (charCode != 46 && charCode != 8 && charCode != 16 && (charCode < 48 || charCode > 57) && (charCode < 96 || charCode > 105)) {
                    window.alert("Please Enter Correct Number");
                    document.getElementById("<%=txtSearch.ClientID %>").value = "";
                    Searchctrl.focus();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr id="Tr1" runat="server">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td style="width: 750px" valign="top">
                <h1>
                    Track Records</h1>
                <table align="center">
                    <tr>
                        <td class="item item-char" valign="middle" width="150px">
                            <span class="filter_item">
                                <asp:DropDownList ID="ddlsearchopt" CssClass="char" runat="server" OnSelectedIndexChanged="ddlsearchopt_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Indent No</asp:ListItem>
                                    <asp:ListItem>PO Number</asp:ListItem>
                                    <asp:ListItem>MRR Number</asp:ListItem>
                                    <asp:ListItem>Invoice Number</asp:ListItem>
                                    <asp:ListItem>Reference Number</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td width="75px">
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" CssClass="m2o_search" Width="150px" Height="20px" runat="server"
                                Style="background-image: url(images/search_grey.png); background-position: right;
                                background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller; text-transform: uppercase"
                                onkeydown="restrictComma();" onkeyup="IsNumeric1(event);ValidateInput(this);"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Search here.."
                                WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td width="75px">
                        </td>
                        <td>
                            <asp:Button ID="BtnGo" Height="18px" runat="server" Text="Go" CssClass="button" OnClick="BtnGo_Click"
                                OnClientClick="javascript:return validate()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <h1>
                                <asp:Label ID="lblviewrecords" runat="server" Text="View Records" CssClass="eslbl"
                                    Font-Underline="True" Font-Bold="True" Font-Size="Medium"></asp:Label></h1>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="indentrpt" class="style2" runat="server">
                    <tr>
                        <td colspan="6" align="center">
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Text="INDENT REPORT" Font-Bold="True" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" Text="INDENT NO." Font-Bold="true" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" width="150px" style="border: thin groove #000000" align="center">
                            &nbsp;
                            <asp:Label ID="Label7" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="INDENT DATE" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="center">
                            &nbsp;
                            <asp:Label ID="Label8" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label4" runat="server" Text="INDENT VALUE" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" width="150px" style="border: thin groove #000000" align="center">
                            &nbsp;
                            <asp:Label ID="Label9" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="INDENT TYPE" CssClass="eslbl"
                                ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="center">
                            &nbsp;
                            <asp:Label ID="Label10" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="BALANCE IN INDENT"
                                CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="center">
                            &nbsp;
                            <asp:Label ID="Label61" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="INDENT STATUS" CssClass="eslbl"
                                ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" colspan="2" style="border: thin groove #000000;" align="center">
                            &nbsp;
                            <asp:Label ID="Label11" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="itrpt" class="style2" runat="server">
                    <tr>
                        <td colspan="8" align="center">
                            &nbsp;
                            <asp:Label ID="Label12" runat="server" Text="ISSUE/TRANSFER REPORT" CssClass="eslbl"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr id="ittran" runat="server">
                        <td bgcolor="#E0DDDD" height="28" width="150px" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label13" runat="server" Text="REF.NUMBER" CssClass="eslbl" ForeColor="Black"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" width="150px" height="28" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label14" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" height="28" width="200px" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label15" runat="server" Text="ISSUE/TRANSFER DATE" CssClass="eslbl"
                                ForeColor="Black" Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" height="28" width="150px" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label16" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" height="28" width="150px" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label45" runat="server" Text="Type of Transfer" CssClass="eslbl" ForeColor="Black"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" height="28" width="150px" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label46" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#E0DDDD" height="28" width="150px" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label17" runat="server" Text="ISSUED VALUE" CssClass="eslbl" ForeColor="Black"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" width="150px" height="28" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label18" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" width="150px" height="28" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label19" runat="server" Text="ISSUE STATUS" CssClass="eslbl" ForeColor="Black"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#E0DDDD" width="150px" height="28" colspan="3" style="border: thin groove #000000">
                            &nbsp;
                            <asp:Label ID="Label20" runat="server" Text="" CssClass="eslbl" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="porpt" class="style2" style="border: thin groove #000000;" runat="server">
                    <tr id="trprrpt" runat="server">
                        <td colspan="6" align="center">
                            &nbsp;
                            <asp:Label ID="Label36" runat="server" Text="PO REPORT" CssClass="eslbl" Font-Bold="true"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <asp:Repeater ID="rpt" runat="server" OnItemCommand="rpt_ItemCommand" OnItemDataBound="rpt_ItemDataBound">
                                <ItemTemplate>
                                    <table style="border-bottom: 1px solid #000; width: 100%;">
                                        <tr>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label21" runat="server" CssClass="eslbl" ForeColor="Black" Font-Bold="true"
                                                    Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                                <asp:Label ID="Label37" runat="server" Text=". PO NO :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label38" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("prop1")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" align="left" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label39" runat="server" Text="PO DATE :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label40" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("prop2", "{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label41" runat="server" Text="PO AMOUNT :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label42" runat="server" Text='<%# Eval("prop4")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 200px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label51" runat="server" Text="PO STATUS :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label52" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("prop3")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <table style="border-bottom: 1px solid #000; width: 100%;">
                                        <tr>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label21" runat="server" CssClass="eslbl" ForeColor="Black" Font-Bold="true"
                                                    Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                                <asp:Label ID="Label37" runat="server" Text=". PO NO :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label38" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("prop1")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" align="left" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label39" runat="server" Text="PO DATE :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label40" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("prop2", "{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label41" runat="server" Text="PO AMOUNT :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label42" runat="server" Text='<%# Eval("prop4")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 200px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label51" runat="server" Text="PO STATUS :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label52" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("prop3")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="tblmrr" class="style2" style="border: thin groove #000000;" runat="server">
                    <tr id="trmrr" runat="server">
                        <td colspan="6" align="center">
                            &nbsp;
                            <asp:Label ID="Lblmrr" runat="server" Text="MRR REPORT" CssClass="eslbl" Font-Bold="true"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <asp:Repeater ID="rptmrr" runat="server" OnItemDataBound="rptmrr_ItemDataBound">
                                <ItemTemplate>
                                    <table style="border-bottom: 1px solid #000; width: 100%;">
                                        <tr>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label21" runat="server" CssClass="eslbl" ForeColor="Black" Font-Bold="true"
                                                    Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                                <asp:Label ID="Label37" runat="server" Text=". MRR NO :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label38" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("propmr")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" align="left" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label39" runat="server" Text="DATE :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label40" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("proprd", "{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label41" runat="server" Text="PO NO :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label42" runat="server" Text='<%# Eval("proppo")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 200px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label51" runat="server" Text="MRR STATUS :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label52" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("propst")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <table style="border-bottom: 1px solid #000; width: 100%;">
                                        <tr>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label21" runat="server" CssClass="eslbl" ForeColor="Black" Font-Bold="true"
                                                    Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                                <asp:Label ID="Label37" runat="server" Text=". MRR NO :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label38" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("propmr")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" align="left" style="width: 100px; border: thin groove #000000;">
                                                <asp:Label ID="Label39" runat="server" Text="DATE :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label40" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("proprd", "{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 100px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label41" runat="server" Text="PO NO :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label42" runat="server" Text='<%# Eval("proppo")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="width: 200px; border: thin groove #000000;"
                                                align="left">
                                                <asp:Label ID="Label51" runat="server" Text="MRR STATUS :-" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label52" runat="server" CssClass="eslbl" ForeColor="Black" Text='<%# Eval("propst")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="tblinvoice" class="style2" runat="server">
                    <tr id="trinvoice" runat="server">
                        <td colspan="6" align="center">
                            &nbsp;
                            <asp:Label ID="Label47" runat="server" Text="INVOICE REPORT" CssClass="eslbl" Font-Bold="true"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <asp:Repeater ID="rptinvoice" runat="server" OnItemDataBound="rptinvoice_ItemDataBound">
                                <ItemTemplate>
                                    <table id="invoicerpt" runat="server" width="100%">
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="150px" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label22" runat="server" Text="SUPPLIER NAME" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="8" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("propname")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label24" runat="server" Text="INVOICE NUMBER" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="100px">
                                                &nbsp;
                                                <asp:Label ID="Label25" runat="server" Text='<%# Eval("propinvoice")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label43" runat="server" Text="MRR NUMBER" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label44" runat="server" Text='<%# Eval("propmr")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="100px">
                                                &nbsp;
                                                <asp:Label ID="Label26" runat="server" Text="INVOICE DATE" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="100px">
                                                &nbsp;
                                                <asp:Label ID="Label27" runat="server" Text='<%# Eval("propind")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label28" runat="server" Text="BASIC INVOICE VALUE" CssClass="eslbl"
                                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label29" runat="server" Text='<%# Eval("propbasic")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="70px">
                                                &nbsp;
                                                <asp:Label ID="Label30" runat="server" Text="EXCISES" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000;" width="60px">
                                                &nbsp;
                                                <asp:Label ID="Label31" runat="server" Text='<%# Eval("propED")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label32" runat="server" Text="VAT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="60px">
                                                &nbsp;
                                                <asp:Label ID="Label33" runat="server" Text='<%# Eval("propVAT")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label34" runat="server" Text="NET AMOUNT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label35" runat="server" Text='<%# Eval("propNA")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label49" runat="server" Text="INVOICE STATUS" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label50" runat="server" Text='<%# Eval("propst")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="3" align="center">
                                                &nbsp;
                                                <asp:Label ID="Label57" runat="server" Text="PAID THROUGH CASH" CssClass="eslbl"
                                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label59" runat="server" Text="AMOUNT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="1">
                                                &nbsp;
                                                <asp:Label ID="Label60" runat="server" Text='<%# Eval("propcashamt")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2" align="center">
                                                &nbsp;
                                                <asp:Label ID="Label53" runat="server" Text="PAID THROUGH BANK" CssClass="eslbl"
                                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="3">
                                                &nbsp;
                                                <asp:Label ID="Label54" runat="server" Text='<%# Eval("propchequepay")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label55" runat="server" Text="AMOUNT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label56" runat="server" Text='<%# Eval("propchamount")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="3" align="center">
                                                &nbsp;
                                                <asp:Label ID="Label58" runat="server" Text="PAYMENT STATUS" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="6">
                                                &nbsp;
                                                <asp:Label ID="Label62" runat="server" Text='<%# Eval("propayst")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <table id="invoicerpt" runat="server" width="100%">
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="150px" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label22" runat="server" Text="SUPPLIER NAME" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="8" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("propname")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label24" runat="server" Text="INVOICE NUMBER" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="100px">
                                                &nbsp;
                                                <asp:Label ID="Label25" runat="server" Text='<%# Eval("propinvoice")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label43" runat="server" Text="MRR NUMBER" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" colspan="2" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label44" runat="server" Text='<%# Eval("propmr")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="100px">
                                                &nbsp;
                                                <asp:Label ID="Label26" runat="server" Text="INVOICE DATE" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="100px">
                                                &nbsp;
                                                <asp:Label ID="Label27" runat="server" Text='<%# Eval("propind")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label28" runat="server" Text="BASIC INVOICE VALUE" CssClass="eslbl"
                                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label29" runat="server" Text='<%# Eval("propbasic")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="70px">
                                                &nbsp;
                                                <asp:Label ID="Label30" runat="server" Text="EXCISES" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000;" width="60px">
                                                &nbsp;
                                                <asp:Label ID="Label31" runat="server" Text='<%# Eval("propED")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label32" runat="server" Text="VAT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" width="60px">
                                                &nbsp;
                                                <asp:Label ID="Label33" runat="server" Text='<%# Eval("propVAT")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label34" runat="server" Text="NET AMOUNT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label35" runat="server" Text='<%# Eval("propNA")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" align="left">
                                                &nbsp;
                                                <asp:Label ID="Label49" runat="server" Text="INVOICE STATUS" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000">
                                                &nbsp;
                                                <asp:Label ID="Label50" runat="server" Text='<%# Eval("propst")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="3" align="center">
                                                &nbsp;
                                                <asp:Label ID="Label57" runat="server" Text="PAID THROUGH CASH" CssClass="eslbl"
                                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label59" runat="server" Text="AMOUNT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="1">
                                                &nbsp;
                                                <asp:Label ID="Label60" runat="server" Text='<%# Eval("propcashamt")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2" align="center">
                                                &nbsp;
                                                <asp:Label ID="Label53" runat="server" Text="PAID THROUGH BANK" CssClass="eslbl"
                                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="3">
                                                &nbsp;
                                                <asp:Label ID="Label54" runat="server" Text='<%# Eval("propchequepay")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label55" runat="server" Text="AMOUNT" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="2">
                                                &nbsp;
                                                <asp:Label ID="Label56" runat="server" Text='<%# Eval("propchamount")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="3" align="center">
                                                &nbsp;
                                                <asp:Label ID="Label58" runat="server" Text="PAYMENT STATUS" CssClass="eslbl" ForeColor="Black"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td bgcolor="#E0DDDD" style="border: thin groove #000000" colspan="6">
                                                &nbsp;
                                                <asp:Label ID="Label62" runat="server" Text='<%# Eval("propayst")%>' CssClass="eslbl"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
