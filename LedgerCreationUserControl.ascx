<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LedgerCreationUserControl.ascx.cs"
    Inherits="LedgerCreationUserControl" %>
<script src="Java_Script/NewValidation.js" type="text/javascript"></script>
<script language="javascript">
    
    function capitalize(textboxid, str) {
        // string with alteast one character
        if (str && str.length >= 1) {
            var firstChar = str.charAt(0);
            var remainingStr = str.slice(1);
            str = firstChar.toUpperCase() + remainingStr;
        }
        document.getElementById(textboxid).value = str;
    }
    function isNumber(evt) {
        myFunction();
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
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
                } else {
                    evt.returnValue = false;
                }
            }
        }
    }
</script>
<table align="center" class="estbl" width="100%" runat="server">
    <tr id="tr1" runat="server">
        <td align="right" style="width: 150px">
            <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Ledger Name"></asp:Label>
        </td>
        <td align="left" style="width: 250px">
            <asp:TextBox ID="txtledgername" CssClass="estbox" runat="server" onkeyup="javascript:capitalize(this.id, this.value);"
                Width="200px" ToolTip="Ledger Name" MaxLength="50"></asp:TextBox>
        </td>
        <td align="right" style="width: 150px">
            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Sub-Groups"></asp:Label>
        </td>
        <td align="left" style="width: 250px">
            <asp:DropDownList ID="ddlsubgroup" CssClass="esddown" Width="200px" runat="server"
                ToolTip="Sub-Groups">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 150px">
            <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Balance As On "></asp:Label>
        </td>
        <td align="left" style="width: 250px">
            <asp:TextBox ID="txtledbaldate" CssClass="estbox" Width="200px" ToolTip="Balance As On"
                onKeyDown="preventBackspace();" onpaste="return false;" onkeypress="return false;"
                runat="server"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtledbaldate"
                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                PopupButtonID="txtledbaldate">
            </cc1:CalendarExtender>
        </td>
        <td align="right" style="width: 150px">
            <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="Opening Balance"></asp:Label>
        </td>
        <td align="left" style="width: 250px">
            <asp:TextBox ID="txtopeningbal" CssClass="estbox" onkeypress="return isNumber(event)"
                runat="server" Width="200px" ToolTip="Opening Balance" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr align="center">
        <td colspan="4" align="center">
            <asp:RadioButtonList ID="rbtnpaymenttype" CssClass="esrbtn" Style="font-size: small"
                ToolTip="Credit or Debit Type" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                CellSpacing="0">
                <asp:ListItem Value="0">Debit</asp:ListItem>
                <asp:ListItem Value="1">Credit</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
