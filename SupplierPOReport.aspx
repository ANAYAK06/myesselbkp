<%@ Page Title="Supplier PO Report" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="SupplierPOReport.aspx.cs" Inherits="SupplierPOReport" %>

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
        function print() {
            //debugger;          
            var Type = document.getElementById("<%=hfpotype.ClientID %>").value;
            if (Type == "DO") {
                var grid_obj = document.getElementById("<%=tbldo.ClientID %>");
            }
            else {
                var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            }
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            $find('mdlindent').hide();

        }       
    </script>
    <script language="javascript" type="text/javascript">

        function validate() {
            //debugger;
            var costcenter = document.getElementById("<%=ddlcccode.ClientID %>").value;
            var dca = document.getElementById("<%=ddldca.ClientID %>").value;
            var vendorid = document.getElementById("<%=ddlvendor.ClientID %>").value;
            var month = document.getElementById("<%=ddlmonth.ClientID %>").value;
            var year = document.getElementById("<%=ddlyear.ClientID %>").value;
            var DdlPoType = document.getElementById("<%=Ddltype.ClientID %>").value;
            if (costcenter == "Select") {
                window.alert("Select Cost Center");

                return false;
            }
            else if (dca == "Select DCA") {
                window.alert("Select DCA Code");

                return false;
            }
            else if (vendorid == "Select vendor" || vendorid == "") {
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
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="750px">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="750px" style="border: 1px solid #000" class="estbl">
                                        <tr>
                                            <th align="center" colspan="4">
                                                SUPPLIER PO/DO REPORT
                                            </th>
                                        </tr>
                                        <tr style="border: none">
                                            <td align="center" colspan="2" style="width: 370">
                                                <asp:Label ID="lblcccode" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                                <asp:DropDownList ID="ddlcccode" runat="server" CssClass="esddown" AutoPostBack="true"
                                                    ToolTip="CC Code" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="2" style="width: 370">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                                                <asp:DropDownList ID="ddldca" runat="server" Width="99px" CssClass="esddown" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddldca_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="border: none" id="trdca" runat="server">
                                            <td align="center" colspan="2" style="width: 370">
                                                <asp:Label ID="lblvendor" CssClass="eslbl" runat="server" Text="Vendor ID"></asp:Label>
                                                <asp:DropDownList ID="ddlvendor" Width="250px" CssClass="esddown" ToolTip="Select Vendor"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="2" style="width: 370">
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="SPPO Status: "></asp:Label>&nbsp&nbsp&nbsp&nbsp
                                                <asp:DropDownList ID="Ddltype" Width="200px" CssClass="esddown" ToolTip="Select Vendor"
                                                    runat="server" AutoPostBack="true">
                                                    <%--OnSelectedIndexChanged="Ddltype_SelectedIndexChanged"--%>
                                                    <asp:ListItem Value="Select POType">Select Type</asp:ListItem>
                                                    <asp:ListItem Value="1">Select All</asp:ListItem>
                                                    <asp:ListItem Value="2">Running PO</asp:ListItem>
                                                    <asp:ListItem Value="3">Closed PO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="border: none" id="trddlpo" runat="server">
                                            <td align="center" colspan="2" style="width: 370">
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
                                            <td align="center" colspan="2" style="width: 370">
                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                <asp:DropDownList ID="ddlyear" runat="server" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" style="width: 740">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="esbtn" Text="View" OnClick="btnSearch_Click"
                                                    OnClientClick="javascript:return validate();" /><%-- --%>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr class="grid-content">
                                            <td colspan="3" style="width: 750px">
                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                        AlternatingRowStyle-CssClass="alt" GridLines="None" EmptyDataText="No Data For The Above Selection Criteria"
                                                        Font-Size="Small" AllowPaging="false" DataKeyNames="po_no,id,type,indent_no,Approved_Users"
                                                        ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                        <%----%>
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                            <asp:BoundField DataField="po_no" ItemStyle-Width="80px" HeaderText="PONO" />
                                                            <asp:BoundField DataField="po_date" ItemStyle-Width="80px" HeaderText="Date" />
                                                            <asp:BoundField DataField="vendor_name" ItemStyle-Width="80px" HeaderText="Vendor Name" />
                                                            <asp:BoundField DataField="amount" ItemStyle-Width="80px" DataFormatString="{0:#,##,##,###.00}"
                                                                HtmlEncode="False" HeaderText="Amount" />
                                                            <asp:BoundField DataField="indent_no" ItemStyle-Width="80px" HeaderText="Indent No" />
                                                            <asp:BoundField DataField="cc_code" ItemStyle-Width="80px" HeaderText="CC Code" />
                                                            <asp:BoundField DataField="dca_code" ItemStyle-Width="80px" HeaderText="DCA Code" />
                                                            <asp:BoundField DataField="type" ItemStyle-Width="80px" HeaderText="Type" />
                                                            <asp:BoundField DataField="Status" ItemStyle-Width="80px" HeaderText="Status" />
                                                            <asp:BoundField DataField="Status" ItemStyle-Width="80px" HeaderText="Po Approval Stage" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </table>
                                                <asp:HiddenField ID="hfpotype" runat="server" />
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
                                                                    height: 580px;">
                                                                    <table style="vertical-align: middle;" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <table id="tblrun" runat="server">
                                                                                    <tr valign="bottom" id="tb12" runat="server">
                                                                                        <td align="center">
                                                                                            <table id="tbldo" runat="server" width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table id="Table2" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid  #000;">
                                                                                                                <td align="center" colspan="2">
                                                                                                                    <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/essellogo1.jpg" Height="40px"
                                                                                                                        Width="89px" />
                                                                                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                        Text="ESSEL PROJECTS PVT LTD." Font-Size="XX-Large" Font-Names="Rockwell"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label13" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                        Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label15" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                        Text="Tel/Fax:0771-4268469/4075401." Font-Size="Smaller"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="lblpo" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                        Text="DELIVERY ORDER"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table3" width="100%" runat="server" class="pestbl" style="">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td width="50%" align="left" style="border: 1px solid #000">
                                                                                                                    <asp:Label ID="lblname" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="lbladdress" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left" width="100%" style="border: 1px solid #000">
                                                                                                                    <asp:Label ID="Label4" Width="25%" CssClass="peslbl1" runat="server" Text="DO No:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtpono" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="DO NO"
                                                                                                                        Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                                    <asp:Label ID="Label5" Width="25%" CssClass="peslbl1" runat="server" Text="DO Date:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtpodate" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="DO Date"
                                                                                                                        Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label6" Width="25%" CssClass="peslbl1" runat="server" Text="Ref No:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtrefno" Width="60%" CssClass="pestbox" ToolTip="Ref No" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                                    <asp:Label ID="Label7" Width="25%" CssClass="peslbl1" runat="server" Text="Ref Date:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtrefdate" Width="60%" CssClass="pestbox" ToolTip="Ref Date" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                    <br />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table4" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2">
                                                                                                                    <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                            <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                            <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="50px" HeaderText="Units" />
                                                                                                                            <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblremarks" runat="server" Text="Remarks" CssClass="peslbl1"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtremarks" MaxLength="150" ToolTip="Remarks" CssClass="peslbl1"
                                                                                                                        runat="server" Width="600px" Text="" Style="border: None; border-bottom: 1px solid #000"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table5" width="100%" runat="server" class="pestbl" style="border-collapse: collapse">
                                                                                                            <tr style="border-collapse: collapse">
                                                                                                                <td colspan="2">
                                                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label8" CssClass="peslbl1" runat="server" Text="">You are requested to supply/deliver above item/s to our work site / Central store  at</asp:Label>
                                                                                                                    <asp:TextBox ID="txtrecievedcc" Width="25%" Enabled="false" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                    <asp:Label ID="Label10" CssClass="peslbl1" runat="server" Text="">by</asp:Label>
                                                                                                                    <asp:TextBox ID="txtrecieveddate" Width="25%" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                    <asp:Label ID="Label12" CssClass="peslbl1" Enabled="false" runat="server" Text="">on credit basis which should at par with the specifications, make etc. Further it is cleared that if   the item(s) supplied by you is/are found inferior/defective, the same will be return to you or deduct the amount of such items from your invoice without any notice.</asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label14" CssClass="peslbl1" runat="server" Enabled="false" Text=""> Validity of this DO :-&nbsp&nbsp This delivery order valid only if the above material delivered at below mentioned address/ location  on or before above specified date.</asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table6" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label9" runat="server" CssClass="peslblfooter" Width="170px" Text="">For Essel Projects Pvt Ltd</asp:Label>
                                                                                                                    &nbsp;&nbsp;
                                                                                                                    <asp:Label ID="lblinv" runat="server" CssClass="peslblfooter" Width="223px" Text="INVOICE ADDRESS :"
                                                                                                                        Font-Underline="True"></asp:Label>
                                                                                                                    <asp:Label ID="Label16" runat="server" CssClass="peslblfooter" Text="" Font-Underline="True">DELIVERY SITE ADDRESS:</asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label18" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvoiceAdd" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblSaddress" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label19" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvoiceAdd2" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblSaddress2" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label21" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvgst" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblCperson" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label23" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvMobileNum" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblMobileNum" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label11" CssClass="peslblfooter" runat="server" Text=""> Authorized Signatory</asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                                                                                                                        Text=""></asp:Label>)
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left" colspan="3">
                                                                                                                    <asp:Label ID="Label30" runat="server" CssClass="peslblfooter" Text="" Font-Size="XX-Small">*    It is an electronically generated DO
                                                                                                                    </asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <table id="tblpo" runat="server" width="100%" class="pestbl" style="border: 1px solid #000;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table id="Table7" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid  #000;">
                                                                                                                <td align="center" colspan="2">
                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/essellogo1.jpg" Height="40px"
                                                                                                                        Width="89px" />
                                                                                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                                                                    <asp:Label ID="Label20" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                        Text="ESSEL PROJECTS PVT LTD." Font-Size="XX-Large" Font-Names="Rockwell"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label22" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                        Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label24" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                        Text="Tel/Fax:0771-4268469/4075401." Font-Size="Smaller"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label25" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                        Text="PURCHASE ORDER"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table3po" width="100%" runat="server" class="pestbl" style="">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td width="50%" align="left" style="border: 1px solid #000">
                                                                                                                    <%--<asp:TextBox ID="txtaddress" runat="server" Width="100%" TextMode="MultiLine" Style="border: None;"></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="lblnamepo" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="lbladdresspo" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left" width="100%" style="border: 1px solid #000">
                                                                                                                    <asp:Label ID="Label4po" Width="25%" CssClass="peslbl1" runat="server" Text="PO No:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtponopo" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="PO NO"
                                                                                                                        Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox><br />
                                                                                                                    <asp:Label ID="Label5po" Width="25%" CssClass="peslbl1" runat="server" Text="PO Date:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtpodatepo" Width="60%" Enabled="false" CssClass="pestbox" ToolTip="PO Date"
                                                                                                                        Style="border: None; border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label6po" Width="25%" CssClass="peslbl1" runat="server" Text="Ref No:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtrefnopo" Width="60%" CssClass="pestbox" ToolTip="Ref No" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server" Enabled="false"></asp:TextBox><br />
                                                                                                                    <asp:Label ID="Label7po" Width="25%" CssClass="peslbl1" runat="server" Text="Ref Date:-"></asp:Label>
                                                                                                                    <asp:TextBox ID="txtrefdatepo" Width="60%" CssClass="pestbox" ToolTip="Ref Date"
                                                                                                                        Style="border: None; border-bottom: 1px solid #000" runat="server" Enabled="false"></asp:TextBox>
                                                                                                                    <br />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table4po" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2">
                                                                                                                    <div id="divGrid" style="width: auto; float: left;">
                                                                                                                        <asp:GridView CssClass="" ID="grdbillpoprint" Width="750px" runat="server" AutoGenerateColumns="false"
                                                                                                                            ShowFooter="true" OnRowDataBound="grdbillpoprint_RowDataBound">
                                                                                                                            <Columns>
                                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                    ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderText="Item Code"
                                                                                                                                    HeaderStyle-BackColor="White" />
                                                                                                                                <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                                <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                    ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                                <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                    ItemStyle-Width="50px" HeaderText="Units" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                    ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:BoundField DataField="New_basicprice" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                    HeaderText="Purchase Price" />
                                                                                                                                <asp:BoundField DataField="Amt" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                    ItemStyle-Width="50px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                                                                                            </Columns>
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="2">
                                                                                                                    <asp:GridView ID="grdtermsprint" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                        AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                        Width="100%" GridLines="None">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:BoundField DataField="splitdata" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" />
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table id="Table6po" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                                                                            <tr style="border: 1px solid #000">
                                                                                                                <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label9po" runat="server" CssClass="peslblfooter" Width="170px" Text="">For Essel Projects Pvt Ltd</asp:Label>
                                                                                                                    &nbsp;&nbsp;
                                                                                                                    <asp:Label ID="lblinvpo" runat="server" CssClass="peslblfooter" Width="223px" Text="INVOICE ADDRESS :"
                                                                                                                        Font-Underline="True"></asp:Label>
                                                                                                                    <asp:Label ID="Label16po" runat="server" CssClass="peslblfooter" Text="" Font-Underline="True">DELIVERY SITE ADDRESS:</asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label18po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvoiceAddpo" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblSaddresspo" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label19po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvoiceAdd2po" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblSaddress2po" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label21po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvgstpo" CssClass="peslblfooter" runat="server" Text="" Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblCpersonpo" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                                                    <asp:Label ID="Label23po" CssClass="peslblfooter" runat="server" Text="" Width="180px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblinvMobileNumpo" CssClass="peslblfooter" runat="server" Text=""
                                                                                                                        Width="200px"> </asp:Label>
                                                                                                                    <asp:Label ID="lblMobileNumpo" CssClass="peslblfooter" runat="server" Text="" Width="250px"> </asp:Label>
                                                                                                                    <br />
                                                                                                                    <asp:Label ID="Label11po" CssClass="peslblfooter" runat="server" Text=""> Authorized Signatory</asp:Label>
                                                                                                                    <br />
                                                                                                                    &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagernamepo" runat="server" Style="vertical-align: middle"
                                                                                                                        Text=""></asp:Label>)
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left" colspan="3">
                                                                                                                    <asp:Label ID="Label30po" runat="server" CssClass="peslblfooter" Text="" Font-Size="XX-Small">*    It is an electronically generated DO
                                                                                                                    </asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Trgvusers" runat="server" style="border: 1px #000000 solid;">
                                                                                        <td>
                                                                                            <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                DataKeyNames="" GridLines="none" Width="740px" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                                                                                <HeaderStyle CssClass="headerstyle" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Status" ItemStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                                                                </Columns>
                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="print" runat="server" align="center">
                                                                                            <input class="buttonprint" type="button" onclick="print();" value="Print" title="Print Report">
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
