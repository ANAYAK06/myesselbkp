<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="viewDebitTransactions.aspx.cs"
    Inherits="viewDebitTransactions" Title="View Debit Transactions" %>
<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
        function expandcollapse(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "images/minus.png";
                }
                else {
                    img.src = "images/minus.png";
                }
                img.alt = "Close to view other Vendor Details";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "images/plus.png";
                }
                else {
                    img.src = "images/plus.png";
                }
                img.alt = "Expand to show Vendor Details";
            }
        } 
    </script>

    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <accountmenu:menu id="Menu1" runat="server" />
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <table width="500px" style="border: 1px solid #000" class="estbl">
                                <tr>
                                    <th align="center" class="style9">
                                        View Bank Statement
                                    </th>
                                </tr>
                                <tr>
                                    <td>
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
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="innertab" visible="false">
                                                    <tr align="center">
                                                        <td>
                                                            <table id="Statement" runat="server">
                                                                <tr id="paytype" runat="server" align="center">
                                                                    <%--  <td>
                                                                        <asp:Label ID="lblbank" runat="server" Text="Bank" CssClass="eslbl"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="100px">
                                                                        </asp:DropDownList>
                                                                        <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                                            ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                                            PromptText="Select">
                                                                        </cc1:CascadingDropDown>
                                                                    </td>--%>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Monthly"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="Month">
                                                                            <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="yearly"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Search" runat="server">
                                                                    <td>
                                                                        Transaction No:
                                                                    </td>
                                                                    <td align="center" colspan="3">
                                                                        <asp:TextBox ID="txtSearch" ToolTip="Search" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rbtntype" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClick="btnsubmit_Click" />&nbsp
                                        <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                    BorderColor="White" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    Font-Size="Small" GridLines="Vertical" ShowFooter="true" DataKeyNames="Transaction_No"
                                    OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:expandcollapse('div<%# Eval("Transaction_No") %>', 'one');">
                                                    <img id="imgdiv<%# Eval("Transaction_No") %>" alt="Click to show/hide Orders for Customer <%# Eval("Transaction_No") %>"
                                                        width="9px" border="0" src="images/plus.png" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Transaction_No" HeaderText="Transaction No" />
                                        <asp:BoundField DataField="created_date" HeaderText="Date" />
                                        <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                        <asp:BoundField DataField="Dca_Code" HeaderText="DCA Code" />
                                        <asp:BoundField DataField="SDCA_Code" HeaderText="SDCA Code" />
                                        <asp:BoundField DataField="Party_Name" HeaderText="Vendor Name" />
                                        <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                        <asp:BoundField DataField="Mode_Of_Pay" HeaderText="Payment Mode" />
                                        <asp:BoundField DataField="Cheque_No" HeaderText="Cheque No" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                        <asp:BoundField DataField="Created_By" HeaderText="Submitted By" />
                                        <asp:BoundField DataField="Modified_By" HeaderText="Approved By" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" align="center">
                                                        <div id="div<%# Eval("Transaction_No") %>" style="display: none; position: relative;
                                                            left: 15px; overflow: auto; width: 97%">
                                                            <asp:GridView ID="GridView2" Width="60%" BorderColor="White" AutoGenerateColumns="False"
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                Font-Size="Small" GridLines="Vertical" runat="server" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                                                    <asp:BoundField DataField="DCA_Code" HeaderText="DCA Code" />
                                                                    <asp:BoundField DataField="Sub_DCA" HeaderText="SDCA Code" />
                                                                    <asp:BoundField DataField="date" HeaderText="Date" />
                                                                    <asp:TemplateField HeaderText="Net Paid">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("debit").ToString()%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
