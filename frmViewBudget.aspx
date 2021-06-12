<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmViewBudget.aspx.cs"
    Inherits="Admin_Default" EnableEventValidation="false" Title="View Budget - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style9
        {
            width: 40px;
        }
        .style10
        {
            width: 30px;
        }
        .style11
        {
            width: 156px;
        }
    </style>

    <script language="javascript">
        function print() {
            //            w=window.open();
            //            w.document.write('<html><body onload="window.print()">'+content+'</body></html>'); 
            //            w.document.close(); 
            //            setTimeout(function(){w.close();},10);             
            //            return false;
            var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            //	var grid_obj = document.getElementById(grid_ID);
            //            if (grid_obj != null) {
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            //		new_window.close();
            //            }
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
                <table>
                    <tr>
                        <td align="center">
                            <table class="estbl" width="700px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" align="center">
                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Budget Status"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <td colspan="6">
                                                <table class="innertab" align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                                AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                                CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                                <asp:ListItem>Normal View</asp:ListItem>
                                                                <asp:ListItem>Detail View</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="estbl" width="700px">
                                            <tr id="CC" runat="server">
                                                <td align="right" style="width: 10px">
                                                    <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="105px"
                                                        CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);">
                                                    </asp:DropDownList>
                                                    <span class="starSpan">*</span>
                                                    <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlCCcode"
                                                        ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="StoreCC">
                                                    </cc1:CascadingDropDown>
                                                    <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                        TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                        ServiceMethod="GetCCName">
                                                    </cc1:DynamicPopulateExtender>
                                                </td>
                                            </tr>
                                            <tr id="dca" runat="server">
                                                <td align="right" style="width: 10px">
                                                    <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA"></asp:Label>
                                                </td>
                                                <td height="30" align="left">
                                                    <asp:DropDownList ID="ddldetailhead" CssClass="esddown" onchange="SetDynamicKey('dp3',this.value);"
                                                        runat="server" ToolTip="DCA Code" Width="100px">
                                                    </asp:DropDownList>
                                                    <span class="starSpan">*</span>
                                                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                                        ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                                                    </cc1:CascadingDropDown>
                                                    <asp:Label ID="Label2" class="ajaxspan" runat="server"></asp:Label>
                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                        TargetControlID="Label2" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                        ServiceMethod="GetDCAName">
                                                    </cc1:DynamicPopulateExtender>
                                                </td>
                                            </tr>
                                            <tr id="year" runat="server">
                                                <td align="right" style="width: 10px">
                                                    <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                </td>
                                                <td align="left" class="style11">
                                                    <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr align="center" id="btn" runat="server">
                                    <td align="center">
                                        <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                            Text="View Budget" OnClick="btnAssign_Click" />
                                        <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                            Text="Reset" OnClick="btnCancel1_Click" />
                                        <input id="print" class="esbtn" type="button" runat="server" onclick="print();" value="Print"
                                            title="Print Report" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tblpo" runat="server" class="pestbl">
                                <tr>
                                    <td>
                                        <cc1:Accordion ID="Accordion1" runat="server" TransitionDuration="100" FramesPerSecond="200"
                                            OnItemDataBound="Accordion1_ItemDataBound" FadeTransitions="true" RequireOpenedPane="false"
                                            ContentCssClass="accordionContent" HeaderCssClass="accordionHeader" Width="750px"
                                            Style="border: 1px solid #000;" HeaderSelectedCssClass="accordionHeaderSelected">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="100px">
                                                            <%# Eval("CC_Code") %>
                                                        </td>
                                                        <td width="70px">
                                                            Financial Year
                                                        </td>
                                                        <td width="70px">
                                                            Budget Assigned
                                                        </td>
                                                        <td width="50px">
                                                            <asp:Label ID="lblbal" runat="server" Text="" ToolTip="Balance"></asp:Label>
                                                        </td>
                                                        <td width="150px" align="right">
                                                            Concumed Upto Previous Year
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100px">
                                                            <%# Eval("CC_Name") %>
                                                        </td>
                                                        <td width="70px">
                                                            <%# Eval("Year") %>
                                                        </td>
                                                        <td width="110px">
                                                            <%# Eval("Budget") %>
                                                        </td>
                                                        <td width="100px">
                                                            <%# Eval("Balance") %>
                                                        </td>
                                                        <td width="150px" align="right">
                                                            <asp:Label ID="Label3" runat="server" ToolTip="Balance"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView2" runat="server" Width="750px" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="vertical-align: middle"
                                                    ShowFooter="true" OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="DCA Name" ItemStyle-Width="300px" HeaderText="DCA Name" />
                                                        <asp:BoundField DataField="DCACode" HeaderText="DCA Code" />
                                                        <asp:BoundField DataField="Prevoius Consumed" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            HeaderText="Prevoius Consumed" />
                                                        <asp:BoundField DataField="Current Year Budget" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            HeaderText="Current Year Budget" />
                                                        <asp:BoundField DataField="Current Year Consumed Budget" ItemStyle-Width="100px"
                                                            ItemStyle-HorizontalAlign="Right" HeaderText="Current Year Consumed Budget" />
                                                        <asp:BoundField DataField="Current Year Balance" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            HeaderText="Current Year Balance" />
                                                    </Columns>
                                                    <RowStyle ForeColor="#000066" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </cc1:Accordion>
                                        <cc1:Accordion ID="Accordion2" runat="server" TransitionDuration="100" FramesPerSecond="200"
                                            OnItemDataBound="Accordion2_ItemDataBound" FadeTransitions="true" RequireOpenedPane="false"
                                            ContentCssClass="accordionContent" Style="border: 1px solid #000;" Width="750px"
                                            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="100px">
                                                            <%# Eval("CC_Code") %>
                                                        </td>
                                                        <td width="70px">
                                                            Financial Year
                                                        </td>
                                                        <td width="70px">
                                                            Budget Assigned
                                                        </td>
                                                        <td width="50px">
                                                            <asp:Label ID="lblbal" runat="server" Text="" ToolTip="Balance"></asp:Label>
                                                        </td>
                                                        <td width="150px" align="right">
                                                            Concumed Upto Previous Year
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100px">
                                                            <%# Eval("CC_Name") %>
                                                        </td>
                                                        <td width="70px">
                                                            <%# Eval("Year") %>
                                                        </td>
                                                        <td width="110px">
                                                            <%# Eval("Budget") %>
                                                        </td>
                                                        <td width="100px">
                                                            <%# Eval("Balance") %>
                                                        </td>
                                                        <td width="150px" align="right">
                                                            <asp:Label ID="Label3" runat="server" ToolTip="Balance"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="vertical-align: middle"
                                                    ShowFooter="true" Width="750px" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Date" ItemStyle-Width="70px" HeaderText="Date" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="300px" />
                                                        <asp:BoundField DataField="Credit" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            HeaderText="Credit" />
                                                        <asp:BoundField DataField="Debit" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                            HeaderText="Debit" />
                                                    </Columns>
                                                    <RowStyle ForeColor="#000066" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </cc1:Accordion>
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
