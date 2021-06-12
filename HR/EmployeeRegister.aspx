 <%@ Page Language="C#" MasterPageFile="~/Essel.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="EmployeeRegister.aspx.cs" Inherits="EmployeeRegister"
    Title="Employee Registration Form" %>

<%@ Register Src="~/HR/HRVerticalMenu.ascx" TagName="Menu" TagPrefix="Hr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../Css/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
<link href="../Css/buttons.css" rel="stylesheet" type="text/css" />
<link href="../Css/print.css" rel="stylesheet" type="text/css" />
<link href="../Css/esselCssStyle.css" rel="stylesheet" type="text/css" />


  <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();
        }
        function closepopup1() {
            $find('mdlindent1').hide();
        }
        function showpopup() {
            $find('mdlindent').show();
        }

      
        </script>
    <style type="text/css">
        .TabControl .ajax__tab_body /* Body */
        {
            font-family: Arial;
            font-size: 8pt;
            padding: 0.5em 0.25em;
            background-color: White;
            border: solid 1px #373535; /*LightSteelBlue*/
            border-top-width: 0px;
        }
        .TabControl .ajax__tab_header /* Tab */
        {
            font-family: Tahoma;
            font-size: 9px;
            font-weight: bold;
            border-bottom: solid 1px #050505; /*LightSteelBlue*/
        }
        .TabControl .ajax__tab_header .ajax__tab_outer /* Unselected tab */
        {
            background-color: #d0d0d0;
            margin: 0px 0.16em 0px 0px;
            padding: 1px 0px 1px 0px;
            vertical-align: bottom;
            border-bottom-width: 0px;
            border: solid 1px #E1DDDD; /*LightSteelBlue*/
        }
        .TabControl .ajax__tab_header .ajax__tab_tab /* Unselected tab text */
        {
            color: #555555; /* LightSteelBlue */
            padding: 0.35em 0.75em;
            margin-right: 0.01em;
        }
        .TabControl .ajax__tab_active .ajax__tab_outer /* Selected tab */
        {
            background-color: #f9f9f9;
            border-top-width: 1px;
        }
        .TabControl .ajax__tab_active .ajax__tab_tab /* Selected tab text */
        {
            color: #555555; /* RoyalBlue */
        }
        .TabControl .ajax__tab_hover .ajax__tab_outer /* Hover tab */
        {
            background-color: #e0e0e0;
        }
        .TabControl .ajax__tab_hover .ajax__tab_tab /* Hover tab text */
        {
            color: #0D0C0C; /* RoyalBlue */
        }
        #overlay
        {
            position: fixed;
            z-index: 99;
            top: 0px;
            left: 0px;
            background-color: #f8f8f8;
            width: 100%;
            height: 100%;
            filter: Alpha(Opacity=90);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }
        #theprogress
        {
            background-color: #fff;
            border: 1px solid #ccc;
            padding: 10px;
            width: 300px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            filter: Alpha(Opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        #modalprogress
        {
            position: absolute;
            top: 40%;
            left: 50%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
        }
        #search
        {
            width: 20em;
            width: 2em;
        }
    </style>
  

   <script language="javascript">

       function validateddepartment() {
           var dept = document.getElementById("<%=txtddepartment.ClientID %>").value;
           if (dept == "") {
               alert("Please Enter Department");
               document.getElementById("<%=txtddepartment.ClientID %>").focus();
               return false;
           }
       }
           function Deptvalid(){
           var dept = document.getElementById("<%=txtdepartment.ClientID %>").value;
           if (dept == "") {
               alert("Please Enter");
               document.getElementById("<%=txtdepartment.ClientID %>").focus();
               return false;
           }


       }

       function ShowCC(Role) {
           if (Role == "Project Manager") {

               document.getElementById("<%=trcc.ClientID %>").style.display = 'block';
           }
           else {

               document.getElementById("<%=trcc.ClientID %>").style.display = 'none';

           
           }
       }


       /* This function for view button in Registartion page */
       function validation() {
           var estatus = document.getElementById("<%=ddlempstatus.ClientID %>").value;
           if (estatus == "New") {
               var objs1 = new Array("<%=ddljcat.ClientID %>", "<%=ddlempstatus.ClientID %>", "<%=ddlcategory.ClientID %>");
           }
           else {
               var objs1 = new Array("<%=ddljcat.ClientID %>", "<%=ddlempstatus.ClientID %>", "<%=ddloldid.ClientID %>", "<%=ddlcategory.ClientID %>");
           }
           if (!CheckInputs(objs1)) {
               return false;
           }
       }
       /* This is for Prevent backspace in DOB textboxes */
       function preventBackspace(e) {
           var evt = e || window.event;
           if (evt) {
               var keyCode = evt.charCode || evt.keyCode;
               if (keyCode === 8) {
                   if (evt.preventDefault) {
                       evt.preventDefault();
                   }
                   else {
                       evt.returnValue = false;
                   }
               }
           }
       }

       function Dateheck(sender, args, ctrl) {
           var birthdayDate = sender._selectedDate;
           // var re = /(0[1-9]|[12][0-9]|3[01])[\.\/\-](0[1-9]|1[012])[\.\/\-](19|20)\d\d/;

           if (birthdayDate.value != '') {
               var dateNow = new Date();
               // var birthdayDate = e.get_selectedDate();
               var years = dateNow.getFullYear() - birthdayDate.getFullYear();
               var months = dateNow.getMonth() - birthdayDate.getMonth();
               var days = dateNow.getDate() - birthdayDate.getDate();

               if (isNaN(years)) {
                   document.getElementById("<%=txtempage.ClientID %>").value = '';
                   return false;
               }
               else {
                   if (months < 0 || (months == 0 && days < 0)) {
                       years = parseInt(years) - 1;
                       if (years == 1) {
                           document.getElementById("<%=txtempage.ClientID %>").value = years + 'Year';
                       }
                       else {
                           document.getElementById("<%=txtempage.ClientID %>").value = years + 'Years';
                       }
                   }
                   else {
                       if (years == 1) {
                           document.getElementById("<%=txtempage.ClientID %>").value = years + 'Year';
                       }
                       else {
                           document.getElementById("<%=txtempage.ClientID %>").value = years + 'Years';
                       }
                   }

               }
           }
       }

       /* This is for get the age when select calendar*/
       function GetFamilyage(sender, args, ctrl) {
           var birthdayDate = sender._selectedDate;
           // var re = /(0[1-9]|[12][0-9]|3[01])[\.\/\-](0[1-9]|1[012])[\.\/\-](19|20)\d\d/;
           sumgrid = document.getElementById("<%=grdfamilydetails.ClientID %>");
        
       
           if (birthdayDate.value != '') {
               var dateNow = new Date();
               // var birthdayDate = e.get_selectedDate();
               var years = dateNow.getFullYear() - birthdayDate.getFullYear();
               var months = dateNow.getMonth() - birthdayDate.getMonth();
               var days = dateNow.getDate() - birthdayDate.getDate();

               if (isNaN(years)) {
                   sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = '';
                   return false;
               }
               else {
                   if (months < 0 || (months == 0 && days < 0)) {
                       years = parseInt(years) - 1;
                       if (years == 1) {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Year';
                       }
                       else {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Years';
                       }
                   }
                   else {
                       if (years == 1) {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Year';
                       }
                       else {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Years';
                       }
                   }

               }
           }
       }

       /* */
       function GetChildernsAge(sender, args, ctrl) {
           var birthdayDate = sender._selectedDate;
           // var re = /(0[1-9]|[12][0-9]|3[01])[\.\/\-](0[1-9]|1[012])[\.\/\-](19|20)\d\d/;
           sumgrid = document.getElementById("<%=grdchildren.ClientID %>");


           if (birthdayDate.value != '') {
               var dateNow = new Date();
               // var birthdayDate = e.get_selectedDate();
               var years = dateNow.getFullYear() - birthdayDate.getFullYear();
               var months = dateNow.getMonth() - birthdayDate.getMonth();
               var days = dateNow.getDate() - birthdayDate.getDate();

               if (isNaN(years)) {
                   sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = '';
                   return false;
               }
               else {
                   if (months < 0 || (months == 0 && days < 0)) {
                       years = parseInt(years) - 1;
                       if (years == 1) {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Year';
                       }
                       else {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Years';
                       }
                   }
                   else {
                       if (years == 1) {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Year';
                       }
                       else {
                           sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value = years + 'Years';
                       }
                   }

               }
           }
       }
       function DDateheck(sender, args) {
           var birthdayDate = sender._selectedDate;
           // var re = /(0[1-9]|[12][0-9]|3[01])[\.\/\-](0[1-9]|1[012])[\.\/\-](19|20)\d\d/;

           if (birthdayDate.value != '') {
               var dateNow = new Date();
               // var birthdayDate = e.get_selectedDate();
               var years = dateNow.getFullYear() - birthdayDate.getFullYear();
               var months = dateNow.getMonth() - birthdayDate.getMonth();
               var days = dateNow.getDate() - birthdayDate.getDate();

               if (isNaN(years)) {
                   document.getElementById("<%=txtdnage.ClientID %>").value = '';
                   return false;
               }
               else {
                   if (months < 0 || (months == 0 && days < 0)) {
                       years = parseInt(years) - 1;
                       if (years == 1) {
                           document.getElementById("<%=txtdnage.ClientID %>").value = years + 'Year';
                       }
                       else {
                           document.getElementById("<%=txtdnage.ClientID %>").value = years + 'Years';
                       }
                   }
                   else {
                       if (years == 1) {
                           document.getElementById("<%=txtdnage.ClientID %>").value = years + 'Year';
                       }
                       else {
                           document.getElementById("<%=txtdnage.ClientID %>").value = years + 'Years';
                       }
                   }

               }
           }
       }
       function Dateheck1(sender, args) {
           var birthdayDate = sender._selectedDate;
           // var re = /(0[1-9]|[12][0-9]|3[01])[\.\/\-](0[1-9]|1[012])[\.\/\-](19|20)\d\d/;

           if (birthdayDate.value != '') {
               var dateNow = new Date();
               // var birthdayDate = e.get_selectedDate();
               var years = dateNow.getFullYear() - birthdayDate.getFullYear();
               var months = dateNow.getMonth() - birthdayDate.getMonth();
               var days = dateNow.getDate() - birthdayDate.getDate();

               if (isNaN(years)) {
                   document.getElementById("<%=txtnage.ClientID %>").value = '';
                   return false;
               }
               else {
                   if (months < 0 || (months == 0 && days < 0)) {
                       years = parseInt(years) - 1;
                       if (years == 1) {
                           document.getElementById("<%=txtnage.ClientID %>").value = years + 'Year';
                       }
                       else {
                           document.getElementById("<%=txtnage.ClientID %>").value = years + 'Years';
                       }
                   }
                   else {
                       if (years == 1) {
                           document.getElementById("<%=txtnage.ClientID %>").value = years + 'Year';
                       }
                       else {
                           document.getElementById("<%=txtnage.ClientID %>").value = years + 'Years';
                       }
                   }

               }
           }
       }
       
       
       /* This is for validate the fields of family details gridview */
       function Fmailyfootervalidation() {
           sumgrid = document.getElementById("<%=grdfamilydetails.ClientID %>");
           var name = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).value;
           var namectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0);
           var dob = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;
           var dobctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0);
           var age = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
           var agectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0);
           var gender = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0).selectedIndex;
           var genderctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0);
           var Relation = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0).selectedIndex;
           var Relationctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0);
           var Wphone = sumgrid.rows[sumgrid.rows.length - 1].cells(6).children(0).value;
           var Wphonectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(6).children(0);
           var Mob = sumgrid.rows[sumgrid.rows.length - 1].cells(7).children(0).value;
           var Mobctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(7).children(0);
           if (name == "") {
               alert("Enter name");
               namectrl.focus();
               return false;
           }
           else if (dob == "") {
               alert("Enter Date Of Birth");
               dobctrl.focus();
               return false;

           }
           else if (age == "") {
               alert("Enter Age");
               agectrl.focus();
               return false;

           }
           else if (gender == 0) {
               alert("Select gender");
               genderctrl.focus();
               return false;

           }
           else if (Relation == 0) {
               alert("Select Martial Status");
               Relationctrl.focus();
               return false;

           }
           else if (Wphone == 0) {
               alert("Enter Work Phone number");
               Wphonectrl.focus();
               return false;

           }
           else if (Mob == 0) {
               alert("Enter mobile phone number");
               Mobctrl.focus();
               return false;

           }
       }
       /* This is for validate the fields oF Childern details gridview */
       function Childernfootervalidation() {
           sumgrid = document.getElementById("<%=grdchildren.ClientID %>");
           var name = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).value;
           var namectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0);
           var dob = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;
           var dobctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0);
           var age = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
           var agectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0);
           var gender = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0).selectedIndex;
           var genderctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0);
           var mstatus = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0).selectedIndex;
           var mstatusctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0);

           if (name == "") {
               alert("Enter name");
               namectrl.focus();
               return false;
           }
           else if (dob == "") {
               alert("Enter Date Of Birth");
               dobctrl.focus();
               return false;

           }
           else if (age == "") {
               alert("Enter Age");
               agectrl.focus();
               return false;

           }
           else if (gender == 0) {
               alert("Select gender");
               genderctrl.focus();
               return false;

           }
           else if (mstatus == 0) {
               alert("Select Martial Status");
               mstatusctrl.focus();
               return false;

           }
       }
       /* This is for validate the fields in Personal information tab */
       function validate() {
           var objs = new Array("<%=txtfname.ClientID %>", "<%=txtlname.ClientID %>", "<%=txtempdob.ClientID %>", "<%=ddlsex.ClientID %>", "<%=ddlmartialst.ClientID %>", "<%=txtnomineename.ClientID %>", "<%=txtrelation.ClientID %>", "<%=txtndob.ClientID %>", "<%=txtadd.ClientID %>", "<%=txttempaddress.ClientID %>", "<%=txtwph1.ClientID %>", "<%=txtwph2.ClientID %>", "<%=txtmph1.ClientID %>", "<%=txtbirthplace.ClientID %>", "<%=txtemail.ClientID %>");
           if (!CheckInputs(objs)) {
               return false;
           }
           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(1);
       }
       /* This is for  validate the fields in Qualificationdetailsgridview  */
       function Qualificationfootervalidation() {
           var sumgrid = document.getElementById("<%=grdqualification.ClientID %>");
           var class1 = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).selectedIndex;
           var classctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0);
           var univer = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;
           var univerctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0);
           var frm = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
           var frmctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0);
           var to = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0).value;
           var toctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0);
           var perc = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0).value;
           var percctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0);
           if (class1 == 0) {
               alert("select class");
               classctrl.focus();
               return false;
           }
           else if (univer == "") {
               alert("Enter University name");
               univerctrl.focus();
               return false;

           }
           else if (frm == "") {
               alert("Enter From Date");
               frmctrl.focus();
               return false;

           }
           else if (to == "") {
               alert("Enter TO Date");
               toctrl.focus();
               return false;

           }
           else if (perc == "") {
               alert("Enter Percentage");
               percctrl.focus();
               return false;

           }

           var str1 = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;

           var str2 = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0).value;
           var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
           var dt1 = str1.substring(0, 2);
           var dt2 = str2.substring(0, 2);
           var yr1 = str1.substring(7, 11);
           var yr2 = str2.substring(7, 11);
           for (var i = 0; i < args.length; i++) {
               var month = str2.substring(3, 6);
               var month1 = str1.substring(3, 6);
               if (args[i] == month) {
                   var month = parseFloat(i + 1);
                   var date2 = yr2 + "-" + month + "-" + dt2;

               }
               if (args[i] == month1) {
                   var month1 = parseFloat(i + 1);
                   var date1 = yr1 + "-" + month1 + "-" + dt1;
               }

           }
           var one_day = 1000 * 60 * 60 * 24;
           var x = date1.split("-");
           var y = date2.split("-");

           var date4 = new Date(x[0], (x[1] - 1), x[2]);
           var date3 = new Date(y[0], (y[1] - 1), y[2]);

           var month1 = x[1] - 1;
           var month2 = y[1] - 1;

           _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
           if (parseFloat(_Diff) < 0) {
               alert("Invalid To date");
               sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).focus();
               return false;
           }

       }
       /* This is for  validate the fields in TechincalQualificationdetailsgridview  */
       function TechnicalQualificationfootervalidation() {
           sumgrid = document.getElementById("<%=grdTqualifications.ClientID %>");
           var skills1 = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).selectedIndex;
           var skillsctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0);
           var expe1 = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).selectedIndex;
           var expectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0);
           if (skills1 == 0) {
               alert("select Skills");
               skillsctrl.focus();
               return false;
           }
           else if (expe1 == 0) {
               alert("Select Experience");
               expectrl.focus();
               return false;

           }

       }

       /* This is for validate the fields in Technical Qualification tab */
       function footervalidationnext() {
           sumgrid = document.getElementById("<%=grdqualification.ClientID %>");
           var class1 = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).selectedIndex;
           var mstatus = document.getElementById("<%=ddlmartialst.ClientID %>").selectedIndex;
           var classctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0);
           var univer = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;
           var univerctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0);
           var frm = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
           var frmctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0);
           var to = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0).value;
           var toctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0);
           var perc = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0).value;
           var percctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(5).children(0);
           if (class1 != 0) {
               if (class1 == 0) {
                   alert("select class");
                   classctrl.focus();
                   return false;
               }
               else if (univer == "") {
                   alert("Enter University name");
                   univerctrl.focus();
                   return false;

               }
               else if (frm == "") {
                   alert("Enter From Date");
                   frmctrl.focus();
                   return false;

               }
               else if (to == "") {
                   alert("Enter TO Date");
                   toctrl.focus();
                   return false;

               }
               else if (perc == "") {
                   alert("Enter Percentage");
                   percctrl.focus();
                   return false;

               }
           }
           if (mstatus == 1) {
               $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(2);
           }
           else if (mstatus != 0 && mstatus != 1) {

               $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(1);
           }

       }
       /*This is for GridviewHistory ctrl display block or none by selecting of Radio button */
       function rdbtnvalidation() {
           var grds = document.getElementById("<%=grdhistory.ClientID %>");
           if (SelectedIndex("<%=RadioButtonList1.ClientID %>") == 1) {
               document.getElementById("<%=grdhistory.ClientID %>").style.display = 'none';
           }
           else {
               document.getElementById("<%=grdhistory.ClientID %>").style.display = 'block';
           }

       }

       /*This is for validate the fields in Gridviewhistory details */
       function Historyfootervalidation() {
           var sumgrid = document.getElementById("<%=grdhistory.ClientID %>");
           var org = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0).value;
           var orgctrl = sumgrid.rows[sumgrid.rows.length - 1].cells(1).children(0);
           var from1 = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;
           var fromctrl1 = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0);
           var to1 = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
           var toctrl1 = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0);
           var role = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0).value;
           var rolectrl = sumgrid.rows[sumgrid.rows.length - 1].cells(4).children(0);

           if (org == "") {
               alert("Enter Organation name class");
               orgctrl.focus();
               return false;
           }
           else if (from1 == "") {
               alert("Enter from date");
               fromctrl1.focus();
               return false;

           }
           else if (to1 == "") {
               alert("Enter to Date");
               toctrl1.focus();
               return false;

           }
           else if (role == "") {
               alert("Enter Role");
               rolectrl.focus();
               return false;

           }
           var str1 = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;

           var str2 = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;
           var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
           var dt1 = str1.substring(0, 2);
           var dt2 = str2.substring(0, 2);
           var yr1 = str1.substring(7, 11);
           var yr2 = str2.substring(7, 11);
           for (var i = 0; i < args.length; i++) {
               var month = str2.substring(3, 6);
               var month1 = str1.substring(3, 6);
               if (args[i] == month) {
                   var month = parseFloat(i + 1);
                   var date2 = yr2 + "-" + month + "-" + dt2;

               }
               if (args[i] == month1) {
                   var month1 = parseFloat(i + 1);
                   var date1 = yr1 + "-" + month1 + "-" + dt1;
               }

           }
           var one_day = 1000 * 60 * 60 * 24;
           var x = date1.split("-");
           var y = date2.split("-");

           var date4 = new Date(x[0], (x[1] - 1), x[2]);
           var date3 = new Date(y[0], (y[1] - 1), y[2]);

           var month1 = x[1] - 1;
           var month2 = y[1] - 1;

           _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
           if (parseFloat(_Diff) < 0) {
               alert("Invalid To Date");
               sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).focus();
               return false;
           }

       }

       function validateRadio() {

           var hdf = document.getElementById("<%=hdfroles.ClientID%>").value;
           var hdt = document.getElementById("<%=hdftype.ClientID%>").value;

           if (hdt != "1") {
               if (hdt != "2") {
                   var table = document.getElementById('<%= RadioButtonList1.ClientID %>');
                   var radios = table.getElementsByTagName('input');
                   var count = 0;
                   for (var index = 0; index < radios.length; index++) {
                       if (radios[index].checked)
                           count++;
                   }
                   if (count == 0) {
                       alert('Select atleast one item');
                       return false;
                   }
               }
           }
           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(3);

       }


       /*This is for GridviewSalary ctrl display block or none  */
       function ValidateCheck() {
           var grid = document.getElementById("<%=grdsalarybreakup.ClientID %>");
           var chkctrl = grid.rows[grid.rows.length - 1].cells(2).children(0);
           var chk = grid.getElementsByTagName('input');
           var percval = grid.rows[grid.rows.length - 1].cells(3).children(0).value;
           var perc = grid.rows[grid.rows.length - 1].cells(3).children(0);
           var mnthlyval = grid.rows[grid.rows.length - 1].cells(4).children(0).value;
           var mnthly = grid.rows[grid.rows.length - 1].cells(4).children(0);
           var yearlyval = grid.rows[grid.rows.length - 1].cells(5).children(0).value;
           var yearly = grid.rows[grid.rows.length - 1].cells(5).children(0);

           for (var index = 0; index < chk.length; index++) {
               if (chk[index].checked) {
                   perc.disabled = false;
                   mnthly.disabled = true;
                   yearly.disabled = true;
                   return false;
               }
               else if (!chk[index].checked) {
               if (grid.rows[grid.rows.length - 1].cells(1).children(0).value == "12") {
               }
               else {
                   grid.rows[grid.rows.length - 1].cells(3).children(0).value = "";
                   perc.disabled = true;
                   mnthly.disabled = false;
                   yearly.disabled = false;
               }
               }
           }

       }

       /*This is for validate the fields in GridviewSalary details */

       function Total() {

           var amountm = 0;
           var amounty = 0;
           var vardedm = 0;
           var vardedy = 0;
           var vargrsm = 0;
           var vargrsy = 0;
           var vatnetm = 0;
           var varnety = 0;


           var grid = document.getElementById("<%=grdsalarybreakup.ClientID %>");
           var des = grid.rows[grid.rows.length - 1].cells(1).children(0).selectedIndex;
           var pers = grid.rows[grid.rows.length - 1].cells(3).children(0).value;
           var basicm = document.getElementById("<%=txtbasicm.ClientID %>").value;
           var basicy = document.getElementById("<%=txtbasicy.ClientID %>").value;
           var grsm = document.getElementById("<%=txtgrossm.ClientID %>").value;
           var grsy = document.getElementById("<%=txtgrossy.ClientID %>").value;
          
        
           var mstatus = document.getElementById("<%=ddlmartialst.ClientID %>").selectedIndex;


           if (des == 0) {
               alert("Please select the salary component");
               return false;
           }
           else if (pers == "") {
               alert("Please enter the percentage");
               return false;
           }

         

           if (des != 0) {
               if (pers != 0) {
                   if (des == 1) {
                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                      

                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                     
                 
                   }
                   else if (des == 2) {
                   
                           amountm = basicm * (pers / 100);
                           amounty = basicy * (pers / 100);
                           grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                           grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                           vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                           vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                          

                           document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                           document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                         
                   
                    
                   }
                   else if (des == 3) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                      


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                       
                      
                   }
                   else if (des == 4) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                     


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                    
                      
                   }
                   else if (des == 5) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                     


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                    
                     
                   }
                   else if (des == 6) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));


                    


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                    
                      
                   }
                   else if (des == 7) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                    


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                      
                     
                   }
                   else if (des == 8) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                    


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                     
                    
                   }
                   else if (des == 9) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                    


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm.toFixed(2);
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy.toFixed(2);
                     
                     
                   }
                   else if (des == 10) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                    


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm;
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy;
                     
                   
                   }
                   else if (des == 11) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                    
                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm;
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy;
                      
                     
                   }
                   else if (des == 12) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                     


                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm;
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy;
                      
                      
                   }
                   else if (des == 13) {

                       amountm = basicm * (pers / 100);
                       amounty = basicy * (pers / 100);
                       grid.rows[grid.rows.length - 1].cells(4).children(0).value = amountm;
                       grid.rows[grid.rows.length - 1].cells(5).children(0).value = amounty;

                       vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                       vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                      

                       document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm;
                       document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy;
                   
                   }


               }
               else {
                   amountm = grid.rows[grid.rows.length - 1].cells(4).children(0).value;
                   amounty = grid.rows[grid.rows.length - 1].cells(5).children(0).value;

                   vargrsm = eval((parseFloat(grsm) + parseFloat(amountm)));
                   vargrsy = eval((parseFloat(grsy) + parseFloat(amounty)));
                  

                   document.getElementById("<%=txtgrossm.ClientID %>").value = vargrsm;
                   document.getElementById("<%=txtgrossy.ClientID %>").value = vargrsy;
               
                  
                  
               }

           }



       }

       function validatesalback() {

           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(2);
       }

       function validate2() {
           var objs = new Array("<%=txtbasicy.ClientID%>", "<%=txtacnumber.ClientID%>", "<%=txtbankname.ClientID%>", "<%=txtbankaddress.ClientID%>");
           if (!CheckInputs(objs)) {
               return false;
           }
           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(4);
       }

       function report(addr, val) {

           if (addr != "") {
               window.open(addr, "_blank", 'width=780,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
               return false;
           }
           else {
               var id = 0;
               window.open("CertificatePreview.aspx?id=" + id + "&value=" + val, "_blank", 'width=780,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
               return false;
           }
       }

       function validateback() {

           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(3);
       }

       function upvalidation() {
           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(5);
       }
       function validateofficeback() {
           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(4);
       }
       function validateback4() {

           $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(0);
       }
       function dfinalvalidation() {
           if (document.getElementById("<%=ddldrole.ClientID%>").value == "Project Manager") {
               var objs = new Array("<%=txtdfname.ClientID %>", "<%=txtdmname.ClientID %>", "<%=txtdlname.ClientID %>", "<%=txtdndob.ClientID %>", "<%=ddldsex.ClientID %>", "<%=ddldmartialst.ClientID %>", "<%=txtdnominee.ClientID %>", "<%=ddldgender.ClientID %>", "<%=ddldRelation.ClientID %>", "<%=txtdmobileno.ClientID %>", "<%=txtdemail.ClientID %>", "<%=ddldjobpos.ClientID %>", "<%=ddldrole.ClientID %>", "<%=ddlddepartment.ClientID %>", "<%=ddlcc.ClientID %>", "<%=txtdroleid.ClientID %>");
           }
           else {
               var objs = new Array("<%=txtdfname.ClientID %>", "<%=txtdmname.ClientID %>", "<%=txtdlname.ClientID %>", "<%=txtdndob.ClientID %>", "<%=ddldsex.ClientID %>", "<%=ddldmartialst.ClientID %>", "<%=txtdnominee.ClientID %>", "<%=ddldgender.ClientID %>", "<%=ddldRelation.ClientID %>", "<%=txtdmobileno.ClientID %>", "<%=txtdemail.ClientID %>", "<%=ddldjobpos.ClientID %>", "<%=ddldrole.ClientID %>", "<%=ddlddepartment.ClientID %>", "<%=txtdroleid.ClientID %>");

           }
           if (!CheckInputs(objs)) {
               return false;
           }
       }
       function finalvalidation() {
           var hdf = document.getElementById("<%=hdfroles.ClientID%>").value;

           if (hdf == "Chairman Cum Managing Director") {

               var objs = new Array("<%=txtfname.ClientID %>", "<%=txtlname.ClientID %>", "<%=txtempdob.ClientID %>", "<%=ddlsex.ClientID %>", "<%=ddlmartialst.ClientID %>", "<%=txtnomineename.ClientID %>", "<%=txtrelation.ClientID %>", "<%=txtndob.ClientID %>", "<%=txtadd.ClientID %>", "<%=txttempaddress.ClientID %>", "<%=txtwph1.ClientID %>", "<%=txtwph2.ClientID %>", "<%=txtmph1.ClientID %>", "<%=txtemail.ClientID %>");
           }
           else if (hdf == "Hr.Asst") {
               var objs = new Array("<%=txtfname.ClientID %>", "<%=txtlname.ClientID %>", "<%=txtempdob.ClientID %>", "<%=ddlsex.ClientID %>", "<%=ddlmartialst.ClientID %>", "<%=txtnomineename.ClientID %>", "<%=txtrelation.ClientID %>", "<%=txtndob.ClientID %>", "<%=txtadd.ClientID %>", "<%=txttempaddress.ClientID %>", "<%=txtwph1.ClientID %>", "<%=txtwph2.ClientID %>", "<%=txtmph1.ClientID %>", "<%=txtbirthplace.ClientID %>", "<%=txtemail.ClientID %>", "<%=ddlrole.ClientID %>", "<%=txtjdate.ClientID %>", "<%=ddldepartment.ClientID %>", "<%=ddlcategory.ClientID %>", "<%=txtcomment.ClientID %>");
           }
           else {
              
                   var objs = new Array("<%=txtfname.ClientID %>", "<%=txtlname.ClientID %>", "<%=txtempdob.ClientID %>", "<%=ddlsex.ClientID %>", "<%=ddlmartialst.ClientID %>", "<%=txtnomineename.ClientID %>", "<%=txtrelation.ClientID %>", "<%=txtndob.ClientID %>", "<%=txtadd.ClientID %>", "<%=txttempaddress.ClientID %>", "<%=txtwph1.ClientID %>", "<%=txtwph2.ClientID %>", "<%=txtmph1.ClientID %>", "<%=txtbirthplace.ClientID %>", "<%=txtemail.ClientID %>", "<%=ddlrole.ClientID %>", "<%=txtjdate.ClientID %>", "<%=ddldepartment.ClientID %>", "<%=ddlcategory.ClientID %>", "<%=txtroleid.ClientID %>", "<%=ddlstatus.ClientID %>", "<%=txtcomment.ClientID %>");
              
           }
           if (!CheckInputs(objs)) {
               return false;
           }
           if (hdf == "Chairman Cum Managing Director") {
                $find('<%=TabContainer1.ClientID%>').set_activeTabIndex(5);
                var objs = new Array( "<%=ddldepartment.ClientID %>", "<%=ddlcategory.ClientID %>", "<%=txtroleid.ClientID %>", "<%=ddlstatus.ClientID %>", "<%=txtcomment.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
           }
       }
       function minmax(value, min, max) {
           grid = document.getElementById("<%=grdqualification.ClientID %>");
           if (parseInt(value) < 0 || isNaN(value))
               return 0;
           else if (parseInt(value) > 100)
               grid.rows[grid.rows.length - 1].cells(5).children(0).value = "";
           else return value;
       }
       function validateEmail(emailField) {
           var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

           if (reg.test(emailField.value) == false) {
               alert('Invalid Email Address');
               document.getElementById("<%=txtemail.ClientID %>").value = "";
           }

           return true;

       }
       function DvalidateEmail(emailField) {
           var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

           if (reg.test(emailField.value) == false) {
               alert('Invalid Email Address');
               document.getElementById("<%=txtdemail.ClientID %>").value = "";
           }

           return true;

       }
       function monthlybasic() {
           var basicy = document.getElementById("<%=txtbasicy.ClientID %>").value;
           var basicm = document.getElementById("<%=txtbasicm.ClientID %>").value;
           var grid = document.getElementById("<%=grdsalarybreakup.ClientID %>");
           var monthlygross=0;
           var yearlygross=0;
           if (basicy != "") {
               basicm = basicy / 12;
               document.getElementById("<%=txtbasicm.ClientID %>").value = basicm.toFixed(2);


               for (var i = 1; i < grid.rows.length - 2; i++) {
                   var id = grid.rows(i).cells(0).children(0).value;

                   grid.rows(i).cells(5).children(0).value = Math.round(eval((parseFloat(basicy) * parseFloat(grid.rows(i).cells(3).children(0).value)) / 100) * Math.pow(10, 2)) / Math.pow(10, 2);
                   grid.rows(i).cells(4).children(0).value = Math.round(eval((parseFloat(basicm) * parseFloat(grid.rows(i).cells(3).children(0).value)) / 100) * Math.pow(10, 2)) / Math.pow(10, 2);
                   yearlygross = yearlygross + Math.round(eval((parseFloat(basicy) * parseFloat(grid.rows(i).cells(3).children(0).value)) / 100) * Math.pow(10, 2)) / Math.pow(10, 2);
                   monthlygross = monthlygross + Math.round(eval((parseFloat(basicm) * parseFloat(grid.rows(i).cells(3).children(0).value)) / 100) * Math.pow(10, 2)) / Math.pow(10, 2);
               }
               document.getElementById("<%=txtgrossm.ClientID %>").value = Math.round(eval(parseFloat(monthlygross) + parseFloat(basicm)) * Math.pow(10, 2)) / Math.pow(10, 2);

               document.getElementById("<%=txtgrossy.ClientID %>").value = Math.round(eval(parseFloat(yearlygross) + parseFloat(basicy)) * Math.pow(10, 2)) / Math.pow(10, 2);



           }
           else {
               document.getElementById("<%=txtbasicm.ClientID %>").value = "";
               document.getElementById("<%=txtgrossm.ClientID %>").value = "";
               document.getElementById("<%=txtgrossy.ClientID %>").value = "";
               for (var i = 1; i < grid.rows.length - 1; i++) {
                   grid.rows(i).cells(5).children(0).value = "";
                   grid.rows(i).cells(4).children(0).value = "";
               }
           }
       }

       function clicked() {
           if (document.getElementById("<%=chkpermanent.ClientID %>").checked) {
               document.getElementById("<%=txttempaddress.ClientID %>").value = document.getElementById("<%=txtadd.ClientID %>").value;
           }
           else {
               document.getElementById("<%=txttempaddress.ClientID %>").value = "";
           }
       }
       function checkcomponents() {
           var yearlybasic = document.getElementById("<%=txtbasicy.ClientID %>").value;
           var monthlybasic = document.getElementById("<%=txtbasicm.ClientID %>").value;
          
           var grid = document.getElementById("<%=grdsalarybreakup.ClientID %>");
           var comval = grid.rows[grid.rows.length - 1].cells(1).children(0).selectedIndex;
           var com = grid.rows[grid.rows.length - 1].cells(1).children(0);
           if (yearlybasic == "") {
               alert("Please Enter Yearly Basic");
               document.getElementById("<%=txtbasicy.ClientID %>").focus();
               grid.rows[grid.rows.length - 1].cells(1).children(0).selectedIndex = 0;
               return false;
           }
           else if (monthlybasic == "") {
               alert("Please Enter Monthly Basic");
               document.getElementById("<%=txtbasicm.ClientID %>").focus();
               grid.rows[grid.rows.length - 1].cells(1).children(0).selectedIndex = 0;
               return false;
           }
           for (var i = 1; i < grid.rows.length - 1; i++) {
               var id = grid.rows(i).cells(0).children(0).value;
               if (comval == id) {
                   alert("You Already Added this Item");
                   grid.rows[grid.rows.length - 1].cells(1).children(0).selectedIndex = 0;
                   return false;
               }
               if (grid.rows[grid.rows.length - 1].cells(1).children(0).value == "12") {
                   grid.rows[grid.rows.length - 1].cells(2).children(0).children.ctl00$ContentPlaceHolder1$TabContainer1$TabPanelSalary$grdsalarybreakup$ctl03$chkpermission.checked = true;
                   grid.rows[grid.rows.length - 1].cells(3).children(0).value = document.getElementById("<%=pfemployerper.ClientID %>").value;
                   grid.rows[grid.rows.length - 1].cells(5).children(0).value = Math.round(eval((parseFloat(yearlybasic) * parseFloat(document.getElementById("<%=pfemployerper.ClientID %>").value)) / 100) * Math.pow(10, 2)) / Math.pow(10, 2);
                   grid.rows[grid.rows.length - 1].cells(4).children(0).value = Math.round(eval((parseFloat(monthlybasic) * parseFloat(document.getElementById("<%=pfemployerper.ClientID %>").value)) / 100) * Math.pow(10, 2)) / Math.pow(10, 2);
                   return false;
               }

           }

       }
       function calculate(value) {
           var grd = document.getElementById("<%=grdsalarybreakup.ClientID %>");
           var basicy = document.getElementById("<%=txtbasicy.ClientID %>").value;
           var basicm = document.getElementById("<%=txtbasicm.ClientID %>").value;
           var mnth = grd.rows[grd.rows.length - 1].cells(4).children(0).value;
           var year = grd.rows[grd.rows.length - 1].cells(5).children(0).value;
           var perc = grd.rows[grd.rows.length - 1].cells(3).children(0).value;
           if (perc != "") {
               mnth = basicm * (perc / 100);
               year = basicy * (perc / 100);
               grd.rows[grd.rows.length - 1].cells(4).children(0).value = mnth.toFixed(2);
               grd.rows[grd.rows.length - 1].cells(5).children(0).value = year.toFixed(2);
           }

       }
   </script>
   <script language="javascript">
       function Select(Check,type,id) {
           var GridView2 = document.getElementById("<%=grdfamilydetails.ClientID %>");
           var GridView1 = document.getElementById("<%=grdchildren.ClientID %>");


           var i = 0;
           var j = 0;
           if (type == "1") {
               for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {

                   if (GridView1.rows(rowCount).cells(6).children(0) != null) {
                       if (GridView1.rows(rowCount).cells(6).children(0).checked == true) {
                           GridView1.rows(rowCount).cells(6).children(0).checked = false;
                       } 
                   }
               }
               for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {

                   if (GridView2.rows(rowCount).cells(8).children(0) != null) {
                       if (GridView2.rows(rowCount).cells(8).children(0).checked == true && id == i) {
                           document.getElementById('<%= txtnomineename.ClientID%>').value = GridView2.rows(rowCount).cells(1).children(0).value;
                           document.getElementById('<%= txtrelation.ClientID%>').value = GridView2.rows(rowCount).cells(5).children(0).value;
                           document.getElementById('<%= txtndob.ClientID%>').value = GridView2.rows(rowCount).cells(2).children(0).value;
                           document.getElementById('<%= txtnage.ClientID%>').value = GridView2.rows(rowCount).cells(3).children(0).value;


                       }
                       else {
                           GridView2.rows(rowCount).cells(8).children(0).checked = false;

                       }
                       i = i + 1;
                   }
               }
           }
           if (type == "2") {
               for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {

                   if (GridView2.rows(rowCount).cells(8).children(0) != null) {
                       if (GridView2.rows(rowCount).cells(8).children(0).checked == true) {
                           GridView2.rows(rowCount).cells(8).children(0).checked = false;

                       }
                   } 
               }
               for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {

                   if (GridView1.rows(rowCount).cells(6).children(0) != null) {
                       if (GridView1.rows(rowCount).cells(6).children(0).checked == true && id == j) {
                           document.getElementById('<%= txtnomineename.ClientID%>').value = GridView1.rows(rowCount).cells(1).children(0).value;
                           if (GridView1.rows(rowCount).cells(4).children(0).value == "Male") {
                               document.getElementById('<%= txtrelation.ClientID%>').value = "Son";
                           }
                           else if (GridView1.rows(rowCount).cells(4).children(0).value == "Female") {
                               document.getElementById('<%= txtrelation.ClientID%>').value = "Daughter";
                           }

                           document.getElementById('<%= txtndob.ClientID%>').value = GridView1.rows(rowCount).cells(2).children(0).value;
                           document.getElementById('<%= txtnage.ClientID%>').value = GridView1.rows(rowCount).cells(3).children(0).value;


                       }
                       else {
                           GridView1.rows(rowCount).cells(6).children(0).checked = false;

                       }
                       j = j + 1;
                   }
               }
           }

       }
    </script>
     </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="1050px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <Hr:Menu ID="ww" runat="server" />
            </td>
            <td>
               
                    <asp:HiddenField ID="pfemployerper" runat="server" />
                <table width="100%">
                    <tr id="trempjoin" runat="server" align="center">
                   
                        <td>
                          
                            <div id="Div1" class="notebook" style="display: block;">
                                <div class="notebook-tabs">
                                    <div class="right scroller">
                                    </div>
                                    <div class="left scroller">
                                    </div>
                                    <ul class="notebook-tabs-strip">
                                        <li class="notebook-tab notebook-page notebook-tab-active" title="" id="Li2"><span
                                            class="tab-title"><span>Employee Details</span></span></li><li class="notebook-tab notebook-page"
                                                title="" style="display: none;"><span class="tab-title"><span></span></span>
                                        </li>
                                    </ul>
                                </div>
                                <div class="notebook-pages">
                                    <div class="notebook-page notebook-page-active">
                                        <div>
                                        <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                                            <table width="75%" style="height: 75px">
                                                <tbody>
                                                    <tr id="joiningcategory" runat="server"  >
                                                        <td align="right">
                                                            <asp:Label ID="Label43" runat="server" ForeColor="Red">*</asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Text="Joining Category : " Font-Bold="true"
                                                                Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddljcat" Width="120px" runat="server" CssClass="selection selection readonlyfield">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Staff</asp:ListItem>
                                                                <asp:ListItem>Labour</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="Joiningtype" runat="server" >
                                                        <td align="right">
                                                            <asp:Label ID="Label44" runat="server" ForeColor="Red">*</asp:Label>
                                                            <asp:Label ID="lblempstatus" runat="server" Text="Joining Type : " Font-Bold="true"
                                                                Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlempstatus" Width="120px" AutoPostBack="true" runat="server"
                                                                OnSelectedIndexChanged="ddlempstatus_SelectedIndexChanged" CssClass="selection selection readonlyfield">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>New</asp:ListItem>
                                                                <asp:ListItem>ReJoin</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                                                                    <td align="right">
                                                                                                        <asp:Label ID="Label20" runat="server" ForeColor="Red">*</asp:Label>
                                                            <asp:Label ID="Label21" runat="server" Text="Category" Font-Bold="true"
                                                                Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td  align="left">
                                                                                                        <asp:DropDownList ID="ddlcategory"  Width="120px" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                            ToolTip="Category">
                                                                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                            <asp:ListItem Value="Directors" Text="Directors">
                                                                                                            </asp:ListItem>
                                                                                                            <asp:ListItem Value="Senior Management Staff" Text="Senior Management Staff">
                                                                                                            </asp:ListItem>
                                                                                                            <asp:ListItem Value="Management Staff" Text="Management Staff">
                                                                                                            </asp:ListItem>
                                                                                                            <asp:ListItem Value="Contract Management Staff" Text="Contract Management Staff">
                                                                                                            </asp:ListItem>
                                                                                                            <asp:ListItem Value="Dummy Employees" Text="Dummy Employees">
                                                                                                            </asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                  <tr id="appointtype" runat="server">
                                                                                                    <td align="right">
                                                                                                        <asp:Label ID="Label51" runat="server" ForeColor="Red">*</asp:Label>
                                                            <asp:Label ID="Label52" runat="server" Text="Appointment type" Font-Bold="true"
                                                                Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td  align="left">
                                                                                                        <asp:DropDownList ID="ddlappointmenttype"  Width="120px" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                            ToolTip="Category">
                                                                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                            <asp:ListItem Value="Direct" Text="Direct">
                                                                                                            </asp:ListItem>
                                                                                                            <asp:ListItem Value="Normal" Text="Normal">
                                                                                                            </asp:ListItem>
                                                                                                            
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>              
                                                    <tr id="troldid" runat="server" align="right">
                                                        <td>
                                                            <asp:Label ID="Label45" runat="server" ForeColor="Red">*</asp:Label>
                                                            <asp:Label ID="lbloldid" runat="server" Text="Old Employee Id : " Font-Bold="true"
                                                                Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddloldid" Width="120px" runat="server" CssClass="selection selection readonlyfield">
                                                            </asp:DropDownList>
                                                            <%-- <asp:TextBox ID="txtoldid" CssClass="char" Width="120px" runat="server"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                
                                                   
                                                </tbody>
                                            </table>
                                            </ContentTemplate>
              <Triggers>
              <asp:AsyncPostBackTrigger ControlID="ddlempstatus" />
                
              </Triggers>
                </asp:UpdatePanel>

               
                                                            <asp:Button ID="btnview" Width="80px" Height="20px" CssClass="button" runat="server"
                                                                OnClientClick="validation()" Text="Submit" OnClick="btnview_Click" />
                                                       
                                        </div>
                                    </div>
                                </div>
                            </div>
                              
                       
                            </td>                      
                    </tr>
                  
                    <tr id="trdetails" runat="server">
                        <td>
                            <div class="wrap">
                                <table class="view" cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" colspan="2" bgcolor="#CCCCCC" height="25px">
                                            <asp:Label ID="lblheading" runat="server" Text="Employee Registration Form" CssClass="label"
                                                Font-Bold="True" Font-Size="Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;" align="center">
                                        <td valign="top" class=" item-group" width="100%">
                                            <table border="0" class="fields" align="center">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" class=" item-group" align="left" style="width: 450px">
                                                            <table border="0" class="fields" width="200px">
                                                                <tr align="center">
                                                                    <td class="label" width="1%" align="center">
                                                                        <asp:Label ID="Label1" runat="server" ForeColor="Red">*</asp:Label>
                                                                        <asp:Label ID="lblccode" runat="server" Text="First Name :" CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" align="left">
                                                                        <asp:TextBox ID="txtfname" runat="server" CssClass="char" Width="300px" ToolTip="First Name">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td class="label" width="1%" align="left">
                                                                        <asp:Label ID="Label11" runat="server" ForeColor="Red">*</asp:Label>
                                                                        <asp:Label ID="Label5" runat="server" Text="Last Name :" CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" align="left">
                                                                        <asp:TextBox ID="txtlname" runat="server" CssClass="char" Width="300px" ToolTip="Last Name">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td class="label" width="1%" align="left">
                                                                        <asp:Label ID="Label6" runat="server" Text="Middle Name :" CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" align="left">
                                                                        <asp:TextBox ID="txtmidddle" runat="server" CssClass="char" Width="300px" ToolTip="Middle Name">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="pwd" align="center" runat="server">
                                                                    <td class="label" width="1%" align="left">
                                                                        <asp:Label ID="Label4" runat="server" ForeColor="Red">*</asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text="Date of Birth :" CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-selection" valign="middle" align="left">
                                                                        <asp:TextBox ID="txtempdob" runat="server" CssClass="char" Width="190px" ToolTip="Date of Birth"
                                                                            onKeyDown="preventBackspace()" onKeyPress="return false;">
                                                                        </asp:TextBox>
                                                                        <asp:TextBox ID="txtempage" runat="server" Width="2px" ToolTip="Employee Age" Height="20px">
                                                                        </asp:TextBox>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="       Age"
                                                                            Enabled="false" WatermarkCssClass="watermarked" TargetControlID="txtempage" runat="server">
                                                                        </cc1:TextBoxWatermarkExtender>
                                                                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtempdob"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            OnClientDateSelectionChanged="Dateheck" PopupButtonID="txtempdob">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <%-- <td class="label" width="1%" align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="Confirm Password :" CssClass="char"></asp:Label>
                                                        </td>
                                                        <td class="item item-selection" valign="middle">
                                                            <asp:TextBox ID="txtcpwd" runat="server" CssClass="char" ToolTip="Confirm Password"
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>--%>
                                                                </tr>
                                                                <tr id="uid" runat="server" align="center">
                                                                    <td class="label" width="1%" align="center">
                                                                        <asp:Label ID="Label8" runat="server" ForeColor="Red">*</asp:Label>
                                                                        <asp:Label ID="Label2" runat="server" Text="Sex:" CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" align="left">
                                                                        <asp:DropDownList ID="ddlsex" runat="server" Width="150px" CssClass="selection selection_search readonlyfield"
                                                                            ToolTip="Sex">
                                                                            <asp:ListItem Value="Select" Text="Select">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="Male" Text="Male">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="Female" Text="Female">
                                                                            </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label" width="1%" align="center">
                                                                        <asp:Label ID="lblred" runat="server" ForeColor="Red">*</asp:Label>
                                                                        <asp:Label ID="lblmstatus" runat="server" Text="Martial Status:" CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" align="left">
                                                                        <asp:DropDownList ID="ddlmartialst" runat="server" CssClass="selection selection_search readonlyfield"
                                                                          Width="150px" ToolTip="Martial Status"  AutoPostBack="true" 
                                                                            onselectedindexchanged="ddlmartialst_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Select" Value="Select">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Single" Value="Single">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Married" Value="Married">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Divorced" Value="Divorced">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Widower" Value="Widower">
                                                                            </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                   <tr id="TrDom" runat="server">
                                                                                                    <td class="label" width="1%">
                                                                                                        <label for="passport_id">
                                                                                                            Date of Marriage
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdom" runat="server" Width="150px" CssClass="char" TabIndex="8"
                                                                                                            ToolTip="Marriage Date" onKeyPress="return false;" onKeyDown="preventBackspace()"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtdom"
                                                                                                            TargetControlID="txtdom">
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top" class=" item-group" align="right" style="width: 450px">
                                                        
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%">
                                            <div style="height: 500px; padding-top: 5px; padding-left: 20px;" align="left">
                                                <asp:TextBox ID="txtact" runat="server" Style="display: none" Text="0">
                                                </asp:TextBox>
                                                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="900px"
                                                    CssClass="ajax__tab_opera-theme" Height="500px">
                                                    <cc1:TabPanel runat="server" ID="tab1" HeaderText="Personal Info">
                                                        <HeaderTemplate>
                                                            Personal Info</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <div class="notebook-pages">
                                                                    <div class="notebook-page notebook-page-active">
                                                                        <table width="750px" border="0" align="center">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="2" style="border-bottom: 1px solid #525254;" valign="middle" width="90%">
                                                                                        <div class="separator horizontal" style="border: none; font-size: 10pt">
                                                                                            Family Details</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:UpdatePanel ID="updfamilydetails" runat="server" UpdateMode="Conditional">
                                                                                            <contenttemplate>
                                                                                                <asp:UpdateProgress ID="UpdateProgress16" runat="server" AssociatedUpdatePanelID="updfamilydetails">
                                                                                                    <ProgressTemplate>
                                                                                                        <div id="Div5">
                                                                                                            <div id="Div6">
                                                                                                                <div id="Div7">
                                                                                                                    <img alt="" src="../images/load.gif" />Loading.. Please Wait...
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                               <asp:GridView ID="grdfamilydetails" runat="server" AutoGenerateColumns="False" BorderColor="White"
        CssClass="grid-content" PageSize="1" ShowFooter="True" Width="750px" DataKeyNames="id"  OnRowDataBound="grdfamilydetails_RowDataBound" OnRowDeleting="grdfamilydetails_RowDeleting">
        <columns>
                                                                                                        <asp:BoundField DataField="id" Visible="False" />
                                                                                                        <asp:TemplateField HeaderText="S.No">
                                                                                                            <ItemTemplate>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle BackColor="White" />
                                                                                                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Name">
                                                                                                            <ItemTemplate>
                                                                                                             
                                                                                                                <asp:TextBox ID="txthname" runat="server" ReadOnly="true"  CssClass="char" TabIndex="1" ToolTip="Name" Text=  '<%#Eval("Name")%>'></asp:TextBox>
                                                                                                               </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtname" runat="server" CssClass="char" TabIndex="1" ToolTip="Name"></asp:TextBox>
                                                                                                            </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Date of bitrh">
                                                                                                            <ItemTemplate>
                                                                                                           
                                                                                                               <asp:TextBox ID="txthdob" runat="server" CssClass="char"  ReadOnly="True" onKeyDown="preventBackspace()"
                                                                                                            TabIndex="2" ToolTip="DOB" Width="125px" onKeyPress="return false;" Text=  '<%#Eval("Dob")%>'></asp:TextBox>
                                                                                                              </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                  <asp:TextBox ID="txtfdob" runat="server" CssClass="char" onKeyDown="preventBackspace()"
                                                                                                            TabIndex="2" ToolTip="DOB" Width="125px" onKeyPress="return false;"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender11" runat="server" CssClass="cal_Theme1"
                                                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="GetFamilyage"
                                                                                                            PopupButtonID="txtfdob" TargetControlID="txtfdob">
                                                                                                        </cc1:CalendarExtender>
                                                                                                               </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                                                                        </asp:TemplateField>
                                                                                                         <asp:TemplateField HeaderText="Age">
                                                                        <ItemTemplate>
                                                                         
                                                                           <asp:TextBox ID="txthage" MaxLength="25" runat="server"  ReadOnly="True" Width="100%" Style="border: None" Text=  '<%#Eval("Age")%>'></asp:TextBox>
                                                                          </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtfage" MaxLength="25" runat="server" Width="100%" Text="" Style="border: None"></asp:TextBox></FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gender">
                                                                        <ItemTemplate>
                                                                     
                                                                                                                                                       <asp:TextBox ID="txthgender" ReadOnly="True" runat="server" Width="100%"  Style="border: None"  Text=  '<%#Eval("Gender")%>'></asp:TextBox>

                                                                         <%--  <asp:DropDownList ID="ddlhgender" runat="server" Width="100%" SelectedValue='<%# Bind("Gender") %>' >
                                                                                <asp:ListItem  Value="">Select</asp:ListItem>
                                                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                            </asp:DropDownList>--%>
                                                                          </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlfgender" runat="server" Width="100%"  OnSelectedIndexChanged="ddlfgender_SelectedIndexChanged"  AutoPostBack="true" >
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                                <asp:ListItem>Male</asp:ListItem>
                                                                                <asp:ListItem>Female</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Relationship">
                                                                        <ItemTemplate>
                                                                  
                                                                               <asp:TextBox ID="txthrelation" ReadOnly="True" runat="server" Width="100%" Style="border: None"  Text=  '<%#Eval("relation")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlRelation" runat="server" CssClass="selection selection_search readonlyfield"  
                                                                                                            ToolTip="Relation">
                                                                                                          <%--  <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Wife" Value="Wife"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Father" Value="Father"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Mother" Value="Mother"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Son" Value="Son"></asp:ListItem>
                                                                                                             <asp:ListItem Text="Daughter" Value="Daughter"></asp:ListItem>--%>
                                                                                                        </asp:DropDownList>
                                                                                                          
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Work">
                                                                        <ItemTemplate>
                                                                       
                                                                               <asp:TextBox ID="txthwno1" runat="server" CssClass="char" ReadOnly="True" onkeypress="javascript:return IsNumeric(event);"
                                                                                                            TabIndex="17" ToolTip="Land Line No" Width="145px"  Text=  '<%#Eval("Workno")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                        <FooterTemplate>
                                                                      
                                                                                                        <asp:TextBox ID="txtfwno1" runat="server" CssClass="char"  onkeypress="javascript:return IsNumeric(event);"
                                                                                                            TabIndex="17" ToolTip="Land Line No" Width="145px"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Mobile No">
                                                                        <ItemTemplate>
                                                                         
                                                                              <asp:TextBox
                                                                                                                ID="txthmobno" runat="server" ReadOnly="True" TabIndex="18" ToolTip="Mobile No" Style="width: 160px"
                                                                                                                CssClass="char" onkeypress="javascript:return IsNumeric(event);" Text=  '<%#Eval("Mobileno")%>'></asp:TextBox> 
                                                                                </ItemTemplate>
                                                                        <FooterTemplate>
                                                                         
                                                                                                            <asp:TextBox
                                                                                                                ID="txtfbobno" runat="server" TabIndex="18" ToolTip="Mobile No" Style="width: 160px"
                                                                                                                CssClass="char" MaxLength="10" onkeypress="javascript:return IsNumeric(event);"></asp:TextBox></span>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                         <asp:CheckBox ID="chkSelect" runat="server"   />
                                                                                                                                                           
                                                                          </ItemTemplate>
                                                                       
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                           <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" Width="60px" CssClass="button"
                                                                                CommandName="Delete" /></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnfamilyadd" runat="server" Text="Add" Width="60px" CssClass="button" OnClick="btnfamilyadd_Click" OnClientClick="return Fmailyfootervalidation()"
                                                                             /></FooterTemplate>
                                                                    </asp:TemplateField>
                                                                                                    </columns>
        <rowstyle cssclass=" grid-row char grid-row-odd"></rowstyle>
        <pagerstyle cssclass="grid pagerbar"></pagerstyle>
        <headerstyle cssclass="grid-header"></headerstyle>
        <alternatingrowstyle cssclass="grid-row grid-row-even"></alternatingrowstyle>
    </asp:GridView>
                                                                                            </contenttemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border-bottom: 1px solid #525254;" valign="middle" width="100%" colspan="2">
                                                                                        <div class="separator horizontal" style="border: none; font-size: 10pt">
                                                                                            Children Details</div>
                                                                                    </td>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <asp:UpdatePanel ID="updchildren" runat="server" UpdateMode="Conditional">
                                                                                                <contenttemplate>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updchildren">
                                                                <ProgressTemplate>
                                                                    <div id="Div2">
                                                                        <div id="Div3">
                                                                            <div id="Div4">
                                                                                <img alt="" src="../images/load.gif" />Loading.. Please Wait...
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:GridView ID="grdchildren" Width="750px" runat="server" AutoGenerateColumns="False"
                                                                OnRowDataBound="grdchildren_RowDataBound" CssClass="grid-content" BorderColor="White"
                                                                ShowFooter="True" PageSize="1" DataKeyNames="id" OnRowDeleting="grdchildren_RowDeleting">
                                                                <Columns>
                                                                    <asp:BoundField DataField="id" Visible="False" />
                                                                    <asp:TemplateField HeaderText="S.No">
                                                                        <ItemTemplate>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BackColor="White" />
                                                                        <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtname" ReadOnly="True"  runat="server" Width="100%" Text='<%#Eval("Children_name")%>'
                                                                                Style="border: None"></asp:TextBox></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtfname1" MaxLength="25" runat="server" Width="100%" Text="" Style="border: None"></asp:TextBox></FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date of birth">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdob" ReadOnly="True" runat="server" Width="100%" Text='<%#Eval("Children_dob")%>'
                                                                                Style="border: None"></asp:TextBox></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtfdob" MaxLength="25" onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                runat="server" Width="100%" Text="" Style="border: None"></asp:TextBox><cc1:CalendarExtender
                                                                                    ID="CalendarExtender9" runat="server" TargetControlID="txtfdob" CssClass="cal_Theme1" OnClientDateSelectionChanged="GetChildernsAge"
                                                                                    Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true" PopupButtonID="txftdob">
                                                                                </cc1:CalendarExtender>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Age">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtage" ReadOnly="True" runat="server" Width="100%" Text='<%#Eval("Age")%>'
                                                                                Style="border: None"></asp:TextBox></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtfage" MaxLength="25" runat="server" Width="100%" Text="" Style="border: None"></asp:TextBox></FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gender">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgender" ReadOnly="True" runat="server" Width="100%" Text='<%#Eval("Gender")%>'
                                                                                Style="border: None"></asp:TextBox></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlfgender" runat="server" Width="100%">
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                                <asp:ListItem>Male</asp:ListItem>
                                                                                <asp:ListItem>Female</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Marital Status">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtmstatus" ReadOnly="True" runat="server" Width="100%" Text='<%#Eval("Martial_status")%>'
                                                                                Style="border: None"></asp:TextBox></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlfmartialstatus" runat="server" Width="100%">
                                                                                <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                                                                                <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
                                                                                <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                                                                                <asp:ListItem Value="Divorced" Text="Divorced"></asp:ListItem>
                                                                                <asp:ListItem Value="Widower" Text="Widower"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                         <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                           
                                                                          </ItemTemplate>
                                                                       
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" Width="60px" CssClass="button"
                                                                                CommandName="Delete" /></ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnadd" runat="server" Text="Add" Width="60px" CssClass="button"
                                                                                OnClick="btnadd_Click" OnClientClick="return Childernfootervalidation()" /></FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                <HeaderStyle CssClass="grid-header" />
                                                            </asp:GridView>
                                                        </contenttemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" width="45%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td colspan="2" style="border-bottom: 1px solid #525254;" valign="middle" width="90%">
                                                                                                        <div class="separator horizontal" style="border: none; font-size: 10pt">
                                                                                                            Nominee Details</div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label102" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                            Nominee
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtnomineename" runat="server" CssClass="char" TabIndex="20" ToolTip="Nominee"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                      <asp:Label ID="Label1021" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label class="help" for="ssnid">
                                                                                                            Date of Birth
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtndob" runat="server" CssClass="char" onKeyDown="preventBackspace()"
                                                                                                            TabIndex="2" ToolTip="DOB" Width="125px" onKeyPress="return false;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top" width="55%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td colspan="2" style="border-bottom: 1px solid #525254;" valign="middle" width="100%">
                                                                                                        <div class="separator horizontal" style="font-size: 10pt; border: none; visibility: hidden">
                                                                                                            Nominee Details
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label14" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="coach_id_text">
                                                                                                            Relation
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtrelation" runat="server" CssClass="char" TabIndex="21" ToolTip="Relation"
                                                                                                            Width="180px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                      <asp:Label ID="Label1023" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label class="help" for="ssnid">
                                                                                                            Age
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtnage" runat="server" Height="22px" TabIndex="3" Width="2px"></asp:TextBox>
                                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" Enabled="False"
                                                                                                            TargetControlID="txtnage" WatermarkCssClass="watermarked" WatermarkText="       Age">
                                                                                                        </cc1:TextBoxWatermarkExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" width="45%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class=" item-separator" colspan="2" style="border-bottom: 1px solid #525254;"
                                                                                                        valign="middle" width="100%">
                                                                                                        <div class="separator horizontal" style="font-size: 10pt; border: none">
                                                                                                            Contact Information</div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label19" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="address_home_id_text">
                                                                                                            Permanent Address
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtadd" runat="server" CssClass="char" MaxLength="100" TabIndex="14"
                                                                                                            TextMode="MultiLine" ToolTip="Permanent Address" Width="200px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                    <asp:Label ID="Label111" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_phone">
                                                                                                            Work Phone
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td align="left">
                                                                                                        <asp:TextBox ID="txtwph1" runat="server" MaxLength="5" onkeypress="javascript:return IsNumeric(event);"
                                                                                                            TabIndex="16" ToolTip="STD Code" Width="50px"></asp:TextBox>
                                                                                                        &nbsp;
                                                                                                        <asp:TextBox ID="txtwph2" runat="server" CssClass="char" MaxLength="8" onkeypress="javascript:return IsNumeric(event);"
                                                                                                            TabIndex="17" ToolTip="Land Line No" Width="145px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                    <asp:Label ID="Label110" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="mobile_phone">
                                                                                                            Place Of Birth
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td align="left" class="item item-char">
                                                                                                        <asp:TextBox ID="txtbirthplace" runat="server" CssClass="char" TabIndex="19" ToolTip="Birth Place"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top" width="55%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class=" item-separator" colspan="2" style="border-bottom: 1px solid #525254;"
                                                                                                        valign="middle" width="100%">
                                                                                                        <div class="separator horizontal" style="font-size: 10pt; border: none; visibility: hidden;">
                                                                                                            Position</div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center" class="label" width="1%">
                                                                                                        <asp:CheckBox ID="chkpermanent" runat="server" onclick="clicked()" ToolTip="Same As Permanent Address" />
                                                                                                        <asp:Label ID="Label12" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <asp:Label ID="lbldca" runat="server" CssClass="char" Text="Present Address:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txttempaddress" runat="server" CssClass="char" MaxLength="100" TabIndex="15"
                                                                                                            TextMode="MultiLine" ToolTip="Present Address" Width="180px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center" class="label" width="1%">
                                                                                                        <asp:Label ID="Label15" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <asp:Label ID="Label7" runat="server" CssClass="char" Text="Mobile:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <span style="border: 1px solid #d8d8d8; font-size: small;">
                                                                                                            <asp:Label ID="Lbl91" runat="server" Font-Size="Small" Height="5px" Text=" +91"></asp:Label>
                                                                                                            <asp:TextBox ID="txtmph1" runat="server" CssClass="char" MaxLength="10" onkeypress="javascript:return IsNumeric(event);"
                                                                                                                Style="width: 160px" TabIndex="18" ToolTip="Mobile No"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                    <asp:Label ID="Label112" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="job_id_text">
                                                                                                            Work E-mail
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtemail" runat="server" CssClass="char" onblur="validateEmail(this);"
                                                                                                            TabIndex="19" ToolTip="Email id" Width="180px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="2" style="height: 35px" valign="top">
                                                                                        <table border="0">
                                                                                            <tbody>
                                                                                                <tr align="center" style="height: 30px">
                                                                                                    <td align="left">
                                                                                                        <input id="btnnext" class="button" value="      Next" onclick="return validate();"
                                                                                                            align="middle" size="20em" style="font-size: medium; height: 17px; width: 71px"
                                                                                                            readonly="readonly" /><asp:HiddenField ID="hf1" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel runat="server" ID="TabPanel1" HeaderText="Qualification Info">
                                                        <HeaderTemplate>
                                                            Qualification Details</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <div class="notebook-pages">
                                                                    <div class="notebook-page notebook-page-active">
                                                                        <table border="0" align="center" width="750px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="border-bottom: 1px solid #525254;" valign="middle" width="100%">
                                                                                        <div class="separator horizontal" style="border: none; font-size: 10pt">
                                                                                            Academic Qualification</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:UpdatePanel ID="updqualification" runat="server" UpdateMode="Conditional">
                                                                                            <contenttemplate>
                                                                                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updqualification">
                                                                                                    <ProgressTemplate>
                                                                                                        <div id="overlay">
                                                                                                            <div id="modalprogress">
                                                                                                                <div id="theprogress">
                                                                                                                    <img alt="" src="../images/load.gif" />Loading.. Please Wait...
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                                <asp:GridView ID="grdqualification" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                                                                                    CssClass="grid-content" OnRowDeleting="grdqualifications_RowDeleting" OnDataBound="grdqualification_DataBound"
                                                                                                    PageSize="1" ShowFooter="True" Width="750px" DataKeyNames="id" OnRowDataBound="grdqualification_RowDataBound">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id" Visible="False" />
                                                                                                        <asp:TemplateField HeaderText="S.No">
                                                                                                            <ItemTemplate>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle BackColor="White" />
                                                                                                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Class">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtclass" MaxLength="200" Enabled="false" runat="server" Width="100%"
                                                                                                                    Text='<%#Eval("Class")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:DropDownList ID="ddlfclass" runat="server" ToolTip="Degree" CssClass="selection selection_search readonlyfield"
                                                                                                                    Width="100%">
                                                                                                                </asp:DropDownList>
                                                                                                            </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Name of the University">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtnameunversty" runat="server" Enabled="false" MaxLength="200"
                                                                                                                    Style="border: None" Text='<%#Eval("University_Name")%>' Width="100%"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtfnameunversty" runat="server" MaxLength="200" Text="" Width="100%"></asp:TextBox></FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="From">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtfrom1" runat="server" Enabled="false" MaxLength="25" Style="border: None"
                                                                                                                    Text='<%#Eval("From")%>' onKeyDown="preventBackspace()" Width="100%"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtffrom1" runat="server" onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                                                    MaxLength="25" Width="100%"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender12"
                                                                                                                        runat="server" Animated="true" CssClass="cal_Theme1" FirstDayOfWeek="Monday"
                                                                                                                        Format="dd-MMM-yyyy" PopupButtonID="txtffrom1" TargetControlID="txtffrom1">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                            </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="To">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtto1" runat="server" Enabled="false" MaxLength="25" Style="border: None"
                                                                                                                    Text='<%#Eval("To")%>' onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                                                    Width="100%"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtfto1" runat="server" onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                                                    MaxLength="25" Text="" Width="100%"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender11"
                                                                                                                        runat="server" Animated="true" CssClass="cal_Theme1" FirstDayOfWeek="Monday"
                                                                                                                        Format="dd-MMM-yyyy" PopupButtonID="txtfto1" TargetControlID="txtfto1">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                            </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Percentage">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtpers" runat="server" Enabled="false" MaxLength="25" Style="border: None"
                                                                                                                    Text='<%#Eval("Percentage")%>' Width="100%"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtfpers" runat="server" MaxLength="25" Text="" Width="100%" onKeyUp="minmax(this.value, 0, 100)"
                                                                                                                    onkeypress="javascript:return IsNumeric(event);"></asp:TextBox></FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btndelete1" runat="server" CommandName="Delete" CssClass="button"
                                                                                                                    Text="Delete" Width="60px" /></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Button ID="btninsert1" runat="server" CssClass="button" OnClientClick="return Qualificationfootervalidation()"
                                                                                                                    OnClick="btninsert1_Click" Text="Add" Width="60px" /></FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                </asp:GridView>
                                                                                            </contenttemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" align="center" width="750px">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                 
                                                                                                                  <td style="border-bottom: 1px solid #525254;" valign="middle" width="100%">
                                                                                                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                        <div class="separator horizontal" style="border: none; font-size: 10pt">
                                                                                                            Technical Qualification     <asp:LinkButton ID="lnkbtn" runat="server" 
                                                                                                                Text="Add New Skill" onclick="lnkbtn_Click" 
                                                                                                             ></asp:LinkButton> </div>
                                                                                                                </ContentTemplate>

                                                                                                                </asp:UpdatePanel>
                                                                                                  
                                                                                                          
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="updTqualification" runat="server" UpdateMode="Conditional">
                                                                                                            <contenttemplate>
                                                                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updTqualification">
                                                                                                                    <ProgressTemplate>
                                                                                                                        <div id="overlay">
                                                                                                                            <div id="modalprogress">
                                                                                                                                <div id="theprogress">
                                                                                                                                    <img alt="" src="../images/load.gif" />Loading.. Please Wait...
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </ProgressTemplate>
                                                                                                                </asp:UpdateProgress>
                                                                                                                <asp:GridView ID="grdTqualifications" runat="server" AutoGenerateColumns="False"
                                                                                                                    OnDataBound="grdTqualifications_DataBound" BorderColor="White" CssClass="grid-content"
                                                                                                                    PageSize="1" ShowFooter="True" Width="750px" OnRowDeleting="grdTqualifications_RowDeleting"
                                                                                                                    OnRowDataBound="grdTqualifications_RowDataBound" DataKeyNames="id">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="id" Visible="False" />
                                                                                                                        <asp:TemplateField HeaderText="S.No">
                                                                                                                            <ItemTemplate>
                                                                                                                            </ItemTemplate>
                                                                                                                            <HeaderStyle BackColor="White" />
                                                                                                                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Technical Skills">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtskills" MaxLength="150" Enabled="false" runat="server" Width="100%"
                                                                                                                                    Text='<%#Eval("Technical_Skills")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:DropDownList ID="ddlfskills" runat="server" ToolTip="Degree" Width="100%">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Years Of Experience">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtexperience" MaxLength="150" Enabled="false" runat="server" Width="100%"
                                                                                                                                    Text='<%#Eval("experience")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:DropDownList ID="ddlexperience" runat="server" ToolTip="Degree" Width="100%">
                                                                                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                                                                                    <asp:ListItem>Fresher</asp:ListItem>
                                                                                                                                    <asp:ListItem>1-2 Years</asp:ListItem>
                                                                                                                                    <asp:ListItem>2-5 Years</asp:ListItem>
                                                                                                                                    <asp:ListItem> > 5 Years
                                                                                                                                    </asp:ListItem>
                                                                                                                                </asp:DropDownList>
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="450px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Button ID="btndelete2" runat="server" CommandName="Delete" CssClass="button"
                                                                                                                                    Text="Delete" /></ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:Button ID="btninsert2" runat="server" CssClass="button" OnClientClick="return TechnicalQualificationfootervalidation()"
                                                                                                                                    OnClick="btninsert2_Click" Text="Add" Width="100%" /></FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                    <PagerStyle CssClass="grid pagerbar" />
                                                                                                                    <HeaderStyle CssClass="grid-header" />
                                                                                                                </asp:GridView>
                                                                                                            </contenttemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" align="center" style="height: 35px">
                                                                                        <table border="0">
                                                                                            <tbody>
                                                                                                <tr align="center" style="height: 30px">
                                                                                                    <td align="left">
                                                                                                        <input id="btnback4" class="button" value="     Back" onclick="return validateback4();"
                                                                                                            align="middle" style="font-size: medium; height: 17px; width: 71px" readonly="readonly" /><input
                                                                                                                id="btnnext1" class="button" value="     Next" onclick="return footervalidationnext();"
                                                                                                                align="middle" style="font-size: medium; height: 17px; width: 71px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel runat="server" ID="tab3" HeaderText="Employment History">
                                                        <HeaderTemplate>
                                                            Employment History</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <div class="notebook-pages">
                                                                    <div class="notebook-page notebook-page-active">
                                                                        <table border="0" align="center" width="750px">
                                                                            <tbody>
                                                                                <tr id="trRadioButtonList1" runat="server" style="width: 100%">
                                                                                    <td id="Td1" align="left" style="height: 35px" valign="top" runat="server">
                                                                                        <table border="0" align="center">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellPadding="3" CellSpacing="2"
                                                                                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                                                            onchange="return rdbtnvalidation();">
                                                                                                            <asp:ListItem Text="Experience" Value="Yes">
                                                                                                            </asp:ListItem>
                                                                                                            <asp:ListItem Text="Fresher" Value="No" Selected="True">
                                                                                                            </asp:ListItem>
                                                                                                        </asp:RadioButtonList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trgrdhistory" runat="server">
                                                                                    <td id="Td2" runat="server">
                                                                                        <asp:UpdatePanel ID="updhistory" runat="server" UpdateMode="Conditional">
                                                                                            <contenttemplate>
                                                                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updhistory">
                                                                                                    <ProgressTemplate>
                                                                                                        <div id="overlay">
                                                                                                            <div id="modalprogress">
                                                                                                                <div id="theprogress">
                                                                                                                    <img alt="" src="../images/load.gif" />Loading.. Please Wait...
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                                <asp:GridView ID="grdhistory" Width="750px" runat="server" AutoGenerateColumns="False"
                                                                                                    OnRowDataBound="grdhistory_RowDataBound" CssClass="grid-content" BorderColor="White"
                                                                                                    ShowFooter="True" PageSize="1" OnRowDeleting="grdhistory_RowDeleting" DataKeyNames="id">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id" Visible="False" />
                                                                                                        <asp:TemplateField HeaderText="S.No">
                                                                                                            <ItemTemplate>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle BackColor="White" />
                                                                                                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Name of the Organisation">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtnameorg" MaxLength="200" Enabled="false" runat="server" Width="100%"
                                                                                                                    Text='<%#Eval("Organization_name")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtfnameorg" MaxLength="200" runat="server" Width="100%" Text=""></asp:TextBox></FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="From">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtfrom" MaxLength="25" Enabled="false" runat="server" Width="100%"
                                                                                                                    Text='<%#Eval("From")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtffrom" MaxLength="25" onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                                                    runat="server" Width="100%"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender12"
                                                                                                                        runat="server" TargetControlID="txtffrom" CssClass="cal_Theme1" Format="dd-MMM-yyyy"
                                                                                                                        FirstDayOfWeek="Monday" Animated="true" PopupButtonID="txtffrom">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                            </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="To">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtto" MaxLength="25" Enabled="false" runat="server" Width="100%"
                                                                                                                    Text='<%#Eval("To")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtfto" MaxLength="25" onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                                                    runat="server" Width="100%" Text=""></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender11"
                                                                                                                        runat="server" TargetControlID="txtfto" CssClass="cal_Theme1" Format="dd-MMM-yyyy"
                                                                                                                        FirstDayOfWeek="Monday" Animated="true" PopupButtonID="txtfto">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                            </FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Role/Designation">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtdesig" MaxLength="25" runat="server" Enabled="false" Width="100%"
                                                                                                                    Text='<%#Eval("Roledesignation")%>' Style="border: None"></asp:TextBox></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtfdesig" MaxLength="25" runat="server" Width="100%" Text=""></asp:TextBox></FooterTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="button" Width="60px"
                                                                                                                    CommandName="Delete" /></ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Button ID="btninsert" runat="server" Text="Add" CssClass="button" Width="60px"
                                                                                                                    OnClick="btninsert_Click" OnClientClick="return Historyfootervalidation()" /></FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                    <PagerStyle CssClass="grid pagerbar" />
                                                                                                    <HeaderStyle CssClass="grid-header" />
                                                                                                </asp:GridView>
                                                                                            </contenttemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top" style="height: 35px" align="center">
                                                                                        <table border="0">
                                                                                            <tbody>
                                                                                                <tr style="height: 30px" align="center">
                                                                                                    <td align="left">
                                                                                                        <input id="btnback1" class="button" value="     Back" 
                                                                                                            align="middle" style="font-size: medium; height: 17px; width: 71px" readonly="readonly" /><input
                                                                                                                id="btnnext3" class="button" value="     Next" readonly="readonly" onclick="return validateRadio();"
                                                                                                                align="middle" style="font-size: medium; height: 17px; width: 71px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel runat="server" ID="TabPanelSalary" HeaderText="Salary Structure">
                                                        <%--<HeaderTemplate>
                                                            Salary Structure
                                                        </HeaderTemplate>--%>
                                                        <ContentTemplate>
                                                            <div>
                                                                <div class="notebook-pages">
                                                                    <div class="notebook-page notebook-page-active">
                                                                        <table border="0" align="center" width="750px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table width="750px" align="center" style="border: 1px solid #000">
                                                                                            <tbody>
                                                                                                <tr style="width: 100%; border: 1px solid #000">
                                                                                                    <td align="center" bgcolor="#CCCCCC" style="table-layout: inherit;" width="100%">
                                                                                                        <asp:Label ID="lblrem" runat="server" Font-Bold="True" Font-Size="Small" Text="Remuneration"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="width: 100%">
                                                                                                    <td>
                                                                                                        <table width="450px">
                                                                                                            <tr>
                                                                                                                <td width="200px">
                                                                                                                    <asp:Label ID="lblbasic" runat="server" CssClass="label" Font-Bold="True" Font-Size="Small"
                                                                                                                        Text="Basic Amount:"></asp:Label>
                                                                                                                </td>
                                                                                                                <td width="50px">
                                                                                                                    <asp:TextBox ID="txtbasicy" runat="server" ToolTip="Yearly Basic Amount" onKeyUp="monthlybasic();"></asp:TextBox>
                                                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server"
                                                                                                                        Enabled="False" TargetControlID="txtbasicy" 
                                                                                                                        WatermarkCssClass="watermarked" WatermarkText="Yearly Basic">
                                                                                                                    </cc1:TextBoxWatermarkExtender>
                                                                                                                </td>
                                                                                                                <td width="50px">
                                                                                                                    <asp:TextBox ID="txtbasicm" runat="server" ToolTip="Monthly Basic Amount" onKeyDown="preventBackspace()"
                                                                                                                        onKeyPress="return false;"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="width: 700px">
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="updsalarybreakup" runat="server" UpdateMode="Conditional">
                                                                                                            <contenttemplate>
                                                                                                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updsalarybreakup">
                                                                                                                    <ProgressTemplate>
                                                                                                                        <div id="overlay">
                                                                                                                            <div id="modalprogress">
                                                                                                                                <div id="theprogress">
                                                                                                                                    <img alt="" src="../images/load.gif" />
                                                                                                                                    Loading.. Please Wait...
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </ProgressTemplate>
                                                                                                                </asp:UpdateProgress>
                                                                                                                <asp:GridView ID="grdsalarybreakup" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                                                                                                    OnRowDataBound="grdsalarybreakup_RowDataBound" CssClass="grid-content" OnRowDeleting="grdsalarybreakup_RowDeleting"
                                                                                                                    OnDataBound="grdsalarybreakup_DataBound" PageSize="1" ShowFooter="True" Width="750px"
                                                                                                                    DataKeyNames="id" OnSelectedIndexChanged="grdsalarybreakup_SelectedIndexChanged">
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField ItemStyle-Width="1px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:HiddenField ID="id" runat="server" Value='<%#Eval("id")%>' />
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtdescription" MaxLength="200" Enabled="false" runat="server" Width="100%"
                                                                                                                                    Text='<%#Eval("Component_Name")%>' Style="border: None"></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:DropDownList ID="ddlfdescription" runat="server" ToolTip="Description" CssClass="selection selection_search readonlyfield"
                                                                                                                                    Width="100%" onchange="checkcomponents()">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Check">
                                                                                                                            <ItemTemplate>
                                                                                                                            </ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<asp:CheckBox ID="chkpermission"
                                                                                                                                    onclick="ValidateCheck();" runat="server" Width="100%" />
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Percentage">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtpercentage" runat="server" Enabled="false" MaxLength="3" Style="border: None"
                                                                                                                                    Text='<%#Eval("Percentage")%>' Width="100%"></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:TextBox ID="txtfpercentage" runat="server" MaxLength="3" onkeypress="IsNumeric(event)"
                                                                                                                                    onkeyup="calculate(this.value)" Text="" Width="80%"></asp:TextBox>
                                                                                                                                <asp:Label ID="lblperc"
                                                                                                                                        runat="server" Text="%"></asp:Label>
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Monthly">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtmonthly" runat="server" Enabled="false" MaxLength="25" Style="border: None"
                                                                                                                                    Text='<%#Eval("Monthly")%>' Width="100%"></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:TextBox ID="txtfmonthly" runat="server" MaxLength="25" onkeypress="IsNumeric(event)"
                                                                                                                                    Width="100%"></asp:TextBox>
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Yearly">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtyearly" runat="server" Enabled="false" MaxLength="25" Style="border: None"
                                                                                                                                    Text='<%#Eval("Yearly")%>' Width="100%"></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:TextBox ID="txtfyearly" runat="server" MaxLength="25" Text="" Width="100%" onkeypress="IsNumeric(event)"></asp:TextBox>
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Button ID="btndeleterecord" runat="server" CommandName="Delete" CssClass="button"
                                                                                                                                    Text="Delete" Width="60px" />
                                                                                                                            </ItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                                                                                                <asp:Button ID="btninsertrecord" runat="server" CssClass="button" OnClientClick="return Total()"
                                                                                                                                    OnClick="btninsertrecord_Click" Text="Add" Width="60px" />
                                                                                                                            </FooterTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                </asp:GridView>
                                                                                                            </contenttemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                        <br />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr align="center">
                                                                                                    <td align="center" width="400px">
                                                                                                        <table align="center" width="400px">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td width="80px">
                                                                                                                    <asp:Label ID="lblmonthlygross" runat="server" Font-Bold="True" Font-Size="Small"
                                                                                                                        Text="Monthly"></asp:Label>
                                                                                                                </td>
                                                                                                                <td width="80px">
                                                                                                                    <asp:Label ID="lblyearlygross" runat="server" Font-Bold="True" Font-Size="Small"
                                                                                                                        Text="Yearly"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblgross" runat="server" Font-Bold="True" Font-Size="Small" Text="Gross Amount:"></asp:Label>
                                                                                                                </td>
                                                                                                                <td width="80px">
                                                                                                                    <asp:TextBox ID="txtgrossm" runat="server" onKeyPress="return false;" 
                                                                                                                        onKeyDown="preventBackspace()" Enabled="False"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="80px">
                                                                                                                    <asp:TextBox ID="txtgrossy" runat="server" onKeyPress="return false;" 
                                                                                                                        onKeyDown="preventBackspace()" Enabled="False"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                         
                                                                                             
                                                                                                <tr align="center">
                                                                                                  
                                                                                                    
                                                                                                          
                                                                                                          
                                                                                                              
                                                                                                    <td colspan="3" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                        <div class="separator horizontal" style="border: none" style="font-size: 10pt">
                                                                                                           Bank Details</div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label9" runat="server" ForeColor="Red">*</asp:Label><label for="ssnid"
                                                                                                            class="help">Bank Account Number:
                                                                                                        </label>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle" colspan="2">
                                                                                                        <asp:TextBox ID="txtacnumber" runat="server" onKeyDown="preventBackspace()"
                                                                                                            CssClass="char" ToolTip="Bank Account number"/>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                 <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label46" runat="server" ForeColor="Red">*</asp:Label><label for="gender">Bank
                                                                                                            Name
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle" colspan="2">
                                                                                                        <asp:TextBox ID="txtbankname" runat="server" onKeyDown="preventBackspace()" 
                                                                                                            CssClass="char" ToolTip="Bank Name"/>
                                                                                                       
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label10" runat="server" ForeColor="Red">*</asp:Label><label for="ssnid" 
                                                                                                            class="help">Bank Address:
                                                                                                        </label>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle" colspan="2">
                                                                                                         <asp:TextBox ID="txtbankaddress" runat="server" onKeyDown="preventBackspace()" 
                                                                                                            CssClass="char" ToolTip="Bank Account number"/>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                  <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <label for="gender">
                                                                                                           IFSC Code
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle" colspan="2">
                                                                                                         <asp:TextBox ID="txtifsccode" runat="server" onKeyDown="preventBackspace()" 
                                                                                                            CssClass="char" ToolTip="IFSC Code"/>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                
                                                                                           
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>

                                                                                             <tr>
                                                                                                    <td align="center" style="height: 35px" valign="top">
                                                                                                        <table border="0">
                                                                                                            <tbody>
                                                                                                                <tr align="center" style="height: 30px">
                                                                                                                    <td align="left">
                                                                                                                        <input id="btnback" class="button" value="     Back" onclick="return validatesalback();"
                                                                                                                            align="middle" style="font-size: medium; height: 17px; width: 71px" readonly="readonly" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                        <input id="btnnext4" class="button" value="     Next" onclick="return validate2();"
                                                                                                                            align="middle" style="font-size: medium; height: 17px; width: 71px" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                 
                                                                                          
                                                                                     
                                                                                            </tbody>
                                                                                        </table>

                                                                                        

                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                                
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel runat="server" ID="tab5" HeaderText="Upload Documents">
                                                        <HeaderTemplate>
                                                            Upload Documents</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <div class="notebook-pages">
                                                                    <div class="notebook-page notebook-page-active">
                                                                        <table border="0" align="center" width="750px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="border-bottom: 1px solid #525254;" valign="middle" align="center" colspan="2">
                                                                                        <div class="separator horizontal" style="border: none; font-size: 10pt">
                                                                                            Upload Documents</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="newuploads" runat="server">
                                                                                    <td valign="top" class=" item-group" width="50%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="label" valign="top" width="45%">
                                                                                                        <asp:Label ID="Label16" runat="server" ForeColor="Red">*</asp:Label><asp:Label ID="lblphoto"
                                                                                                            runat="server" Text="Photo"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" valign="top" width="45%">
                                                                                                        <asp:Label ID="Label17" runat="server" ForeColor="Red">*</asp:Label><asp:Label ID="lblbankdetails"
                                                                                                            runat="server" Text="Bank Details:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload2" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" valign="top" width="45%">
                                                                                                        <asp:Label ID="lblsslccertificate" runat="server" Text="SSLC Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload3" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="lblintercertificate" runat="server" Text="Inter Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload4" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                 <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="Label48" runat="server" Text="Pre-degree/+2 Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload14" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="lbldegreecertificate" runat="server" Text="Degree Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload5" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="lblpgcertificate" runat="server" Text="PG Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload6" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                               
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top" class="item-group" width="50%">
                                                                                        <table border="0" width="100%">
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="lbljbappli" runat="server" Text="Job Application"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload8" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="lblbiodata" runat="server" Text="BioData"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload9" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label18" runat="server" ForeColor="Red">*</asp:Label><asp:Label ID="lblidverify"
                                                                                                        runat="server" Text="ID Proof"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload10" runat="server" ToolTip="DrivingLicence/PanCard/VoterCard/PassPort" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="lblform21" runat="server" Text="Form-2(PF)Page-01"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload11" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="lblform22" runat="server" Text="Form-2(PF)Page-02"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload12" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="lblform11" runat="server" Text="Form-11"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload13" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                             <tr>
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="lblanyothercertificat" runat="server" Text="Other Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:FileUpload ID="FileUpload7" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="upduploads" runat="server" visible="false">
                                                                                    <td valign="top" class=" item-group" width="50%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="label" valign="top" width="45%">
                                                                                                        <asp:Label ID="Label28" runat="server" ForeColor="Red">*</asp:Label><asp:Label ID="Label29"
                                                                                                            runat="server" Text="Photo"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkphoto" runat="server" onclick="javascript:return report(this.href,'Photo');">
                                                                                                            <asp:Image ID="Image2" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" valign="top" width="45%">
                                                                                                        <asp:Label ID="Label30" runat="server" ForeColor="Red">*</asp:Label><asp:Label ID="Label31"
                                                                                                            runat="server" Text="Bank Details:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkbank" runat="server" onclick="javascript:return report(this.href,'BankDetails');">
                                                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" valign="top" width="45%">
                                                                                                        <asp:Label ID="Label32" runat="server" Text="SSLC Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkssc" runat="server" onclick="javascript:return report(this.href,'SSLC');">
                                                                                                            <asp:Image ID="Image3" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="Label33" runat="server" Text="Inter Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkinter" runat="server" onclick="javascript:return report(this.href,'Inter');">
                                                                                                            <asp:Image ID="Image4" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                 <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="Label47" runat="server" Text="Pre-degree/+2 Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkpredegree" runat="server" onclick="javascript:return report(this.href,'Degree');">
                                                                                                            <asp:Image ID="Image14" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="lbldegree" runat="server" Text="Degree Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkdegree" runat="server" onclick="javascript:return report(this.href,'Degree');">
                                                                                                            <asp:Image ID="Image5" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="border-collapse: collapse">
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="Label34" runat="server" Text="PG Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <asp:HyperLink ID="lnkpg" runat="server" onclick="javascript:return report(this.href,'PG');">
                                                                                                            <asp:Image ID="Image6" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top" class="item-group" width="50%">
                                                                                        <table border="0" width="100%">
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label36" runat="server" Text="Job Application"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="lnkjb" runat="server" onclick="javascript:return report(this.href,'JobApplication');">
                                                                                                        <asp:Image ID="Image8" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                            CssClass="listImage" />
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label37" runat="server" Text="BioData"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="lnkbiodata" runat="server" onclick="javascript:return report(this.href,'BioData');">
                                                                                                        <asp:Image ID="Image9" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                            CssClass="listImage" />
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label38" runat="server" ForeColor="Red">*</asp:Label><asp:Label ID="Label39"
                                                                                                        runat="server" Text="ID Proof"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="lnkid" runat="server" onclick="javascript:return report(this.href,'IDProof');">
                                                                                                        <asp:Image ID="Image10" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                            CssClass="listImage" />
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label40" runat="server" Text="Form-2(PF)Page-01 "></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="lnkform21" runat="server" onclick="javascript:return report(this.href,'Form2A');">
                                                                                                        <asp:Image ID="Image11" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                            CssClass="listImage" />
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label41" runat="server" Text="Form-2(PF)Page-02"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="lnkform22" runat="server" onclick="javascript:return report(this.href,'Form2B');">
                                                                                                        <asp:Image ID="Image12" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                            CssClass="listImage" />
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="30%">
                                                                                                    <asp:Label ID="Label42" runat="server" Text="Form-11"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="lnkform11" runat="server" onclick="javascript:return report(this.href,'Form11');">
                                                                                                        <asp:Image ID="Image13" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                            CssClass="listImage" />
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                    <td class="label" width="45%">
                                                                                                        <asp:Label ID="Label35" runat="server" Text="Other Certificate"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="left">
                                                                                                        <asp:HyperLink ID="lnkother" runat="server" onclick="javascript:return report(this.href,'Other');">
                                                                                                            <asp:Image ID="Image7" runat="server" ImageUrl="images/view.jpg" AlternateText="Print"
                                                                                                                CssClass="listImage" />
                                                                                                        </asp:HyperLink>
                                                                                                    </td>
                                                                                                </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" valign="top" style="height: 35px" align="center">
                                                                                        <table border="0">
                                                                                            <tbody>
                                                                                                <tr style="height: 30px" align="center">
                                                                                                    <td align="left">
                                                                                                        <input id="btnbackupload" class="button" value="     Back" onclick="return validateback();"
                                                                                                            align="middle" style="font-size: medium; height: 17px; width: 71px" readonly="readonly" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                        <input id="btnnextupload" class="button" value="     Next" onclick="return upvalidation();"
                                                                                                            align="middle" style="font-size: medium; height: 17px; width: 71px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel runat="server" ID="tab4" HeaderText="For Office Use">
                                                        <HeaderTemplate>
                                                            Joining Details</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <div class="notebook-pages">
                                                                    <div class="notebook-page notebook-page-active">
                                                                        <table border="0" align="center" width="750px">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td valign="top" class=" item-group" width="50%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td colspan="2" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                        <div class="separator horizontal" style="border: none" style="font-size: 10pt">
                                                                                                            Official Use</div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                               
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label22" runat="server" ForeColor="Red">*</asp:Label><label for="ssnid"
                                                                                                            class="help">Appointed/Designated as:
                                                                                                        </label>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:DropDownList ID="ddlrole" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                            ToolTip="Appointed/Designated as">
                                                                                                        </asp:DropDownList>
                                                                                                        <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlrole"
                                                                                                            ServiceMethod="role" ServicePath="~/cascadingDCA.asmx" Category="cc" PromptText="Select Role"
                                                                                                            Enabled="True">
                                                                                                        </cc1:CascadingDropDown>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <label for="passport_id">
                                                                                                            JobType
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:DropDownList ID="ddlcat" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                            ToolTip="JobType">
                                                                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                            <asp:ListItem Value="Permanent" Text="Permanent"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Semi Permanent/Contractual" Text="Semi Permanent/Contractual"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Temporary" Text="Temporary"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                           <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label25" runat="server" ForeColor="Red">*</asp:Label><label for="birthday">Department
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-datetime" align="left">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                        <asp:DropDownList ID="ddldepartment" runat="server" CssClass="selection selection readonlyfield"
                                                                                                            ToolTip="Department" Width="50%">
                                                                                                            <%--<asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                            <asp:ListItem Value="Project Management" Text="Project Management"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Safety" Text="Safety"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Adminstration" Text="Adminstration"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Human Resource" Text="Human Resource"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Finance/Accounts" Text="Finance/Accounts"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Training" Text="Training"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Stores" Text="Stores"></asp:ListItem>--%>
                                                                                                        </asp:DropDownList>
                                                                                                        
                                                                                                        <asp:LinkButton ID="hlnk" runat="server" Text="Add Department" 
                                                                                                            onclick="hlnk_Click" ></asp:LinkButton>
                                                                                                            </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                                <tr id="trroleid" runat="server">
                                                                                                    <td class="label" width="1%" runat="server">
                                                                                                        <asp:Label ID="Label23" runat="server" ForeColor="Red">*</asp:Label><label for="gender">Role
                                                                                                            Id
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle" runat="server">
                                                                                                        <asp:TextBox ID="txtroleid" runat="server" 
                                                                                                            CssClass="char" ToolTip="Role Id"/>
                                                                                                     
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top" class=" item-group" width="50%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td colspan="2" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                        <div class="separator horizontal" style="font-size: 10pt; border: none; visibility: hidden">
                                                                                                            official use
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label24" runat="server" ForeColor="Red">*</asp:Label><label for="gender">Joining
                                                                                                            Date
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtjdate" runat="server" onKeyDown="preventBackspace()" onKeyPress="return false;"
                                                                                                            CssClass="char" ToolTip="Joining Date"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtjdate"
                                                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" PopupButtonID="txtjdate"
                                                                                                            Enabled="True">
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <label for="gender">
                                                                                                            Transit Days
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:DropDownList ID="ddltransitdays" runat="server" ToolTip="Number of Transit Days">
                                                                                                            <asp:ListItem Value="0" Text="Select Days"></asp:ListItem>
                                                                                                            <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                                                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                               <td class="label" width="50%" colspan="2">
                                                                                                        <asp:Label ID="Label50" runat="server" ForeColor="Red"></asp:Label><label for="gender"
                                                                                                        </label>
                                                                                                        .
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                                 
                                                                                                <tr id="trpwd" runat="server">
                                                                                                    <td class="label" width="1%" runat="server">
                                                                                                        <asp:Label ID="Label49" runat="server" ForeColor="Red">*</asp:Label><label for="gender">Password
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle" runat="server">
                                                                                                       
                                                                                                      <asp:Label ID="lblpwd" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                         
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" valign="top" style="height: 35px" align="center">
                                                                                        <table border="0">
                                                                                            <tbody>
                                                                                                <tr style="height: 30px" align="center">
                                                                                                    <td align="left">
                                                                                                        <input id="btnoffice" class="button" value="     Back" align="middle" style="font-size: medium;
                                                                                                            height: 17px; width: 71px" readonly="readonly" onclick="return validateofficeback();" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                </cc1:TabContainer>
                                                  
                                                <cc1:ModalPopupExtender ID="popindents" BehaviorID="mdlindent" runat="server" TargetControlID="Button1"
                                                                                            PopupControlID="pnlindent" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                        <asp:Panel ID="pnlindent" runat="server" Style="display: none;">
                                                                                         <table width="300px" style="height:150px" border="0"  align="center" cellpadding="0" cellspacing="0" id="tblbank"
                                                                                            runat="server">
                                                                                            <tr>
                                                                                                <td width="13" valign="bottom">
                                                                                                    <img src="../images/leftc.jpg">
                                                                                                </td>
                                                                                                <td class="pop_head" align="left" id="Td3" runat="server">
                                                                                                    <div class="popclose">
                                                                                                        <img width="20" height="20" border="0" onclick="closepopup();" src="../images/mpcancel.png">
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td width="13" valign="bottom">
                                                                                                    <img src="../images/rightc.jpg">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td bgcolor="#FFFFFF">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td height="180" valign="middle" class="popcontent">
                                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                 <asp:UpdateProgress ID="UpdateProgress5" AssociatedUpdatePanelID="upindent" runat="server">
                                                                                                                        <ProgressTemplate>
                                                                                                                            <asp:Panel ID="pnlBackGround1" runat="server" CssClass="popup-div-background">
                                                                                                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                                                                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                                                                                    <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                                                                                                                    <img alt="Loading..." id="Img2" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                                                                                </div>
                                                                                                                            </asp:Panel>
                                                                                                                        </ProgressTemplate>
                                                                                                                    </asp:UpdateProgress>
                                                                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 100px;">
                                                                                                       
                                                                                                               
                                                                                       <div>
                                                                                                                                                    
                                                                                                                                                   
                                                                                                                                                     <asp:TextBox ID="txtdepartment" runat="server" Width="100px"  CssClass="char"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <div></div>
                                                                                                                                                    <div style="vertical-align:middle">
                                                                                                                                                 <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="button" OnClick="btnsave_Click" OnClientClick="return Deptvalid();"   />
                                                                                                                                                      </div>
                                                                                                                                                     
                                                                                                    </div>
                                                                                                      </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td bgcolor="#FFFFFF">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button ID="Button1" runat="server" Text="" Style="display: none" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        </asp:Panel>
                                                                                        
                                                                                       
                                            </div>
                                                
                                            <div>
                                                <table id="Table1" width="100%" runat="server">
                                                    <tbody>
                                                        <tr id="trstatus" runat="server">
                                                            <td>
                                                                <table align="left">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="40px" align="right">
                                                                            <asp:Label ID="Label26" runat="server" ForeColor="Red">*</asp:Label>
                                                                            <asp:Label ID="lblstatus" runat="server" Text="Status:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlstatus" Width="225px" runat="server" ToolTip="Status">
                                                                                <asp:ListItem Text="--Select--" Value="0">
                                                                                </asp:ListItem>
                                                                                <asp:ListItem Text="Approved" Value="1">
                                                                                </asp:ListItem>
                                                                                <asp:ListItem Text="Hold" Value="2">
                                                                                </asp:ListItem>
                                                                                <asp:ListItem Text="Reject" Value="3">
                                                                                </asp:ListItem>
                                                                                <%--<asp:ListItem Text="Cancel" Value="4"></asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lbldate" runat="server" Text="Date:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdate" Width="225px" runat="server" onchange="return false;">
                                                                            </asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label27" runat="server" ForeColor="Red">*</asp:Label>
                                                                            <asp:Label ID="lblcomment" runat="server" Text="Comment:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcomment" runat="server" Width="225px" TextMode="MultiLine" ToolTip="Comments">
                                                                            </asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnsubmit" runat="server" class="button" Style="font-size: medium;
                                                                    height: 20px; width: 80px" OnClientClick="return finalvalidation()" Text="Submit"
                                                                    Font-Bold="true" Font-Size="Small" OnClick="btnsubmit_Click1" />&nbsp;
                                                                <asp:Button ID="btncancel" runat="server" Font-Bold="true" Font-Size="Small" class="button"
                                                                    Style="font-size: medium; height: 20px; width: 80px" OnClick="btncancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                             
                                                                                        
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr id="trdetails1" runat="server">
                    <td>
                    <table>
                     <tr>
                                                                                    <td valign="top" width="45%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                               
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label53" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                            First Name
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdfname" runat="server" CssClass="char" TabIndex="20" ToolTip="First Name"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label55" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="coach_id_text">
                                                                                                            Last Name
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtdlname" runat="server" CssClass="char" TabIndex="21" ToolTip="Relation"
                                                                                                            Width="180px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                      <asp:Label ID="Label54" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label class="help" for="ssnid">
                                                                                                            Date of Birth
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdndob" runat="server" CssClass="char" onKeyDown="preventBackspace()"
                                                                                                            TabIndex="2" ToolTip="DOB" Width="125px" onKeyPress="return false;"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="DDateheck"
                                                                                                            PopupButtonID="txtdndob" TargetControlID="txtdndob">
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                  <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                      <asp:Label ID="Label67" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label class="help" for="ssnid">
                                                                                                            Martial Status
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                     <td class="item item-char" valign="middle" align="left">
                                                                                                        <asp:DropDownList ID="ddldmartialst" runat="server" CssClass="selection selection_search readonlyfield"
                                                                          Width="150px" ToolTip="Martial Status" >
                                                                            <asp:ListItem Text="Select" Value="Select">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Single" Value="Single">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Married" Value="Married">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Divorced" Value="Divorced">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Widower" Value="Widower">
                                                                            </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                       
                                                                    </td>
                                                                                                </tr>
                                                                                               
                                                                                          <tr>
                                                                                          <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label70" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                           Nominee Name
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdnominee" runat="server" CssClass="char"  ToolTip="Nominee Name"></asp:TextBox>
                                                                                                    </td>
                                                                                          </tr>
                                                                                                 
                                                                                                <tr>
                                                                                                    
                                                                                                     <td align="center" class="label" width="1%">
                                                                                                        <asp:Label ID="Label62" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <asp:Label ID="Label63" runat="server" CssClass="char" Text="Mobile:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <span style="border: 1px solid #d8d8d8; font-size: small;">
                                                                                                            <asp:Label ID="Label64" runat="server" Font-Size="Small" Height="5px" Text=" +91"></asp:Label>
                                                                                                            <asp:TextBox ID="txtdmobileno" runat="server" CssClass="char" MaxLength="10" onkeypress="javascript:return IsNumeric(event);"
                                                                                                                Style="width: 160px" TabIndex="18" ToolTip="Mobile No"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label57" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="address_home_id_text">
                                                                                                            Permanent Address
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdpaddress" runat="server" CssClass="char" MaxLength="100" TabIndex="14"
                                                                                                            TextMode="MultiLine" ToolTip="Permanent Address" Width="200px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                  
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="lbl59" runat="server" ForeColor="Red">*</asp:Label><label for="ssnid"
                                                                                                            class="help">Appointed/Designated as:
                                                                                                        </label>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:DropDownList ID="ddldrole" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                            ToolTip="Appointed/Designated as" onchange="ShowCC(this.value);" Width="60%">
                                                                                                        </asp:DropDownList>
                                                                                                        <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddldrole"
                                                                                                            ServiceMethod="role" ServicePath="~/cascadingDCA.asmx" Category="cc" PromptText="Select Role"
                                                                                                            Enabled="True">
                                                                                                        </cc1:CascadingDropDown>
                                                                                                       
                                                                                                    </td>
                                                                                                </tr>
                                                                                                
                                                                                           
                                                                                                <tr id="tr1" runat="server">
                                                                                                    <td id="Td4" class="label" width="1%" runat="server">
                                                                                                        <asp:Label ID="Label61" runat="server" ForeColor="Red">*</asp:Label><label for="gender">Role
                                                                                                            Id
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td id="Td5" class="item item-selection" valign="middle" runat="server">
                                                                                                        <asp:TextBox ID="txtdroleid" runat="server" 
                                                                                                            CssClass="char" ToolTip="Role Id"/>
                                                                                                     
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top" width="55%">
                                                                                        <table border="0" width="100%">
                                                                                            <tbody>
                                                                                               <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label66" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                            Middle Name
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdmname" runat="server" CssClass="char" TabIndex="20" ToolTip="Middle Name"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                      <asp:Label ID="Label68" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label class="help" for="ssnid">
                                                                                                            Sex
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td>
                                                                                                   

                                                                         <asp:DropDownList ID="ddldsex" runat="server" Width="150px" CssClass="selection selection_search readonlyfield"
                                                                            ToolTip="Sex" >
                                                                            <asp:ListItem Value="Select" Text="Select">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="Male" Text="Male">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="Female" Text="Female">
                                                                            </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                      <asp:Label ID="Label56" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label class="help" for="ssnid">
                                                                                                            Age
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtdnage" runat="server" Height="22px" TabIndex="3" Width="2px"></asp:TextBox>
                                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="False"
                                                                                                            TargetControlID="txtdnage" WatermarkCssClass="watermarked" WatermarkText="       Age">
                                                                                                        </cc1:TextBoxWatermarkExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                 
                                                                                                   <tr id="spouce" runat="server">
                                                                                                    
                                                                                                  
                                                                                                    <td class="label" width="1%" >
                                                                                                        <asp:Label ID="Label69" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                           Spouse Name
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:TextBox ID="txtspousename" runat="server" CssClass="char" TabIndex="20" ToolTip="Spouse Name"></asp:TextBox>
                                                                                                    </td>
                                                                                             
                                                                                                </tr>
                                                                                                 <tr>
                                                                                                   
                                                                                                     <td class="item item-char" align="right">
                                                                                                        <asp:Label ID="Label58" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                           Gender
                                                                                                        </label>
                                                                                                        :
                                                                                                        
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                    <asp:DropDownList ID="ddldgender" Width="40%" runat="server" CssClass="selection selection_search readonlyfield"  
                                                                                                            ToolTip="Gender" onselectedindexchanged="ddldgender_SelectedIndexChanged" AutoPostBack="true">
                                                                                                        <asp:ListItem Value="Select" Text="Select">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="Male" Text="Male">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="Female" Text="Female">
                                                                            </asp:ListItem>

                                                                                                        </asp:DropDownList>
                                                                                                    <asp:Label ID="Label59" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="work_email">
                                                                                                           Relation
                                                                                                        </label>
                                                                                                        :
                                                                                                          <asp:DropDownList ID="ddldRelation" runat="server" Width="100%" CssClass="selection selection_search readonlyfield"  
                                                                                                            ToolTip="Relation">
                                                                                                        
                                                                                                        </asp:DropDownList>
                                                                                                        
                                                                                                        </td>
                                                                                                </tr>
                                                                                                  <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                    <asp:Label ID="Label65" runat="server" ForeColor="Red">*</asp:Label>
                                                                                                        <label for="job_id_text">
                                                                                                            Work E-mail
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtdemail" runat="server" CssClass="char" onblur="DvalidateEmail(this);"
                                                                                                            TabIndex="19" ToolTip="Email id" Width="180px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                               
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <label for="passport_id">
                                                                                                            JobType
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <asp:DropDownList ID="ddldjobpos" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                            ToolTip="JobType">
                                                                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                            <asp:ListItem Value="Permanent" Text="Permanent"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Semi Permanent/Contractual" Text="Semi Permanent/Contractual"></asp:ListItem>
                                                                                                            <asp:ListItem Value="Temporary" Text="Temporary"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                               
                                                                                                <tr>
                                                                                                    <td class="label" width="1%">
                                                                                                        <asp:Label ID="Label60" runat="server" ForeColor="Red">*</asp:Label><label for="birthday">Department
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-datetime" align="left">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                        <asp:DropDownList ID="ddlddepartment" runat="server" CssClass="selection selection readonlyfield"
                                                                                                            ToolTip="Department" Width="50%">
                                                                                                           
                                                                                                        </asp:DropDownList>
                                                                                                        
                                                                                                        <asp:LinkButton ID="hlnkdbtn" runat="server" Text="Add Department" 
                                                                                                            onclick="hlnkdbtn_Click" ></asp:LinkButton>
                                                                                                            </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                            <cc1:ModalPopupExtender ID="popext1" BehaviorID="mdlindent1" runat="server" TargetControlID="btnmdlpopup"
                                                                                            PopupControlID="Panel1" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                        <asp:Panel ID="Panel1" runat="server" Style="display: none;">
                                                                                         <table width="300px" style="height:150px" border="0"  align="center" cellpadding="0" cellspacing="0" id="Table2"
                                                                                            runat="server">
                                                                                            <tr>
                                                                                                <td width="13" valign="bottom">
                                                                                                    <img src="../images/leftc.jpg">
                                                                                                </td>
                                                                                                <td class="pop_head" align="left" id="Td8" runat="server">
                                                                                                    <div class="popclose">
                                                                                                        <img width="20" height="20" border="0" onclick="closepopup1();" src="../images/mpcancel.png">
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td width="13" valign="bottom">
                                                                                                    <img src="../images/rightc.jpg">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td bgcolor="#FFFFFF">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td height="180" valign="middle" class="popcontent">
                                                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                 <asp:UpdateProgress ID="UpdateProgress7" AssociatedUpdatePanelID="upindent" runat="server">
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
                                                                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 100px;">
                                                                                                       
                                                                                                               
                                                                                       <div>
                                                                                                                                                    Department
                                                                                                                                                   
                                                                                                                                                     <asp:TextBox ID="txtddepartment" runat="server" Width="100px"  CssClass="char"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <div></div>
                                                                                                                                                    <div style="vertical-align:middle">
                                                                                                                                                 <asp:Button ID="btndsave" runat="server" Text="Save" CssClass="button"  Width="30%" OnClick="btndsave_Click"  OnClientClick="return validateddepartment()"  />
                                                                                                                                                      </div>
                                                                                                                                                     
                                                                                                    </div>
                                                                                                      </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td bgcolor="#FFFFFF">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button ID="btnmdlpopup" runat="server" Text="" Style="display: none" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        </asp:Panel> 
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                                <tr id="trcc" runat="server" style="display:none">
                                                                                                    <td class="label" width="1%">
                                                                                                        <label for="passport_id">
                                                                                                            CC Code
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                      <asp:DropDownList ID="ddlcc" runat="server" CssClass="esddown" ToolTip="Cost Center"
                                                Width="200px">
                                            </asp:DropDownList>
                                            
                                            <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="cc" LoadingText="Please Wait"
                                                PromptText="Select Cost Center" ServiceMethod="newcostcode" ServicePath="~/cascadingDCA.asmx"
                                                TargetControlID="ddlcc">
                                            </cc1:CascadingDropDown>
                                            </td>
                                                                                                    </tr>
                                                                                                 <tr id="tr2" runat="server">
                                                                                                    <td id="Td6" class="label" width="1%" runat="server">
                                                                                                        <asp:Label ID="Label71" runat="server" ForeColor="Red">*</asp:Label><label for="gender">Password
                                                                                                        </label>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td id="Td7" class="item item-selection" valign="middle" runat="server">
                                                                                                       
                                                                                                      <asp:Label ID="lbldpwd" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                      <tr>
                      <td colspan="2" align="center" >
                      <asp:Button ID="btndsubmit" runat="server" class="button" Style="font-size: medium;
                                                                    height: 20px; width: 80px" 
                              OnClientClick="return dfinalvalidation()" Text="Submit"
                                                                    Font-Bold="true" Font-Size="Small" 
                              onclick="btndsubmit_Click"  />&nbsp;
                                                                <asp:Button ID="btndcancel" runat="server" Font-Bold="true" Font-Size="Small" class="button"
                                                                    Style="font-size: medium; height: 20px; width: 80px" Text="Cancel"  />
                      </td>
                      </tr>                                                         
                       
                                                                                        
                    </table>
                    </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdfroles" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdftype" runat="server" />
                             <asp:HiddenField ID="hfdept" runat="server" />
                        </td>
                    </tr>
                </table>
               
            </td>
        </tr>
    </table>
    
</asp:Content>
