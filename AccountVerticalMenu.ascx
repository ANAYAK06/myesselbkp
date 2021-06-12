<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountVerticalMenu.ascx.cs"
    Inherits="AccountVerticalMenu" %>
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
<script src="Java_Script/moment.js" type="text/javascript"></script>
<script src="Java_Script/moment-with-locales.min.js" type="text/javascript"></script>
<script src="Java_Script/jquery-3.3.1.js" type="text/javascript"></script>
<script src="Java_Script/jquery.dataTables.min.js" type="text/javascript"></script>
<link href="Css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td id="secondary" class="sidenav-a" style="background-color: #333333; height: 400px;
                        width: 150px" align="left">
                        <div class="wrap">
                            <ul id="sidenav-a" class="accordion">
                                <li class="accordion-title"><span>Cost Center</span> </li>
                                <li class="accordion-content" id="content_168" style="display: none;">
                                    <table id="tree_168" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="addcc" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddCostCenter.aspx">New Cost Center</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewcc" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmviewcc.aspx">View Cost Center</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Detail Cost Account Head</span> </li>
                                <li class="accordion-content" id="content_169" style="display: none;">
                                    <table id="tree_169" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="adddca" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddDCA.aspx">New DCA</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="viewdca" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmviewDCA.aspx">View DCA</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>IT</span> </li>
                                <li class="accordion-content" id="content_170" style="display: none;">
                                    <table id="tree_170" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="addit" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddIt.aspx">New IT</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Contract</span> </li>
                                <li class="accordion-content" id="content_176" style="display: none;">
                                    <table id="tree_176" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="addcontract" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddContract.aspx">New Contract</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="updatecontract" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmupdateviewcontract.aspx">Update Contract</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewcontract" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmviewcontract.aspx">View Contract</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="newclientpo" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddnewClientPO.aspx">New Client PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="verifyclientpo" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyclientPO.aspx">Verify Client PO</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Client</span> </li>
                                <li class="accordion-content" id="Li4" style="display: block;">
                                    <table id="Table4" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="addclient" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="trclient" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAddClient.aspx">New Client</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="addsubclient" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="tr7" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="subclient.aspx">New SubClient</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="verifyclient" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="tr9" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyClient.aspx">Verify Client</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trwork" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="WorkInProgress.aspx">Work In Progress</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trworkreport" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewWorkInProgress.aspx">View WorkInProgress</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Bank Branch</span> </li>
                                <li class="accordion-content" id="Li6" style="display: block;">
                                    <table id="Table6" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="addbankbranch" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmbankbranch.aspx">Add Bank Branch</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="verifybankbranch" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmbankbranch.aspx">Verify Bank Branch</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewbankbranch" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmviewbankbook.aspx">View Bank Branch</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Budget</span> </li>
                                <li class="accordion-content" id="Li1" style="display: block;">
                                    <table id="Table1" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="assignccbudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmAssignCCBudget.aspx">Assign CC Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="assigndcabudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%-- <a href="frmDCABudget.aspx">Assign DCA Budget</a>--%>
                                                                    <a href="AssignDCABudget.aspx">Assign DCA Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="updateccbudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%--   <a href="frmUpdateCCBudget.aspx">Update CC Budget</a>--%>
                                                                    <a href="Viewccbudget.aspx">Verify CC Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="updatedcabudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%-- <a href="frmUpdateDCABudget.aspx">Update DCA Budget</a>--%>
                                                                    <a href="VerifyDcaBudget.aspx">Verify DCA Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Amendedccbudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%-- <a href="frmUpdateDCABudget.aspx">Update DCA Budget</a>--%>
                                                                    <a href="Ammendccbudget.aspx">Amend CC Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Verifyamendedccbudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%-- <a href="frmUpdateDCABudget.aspx">Update DCA Budget</a>--%>
                                                                    <a href="Verificationamendccbudget.aspx">Verify Amend CC Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Amenddcabudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%-- <a href="frmUpdateDCABudget.aspx">Update DCA Budget</a>--%>
                                                                    <a href="AmendDCABudget.aspx">Amend DCA Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Verifyamenddca" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="Veifyamendeddcas.aspx">Verify Amend DCA Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="closebudget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="closebudget.aspx">Close Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Masters</span> </li>
                                <li class="accordion-content" id="Li8" style="display: none;">
                                    <table id="Table8" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="Trgroupcreation" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CreateGroups.aspx">Create Groups</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Trverifygroup" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyGroups.aspx">Verify Groups</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Trarrovalgroup" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyGroups.aspx">Approval Group</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="traddsubgroups" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AddSubGroups.aspx">Add Sub-Groups</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trupdatesubgroups" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="SubGroupUpdate.aspx">Update Sub-Groups</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifysubgroup" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifySubGroups.aspx">Verify Sub-Group</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trapprovesubgroup" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifySubGroups.aspx">Approve Sub-Group</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trcreateledger" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="LedgerCreation.aspx">Create Ledger</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifyledger" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyLedgerCreation.aspx">Verify Ledger</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trapproveledger" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyLedgerCreation.aspx">Approve Ledger</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="tr10" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="NewGroupTree.aspx">View Group Tree</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewledger" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewLedgers.aspx">View Ledgers</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ProfitandLoss" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewProfitandLossReport.aspx">Profit and Loss</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Balancesheet" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewBalanceSheet.aspx">Balance Sheet</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="traddsharecapital" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AddShareCapital.aspx">Add Share Capital</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifysharecapital" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyShareCapital.aspx">Verify Share Capital</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Cash Voucher</span> </li>
                                <li class="accordion-content" id="Li2" style="display: block;">
                                    <table id="Table2" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="newcashvoucher" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmCashflow.aspx">New Cash Voucher</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="newcashvouchernew" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VendorCashPayment.aspx">Vendor Cash Voucher </a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="transfercash" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmTransferCash.aspx">New Cash Transfer</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="centraldaybook" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmCentralDayBook.aspx">Central Day Book</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Customer Invoice</span>
                                </li>
                                <li class="accordion-content" id="Li5" style="display: block;">
                                    <table id="Table5" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="invoicemaking" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmpendinginvoice.aspx">Salary Invoice</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trspinvoice" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmpendinginvoicenew.aspx">ServiceProvider Invoice</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trclientinvoice" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ClientInvoice.aspx">Client Invoice Creation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Invoicecancel" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyClientInvoice.aspx">Client Invoice Update/Cancellation</a>
                                                                    <%--  <a href="InvoiceCancel.aspx">Invoice Update/Cancellation</a>--%>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="invoicerollback" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmDelete.aspx">Invoice RollBack</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Bank Voucher</span> </li>
                                <li class="accordion-content" id="Li3" style="display: block;">
                                    <table id="Table3" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="newbankvoucher" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmBankflow.aspx">New Bank Voucher</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="paybill" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmPayBill.aspx">Pay Bill</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="pfgenerate" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="PFgenerate.aspx">PF Entry</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="pfverify" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="Verifypf.aspx">Verify PF Entries</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="pfpayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="PFPayment.aspx">PF Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Addagency" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AddAgency.aspx">Add Agency</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Addloan" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="Addloandetails.aspx">Term Loan Entry</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="TLPayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="Termloanpayment.aspx">Term Loan Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="cheques" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="chequebook.aspx">Enter ChequeBook</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="verifycheques" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="chequebook.aspx">Verify ChequeBook</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewcheques" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewChequeDetails.aspx">View Cheque Details</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="generalinvoice" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="GeneralPaymentInvoice.aspx">General Invoice</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Verifygeneralinvoice" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="verifygeneralpayment.aspx">Verify General Invoice</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="RetentionHoldPayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CustomerHoldandRetention.aspx">Retention/Hold Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trserviceproviderpayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ServiceProviderPayment.aspx">ServiceProvider Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trvendortdspayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VendorTDSpayment.aspx">Vendor TDS Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trClientInvoicePayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ClientInvoicePayment.aspx">Client Invoice Reciept</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="tradvancepayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ClientAdvanceReceipt.aspx">Client Advance Reciept</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trsupplierinvoicepayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="SupplierPayment.aspx">Supplier Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trotherreceipts" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CreditPayments.aspx">Other Receipts</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifyotherreceipts" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyCreditPayments.aspx">Verify Other Receipts</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trnewfd" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AddNewFD.aspx">Fixed Deposit/ RD</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifyfd" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyNewFD.aspx">Verify Fixed Deposit/ RD</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trclaimfd" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyClaimFD.aspx">Close Fixed Deposit/ RD</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trunsecuredloan" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CreateUnsecuredLoan.aspx">Unsecured Loan</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifyunsecuredloan" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyUnsecuredLoan.aspx">Verify Unsecured Loan</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title accordion-title-active"><span>Reports</span> </li>
                                <li class="accordion-content" id="content_177" style="display: block;">
                                    <table id="tree_177" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="budget" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewBudget.aspx">Budget</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%--Added TRack--%>
                                            <tr class="row" id="TrTrack" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="SnAcctTracking.aspx">Tracking</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="cash" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmViewcashflow.aspx">Cash</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="bank" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmViewBankFlow.aspx">Bank</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="bankstatement" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmBankStatement.aspx">Bank Statement</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="servicetax" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmViewServiceTaxPayment.aspx">Service Tax/Excise Duty</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="vendorpayment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmViewVendorPayment.aspx">Vendor Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="vendorservicetax" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmViewVendorServiceTax.aspx">Vendor Service Tax/Excise Duty</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ViewVAT" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VATReport.aspx">VAT Report</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ViewGST" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewGST.aspx">GST Report</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="vendortds" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <%--<a href="frmViewTDSPayment.aspx">Vendor TDS</a>--%>
                                                                    <a href="ViewVendorTDS.aspx">Vendor TDS</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewusc" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="viewunsecuredloan.aspx">View Unsecured Loan</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewfd" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="viewfd.aspx">View Fixed Deposits</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="Termloan" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="viewtermloan.aspx">View Term Loan</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="vdebittran" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="viewDebitTransactions.aspx">Bank Debit Vouchers</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewclientinvoice" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewClientInvoice.aspx">View Client Invoice</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ViewRentionHoldPAyment" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewClientRetentionandHold.aspx">View Retention/Hold Payment</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="viewpf" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewPF.aspx">View PF</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="clientadvance" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewClientAdvance.aspx">View Client Advance</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="bankvouchersbyit" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="expand"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="BankVocuhersByITwise.aspx">Bank Voucher By IT Wise</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Financial Report</span> </li>
                                <li class="accordion-content" id="Li7" style="display: none;">
                                    <table id="Table7" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="companystatus" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CompanyOverallStatus.aspx">Overall Company Status</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ccstatus" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr2" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CompanyStatus.aspx">Overall Cost Center Status</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="itstatus" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr5" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CompanyITstatus.aspx">IT Status</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="csummary" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr6" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CashDebitSummary.aspx">Cash Flow Debit Summary</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="cashcreditsummary" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr8" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="CashCreditSummary.aspx">Cash Flow Credit Summary</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="dsummary" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr3" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="Bankflowdebitsummary.aspx">Bank Flow Debit Summary</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="banksummary" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr4" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="creditsummary.aspx">Bank Flow Credit Summary</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="AssetDep" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr1" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="AssetDepreciation.aspx">Asset Depreciation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ITDep" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr12" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ITDepreciationReport.aspx">Company Depreciation</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="ViewAccuredint" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr id="Tr11" runat="server">
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="ViewAccruedInterest.aspx">View Accured Interest</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li class="accordion-title"><span>Journal</span> </li>
                                <li class="accordion-content" id="Li9" style="display: none;">
                                    <table id="Table9" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="trjournalentry" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="JournalEntries.aspx">Journal Entry</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trverifyjournalentry" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyJournalEntries.aspx">Verify Journal Entry</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="row" id="trapprovejournalentry" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="VerifyJournalEntries.aspx">Approve Journal Entry</a>
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
