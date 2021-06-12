<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="PendingApprovels.aspx.cs" Inherits="PendingApprovels" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table runat="server" id="tblindent" width="100%">
                    <tr>
                        <td align="left">
                            <fieldset class="box">
                                <legend style="padding: 4px;">
                                    <asp:Label ID="lblindents" runat="server" Style="border: 1px solid #000" CssClass="char"
                                        Font-Bold="True" Font-Size="Small" Text="PENDING INDENTS:"></asp:Label>
                                </legend>
                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="Gridindent" Width="100%" runat="server" AutoGenerateColumns="false"
                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                DataKeyNames="Indent No" EmptyDataText="There Is No Records" OnRowEditing="Gridindent_RowEditing">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                        EditImageUrl="~/images/iconset-b-edit.gif" />
                                                    <asp:BoundField DataField="id" Visible="false" />
                                                    <asp:TemplateField HeaderText="Indent No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-VerticalAlign="Bottom">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lablindentno" runat="server" Text='<%#Eval("Indent No") %>' />
                                                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CC Code" HeaderText="CC Code" />
                                                    <asp:BoundField DataField="Indent Date" HeaderText="Indent Date" />
                                                    <asp:BoundField DataField="Indent Cost" HeaderText="Indent Cost" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                </Columns>
                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblpo" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="lblpos" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING PO'S:"></asp:Label>
                                    </legend>
                                    <table id="Table1" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="GridPO" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="Po_no" EmptyDataText="There Is No Records" OnRowEditing="GridPO_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="PO No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("Po_no") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CC_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="Indent_no" HeaderText="Indent No" />
                                                        <asp:BoundField DataField="Po_date" HeaderText="PO DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblmrr" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="Label2" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING MRR :"></asp:Label>
                                    </legend>
                                    <table id="Table7" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="GridMrr" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="mrr_no" EmptyDataText="There Is No Records" OnRowEditing="GridMrr_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="MRR No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("mrr_no") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="po_no" HeaderText="PO NO" />
                                                        <asp:BoundField DataField="recieved_date" HeaderText="PO DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblinv" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="Label3" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING Supplier Invoices :"></asp:Label>
                                    </legend>
                                    <table id="Table8" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="GridInvoice" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="invoiceno" EmptyDataText="There Is No Records" OnRowEditing="GridInvoice_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("invoiceno") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CC_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="total" HeaderText="Total" />
                                                        <asp:BoundField DataField="invoice_date" HeaderText="PO DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tbltransfer" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="lblmaterials" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING TRANSFER OF MATERIALS:"></asp:Label>
                                    </legend>
                                    <table id="Table2" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="GridTransfer" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="ref_no" EmptyDataText="There Is No Records" OnRowEditing="GridTransfer_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="Reference No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("ref_no") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Transfer Out" HeaderText="Transfer Out" />
                                                        <asp:BoundField DataField="Transfer In" HeaderText="Transfer In" />
                                                        <asp:BoundField DataField="transfer_date" HeaderText="DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblissue" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="Label1" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING ISSUE OF MATERIALS:"></asp:Label>
                                    </legend>
                                    <table id="Table3" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="Gridissue" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="ref_no" EmptyDataText="There Is No Records" OnRowEditing="Gridissue_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="Reference No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("ref_no") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Transfer Out" HeaderText="Transfer Out" />
                                                        <asp:BoundField DataField="Transfer In" HeaderText="Transfer In" />
                                                        <asp:BoundField DataField="transfer_date" HeaderText="DATE" ItemStyle-Width="50px" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblsppo" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="lblsppo" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING SPPO's:"></asp:Label>
                                    </legend>
                                    <table id="Table4" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="Gridsppo" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="pono" EmptyDataText="There Is No Records" OnRowEditing="Gridsppo_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="PO No" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("pono") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="po_value" HeaderText="PO COST" />
                                                        <asp:BoundField DataField="po_date" HeaderText="SPPO DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" ItemStyle-Width="300px" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblAsppo" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="lblASppo" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING AMEND SPPO's:"></asp:Label>
                                    </legend>
                                    <table id="Table10" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="GridASppo" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="pono" EmptyDataText="There Is No Records" OnRowEditing="GridASppo_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="PO No" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("pono") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="po_value" HeaderText="AMEND VALUE" />
                                                        <asp:BoundField DataField="po_date" HeaderText="AMEND DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" ItemStyle-Width="300px" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="TblCSPPO" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="LblCSppo" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING Close SPPO's:"></asp:Label>
                                    </legend>
                                    <table id="Table11" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="GridCSPPO" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="pono" EmptyDataText="There Is No Records" OnRowEditing="GridCSPPO_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="PO No" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("pono") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="po_value" HeaderText="PO COST" />
                                                        <asp:BoundField DataField="po_date" HeaderText="SPPO DATE" />
                                                        <asp:BoundField DataField="Remarks" HeaderText="Description" ItemStyle-Width="300px" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblclientpo" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="Label4" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING Client PO's:"></asp:Label>
                                    </legend>
                                    <table id="Table12" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="grdiclientpo" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="po_no" EmptyDataText="There Is No Records" 
                                                    onrowediting="grdiclientpo_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="PO No" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablpono" runat="server" Text='<%#Eval("po_no") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="po_value" HeaderText="PO COST" />
                                                        <asp:BoundField DataField="po_date" HeaderText="SPPO DATE" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblgeninv" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="lblinvoice" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDINGS STOCKUPDATION:"></asp:Label>
                                    </legend>
                                    <table id="Table5" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridStock" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="request_no" EmptyDataText="There Is No Records" OnRowEditing="GridStock_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="Invoiceno No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablinv" runat="server" Text='<%#Eval("request_no") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                        <asp:BoundField DataField="date" HeaderText="DATE" />
                                                        <asp:BoundField DataField="description" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblitem" width="100%">
                    <tr>
                        <td align="left">
                            <div>
                                <fieldset class="box">
                                    <legend style="padding: 4px;">
                                        <asp:Label ID="lblcodeapprove" runat="server" Style="border: 1px solid #000" CssClass="char"
                                            Font-Bold="true" Font-Size="Small" Text="PENDING ITEM CODE APPROVEL'S:"></asp:Label>
                                    </legend>
                                    <table id="Table6" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="Griditems" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="id" EmptyDataText="There Is No Records" OnRowEditing="Griditems_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Bottom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lablinv" runat="server" Text='<%#Eval("item_code") %>' />
                                                                <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                        <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                        <asp:BoundField DataField="date" HeaderText="DATE" />
                                                        <asp:BoundField DataField="specification" HeaderText="Specification" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
