<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewPF.aspx.cs" Inherits="ViewPF" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validate() {
            var month = document.getElementById("<%=ddldcacode.ClientID %>").value;
            var sub = document.getElementById("<%=ddldetailhead.ClientID %>").value;
            var year = document.getElementById("<%=ddlyear.ClientID %>").value;


            if (month == "0") {
                window.alert("Select Dca");

                return false;
            }
            else if (sub == "Select SubDca") {
                window.alert("Select Sub Dca");

                return false;
            }
            else if (year == "Select Year") {
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <table width="700px" style="border: 1px solid #000" class="estbl">
                                        <tr>
                                            <th align="center" colspan="4">
                                                View PF Payment
                                            </th>
                                        </tr>
                                        <tr style="border: none">
                                            <td align="center">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="150px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" Category="dd1" PromptText="Select All" LoadingText="Please Wait"
                                                    ServiceMethod="newcostcode">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldcacode" CssClass="esddown" runat="server" Width="120px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddldcacode_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Select Dca"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Select All"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="DCA-01"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="DCA-02"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddldetailhead" CssClass="esddown" runat="server" Width="120px">
                                                    <%--     <asp:ListItem Value="0" Text="Select SubDca"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="DCA-01 .5"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="DCA-01 .8"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="DCA-02 .5"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="DCA-02 .7"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:Button ID="btnSearch" runat="server" CssClass="esbtn" Text="View" OnClientClick="return validate();"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 700px">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                        AlternatingRowStyle-CssClass="alt" GridLines="Both" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"
                                        Font-Size="Small" AllowPaging="false" EmptyDataText="No Records Available">
                                        <Columns>
                                            <asp:BoundField DataField="date" ItemStyle-Width="80px" HeaderText="Due Date" />
                                            <asp:BoundField DataField="modifieddate" ItemStyle-Width="80px" HeaderText="Paid Date" />
                                            <asp:BoundField DataField="cc_code" ItemStyle-Width="80px" HeaderText="CC Code" />
                                            <asp:BoundField DataField="sub_dca" ItemStyle-Width="80px" HeaderText="SDCA Code" />
                                            <asp:BoundField DataField="bank_name"  HeaderText="Bank Name" />
                                            <asp:BoundField DataField="no" ItemStyle-Width="80px" HeaderText="ChequeNo" />
                                            <asp:BoundField DataField="debit" ItemStyle-Width="80px" HeaderText="Amount" />
                                            <asp:BoundField DataField="description"  HeaderText="Description" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr id="trexcel" runat="server">
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                                    <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                        OnClick="btnExcel_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
