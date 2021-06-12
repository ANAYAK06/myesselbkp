<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Verifypf.aspx.cs"
    Inherits="Verifypf" Title="Verify PF" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validate() {

            var objs = new Array("<%=txtamount.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnupdate.ClientID %>").style.display = 'none';
            return true;

        }

        function ValidateNumberKeyPress(field, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var keychar = String.fromCharCode(charCode);

            if (charCode > 31 && (charCode < 48 || charCode > 57) && keychar != "." && keychar != "-") {
                return false;
            }

            if (keychar == "." && field.value.indexOf(".") != -1) {
                return false;
            }

            if (keychar == "-") {
                if (field.value.indexOf("-") != -1 /* || field.value[0] == "-" */) {
                    return false;
                }
                else {
                    //save caret position
                    var caretPos = getCaretPosition(field);
                    if (caretPos != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        function ValidateNumberKeyUp(field) {
            if (document.selection.type == "Text") {
                return;
            }

            //save caret position
            var caretPos = getCaretPosition(field);

            var fdlen = field.value.length;

            UnFormatNumber(field);

            var IsFound = /^-?\d+\.{0,1}\d*$/.test(field.value);
            if (!IsFound) {
                setSelectionRange(field, caretPos, caretPos);
                return false;
            }

            field.value = FormatNumber(field.value);

            fdlen = field.value.length - fdlen;


            setSelectionRange(field, caretPos + fdlen, caretPos + fdlen);
        }

        function ValidateAndFormatNumber(NumberTextBox) {
            if (NumberTextBox.value == "") return;

            UnFormatNumber(NumberTextBox);

            var IsFound = /^-?\d+\.{0,1}\d*$/.test(NumberTextBox.value);
            if (!IsFound) {
                alert("Not a number");
                NumberTextBox.focus();
                NumberTextBox.select();
                return;
            }

            if (isNaN(parseFloat(NumberTextBox.value))) {
                alert("Number exceeding float range");
                NumberTextBox.focus();
                NumberTextBox.select();
            }

            NumberTextBox.value = FormatNumber(NumberTextBox.value);
        }

        function FormatNumber(fnum) {
            var orgfnum = fnum;
            var flagneg = false;

            if (fnum.charAt(0) == "-") {
                flagneg = true;
                fnum = fnum.substr(1, fnum.length - 1);
            }

            psplit = fnum.split(".");

            var cnum = psplit[0],
	            parr = [],
	            j = cnum.length,
	            m = Math.floor(j / 3),
	            n = cnum.length % 3 || 3;

            // break the number into chunks of 3 digits; first chunk may be less than 3
            for (var i = 0; i < j; i += n) {
                if (i != 0) { n = 3; }
                parr[parr.length] = cnum.substr(i, n);
                m -= 1;
            }

            // put chunks back together, separated by comma
            fnum = parr.join(",");

            // add the precision back in
            //if (psplit[1]) {fnum += "." + psplit[1];}
            if (orgfnum.indexOf(".") != -1) {
                fnum += "." + psplit[1];
            }

            if (flagneg == true) {
                fnum = "-" + fnum;
            }

            return fnum;
        }

        function UnFormatNumber(obj) {
            if (obj.value == "") return;

            obj.value = obj.value.replace(/,/gi, "");
        }

        function getCaretPosition(objTextBox) {

            var objTextBox = window.event.srcElement;

            var i = objTextBox.value.length;

            if (objTextBox.createTextRange) {
                objCaret = document.selection.createRange().duplicate();
                while (objCaret.parentElement() == objTextBox &&
                  objCaret.move("character", 1) == 1) --i;
            }
            return i;
        }

        function setSelectionRange(input, selectionStart, selectionEnd) {
            if (input.setSelectionRange) {
                input.focus();
                input.setSelectionRange(selectionStart, selectionEnd);
            }
            else if (input.createTextRange) {
                var range = input.createTextRange();
                range.collapse(true);
                range.moveEnd('character', selectionEnd);
                range.moveStart('character', selectionStart);
                range.select();
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
                        <table style="width: 750px">
                            <tr align="center">
                                <td>
                                    <table style="width: 500px;">
                                        <tr align="center">
                                            <th>
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify PF"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="500px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="id" FooterStyle-BackColor="DarkGray"
                                                    EmptyDataText="There is no Records" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="CC_Code" ItemStyle-Width="50px" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="Sub_DCA" ItemStyle-Width="75px" HeaderText="SDCA Code" />
                                                        <asp:BoundField DataField="Date" ItemStyle-Width="100px" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="debit" ItemStyle-Width="100px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" DeleteText="Delete"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tblinvoice" runat="server" style="margin: 1em; border-collapse: collapse;"
                                        width="500px">
                                        <tr align="center">
                                            <td>
                                                <table id="Table1" width="600px" runat="server" style="background-color: White">
                                                    <tr>
                                                        <th colspan="4" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small;
                                                            background: #E3E4FA;" align="center">
                                                            Verify PF
                                                        </th>
                                                    </tr>
                                                    <tr style="padding: .3em; border: 1px #000000 solid;">
                                                        <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                            align="center">
                                                            <asp:Label ID="lbl1" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                                Text="CC Code"></asp:Label>
                                                        </td>
                                                        <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                            <asp:TextBox ID="txtcccode" runat="server" Enabled="false" Style="color: #000000;
                                                                font-family: Tahoma; text-decoration: none" Width="90%"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                            align="center">
                                                            <asp:Label ID="Label3" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                                Text="SDCA Code"></asp:Label>
                                                        </td>
                                                        <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                            <asp:TextBox ID="txtsdca" runat="server" Enabled="false" Style="color: #000000; font-family: Tahoma;
                                                                text-decoration: none" Width="90%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="padding: .3em; border: 1px #000000 solid;">
                                                        <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                            align="center">
                                                            <asp:Label ID="Label7" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                                Text="Date"></asp:Label>
                                                        </td>
                                                        <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                            <asp:TextBox ID="txtdate" runat="server" ToolTip="Date" Style="color: #000000; font-family: Tahoma;
                                                                text-decoration: none" Width="90%"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtdate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                            align="center">
                                                            <asp:Label ID="Label2" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                                Text="Amount"></asp:Label>
                                                        </td>
                                                        <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                                                            align="center">
                                                            <asp:TextBox ID="txtamount" runat="server" Width="90%" ToolTip="Amount" Style="color: #000000;
                                                                font-family: Tahoma; text-decoration: none" onkeypress="return ValidateNumberKeyPress(this, event);"
                                                                onkeyup="ValidateNumberKeyUp(this);" onblur="ValidateAndFormatNumber(this)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="padding: .3em; border: 1px #000000 solid;">
                                                        <td colspan="4" align="center" style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;">
                                                            <asp:Button CssClass="esbtn" Style="font-size: small; height: 26px;" ID="btnupdate"
                                                                runat="server" Text="Update" OnClick="btnupdate_Click" OnClientClick="javascript:return validate()" />
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnreset" runat="server" CssClass="esbtn" Style="font-size: small;
                                                                height: 26px;" Text="Back" OnClick="btnreset_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
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
