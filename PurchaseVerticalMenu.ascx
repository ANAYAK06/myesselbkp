<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseVerticalMenu.ascx.cs"
    Inherits="PurchaseVerticalMenu" %>
<link rel="stylesheet" type="text/css" href="Css/style.css" />
<link rel="stylesheet" type="text/css" href="Css/menu.css" />
<link href="Css/screen.css" rel="stylesheet" type="text/css" />
<link href="Css/V-Menu.css" rel="stylesheet" type="text/css" />
<script src="Java_Script/bubble-tooltip.js" type="text/javascript"></script>
<script src="Java_Script/newcalendar.js" type="text/javascript"></script>
<script src="Java_Script/MochiKit.js" type="text/javascript"></script>
<script src="Java_Script/validations.js" type="text/javascript"></script>
<script src="Java_Script/JScript.js" type="text/javascript"></script>
<script src="Java_Script/calendar.js" type="text/javascript"></script>
<script src="Java_Script/calendar-setup.js" type="text/javascript"></script>
<script src="Java_Script/accordion.js" type="text/javascript"></script>
<%--<link href="App_Themes/Theme1/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="Java_Script/jquery-1.3.2.min.js"></script>

<script type="text/javascript" src="Java_Script/jquery-ui-1.7.0.min.js"></script>

<script type="text/javascript" src="Java_Script/jquery.bgiframe-2.1.1.pack.js"></script>
--%>
<table>
    <tr>
        <td>
            <table>
                <%--<tr>
                        <td id="main_nav" colspan="2" valign="top">
                            <div id="applications_menu">
                                <div class="right scroller">
                                </div>
                                <div class="left scroller">
                                </div>
                                <ul>
                                    <li><a href="" target="_top" class=""><span>Purchases</span>
                                    </a></li>
                                    <li><a href="" target="_top" class=""><span>Warehouse</span>
                                    </a></li>
                                    <li><a href="" target="_top" class=""><span>Project</span> </a>
                                    </li>
                                    <li><a href="" target="_top" class="active"><span>Accounts</span>
                                    </a></li>
                                    <li><a href="" target="_top" class=""><span>Human Resources</span>
                                    </a></li>
                                    <li><a href="" target="_top" class=""><span>Tools</span> </a>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>--%>
                <tr>
                    <td id="secondary" class="sidenav-a" style="background-color: #333333; height: 500px;
                        width: 150px" align="left">
                        <div class="wrap">
                            <ul id="sidenav-a" class="accordion-content">
                                <li class="accordion-title"><span>Indents</span> </li>
                                <li class="accordion-content" id="content_168" style="display: none;">
                                    <table id="tree_168" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Raise_Indent" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="RaiseIndent.aspx">Raise Indent</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="View_Indent" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="Indent.aspx">View Indent</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Purchase Management</span> </li>
                                <li class="accordion-content" id="Li1" style="display: none;">
                                    <table id="Table1" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Raise_Po" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="RaisePO.aspx">Raise Supplier Po</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trsppo" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="serviceproviderpo.aspx">Service Provider PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifysppo" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="verifysppo.aspx">Verify SP PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trclosepo" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ClosePO.aspx">Service Provider Close PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trspreport" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ServiceproviderReportNew.aspx">Service Provider PO Report</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                              <tr class="row" id="trsupplierporeport" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="SupplierPOReport.aspx">Supplier PO/DO Report</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="tramenedsppo" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AmendSPPO.aspx">Amend Service Provider PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="View_Po" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VendorPO.aspx">View PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Vendor Info</span> </li>
                                <li class="accordion-content" id="content_169" style="display: none;">
                                    <table id="tree_169" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Add_Vendor" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddVendor.aspx">Add/Update Vendor</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifyvendor" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyVendor.aspx">Verify Vendor</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="View_Vendor" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmviewvendor.aspx">View Vendor</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Receieve Items</span> </li>
                                <li class="accordion-content" id="content_170" style="display: none;">
                                    <table id="tree_170" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Receive_Items" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="StockUpdation.aspx">Receive Items</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Invoice Controls</span> </li>
                                <li class="accordion-content" id="content_176" style="display: none;">
                                    <table id="tree_176" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                           <%-- <tr class="row" id="Invoice_Entry" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VendorInvoicenew.aspx">Invoice Entry VAT</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>--%>
                                            <tr class="row" id="Invoice_EntryGST" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VendorInvoiceGST.aspx">Invoice Entry GST</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                         <%--   <tr class="row" id="Invoice_Verification" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="BasicPriceUpdationnew.aspx">Supplier Invoice verification VAT</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>--%>
                                            <tr class="row" id="Invoice_VerificationGST" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="BasicPriceUpdationNewGST.aspx">Supplier Invoice verification GST</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="SPInvoice_Verification" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="InvoiceVerficationNew.aspx">Service Provider Invoice verifcation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Stores</span> </li>
                                <li class="accordion-content" id="content_177" style="display: block;">
                                    <table id="tree_177" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Add_Items" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ItemCodeCreation.aspx">Add Items</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Amend_Items" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="EditItemCode.aspx">Amend item codes</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Verify_Items" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="verifyitemcode.aspx">Verify Items</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Approved_Items" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ApprovedItemcodes.aspx">Item code Status</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="View_Stock" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewStock.aspx">View Stock</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                            </ul>
                            <script type="text/javascript">
                                new Accordion("sidenav-a");
                            </script>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
