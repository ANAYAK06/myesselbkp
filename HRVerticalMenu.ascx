<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HRVerticalMenu.ascx.cs"
    Inherits="HRVerticalMenu" %>
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

<script type="text/javascript" src="Java_Script/jquery.bgiframe-2.1.1.pack.js"></script>--%>

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
                                    <li><a href="" target="_top" class="active"><span>Accounting</span>
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
                    <td id="secondary" class="sidenav-a" style="background-color: #333333; height: 400px;
                        width: 150px" align="left">
                        <div class="wrap">
                            <ul id="sidenav-a" class="accordion">
                                <li class="accordion-title"><span>Employees</span> </li>
                                <li class="accordion-content" id="content_168" style="display: none;">
                                    <table id="tree_168" class="tree-grid">
                                        <thead class="tree-head">
                                        </thead>
                                        <tbody class="tree-body">
                                            <tr class="row" id="employee" runat="server">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmEmployees.aspx">Employees</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%-- <tr class="row">
                                                <td class="char">
                                                    <table class="tree-field" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class="indent"></span>
                                                                </td>
                                                                <td>
                                                                    <a href="frmEmployeeData.aspx">Employee Info</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>--%>
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
