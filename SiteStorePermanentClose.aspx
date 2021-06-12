<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteStorePermanentClose.aspx.cs"
    Inherits="SiteStorePermanentClose" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Css/style.css" rel="stylesheet" type="text/css" />
    <link href="Css/listgrid.css" rel="stylesheet" type="text/css" />
    <link href="Css/calender-blue.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script language="javascript" type="text/javascript">
        function IsNumeric(evt) {
            GridView = document.getElementById("gvccpermanentclosed");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }      
    </script>
    <script language="javascript" type="text/javascript">
        function Calculate() {
            var issuefromcctotal = 0;
            var lostanddamagetotal = 0;
            var transfertocctotal = 0;
            var GridView1 = document.getElementById("gvccpermanentclosed");
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(0).innerText;
                var itemtype = GridView1.rows(rowCount).cells(0).innerHTML.substring(0, 1);
                var quantity = GridView1.rows(rowCount).cells(3).innerText;
                var issuefromcc = GridView1.rows(rowCount).cells(4).children[0].value;
                var lostanddamage = GridView1.rows(rowCount).cells(5).children[0].value;
                var transfertocc = GridView1.rows(rowCount).cells(6).children[0].value;
                issuefromcctotal = issuefromcctotal + parseFloat(issuefromcc);
                lostanddamagetotal = lostanddamagetotal + parseFloat(lostanddamage);
                transfertocctotal = transfertocctotal + parseFloat(transfertocc);
                if (parseFloat(issuefromcc) != "") {
                    if (parseFloat(issuefromcc) > parseFloat(quantity)) {
                        window.alert("Consumed quantity is not more than balance quantity for the itemcode" + itemcode);
                        GridView1.rows(rowCount).cells(4).children[0].value = "0";
                        issuefromcctotal = issuefromcctotal + parseFloat(issuefromcc);
                        return false;
                    }
                }
                if (parseFloat(lostanddamage) != "") {
                    if (parseFloat(lostanddamage) > parseFloat(quantity)) {
                        window.alert("Lost/Damaged quantity is not more than balance quantity for the itemcode" + itemcode);
                        GridView1.rows(rowCount).cells(5).children[0].value = "0";
                        lostanddamagetotal = lostanddamagetotal + parseFloat(lostanddamage);
                        return false;
                    }
                }
                if (parseFloat(transfertocc) != "") {
                    if (parseFloat(transfertocc) > parseFloat(quantity)) {
                        window.alert("Transfer to central store quantity is not more than balance quantity for the itemcode" + itemcode);
                        GridView1.rows(rowCount).cells(6).children[0].value = "0";
                        transfertocctotal = transfertocctotal + parseFloat(transfertocc);
                        return false;
                    }
                }
                if ((parseFloat(issuefromcc) + parseFloat(lostanddamage) + parseFloat(transfertocc) > parseFloat(quantity))) {
                    window.alert("Sum of Consumed,Lost/Damaged and Transfer to central store quantity is not more than balance quantity for the itemcode" + itemcode);
                    GridView1.rows(rowCount).cells(4).children[0].value = "0";
                    GridView1.rows(rowCount).cells(5).children[0].value = "0";
                    GridView1.rows(rowCount).cells(6).children[0].value = "0";
                    issuefromcctotal = issuefromcctotal + parseFloat(issuefromcc);
                    lostanddamagetotal = lostanddamagetotal + parseFloat(lostanddamage);
                    transfertocctotal = transfertocctotal + parseFloat(transfertocc);
                    return false;


                }
                GridView1.rows[GridView1.rows.length - 1].cells[4].innerHTML = parseFloat(issuefromcctotal);
                GridView1.rows[GridView1.rows.length - 1].cells[5].innerHTML = parseFloat(lostanddamagetotal);
                GridView1.rows[GridView1.rows.length - 1].cells[6].innerHTML = parseFloat(transfertocctotal);
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function validation() {
            var GvClose = document.getElementById("gvccpermanentclosed");
            if (GvClose != null) {
                for (var rowCount = 1; rowCount < GvClose.rows.length - 1; rowCount++) {
                    var itemcode = GvClose.rows(rowCount).cells(0).innerText;
                    var quantity = GvClose.rows(rowCount).cells(3).innerText;
                    var issuefromcc = GvClose.rows(rowCount).cells(4).children[0].value;
                    var lostanddamage = GvClose.rows(rowCount).cells(5).children[0].value;
                    var transfertocc = GvClose.rows(rowCount).cells(6).children[0].value;
                    if (GvClose.rows(rowCount).cells(4).children[0].value == "") {
                        window.alert("Consumed quantity can't left blank for the itemcode" + itemcode);
                        return false;
                    }
                    if (GvClose.rows(rowCount).cells(5).children[0].value == "") {
                        window.alert("Lost/Damaged quantity can't left blank balance for the itemcode" + itemcode);
                        return false;
                    }
                    if (GvClose.rows(rowCount).cells(6).children[0].value == "") {
                        window.alert("Transfer to central store can't left blank for the itemcode" + itemcode);
                        return false;
                    }
                    if ((parseFloat(issuefromcc) + parseFloat(lostanddamage) + parseFloat(transfertocc) < parseFloat(quantity))) {
                        window.alert("Sum of Consumed,Lost/Damaged and Transfer to central store quantity is not less than balance quantity for the itemcode" + itemcode);
                        return false;
                    }
                }
            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div align="center">
        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Larger" ForeColor="Maroon"></asp:Label></div>
    <div style="border: 1px solid maroon">
        <asp:HiddenField ID="hfrole" runat="server" />
        <asp:GridView ID="gvccpermanentclosed" Width="100%" EnableViewState="true" runat="server"
            Height="100px" Font-Size="Small" AutoGenerateColumns="False" CssClass="grid-content"
            BorderColor="White" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
            FooterStyle-BackColor="White" ShowFooter="true" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
            OnRowDataBound="gvccpermanentclosed_RowDataBound">
            <Columns>
                <%-- <asp:BoundField DataField="id" Visible="false" />--%>
                <asp:BoundField DataField="item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                <%--Cell[0] --%>
                <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                <%--Cell[1] --%>
                <asp:BoundField DataField="specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                <%--Cell[2] --%>
                <asp:BoundField DataField="quantity" HeaderText="Balance Quantity" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false" />
                <%--Cell[3] Balance Stock at CC --%>
                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Consumed(issued from CC)"
                    ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtissuefromcc" Width="40px" runat="server" onkeypress='IsNumeric(event)'
                            onkeyup="Calculate()"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtissuefromccwe" WatermarkText="0" WatermarkCssClass="watermarked"
                            TargetControlID="txtissuefromcc" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Cell[4]--%>
                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Lost&damaged" ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtlostordamaged" Width="40px" runat="server" onkeypress='IsNumeric(event)'
                            onkeyup="Calculate()"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtlostordamagedwe" WatermarkText="0" WatermarkCssClass="watermarked"
                            TargetControlID="txtlostordamaged" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Cell[5]--%>
                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Transfer to CS" ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:TextBox ID="txttransfertocs" Width="40px" runat="server" onkeypress='IsNumeric(event)'
                            onkeyup="Calculate()"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txttransfertocswe" WatermarkText="0" WatermarkCssClass="watermarked"
                            TargetControlID="txttransfertocs" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Cell[6]--%>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <table width="100%" id="tbltempclose" runat="server">
            <tr style="width: 750px">
                <td>
                    <table>
                        <%-- <tr align="left">
                            <td valign="top" colspan="5">
                                <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddltype" runat="server" Width="150px">
                                    <asp:ListItem Value="Select Status" Text="Select Status"></asp:ListItem>
                                    <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                                    <asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtdate" Font-Size="Small" runat="server" Style="width: 130px; vertical-align: middle"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtdate">
                                </cc1:CalendarExtender>
                                &nbsp;&nbsp;
                                <asp:Label ID="lbldescr" runat="server" Text="Description"></asp:Label>
                                <asp:TextBox ID="txtdesc" TextMode="MultiLine" Width="340px" runat="server"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr align="center" style="width: ">
                            <td colspan="5" class="item search_filters item-group" valign="top">
                                <div class="group-expand">
                                </div>
                            </td>
                        </tr>
                        <tr style="width: 750px;" align="center">
                            <td colspan="5">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="submit" OnClick="btnsubmit_Click"
                                    OnClientClick="return validation()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
