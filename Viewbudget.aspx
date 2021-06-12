<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Viewbudget.aspx.cs"
    Inherits="Viewbudget" Title="View Budget" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .popup-div-background
        {
            position: absolute;
            top: 0;
            left: 0;
            background-color: #ccc;
            filter: alpha(opacity=90);
            opacity: 0.9; /* the following two line will make sure
             /* that the whole screen is covered by
                 /* this transparent layer */
            height: 100%;
            width: 100%;
            min-height: 100%;
            min-width: 100%;
        }
    </style>
    <script type="text/javascript">
        function validate() {
            var type = document.getElementById("<%=ddlcctype.ClientID %>").value;
            var ccode = document.getElementById("<%=ddlCCcode.ClientID %>").value;
            var role = document.getElementById("<%=hf1.ClientID %>").value;

            if (role == "Project Manager") {
                if (ccode == "Select Cost Center") {
                    window.alert("Select Cost Center");
                    document.getElementById("<%=ddlCCcode.ClientID %>").focus();
                    return false;
                }
            }

            else {

                if (type == "Select") {
                    window.alert("Select Cost Center Type");
                    document.getElementById("<%=ddlcctype.ClientID %>").focus();
                    return false;
                }
                if (type == "Performing") {
                    var ddltype = document.getElementById("<%=ddltype.ClientID %>").value;
                    if (ddltype == "Select") {
                        window.alert("Select Sub Type");
                        document.getElementById("<%=ddltype.ClientID %>").focus();
                        return false;
                    }
                }
                if (ccode == "Select Cost Center") {
                    window.alert("Select Cost Center");
                    document.getElementById("<%=ddlCCcode.ClientID %>").focus();
                    return false;
                }

                if (type != "Performing") {
                    var year = document.getElementById("<%=ddlyear.ClientID %>").value;
                    if (year == "Select Year") {
                        window.alert("Select Year");
                        document.getElementById("<%=ddlyear.ClientID %>").focus();
                        return false;
                    }
                }

            }

        }

     
    </script>
    <script language="javascript" type="text/javascript">

        function print() {

            var grid_obj = document.getElementById("<%=print.ClientID %>");

            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();

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
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table id="_terp_list_grid" cellpadding="0" cellspacing="0" class="grid" width="100%">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Cost Center Budget"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="estbl" width="400px">
                                                    <tr>
                                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </tr>
                                                    <tr id="type" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlcctype" runat="server" ToolTip="Cost Center" Width="200px"
                                                                CssClass="esddown" AutoPostBack="true" onchange="javascript:SetContextKey('dp2',this.value);"
                                                                OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Performing</asp:ListItem>
                                                                <asp:ListItem>Non-Performing</asp:ListItem>
                                                                <asp:ListItem>Capital</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hf1" runat="server" />
                                                            <%-- --%>
                                                        </td>
                                                    </tr>
                                                    <tr id="trtype" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label14" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="200px" CssClass="esddown"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Service</asp:ListItem>
                                                                <asp:ListItem>Trading</asp:ListItem>
                                                                <asp:ListItem>Manufacturing</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="pcc" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" onchange="SetDynamicKey('dp1',this.value);"
                                                                Width="200px" CssClass="esddown">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="dca" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddldetailhead" CssClass="esddown" runat="server" ToolTip="DCA Code"
                                                                Width="200px">
                                                            </asp:DropDownList>
                                                            <span class="starSpan">*</span>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                                                ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="month" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown" Width="200px">
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
                                                    </tr>
                                                    <tr id="year" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="200px" runat="server" ToolTip="Year">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View" OnClick="btnAssign_Click" OnClientClick="return  validate()" />
                                                <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                                    Text="Reset" OnClick="btnCancel1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="print" runat="server">
                                <td>
                                    <table id="tblgrid" runat="server">
                                        <tr>
                                            <td>
                                                <table id="header" runat="server" width="100%" class="accordionHeader">
                                                    <tr>
                                                        <td width="400px">
                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="400px">
                                                            <asp:Label ID="Label9" runat="server" Text="Budget Assigned"></asp:Label>
                                                        </td>
                                                        <td width="200px">
                                                            <asp:Label ID="Label10" runat="server" Text="Balance"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="400px">
                                                            <asp:Label ID="Label5" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="400px">
                                                            <asp:Label ID="Label6"  runat="server"></asp:Label>
                                                        </td>
                                                        <td width="200px">
                                                            <asp:Label ID="Label7" runat="server" Text='<%#Bind("balance") %>' BackColor='<%# Convert.ToDecimal(Eval("balance"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="gridprint" runat="server" align="center">
                                            <td style="border: thin solid #000000">
                                                <asp:GridView ID="GridView1" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="true" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="100%" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                        LastPageText="Last" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr id="Trnet" runat="server">
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="esddown" Text="Total Net DCA budget consumption up to date:- "></asp:Label>
                                                <asp:Label ID="Label13" runat="server" Text="" CssClass="eslblalert"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr id="trgridheader" runat="server">
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label15" Font-Bold="true" Style="border-right: 1px solid black" ForeColor="White"
                                                                Width="550px" BackColor="#0066ff" runat="server" CssClass="esddown" Text="INVOICE AND RECEIPT STATUS OF CC"></asp:Label>
                                                            <asp:Label ID="Label16" Font-Bold="true" BackColor="#0066ff" Style="border-left: 2px solid #0066ff"
                                                                ForeColor="White" Width="150px" runat="server" CssClass="esddown" Text="STATUS"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" runat="server" align="center">
                                                        <td style="border: thin solid #000000">
                                                            <asp:GridView ID="GridView2" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                Width="100%" ShowFooter="false" OnRowDataBound="GridView2_RowDataBound" ShowHeader="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="col1" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" />
                                                                    <asp:BoundField DataField="total" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right"
                                                                        ItemStyle-Width="150px" />
                                                                    <asp:BoundField DataField="" ItemStyle-Width="150px" DataFormatString="{0:0.00}"
                                                                        ItemStyle-HorizontalAlign="Right" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="height: 6px">
                                                        </td>
                                                    </tr>
                                                    <tr id="trgridpercentage" runat="server">
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label17" Font-Bold="true" Style="border-right: 1px solid black" ForeColor="White"
                                                                Width="706px" BackColor="#0066ff" runat="server" CssClass="esddown" Text="PERCENTAGE OF  INVOICE AND BUDGET CONSUMPTION"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: thin solid #000000">
                                                            <asp:GridView ID="GridView3" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                Width="100%" ShowFooter="false" ShowHeader="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="desc" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px" />
                                                                    <asp:BoundField DataField="ProjectbudgetCunsumption" ItemStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="150px" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="style2" width="400px" height="30px" id="btnprint" runat="server">
                                <td align="left">
                                    <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                                    <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                        OnClick="btnExcel_Click" />
                                </td>
                                <td align="center">
                                    <input class="buttonSubmit" onclick="print();" type="button" value="Print" title="Print Report">
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
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function display() {


            if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex == 1) {
                document.getElementById("<%=ddlyear.ClientID %>").style.display = 'none';
                document.getElementById("<%=lblyear.ClientID %>").style.display = 'none';
            }
            else if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex == 2 || document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex == 3) {

                document.getElementById("<%=ddlyear.ClientID %>").style.display = 'block';
                document.getElementById("<%=lblyear.ClientID %>").style.display = 'block';
            }
            else {
                document.getElementById("<%=ddlyear.ClientID %>").style.display = 'none';
                document.getElementById("<%=lblyear.ClientID %>").style.display = 'none';

            }

        }
       
    </script>
</asp:Content>
