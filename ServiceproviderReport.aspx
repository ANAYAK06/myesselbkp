<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ServiceproviderReport.aspx.cs"
    Inherits="ServiceproviderReport" Title="Serviceprovider Report" EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();


        }

        function showpopup() {
            $find('mdlindent').show();

        }
    </script>
    <script language="javascript" type="text/javascript">

        function validate() {
            var vendorid = document.getElementById("<%=ddlvendor.ClientID %>").value;
            var month = document.getElementById("<%=ddlmonth.ClientID %>").value;
            var year = document.getElementById("<%=ddlyear.ClientID %>").value;
            var DdlPoType = document.getElementById("<%=Ddltype.ClientID %>").value;

            if (vendorid == "Select vendor") {
                window.alert("Select Vendor");

                return false;
            }
            else if (DdlPoType == "Select POType") {
                window.alert("Select POType");
                return false;
            }

            else if ((month != "Select Month") && (year == "Any Year")) {
                window.alert("Select Year");

                return false;
            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        function print() {
            var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            $find('mdlindent').hide();

        }
        function printclose() {
            var grid_obj = document.getElementById("<%=tblpodata.ClientID %>");
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            $find('mdlindent').hide();

        }
    </script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="700px">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="700px" style="border: 1px solid #000" class="estbl">
                                        <tr>
                                            <th align="center" colspan="4">
                                                SERVICE PROVIDER PO REPORT OLD &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnnewreport" runat="server" CssClass="esbtn" Text="View New Report"
                                                    OnClick="btnnewreport_Click" />
                                            </th>
                                        </tr>
                                        <tr style="border: none">
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblcccode" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                                <asp:DropDownList ID="ddlcccode" runat="server" CssClass="esddown" AutoPostBack="true"
                                                    ToolTip="CC Code" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="Cascadingcccode" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" PromptText="Select CC Code" Category="cc" LoadingText="Please Wait"
                                                    ServiceMethod="StoreCC2">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblvendor" CssClass="eslbl" runat="server" Text="Vendor ID"></asp:Label>
                                                <asp:DropDownList ID="ddlvendor" Width="250px" CssClass="esddown" ToolTip="Select Vendor"
                                                    runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="border: none" id="trdca" runat="server">
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                                                <asp:DropDownList ID="ddldca" runat="server" Width="99px" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="SPPO Status: "></asp:Label>&nbsp&nbsp&nbsp&nbsp
                                                <asp:DropDownList ID="Ddltype" Width="200px" CssClass="esddown" ToolTip="Select Vendor"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddltype_SelectedIndexChanged">
                                                    <asp:ListItem Value="Select POType">Select Type</asp:ListItem>
                                                    <asp:ListItem Value="1">Select All</asp:ListItem>
                                                    <asp:ListItem Value="2">Running PO</asp:ListItem>
                                                    <asp:ListItem Value="3">Closed PO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="border: none" id="trddlpo" runat="server">
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                                <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown">
                                                    <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                <asp:DropDownList ID="ddlyear" runat="server" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="esbtn" Text="View" OnClientClick="javascript:return validate();"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr class="grid-content">
                                            <td colspan="3" style="width: 700px">
                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                        AlternatingRowStyle-CssClass="alt" GridLines="None" Font-Size="Small" AllowPaging="false"
                                                        DataKeyNames="pono" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowFooter="true"
                                                        OnRowDataBound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                            <asp:BoundField DataField="pono" ItemStyle-Width="80px" HeaderText="PONO" />
                                                            <asp:BoundField DataField="po_date" ItemStyle-Width="80px" HeaderText="Date" />
                                                            <asp:BoundField DataField="po_value" ItemStyle-Width="80px" HeaderText="Amount" />
                                                            <asp:BoundField DataField="balance" ItemStyle-Width="80px" HeaderText="Balance" />
                                                            <asp:BoundField DataField="cc_code" ItemStyle-Width="80px" HeaderText="CC Code" />
                                                            <asp:BoundField DataField="dca_code" ItemStyle-Width="80px" HeaderText="DCA Code" />
                                                            <asp:BoundField DataField="Subdca_code" ItemStyle-Width="80px" HeaderText="SDCA Code" />
                                                            <asp:BoundField DataField="remarks" ItemStyle-Width="250px" HeaderText="Remarks" />
                                                            <asp:BoundField DataField="status" ItemStyle-Width="80px" HeaderText="Status" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </table>
                                                <cc1:ModalPopupExtender ID="poppo" BehaviorID="mdlindent" runat="server" TargetControlID="btnModalPopUp"
                                                    PopupControlID="pnlamendsppo" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                <asp:Panel ID="pnlamendsppo" runat="server" Style="display: none;">
                                                    <table id="Table1" width="800px" border="0" align="center" runat="server" cellpadding="0"
                                                        cellspacing="0">
                                                        <tr>
                                                            <td width="13" valign="bottom">
                                                                <img src="images/leftc.jpg">
                                                            </td>
                                                            <td class="pop_head" align="left">
                                                                <div class="popclose">
                                                                    <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                </div>
                                                                <asp:Label ID="lblviewpo" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td width="13" valign="bottom">
                                                                <img src="images/rightc.jpg">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="#FFFFFF">
                                                                &nbsp;
                                                            </td>
                                                            <td height="180" valign="top" class="popcontent">
                                                                <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                    height: 600px;">
                                                                    <table style="vertical-align: middle;" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <table id="tblrun" runat="server">
                                                                                    <tr valign="bottom" id="tblpo" runat="server">
                                                                                        <td align="center" colspan="2">
                                                                                            <table width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table id="Table2" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid  #000;">
                                                                                                                <td align="center" colspan="2">
                                                                                                                    <asp:Label ID="Label4" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                        Text="Essel Projects Pvt Ltd."></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label20" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                        Text="Corp.Office:No-5,First Floor,Maruti Heritage,Near MMI,Pachpedi Naka,Raipur,492001(CG)."></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label15" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                        Text="Tel/Fax:0771-4268469/4075401."></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label21" runat="server" CssClass="peslbl" Font-Bold="true" Text="INTERNAL WORK ORDER FORMAT FOR PIECE RATE WORKER WHO HAVING PAN NO"
                                                                                                                        Font-Underline="true"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label6" runat="server" CssClass="peslbl" Font-Bold="true" Text="WORK ORDER"
                                                                                                                        Font-Underline="true"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table3" width="100%" runat="server" class="pestbl" style="">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td width="50%" align="left" style="border: 1px solid #000">
                                                                                                                    <asp:Label ID="lblvenname" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                    <asp:Label ID="lblvenaddress" runat="server" Text="" Width="99%" CssClass="pestbox"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left" width="100%" style="border: 1px solid #000">
                                                                                                                    <asp:Label ID="Label7" Width="25%" CssClass="peslbl1" runat="server" Text="PO No:-"></asp:Label>
                                                                                                                    <asp:Label ID="txtpono" Width="60%" CssClass="peslbl1" ToolTip="PO NO" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:Label><br />
                                                                                                                    <asp:Label ID="Label9" Width="25%" CssClass="peslbl1" runat="server" Text="CC Code:-"></asp:Label>
                                                                                                                    <asp:Label ID="txtcccode" Width="60%" CssClass="peslbl1" ToolTip="CC Code" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:Label><br />
                                                                                                                    <asp:Label ID="Label10" Width="25%" CssClass="peslbl1" runat="server" Text="DCA Code:-"></asp:Label>
                                                                                                                    <asp:Label ID="txtdcacode" Width="60%" CssClass="peslbl1" ToolTip="DCA Code" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:Label><br />
                                                                                                                    <asp:Label ID="Label11" Width="25%" CssClass="peslbl1" runat="server" Text="Vendor Code:-"></asp:Label>
                                                                                                                    <asp:Label ID="txtvencode" Width="60%" CssClass="peslbl1" ToolTip="Vendor Code" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table4" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2" style="border: 1px solid #000">
                                                                                                                    <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                                        OnRowDataBound="grdbill_RowDataBound" ShowFooter="true">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="PO Date" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="200px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Left"
                                                                                                                                ItemStyle-VerticalAlign="Middle">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtpodate" BorderColor="White" Text='<%# Bind("po_date") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="PO Description" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="600px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Left"
                                                                                                                                ItemStyle-VerticalAlign="Top">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtremarks" BorderColor="White" Text='<%# Bind("remarks") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Unit" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="300px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Right"
                                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtunit" BorderColor="White" Text='<%# Bind("unit") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Rate" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="100px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Right"
                                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtrate" BorderColor="White" Text='<%# Bind("rate") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Quantity" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="200px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Right"
                                                                                                                                ItemStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtquantity" BorderColor="White" Text='<%# Bind("quantity") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lbltotal" BorderColor="White" Text="PO Value" runat="server"></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="PO Value" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="200px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Right"
                                                                                                                                FooterStyle-HorizontalAlign="Right">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtprintpovalue" Text='<%# Bind("po_value") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                    <asp:GridView CssClass="" ID="grdamendbill" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                                        OnRowDataBound="grdamendbill_RowDataBound" ShowFooter="true">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Amendment No" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Amended Date" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="126px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Left"
                                                                                                                                ItemStyle-VerticalAlign="Middle">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtremarks" BorderColor="White" Text='<%# Bind("Amended_date") %>'
                                                                                                                                        runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Remarks" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="500px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="left"
                                                                                                                                ItemStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtunit" BorderColor="White" Text='<%# Bind("remarks") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lbltotal" BorderColor="White" Text="Total Amended PO Value" runat="server"></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="Amended PO Value" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="165px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Right"
                                                                                                                                FooterStyle-HorizontalAlign="Right">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="txtprintpovalue1" Text='<%# Bind("Amended_amount") %>' runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2" style="border: 1px solid #000" align="right">
                                                                                                                    <asp:Label ID="Label2" runat="server" Text="Total PO Value:-   "></asp:Label>
                                                                                                                    <asp:Label ID="totalpovalue" runat="server" Text=""></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table6" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2" align="left">
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label13" runat="server" CssClass="peslblfooter" Text="">For Essel Projects Pvt Ltd</asp:Label><br />
                                                                                                                    <br />
                                                                                                                    <br />
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label14" CssClass="peslblfooter" runat="server"
                                                                                                                        Text=""> Authorized Signatory</asp:Label><br />
                                                                                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                                                                                                                        Text=""></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="print" runat="server" align="center">
                                                                                            <input class="buttonprint" type="button" onclick="print();" value="Print" title="Print Report">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table id="tblclose" runat="server">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table class="style1" id="tblpodata" runat="server">
                                                                                                <tr>
                                                                                                    <th align="left" style="width: 750px; font-size: x-large;" colspan="4">
                                                                                                        Closed SPPO Report
                                                                                                    </th>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" colspan="4">
                                                                                                        <asp:Label ID="Label27" CssClass="eslbl" runat="server" Text="Vendor Name :"></asp:Label>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                        &nbsp
                                                                                                        <asp:Label ID="lblVname" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="PO NO"></asp:Label>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                        &nbsp
                                                                                                        <asp:TextBox ID="txtclsPONO" CssClass="estbox" runat="server" ToolTip="PO NO" Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="PO Date"></asp:Label>
                                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                                                                        <asp:TextBox ID="txtpodate" CssClass="estbox" runat="server" ToolTip="PO Date" Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="lblcc" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                                                                                        &nbsp; &nbsp;
                                                                                                        <asp:TextBox ID="txtClsCC" CssClass="estbox" runat="server" ToolTip="CC Code" Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="lbldca" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                                                                                                        &nbsp; &nbsp;
                                                                                                        <asp:TextBox ID="txtclsDca" CssClass="estbox" runat="server" ToolTip="DCA Code" Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="Label16" CssClass="eslbl" runat="server" Text="PO Value"></asp:Label>
                                                                                                        &nbsp;
                                                                                                        <asp:TextBox ID="txtpovalue" CssClass="estbox" runat="server" ToolTip="PO Value"
                                                                                                            Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="Label17" CssClass="eslbl" runat="server" Text="SubDca Code"></asp:Label>
                                                                                                        &nbsp;
                                                                                                        <asp:TextBox ID="txtsdca" CssClass="estbox" runat="server" ToolTip="SubDca Code"
                                                                                                            Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="Label18" CssClass="eslbl" runat="server" Text="Balance"></asp:Label>
                                                                                                        &nbsp;
                                                                                                        <asp:TextBox ID="txtbalance" CssClass="estbox" runat="server" ToolTip="Balance" Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                                                                                        <asp:Label ID="Label19" CssClass="eslbl" runat="server" Text="Closing Date"></asp:Label>
                                                                                                        &nbsp;
                                                                                                        <asp:TextBox ID="txtclsdate" CssClass="estbox" runat="server" ToolTip="Closing Date"
                                                                                                            Enabled="false"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                                                                        <asp:Label ID="Label22" CssClass="eslbl" runat="server" Text="PO Remarks" Style="color: Black;
                                                                                                            font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                                                                                        <asp:Label ID="lblremarks" CssClass="eslbl" runat="server" Text="" ToolTip="Remarks"
                                                                                                            Style="color: #000000; font-family: Tahoma; text-decoration: none;" Width="100%"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                                                                        <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                                                                                                            Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                                                                                        <asp:Label ID="lblSAdesc" CssClass="eslbl" runat="server" Text="" ToolTip="Description"
                                                                                                            Style="color: #000000; font-family: Tahoma; text-decoration: none;" Width="100%"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                                                                                        <asp:Label ID="lblSAname" CssClass="eslbl" runat="server" Text="by Sr. Accountant"
                                                                                                            Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trpm" runat="server">
                                                                                                    <td colspan="4">
                                                                                                        <table width="100%">
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                                                                                    <asp:Label ID="Label23" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                                                                                                                        Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                                                                                                    <asp:Label ID="lblPMremarks" CssClass="eslbl" runat="server" Text="" ToolTip="Description"
                                                                                                                        Style="color: #000000; font-family: Tahoma; text-decoration: none;" Width="100%"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                                                                                                    <asp:Label ID="lblPMname" CssClass="eslbl" runat="server" Text="by Project Manager"
                                                                                                                        Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trho" runat="server">
                                                                                                    <td colspan="4">
                                                                                                        <table width="100%">
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                                                                                    <asp:Label ID="Label24" CssClass="eslbl" runat="server" Text="PO Closing Remarks "
                                                                                                                        Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                                                                                                    <asp:Label ID="lblHoRemarks" CssClass="eslbl" runat="server" Text="" Style="color: Black;
                                                                                                                        font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                                                                                                    <asp:Label ID="lblHOName" CssClass="eslbl" runat="server" Text="by HO Admin" Style="color: Black;
                                                                                                                        font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trSA" runat="server">
                                                                                                    <td colspan="4">
                                                                                                        <table width="100%">
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                                                                                    <asp:Label ID="Label25" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                                                                                                                        Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="left" colspan="4">
                                                                                                                    <asp:Label ID="lblSremarks" CssClass="eslbl" runat="server" Text="" Style="color: Black;
                                                                                                                        font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                                                                                                    <asp:Label ID="Label26" CssClass="eslbl" runat="server" Text="by Super Admin" Style="color: Black;
                                                                                                                        font-family: Arial; font-weight: bold"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <table width="750px">
                                                                                                <tr align="center">
                                                                                                    <td>
                                                                                                        <input class="btnPrintcls" onclick="printclose();" type="button" value="Print" title="Print Report" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                            <td bgcolor="#FFFFFF">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
