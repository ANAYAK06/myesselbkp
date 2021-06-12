<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ToolsVerticalMenu.ascx.cs"
    Inherits="ToolsVerticalMenu" %>
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
                <tr>
                    <td id="secondary" class="sidenav-a" style="background-color: #333333; height: 400px;
                        width: 150px" align="left">
                        <div class="wrap">
                            <ul id="sidenav-a" class="accordion">
                                <li class="accordion-title"><span>Approved Limit</span> </li>
                                <li class="accordion-content" id="content_168" style="display: none;">
                                    <table id="tree_168" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="voucherlimit" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="paymentlimit.aspx">Approved Limit</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="cashvoucherlimit" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewCC.aspx">Cash Voucher Approved Limit</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Role/CC Change</span> </li>
                                <li class="accordion-content" id="Li1" style="display: none;">
                                    <table id="Table1" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="viewreports" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewAccountdetails.aspx">Role/CC Change</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <%--  <li class="accordion-title"><span>Account Creation</span> </li>
                                <li class="accordion-content" id="Li2" style="display: none;">
                                    <table id="Table2" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="accountcreation" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AccountCreation.aspx">Create Account</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>--%>
                                <li class="accordion-title"><span>CC Over Head and Depreciation</span> </li>
                                <li class="accordion-content" id="Li3" style="display: none;">
                                    <table id="Table3" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Overhead" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CCOverheadandDepreciation.aspx">CC Over Head and Depreciation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>CC Asset Depreciation</span> </li>
                                <li class="accordion-content" id="Li6" style="display: none;">
                                    <table id="Table4" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="AssetDep" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AssetDepPercentage.aspx">CC Asset Depreciation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Company Depreciation</span> </li>
                                <li class="accordion-content" id="Li7" style="display: none;">
                                    <table id="Table7" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="tritdep" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ITDepPercentage.aspx">Company Depreciation </a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>CC interest on negative status</span> </li>
                                <li class="accordion-content" id="Li4" style="display: none;">
                                    <table id="ccaccrued" runat="server" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Tr1" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CCAccrued.aspx">Interest on negative status</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Limit For Dep for Issue & Recipt</span> </li>
                                <li class="accordion-content" id="Li5" style="display: none;">
                                    <table id="Table5" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="TrSemiAssetDep" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="SemiAssetDep.aspx">Semi Asset/Consumable Limit</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title" id="exreg" runat="server"><span>Excise/Vat/Service Tax Registration</span>
                                </li>
                                <li class="accordion-content" style="display: none;">
                                    <table id="Table6" runat="server" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="VATRegistration" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VatRegistration.aspx">Sales Tax/Vat Registration</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ExciseRegistration" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ExciseRegistrationForm.aspx">Excise Registration</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ServiceRegistration" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ServiceTaxRegistrationForm.aspx">Service Tax Registration</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="TrGstregistration" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="GSTRegistrationForm.aspx">GST Registration</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="TrGstApproval" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="GSTRegistrationForm.aspx">GST Approval</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ViewExciseVATRegistration" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewTaxnumbers.aspx">View Tax Registraion</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>HSN/SAC Code</span> </li>
                                <li class="accordion-content" id="Li8" style="display: none;">
                                    <table id="Table8" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Trhsnsaccodecreation" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="HSNSACCodeCreation.aspx">HSN/SAC CodeCreation/Updation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                             <tr class="row" id="Trverifyhsnasncode" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyHSNSACCode.aspx">Verify HSN/SAC Code</a>
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
