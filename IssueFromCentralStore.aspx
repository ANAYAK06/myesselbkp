<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="IssueFromCentralStore.aspx.cs" Inherits="IssueFromCentralStore" Title="Issue From Central Store- - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript">
        function calculate() {
            grd = document.getElementById("<%=grdtransferout.ClientID %>");
            var amt = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                if (grd.rows(rowCount).cells(10).children(0).selectedIndex != 0) {
                    var dep = grd.rows(rowCount).cells(10).children(0).value;
                    if (dep != "Full Value") {
                        var Amount = parseFloat(grd.rows(rowCount).cells(9).innerText.replace(/,/g, "")) * parseFloat(dep) / 100;
                        grd.rows(rowCount).cells(11).innerText = parseFloat(grd.rows(rowCount).cells(9).innerText.replace(/,/g, "")) - parseFloat(Amount);
                        amt += Number(grd.rows(rowCount).cells[11].innerText);
                        grd.rows[grd.rows.length - 1].cells[11].innerHTML = amt;
                    }
                    else if (dep == "Full Value") {
                        grd.rows(rowCount).cells(11).innerText = grd.rows(rowCount).cells(9).innerText;
                        amt += Number(grd.rows(rowCount).cells[11].innerText);
                        grd.rows[grd.rows.length - 1].cells[11].innerHTML = amt;
                    }


                }
            }
        }

        function validate() {
            var GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            var GridView1 = document.getElementById("<%=grdissued.ClientID %>");

            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(10).children(0).selectedIndex == 0 && GridView.rows(1).cells(1).innerHTML.substring(0, 1) != "1") {
                        window.alert("Select Depreciation Percentage");
                        GridView.rows(rowCount).cells(10).children[0].focus();
                        return false;

                    }
                }
            }
            else if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Verify");
                        return false;
                    }
                }
            }
            var objs = new Array("<%=txtdate.ClientID %>", "<%=ddlDays.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }


        }
   
    </script>
    <script language="javascript">
        function print() {
            // w=window.open();
            // w.document.write('<html><body onload="window.print()">'+content+'</body></html>');
            // w.document.close();
            // setTimeout(function(){w.close();},10);
            // return false;
            var grid_obj = document.getElementById("<%=printing.ClientID %>");
            // var grid_obj = document.getElementById(grid_ID);
            if (grid_obj != null) {
                var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
                new_window.document.write(grid_obj.outerHTML);
                new_window.print();
                // new_window.close();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Issue From Central Store<a
                                    class="help" href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="90%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="search_filter_data">
                                                                    <table border="0" class="fields" width="100%">
                                                                        <tr>
                                                                            <td class="item search_filters item-filtersgroup" for="" valign="top">
                                                                                <div class="filters-group">
                                                                                    <div style="display: none;">
                                                                                        <div class="filter-a">
                                                                                            <button type="button" class="filter_with_icon active" title="">
                                                                                                <img src="images/personal+.png" width="16" height="16" alt="">
                                                                                                Active
                                                                                            </button>
                                                                                            <input style="display: none;" type="checkbox" id="filter_241" name="" class="grid-domain-selector"
                                                                                                onclick="" value="" title="">
                                                                                        </div>
                                                                                    </div>
                                                                                    <table border="0" class="fields" width="100%">
                                                                                        <tbody>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table" width="100%">
                                                                                    <tr style="height: 23px">
                                                                                        <td colspan="2" align="left">
                                                                                            <%--                                                                                <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
                                                                                            --%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 23px" valign="top">
                                                                                        <td class="item m2o_search" valign="middle" align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" class="item search_filters item-group" valign="top">
                                                                                <div class="group-expand">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="view_form_options" width="100%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div class="box-a list-a">
                                                                    <div class="inner">
                                                                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr class="pagerbar">
                                                                                    <td class="pagerbar-cell" align="right">
                                                                                        <table class="pager-table" id="tblbtn" runat="server">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="pager-cell" align="left">
                                                                                                        <h2>
                                                                                                            Issued Items:</h2>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="grid-content">
                                                                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0"
                                                                                            style="background: none;">
                                                                                            <asp:GridView ID="grdtransferout" runat="server" AutoGenerateColumns="False" CssClass="grid-content"
                                                                                                HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                DataKeyNames="id" ShowFooter="true" OnRowDataBound="grdtransferout_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                    <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                    <asp:TemplateField HeaderText="Units">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblunit" runat="server" Text='<%#Bind("units") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblqty" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Before Depreciation">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbldepamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <FooterTemplate>
                                                                                                            <asp:Label ID="Label2" runat="server" Text="0.00"></asp:Label>
                                                                                                        </FooterTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Depreciation Value">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:DropDownList ID="ddldep" CssClass="char" runat="server" onchange="calculate(this.value);">
                                                                                                                <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                                                <asp:ListItem Value="0">Full Value</asp:ListItem>
                                                                                                                <asp:ListItem>10</asp:ListItem>
                                                                                                                <asp:ListItem>20</asp:ListItem>
                                                                                                                <asp:ListItem>30</asp:ListItem>
                                                                                                                <asp:ListItem>40</asp:ListItem>
                                                                                                                <asp:ListItem>50</asp:ListItem>
                                                                                                                <asp:ListItem>60</asp:ListItem>
                                                                                                                <asp:ListItem>70</asp:ListItem>
                                                                                                                <asp:ListItem>80</asp:ListItem>
                                                                                                                <asp:ListItem>90</asp:ListItem>--%>
                                                                                                            </asp:DropDownList>
                                                                                                            <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddldep"
                                                                                                                ServicePath="cascadingDCA.asmx" Category="ddd" LoadingText="Please Wait" ServiceMethod="Depissuevalues"
                                                                                                                PromptText="--Select--">
                                                                                                            </cc1:CascadingDropDown>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="After Depreciation">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <FooterTemplate>
                                                                                                            <asp:Label ID="Label3" runat="server" Text="0.00"></asp:Label>
                                                                                                        </FooterTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                            </asp:GridView>
                                                                                            <asp:GridView ID="grdissued" BorderColor="White" Width="100%" runat="server" AutoGenerateColumns="False"
                                                                                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                DataKeyNames="id" ShowFooter="true">
                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                    <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                    <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trdesc" runat="server">
                                                                                    <td>
                                                                                        <table>
                                                                                            <td width="150px">
                                                                                                <table class="search_table" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" width="">
                                                                                                            <asp:Label ID="Label3" runat="server" Text="Transfer Date"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle">
                                                                                                            <asp:TextBox ID="txtdate" ToolTip="Date" Font-Size="Small" runat="server" Style="width: 150px;
                                                                                                                vertical-align: middle" Enabled="false"  DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox>
                                                                                                            <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                                PopupButtonID="txtdate">
                                                                                                            </cc1:CalendarExtender>--%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="150px">
                                                                                                <table class="search_table" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" width="">
                                                                                                            <asp:Label ID="Label4" runat="server" Text="Transit Date"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlDays" ToolTip="No of Transit Days" runat="server" CssClass="esddown">
                                                                                                                <asp:ListItem Value="Select No of Days">No of Days</asp:ListItem>
                                                                                                                <asp:ListItem>1</asp:ListItem>
                                                                                                                <asp:ListItem>2</asp:ListItem>
                                                                                                                <asp:ListItem>3</asp:ListItem>
                                                                                                                <asp:ListItem>4</asp:ListItem>
                                                                                                                <asp:ListItem>5</asp:ListItem>
                                                                                                                <asp:ListItem>6</asp:ListItem>
                                                                                                                <asp:ListItem>7</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="200px" align="left">
                                                                                                <table class="search_table" width="100%" align="center">
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                                                                            <asp:Label ID="lbldate" runat="server" Text="Description"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" align="left">
                                                                                                            <asp:TextBox ID="txtdesc" runat="server" CssClass="filter_item" ToolTip="Description"
                                                                                                                TextMode="MultiLine" Width="450px" MaxLength="50"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr valign="bottom" id="printing" runat="server" style="display: none">
                                                                                    <td align="center">
                                                                                        <table width="100%" id="tblprinting" class="pestbl" style="border: 1px solid #000;"
                                                                                            runat="server">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table id="Table2" width="100%" class="pestbl" style="border: 1px solid #000;" runat="server">
                                                                                                        <tr style="border: 1px solid  #000;">
                                                                                                            <td align="center" colspan="2">
                                                                                                                <asp:Label ID="Label11" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                    Text="Essel Projects Pvt Ltd."></asp:Label>
                                                                                                                <br />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="border: 1px solid  #000;">
                                                                                                            <td align="center" colspan="2">
                                                                                                                <asp:Label ID="Label122" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                    Text="Issue Slip"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="border: 1px solid  #000;">
                                                                                                            <td align="left" colspan="1" style="border: 1px solid #000; width: 50%">
                                                                                                                <asp:Label ID="Label134" runat="server" CssClass="peslbl" Font-Bold="false" Text="Issue Slip No:"></asp:Label>
                                                                                                                <asp:Label ID="lblchallanno" runat="server" CssClass="peslbl" Font-Bold="false" Text=""></asp:Label>
                                                                                                            </td>
                                                                                                            <td align="left" colspan="1" style="border: 1px solid #000; width: 50%">
                                                                                                                <asp:Label ID="Label14" runat="server" CssClass="peslbl" Font-Bold="false" Text="Date:"></asp:Label>
                                                                                                                <asp:Label ID="Label1" runat="server" CssClass="peslbl" Font-Bold="false" Text=""></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="border: 1px solid #000; height: 100px">
                                                                                                            <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                <asp:Label ID="lblname" runat="server" Text="Despatched From:" Width="100%" CssClass="pestbox"
                                                                                                                    Font-Underline="true"></asp:Label>
                                                                                                                <asp:Label ID="lbldespfrom" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                <asp:Label ID="lbldespfromadd" runat="server" Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)."
                                                                                                                    Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                            </td>
                                                                                                            <td align="left" width="100%" style="border: 1px solid #000" valign="top">
                                                                                                                <asp:Label ID="Label5" runat="server" Text="Despatched To:" Width="100%" CssClass="pestbox"
                                                                                                                    Font-Underline="true"></asp:Label>
                                                                                                                <asp:Label ID="lbldespto" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                <asp:Label ID="lbldesptoadd" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                <asp:Label ID="Label88" CssClass="peslbl1" runat="server" Text="">The under mentioned old and used erection items / tools / equipments are being dispatched through </asp:Label>
                                                                                                                <asp:Label ID="Label6" CssClass="peslbl1" runat="server" Text="">Mr.</asp:Label>
                                                                                                                <asp:TextBox ID="txtname" Width="39%" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                    runat="server"></asp:TextBox>
                                                                                                                <asp:Label ID="Label10" CssClass="peslbl1" runat="server" Text="">by vehicle/ truck No. </asp:Label>
                                                                                                                <asp:TextBox ID="txttruckno" Width="39%" CssClass="pestbox" Style="border: None;
                                                                                                                    border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                <asp:Label ID="Label12" CssClass="peslbl1" runat="server" Text="">to our project site on stock transfer basis.  These items / tools / equipments are for </asp:Label>
                                                                                                                <asp:Label ID="Label7" CssClass="peslbl1" runat="server" Font-Underline="true" Text="">OUR OWN USE ON RETURNABLE BASIS AND NOT FOR SALE.</asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="border: 1px solid #000">
                                                                                                            <td colspan="2" style="border: 1px solid #000">
                                                                                                                <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                            ItemStyle-Width="50px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <%#Container.DataItemIndex+1 %>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderText="Item Name" />
                                                                                                                        <asp:BoundField DataField="specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                            ItemStyle-Width="200px" HeaderText="Specification" />
                                                                                                                        <asp:BoundField DataField="units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                            ItemStyle-Width="50px" HeaderText="Units" />
                                                                                                                        <asp:BoundField DataField="quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                            ItemStyle-Width="50px" HeaderText="Quantity" />
                                                                                                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Left">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtremarks" TextMode="MultiLine" CssClass="peslbl1" runat="server"
                                                                                                                                    Width="150px" Text="" Style="border: None" MaxLength="25"></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="border: 1px solid #000; height: 100px">
                                                                                                            <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                <asp:Label ID="Label8" Font-Bold="true" runat="server" Text="Despatched by:" Width="100%"
                                                                                                                    CssClass="pestbox"></asp:Label>
                                                                                                                <asp:Label ID="Label9" runat="server" Text="" Width="100%" Height="90px" CssClass="pestbox"></asp:Label>
                                                                                                                <asp:Label ID="Label16" runat="server" Text="" Width="100%" CssClass="pestbox">(Name & Signature)</asp:Label>
                                                                                                                &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                                                                                                                    Text=""></asp:Label>)
                                                                                                            </td>
                                                                                                            <td align="left" width="100%" style="border: 1px solid #000" valign="top">
                                                                                                                <asp:Label ID="Label111" Font-Bold="true" runat="server" Text="Recieved By:" Width="100%"
                                                                                                                    CssClass="pestbox"></asp:Label>
                                                                                                                <asp:Label ID="Label15" runat="server" Text="" Width="100%" Height="90px" CssClass="pestbox"></asp:Label>
                                                                                                                <asp:Label ID="Label17" runat="server" Text="" Width="100%" CssClass="pestbox">(Name & Signature)</asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="pagerbar">
                                                                                    <td class="pager-cell-button" align="center">
                                                                                        <asp:Button ID="btnconfirm" Width="120px" Height="20px" runat="server" Text="Confirm"
                                                                                            CssClass="button" OnClick="btnconfirm_Click" OnClientClick="javascript:return validate()" />
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
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
