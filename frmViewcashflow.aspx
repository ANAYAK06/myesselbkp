<%@ Page Language="C#" MasterPageFile="~/Essel.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="frmViewcashflow.aspx.cs" Inherits="Admin_frmViewcashflow"
    Title="View Cash Flow - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function report(addr) {
            window.open(addr, 'Report', 'width=780,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
            return false;
        }
    </script>

    <script language="javascript">
        function report1(addr1) {
            window.open(addr1, 'Report', 'width=550,height=230,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
            return false;
        }
    </script>

    <script type="text/javascript">
        function validate() {
            var role = document.getElementById("<%=hfrole.ClientID %>").value
            if (role != "Project Manager") {
                var objs = new Array("<%=ddlpaidagainst.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="width: 700px">
                                    <tr valign="top">
                                        <td align="center">
                                            <table class="estbl" width="600px">
                                                <tr style="border: 1px solid #000">
                                                    <th>
                                                        <asp:Label ID="lblview" CssClass="esfmhead" runat="server" Text="View Cash Flow"></asp:Label>
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
                                                                        <asp:ListItem>By DCA</asp:ListItem>
                                                                        <asp:ListItem>By IT</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table class="estbl" id="table" runat="server">
                                                            <tr id="trcc" runat="server">
                                                                <td style="width: 140px">
                                                                    <asp:Label ID="Label1" runat="server" Text="Select Cost Center" CssClass="eslbl"></asp:Label>
                                                                </td>
                                                                <td colspan="1" align="left">
                                                                    <asp:DropDownList ID="ddlcccode" AutoPostBack="true" CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);"
                                                                        runat="server" Width="200px" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged1">
                                                                    </asp:DropDownList>
                                                                    <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlcccode"
                                                                        ServicePath="cascadingDCA.asmx" Category="cc" LoadingText="Loading CC" ServiceMethod="codename"
                                                                        OnDataBinding="ddlcccode_SelectedIndexChanged1" PromptText="Select All">
                                                                    </cc1:CascadingDropDown>
                                                                    <%-- <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                                TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                ServiceMethod="GetCCName">
                                                            </cc1:DynamicPopulateExtender>--%>
                                                                </td>
                                                            </tr>
                                                            <tr id="paid" runat="server">
                                                                <td style="width: 140px">
                                                                    <asp:Label ID="Label6" runat="server" Text="Paid Against" CssClass="eslbl"></asp:Label>
                                                                </td>
                                                                <td colspan="1" align="left">
                                                                    <asp:DropDownList ID="ddlpaidagainst" CssClass="esddown" ToolTip="Paid Against CC"
                                                                        onchange="SetDynamicKey('dp5',this.value);" runat="server" Width="200px">
                                                                    </asp:DropDownList>
                                                                    <cc1:CascadingDropDown ID="CascadingDropDown7" runat="server" TargetControlID="ddlpaidagainst"
                                                                        ServicePath="cascadingDCA.asmx" Category="cc1" LoadingText="Loading CC" ServiceMethod="CC">
                                                                    </cc1:CascadingDropDown>
                                                                    <%--<asp:Label ID="Label7" class="ajaxspan" runat="server"></asp:Label>
                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp5" runat="server"
                                                                TargetControlID="Label7" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                ServiceMethod="GetCCName">
                                                            </cc1:DynamicPopulateExtender>--%>
                                                                </td>
                                                            </tr>
                                                            <tr id="it" runat="server">
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text="IT Code" CssClass="eslbl"></asp:Label>
                                                                </td>
                                                                <td colspan="1" align="left">
                                                                    <asp:DropDownList ID="ddlit" runat="server" Width="100px" CssClass="esddown">
                                                                    </asp:DropDownList>
                                                                    <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddlit"
                                                                        ServicePath="cascadingDCA.asmx" Category="sub1" LoadingText="Please Wait" ServiceMethod="itcode">
                                                                    </cc1:CascadingDropDown>
                                                                </td>
                                                            </tr>
                                                            <tr id="dca" runat="server">
                                                                <td style="width: 140px">
                                                                    <asp:Label ID="Label5" runat="server" Text="DCA Head" CssClass="eslbl"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="top">
                                                                    <asp:DropDownList ID="ddldetailhead" CssClass="esddown" runat="server" Width="150px"
                                                                        onchange="SetDynamicKey('dp3',this.value);" />
                                                                    <cc1:CascadingDropDown ID="CascadingDropDown1" TargetControlID="ddldetailhead" ServicePath="cascadingDCA.asmx"
                                                                        Category="dca" LoadingText="Loading DCA" ServiceMethod="viewcash" runat="server">
                                                                    </cc1:CascadingDropDown>
                                                                    <asp:Label ID="Label3" class="ajaxspan" runat="server"></asp:Label>
                                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                                        TargetControlID="Label3" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                        ServiceMethod="GetDCAName">
                                                                    </cc1:DynamicPopulateExtender>
                                                                </td>
                                                            </tr>
                                                            <tr id="sdca" runat="server">
                                                                <td align="left">
                                                                    <asp:Label ID="lblsubdca" runat="server" Text="Sub DCA Head" CssClass="eslbl"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlsubdetail" CssClass="esddown" Width="150px" runat="server"
                                                                        onchange="SetDynamicKey('dp4',this.value);" />
                                                                    <cc1:CascadingDropDown ID="CascadingDropDown2" TargetControlID="ddlsubdetail" ServicePath="cascadingDCA.asmx"
                                                                        Category="dca" ParentControlID="ddldetailhead" LoadingText="Loading DCA" ServiceMethod="viewsubdca"
                                                                        runat="server">
                                                                    </cc1:CascadingDropDown>
                                                                    <asp:Label ID="Label4" class="ajaxspan" runat="server"></asp:Label>
                                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                                                        TargetControlID="Label4" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                        ServiceMethod="GetSubDCAName">
                                                                    </cc1:DynamicPopulateExtender>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 20px" id="year" runat="server">
                                                                <td style="width: 140px">
                                                                    <asp:Label ID="Label2" runat="server" Text="View Records of" CssClass="eslbl"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="border-width: 0;" align="left">
                                                                                <asp:DropDownList ID="ddlDate" runat="server" CssClass="esddown">
                                                                                    <asp:ListItem Value="Select Date">Select Date</asp:ListItem>
                                                                                    <asp:ListItem>1</asp:ListItem>
                                                                                    <asp:ListItem>2</asp:ListItem>
                                                                                    <asp:ListItem>3</asp:ListItem>
                                                                                    <asp:ListItem>4</asp:ListItem>
                                                                                    <asp:ListItem>5</asp:ListItem>
                                                                                    <asp:ListItem>6</asp:ListItem>
                                                                                    <asp:ListItem>7</asp:ListItem>
                                                                                    <asp:ListItem>8</asp:ListItem>
                                                                                    <asp:ListItem>9</asp:ListItem>
                                                                                    <asp:ListItem>10</asp:ListItem>
                                                                                    <asp:ListItem>11</asp:ListItem>
                                                                                    <asp:ListItem>12</asp:ListItem>
                                                                                    <asp:ListItem>13</asp:ListItem>
                                                                                    <asp:ListItem>14</asp:ListItem>
                                                                                    <asp:ListItem>15</asp:ListItem>
                                                                                    <asp:ListItem>16</asp:ListItem>
                                                                                    <asp:ListItem>17</asp:ListItem>
                                                                                    <asp:ListItem>18</asp:ListItem>
                                                                                    <asp:ListItem>19</asp:ListItem>
                                                                                    <asp:ListItem>20</asp:ListItem>
                                                                                    <asp:ListItem>21</asp:ListItem>
                                                                                    <asp:ListItem>22</asp:ListItem>
                                                                                    <asp:ListItem>23</asp:ListItem>
                                                                                    <asp:ListItem>24</asp:ListItem>
                                                                                    <asp:ListItem>25</asp:ListItem>
                                                                                    <asp:ListItem>26</asp:ListItem>
                                                                                    <asp:ListItem>27</asp:ListItem>
                                                                                    <asp:ListItem>28</asp:ListItem>
                                                                                    <asp:ListItem>29</asp:ListItem>
                                                                                    <asp:ListItem>30</asp:ListItem>
                                                                                    <asp:ListItem>31</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="border-width: 0;">
                                                                                <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown">
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
                                                                            <td style="border-width: 0;">
                                                                                <asp:DropDownList ID="ddlyear" runat="server" CssClass="esddown" />
                                                                            </td>
                                                                        </tr>
                                                                        <asp:HiddenField ID="hfrole" runat="server" />
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    
                                                </tr>
                                                <tr id="btn" runat="server" align="center">
                                                    <td>
                                                        <asp:Button ID="btnview" CssClass="esbtn" runat="server" Text="View" OnClientClick="javascript:return validate()"
                                                            OnClick="btnview_Click" />
                                                        <asp:Button ID="btncancel" CssClass="esbtn" runat="server" Text="Reset" OnClick="btncancel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <span style="background-color: Maroon">
                                                <asp:Label ID="lblavl" Font-Bold="true" ForeColor="White" runat="server" Text="Avaliable Balance : "></asp:Label>
                                                <asp:Label ID="lblbalance" Font-Bold="true" ForeColor="White" runat="server" Text=""></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AllowPaging="True" AllowSorting="True"
                                                EnableSortingAndPagingCallbacks="True" GridLines="None" Font-Size="Small" PageSize="20"
                                                OnPageIndexChanging="GridView1_PageIndexChanging1" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink2" onclick="javascript:return report1(this.href);" NavigateUrl='<%#"~/frmUpdateCashVoucher.aspx?Voucherid="+Eval("id") %>'
                                                                runat="server">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="images/iconset-b-edit.gif" Width="20px"
                                                                    Height="20px" AlternateText="Print" />
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" onclick="javascript:return report(this.href);" NavigateUrl='<%#"~/frmViewReport.aspx?Voucherid="+Eval("id") %>'
                                                                runat="server">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                    Width="30px" />
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="modifiedby_date" HeaderText="Voucher Date" HeaderStyle-CssClass="mGrid header" />
                                                    <asp:BoundField DataField="date" HeaderText="Due Date" HeaderStyle-CssClass="mGrid header" />
                                                    <asp:BoundField DataField="Voucher_Id" HeaderText="VoucherId" />
                                                    <asp:BoundField DataField="Particulars" HeaderText="Description" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC_Code" />
                                                    <asp:BoundField DataField="Paid_against" HeaderText="Paid Against" />
                                                    <asp:BoundField DataField="DCA_Code" HeaderText="DCA_Code" />
                                                    <asp:BoundField DataField="Sub_DCA" HeaderText="Sub_DCA" />
                                                    <asp:BoundField DataField="IT_Code" HeaderText="IT_Code" />
                                                    <asp:BoundField DataField="Credit" HeaderText="Credit" />
                                                    <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="GridView2" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                                TabIndex="1" Width="100%" ShowFooter="False" CellPadding="4" ForeColor="#333333"
                                                GridLines="Both">
                                                <Columns>
                                                    <asp:BoundField DataField="modifiedby_date" HeaderText="Voucher Date" />
                                                    <asp:BoundField DataField="date" HeaderText="Due Date" />
                                                    <asp:BoundField DataField="Voucher_Id" HeaderText="VoucherId" />
                                                    <asp:BoundField DataField="Particulars" HeaderText="Description" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                                    <asp:BoundField DataField="Paid_against" HeaderText="Paid Against" />
                                                    <asp:BoundField DataField="DCA_Code" HeaderText="DCA Code" />
                                                    <asp:BoundField DataField="Sub_DCA" HeaderText="Sub DCA" />
                                                    <asp:BoundField DataField="IT_Code" HeaderText="IT Code" />
                                                    <asp:BoundField DataField="Credit" HeaderText="Credit" />
                                                    <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr style="border: 1px solid #000">
                                        <td>
                                            Total Credit:
                                            <asp:Label ID="lblCredit" runat="server" Text="" ToolTip="credit" Font-Size="Small"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp; Total Debit:
                                            <asp:Label ID="lblDebit" runat="server" Text="" ToolTip="Debit" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
            </td>
        </tr>
    </table>
</asp:Content>
