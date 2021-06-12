<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="viewunsecuredloan.aspx.cs"
    Inherits="viewunsecuredloan" EnableEventValidation="false" Title="View Unsecured Loan" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
         function validate() {
             var month = document.getElemesntById("<%=ddlmonth.ClientID %>").value;
             var year = document.getElementById("<%=ddlyear.ClientID %>").value;

             if (year == "Select Year") {
                 window.alert("Select Year");
                
                 return false;
             }
             else if ((month != "Select Month")&&(year == "Select Year")) {
             window.alert("Select Year");
           
             return false;
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
                <table>
                    <tr>
                        <td align="center">
                            <table width="700px" style="border: 1px solid #000" class="estbl">
                                <tr>
                                    <th align="center" colspan="4">
                                        View Unsecured Loan
                                    </th>
                                </tr>
                                <tr style="border: none">
                                    <td align="center">
                                        <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="200px" runat="server" ToolTip="Month">
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
                                        <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="year"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="200px" runat="server" ToolTip="Year">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="esbtn" Text="View" OnClick="btnSearch_Click"
                                            OnClientClick="return validate();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="width: 700px">
                            <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                AlternatingRowStyle-CssClass="alt" GridLines="Both" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"
                                Font-Size="Small" AllowPaging="false">
                                <Columns>
                                    <asp:BoundField DataField="Date" ItemStyle-Width="80px" HeaderText="Date" />
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit" ItemStyle-Width="125px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcredit" runat="server" Text='<%#Bind("credit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit" ItemStyle-Width="125px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldebit" runat="server" Text='<%#Bind("debit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
