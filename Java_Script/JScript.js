
// Login Page Validation Starts - - - - 

function loginvalidation()
{
//User Name Validation
var txtun=document.getElementById('txtusername').value;
var txtunctrl=document.getElementById('txtusername');

var txtp=document.getElementById('txtpwd').value;
var txtpctrl=document.getElementById('txtpwd');



if(txtun == "")
{
window.alert("Enter Username");
txtunctrl.focus();
return false;
}
else if(txtp == "")
{
window.alert("Enter Password");
txtpctrl.focus();
return false;
}

}
//Login Page Validation ends - - - - 

// Registration Page Validation Starts - - - - 
function registrationvalidation()
{

//User Name Validation


var txtFname = document.getElementById("txtfname").value;
var txtFnamectrl = document.getElementById("txtfname");

var txtLname = document.getElementById("txtlname").value;
var txtLnamectrl = document.getElementById("txtlname");



var fathername= document.getElementById("txtfathername").value; 
var fathernamectrl=document.getElementById("txtfathername"); 


var nname = document.getElementById("txtnname").value; 
var nnamectrl= document.getElementById("txtnname");

var relation = document.getElementById("txtrelation").value;
var relationctrl = document.getElementById("txtrelation");

var date = document.getElementById("txtdate").value;
var datectrl = document.getElementById("txtdate");

var status = document.getElementById("ddlstatus").value;
var statusctrl = document.getElementById("ddlstatus");

var gen = document.getElementById("ddlgen").value;
var genctrl = document.getElementById("ddlgen");

var join = document.getElementById("txtjoin").value;
var joinctrl = document.getElementById("txtjoin");

var referred = document.getElementById("txtreferrred").value;
var referredctrl = document.getElementById("txtreferrred");

var add = document.getElementById("txtadd").value;
var addctrl = document.getElementById("txtadd");

var phone = document.getElementById("txtph").value;
var phonectrl = document.getElementById("txtph");

var mobile = document.getElementById("txtph1").value;
var ctrlm = document.getElementById("txtph1");

var val = document.getElementById("txtemail").value;
var ctrl = document.getElementById("txtemail");

var dep = document.getElementById("ddldep").value;
var depctrl = document.getElementById("ddldep");

var cat = document.getElementById("ddlcat").value;
var catctrl = document.getElementById("ddlcat");

var role = document.getElementById("ddlrole").value;
var rolectrl = document.getElementById("ddlrole");

var cc = document.getElementById("ddlcccode").value;
var ccctrl = document.getElementById("ddlcccode");




if(txtFname =="")
{
window.alert("Please Enter First Name");
txtFnamectrl.focus();
return false;
}
else if(txtLname =="")
{
window.alert("Please Enter Last Name");
txtLnamectrl.focus();
return false;
}
else if(fathername=="")
{
window.alert("Please Enter Father's Name");
fathernamectrl.focus();
return false;
}
else if(nname=="")
{
window.alert("Please Enter Nominee's Name");
nnamectrl.focus();
return false;
}
else if(relation=="")
{
window.alert("Please Enter Realation");
relationctrl.focus();
return false;
}
else if(date=="")
{
window.alert("Please Enter Date of Birth");
datectrl.focus();
return false;
}
else if(statusctrl.selectedIndex == 0)
{
window.alert("Please Select Employee's Martial Status");
statusctrl.focus();
return false;
}
else if(genctrl.selectedIndex == 0)
{
window.alert("Please Select Employee Job Status");
genctrl.focus();
return false;
}
else if(join=="")
{
window.alert("Please Enter Joining Date");
joinctrl.focus();
return false;
}
else if(add=="")
{
window.alert("please Enter Employee's Home Address");
addctrl.focus();
return false;
}
var i; 
if (mobile=="") 
{ 
window.alert("Please Enter Mobile Number "); 
ctrlm.focus(); 
return false; 
} 
else
for(i=0;i<mobile.length;i++) 
{
var temp=mobile.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
ctrlm.focus();
 return false; 
}
} 
if(mobile.length!=10) 
{
 window.alert("Mobile Number Must be 10 digits"); 
ctrlm.focus(); 
return false; 
}
// //CHECKS WHETHER EMAILID IS ENTERED OR NOT 
//if(val=="") 
//{ 
//window.alert("Enter E-mail ID"); 
//ctrl.focus(); 
//return false; 
//} 
//var atPos = val.indexOf('@',0); 
////CHECKS THAT THERE IS AN '@' CHARACTER IN THE STRING 
//if (atPos==-1) 
//{ 
//alert("Invalid E-mail ID"); 
//ctrl.focus();
// return false; 
//}
// //CHECKS THAT THERE IS AT LEAST ONE CHARACTER BEFORE THE '@' CHARACTER 
//if (atPos==0) 
//{ 
//window.alert("Invalid E-mail ID"); 
//ctrl.focus(); 
//return false;
// } 
////CHECKS ATLEAST ONE CHAR BEFORE '.'
// var dot=val.indexOf('.',0); 
//if (dot==0) 
//{ 
//window.alert("Invalid E-mail ID");
// ctrl.focus(); 
//return false; 
//} 
////CHECKS THAT THERE IS a PERIOD IN THE DOMAIN NAME 
//if (val.indexOf('.', atPos) == -1) 
//{ 
//alert("Invalid E-mail ID"); 
//ctrl.focus(); 
//return false; 
//} 
////CHECKS THAT THERE IS ONLY ONE '@' IN THE STRING 
//if (val.indexOf('@', atPos + 1) != - 1) 
//{ 
//window.alert("Invalid E-mail ID"); 
//ctrl.focus(); 
//return false; 
//} 
////CHECKS THAT THERE IS NO PERIOD IMMEDIATELY AFTER '@' i
//if (val.indexOf('@.',0) != -1) 
//{ 
//window.alert("Invalid E-mail ID");
//ctrl.focus(); 
//return false; 
//}
// //CHECKS THAT THERE IS NO PEROID IMMEDIATELY BEFORE '@' 
//if (val.indexOf('.@',0) != -1) 
//{ 
//window.alert("Invalid E-mail ID");
//ctrl.focus(); 
//return false; 
//}
// //CHECKS THAT TWO PERIODS MUST NOT BE ADJACENT 
//if (val.indexOf('..',0) != -1) 
//{ 
//window.alert("Invalid E-mail ID");
//ctrl.focus(); 
//return false; 
//} 
////CHECKS THAT THERE NO SPECIAL CHARACTERS ENTERED 
//var invalidChars = '\/\'\\ ";:?!()[]\{\}^|#*$%&'; 
//for (i=0; i<invalidChars.length; i++) 
//{ 
//if (val.indexOf(invalidChars.charAt(i),0) != -1) 
//{ 
//window.alert("Invalid E-mail ID");
//ctrl.focus(); 
//return false; 
//} 
//}
////CHECKS THAT THERE IS ATLEAST ONE CHARACTER AFTER THE LAST PERIOD 
//var suffix = val.substring(val.lastIndexOf('.')+1); 
//if (suffix.length<1)
// { 
//window.alert("Invalid E-mail ID"); 
//ctrl.focus();
// return false; 
//}

else if(depctrl.selectedIndex == 0)
{
window.alert("Please Select Department");
depctrl.focus();
return false;
}
//else if(catctrl.selectedIndex == 0)
//{
//window.alert("Please Select Category")
//catctrl.focus();
//return false;
//}
}

// Registration Page Validation ends - - - - 

// Cost Center Budget Page Validation Starts - - - - 
function ccBudget()
{

var ddlcc = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode').value;
var ddlccctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode');

var txtBgtAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtBgtAmt').value;
var txtBgtctrl = document.getElementById('ctl00_ContentPlaceHolder1_txtBgtAmt');

var txtdate = document.getElementById('ctl00_ContentPlaceHolder1_txtDate').value;
var txtdatectrl = document.getElementById('ctl00_ContentPlaceHolder1_txtDate');

if(ddlccctrl.selectedIndex == 0)
{
window.alert("Please Select cc code");
ddlccctrl.focus();
return false;
}
else if(txtBgtAmt =="")
{
window.alert("Enter Cost Center Budget");
txtBgtctrl.focus();
return false;
}
else
for(i=0;i<txtBgtAmt.length;i++) 
{ 
var temp=txtBgtAmt.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txtBgtctrl.focus();
 return false; 
} 
}

if(txtdate =="")
{
window.alert("Enter Date");
txtdatectrl.focus();
return false;
}
//var date = new Date();

//var currentTime = new Date()
//var month = currentTime.getMonth() + 1
//var day = currentTime.getDate()
//var year = currentTime.getFullYear()
//var CDate = month + "/" + day + "/" + year
//if( txtdate< CDate)
//{
//window.alert("Date is less than todays Date");
//txtdatectrl.focus();
//return false;
//}
}



//Cost Center Budget Page Validatin Ends

//Update Cost Center Budget Page validation starts
function Updateccbudget()
{
 
var ddlcc = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode').value;
var ddlccctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode');

var rbadd = document.getElementById('ctl00_ContentPlaceHolder1_rbadd').value;
var rbaddCtrl = document.getElementById('ctl00_ContentPlaceHolder1_rbadd');

var rbsub = document.getElementById('ctl00_ContentPlaceHolder1_rbsub').value;
var rbsubCtrl = document.getElementById('ctl00_ContentPlaceHolder1_rbsub');
//var radiobuttonsctrl=document.getElementsByName('ctl00_ContentPlaceHolder1_rblbudget').value;

var txtBgtAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtBgtAmt').value;
var txtBgtctrl = document.getElementById('ctl00_ContentPlaceHolder1_txtBgtAmt');


var txtdatecc = document.getElementById('ctl00_ContentPlaceHolder1_txtDate').value;
var txtdateCCctrl = document.getElementById('ctl00_ContentPlaceHolder1_txtDate');

if(ddlccctrl.selectedIndex == 0)
{
window.alert("Please Select cc code");
ddlccctrl.focus();
return false;
}
if(rbaddCtrl.checked == false && rbsubCtrl.checked == false)
{
alert("Select Type of Transaction");
rbaddCtrl.focus();
return false;
}
else if(txtBgtAmt =="")
{
window.alert("Enter Cost Center Updated Budget");
txtBgtctrl.focus();
return false;
}
else
for(i=0;i<txtBgtAmt.length;i++) 
{ 
var temp=txtBgtAmt.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txtBgtctrl.focus();
 return false; 
} 
}

 if(txtdatecc =="")
{
window.alert("Enter Date");
txtdateCCctrl.focus();
return false;
}
var date = new Date();

var currentTime = new Date()
var month = currentTime.getMonth() + 1
var day = currentTime.getDate()
var year = currentTime.getFullYear()
var CDate = month + "/" + day + "/" + year
if(txtdatecc < CDate)
{
window.alert("Date is less than todays Date");
txtdateCCctrl.focus();
return false;
}
}



//Update Cost Center  Budget Page validation ends



//Assign DCA Budget Page validation starts
function dcaBudget()
{
var ddlcc = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode').value;
var ddlccctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode');

var txtdca = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode').value;
var ddldcactrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode');

var txtBgtAmty = document.getElementById('ctl00_ContentPlaceHolder1_txtDcaBudgetY').value;
var txtBgtAmtyctrl = document.getElementById('ctl00_ContentPlaceHolder1_txtDcaBudgetY');


var txtdate = document.getElementById('ctl00_ContentPlaceHolder1_txtDate').value;
var txtdatectrl = document.getElementById('ctl00_ContentPlaceHolder1_txtDate');

if(ddlccctrl.selectedIndex == 0)
{
window.alert("Please Select cc code");
ddlccctrl.focus();
return false;
}

else if(ddldcactrl.selectedIndex == 0)
{
window.alert("Please Select dca code");
ddldcactrl.focus();
return false;
}
else if(txtBgtAmty =="")
{
window.alert("Enter DCA Yearly Budget");
txtBgtAmtyctrl.focus();
return false;
}
else
for(i=0;i<txtBgtAmty.length;i++) 
{ 
var temp=txtBgtAmty.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txtBgtAmtyctrl.focus();
 return false; 
} 
}

if(txtdate =="")
{
window.alert("Enter Date");
txtdatectrl.focus();
return false;
}
//var date = new Date();

//var currentTime = new Date()
//var month = currentTime.getMonth() + 1
//var day = currentTime.getDate()
//var year = currentTime.getFullYear()
//var CDate = month + "/" + day + "/" + year
//if(txtdate < CDate)
//{
//window.alert("Date is less than todays Date");
//txtdatectrl.focus();
//return false;
//}
}
 

//Assign DCA Budget Page Validation Ends





function AddCC()
{
var txtccCode=document.getElementById('ctl00_ContentPlaceHolder1_txtccCode').value;
var txtccCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtccCode');

var txtccName=document.getElementById('ctl00_ContentPlaceHolder1_txtccName').value;
var txtccNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtccName');

var txtinname = document.getElementById('ctl00_ContentPlaceHolder1_txtinname').value;
var txtinnamectrl = document.getElementById('ctl00_ContentPlaceHolder1_txtinname');

var txtadd = document.getElementById('ctl00_ContentPlaceHolder1_address').value;
var txtaddctrl= document.getElementById('ctl00_ContentPlaceHolder1_address');

var txtpno = document.getElementById('ctl00_ContentPlaceHolder1_phoneno').value;
var txtpnoctrl = document.getElementById('ctl00_ContentPlaceHolder1_phoneno');

if(txtccCode=="")
{
window.alert("Enter CC Code");
txtccCodectrl.focus();
return false;
}
else if(txtccName=="")
{
window.alert("Enter CC Name");
txtccNamectrl.focus();
return false;
}


else if(txtinname =="")
{
window.alert("Enter CC Incharge Name");
txtinnamectrl.focus();
return false;

}
else if(txtadd == "")
{
window.alert(" Please Enter Address")
txtaddctrl.focus();
return false;
}
else if(txtpno == "")
{
window.alert("Please Enter Phone No")
txtpnoctrl.focus();
return false;

}
}

//Add New Cost Center Page Validation Ends


//Update Cost Center Validation Starts
function updatecost()
{
  var ddlca = document.getElementById('ctl00_ContentPlaceHolder1_ddlca').value;
  var ddlcactl = document.getElementById('ctl00_ContentPlaceHolder1_ddlca');
  
  if(ddlcactl.selectedIndex==0)
  {
   alert("Select Cost Center");
   ddlcactl.focus();
   return false;
   }


}

//Update Cost center validation ends

//frmUpdateDCABudget Validation Starts

function updatebudget()
{
  var ddlcostcent = document.getElementById('ctl00_ContentPlaceHolder1_ddlcostcenter').value;
  var ddlccentctl = document.getElementById('ctl00_ContentPlaceHolder1_ddlcostcenter');

  var ddldca = document.getElementById('ctl00_ContentPlaceHolder1_ddldcacode').value;
  var ddldcactr = document.getElementById('ctl00_ContentPlaceHolder1_ddldcacode');
  
  var rbtnadd= document.getElementById('ctl00_ContentPlaceHolder1_rbtnadd').value;
  var rbtnctr= document.getElementById('ctl00_ContentPlaceHolder1_rbtnadd');
  
  var rbtnsub= document.getElementById('ctl00_ContentPlaceHolder1_rbtnsub').value;
  var rbtnctr1= document.getElementById('ctl00_ContentPlaceHolder1_rbtnsub');
  
  var budgeamt= document.getElementById('ctl00_ContentPlaceHolder1_txtBgtAmt1').value;
  var bugetcntl= document.getElementById('ctl00_ContentPlaceHolder1_txtBgtAmt1');
  
  var date= document.getElementById('ctl00_ContentPlaceHolder1_txtDate').value;
  var datecntl= document.getElementById('ctl00_ContentPlaceHolder1_txtDate');
  
  if(ddlccentctl.selectedIndex==0)
  {
    window.alert("Select Cost Center");
    
    ddlccentctl.focus();
    return false;
  }
  
  
  
  
  if(ddldcactr.selectedIndex == "0")
  {
   window.alert("Select DCA Code ");
  
   ddldcactr.focus();
   return false;
   
  }
  
  if(rbtnctr.checked==false && rbtnctr1.checked==false)
  {
   window.alert("Select Updation Type");
   return false;
  }
  
  if(budgeamt=="")
  {
   window.alert("Enter Budget Amount");
   
   return false;
   bugetcntl.focus();
   
  
  }
  
  if(date=="")
  {
   window.alert("Enter Date");
   datecntl.focus();
   return false;
  }
  
 } 

//frmUpdateDCABudget Validation ends

//frmAssignDCAMonthlyBudget Validation Starts

function assigndcamonthly()
{
  var ddlcost = document.getElementById('ctl00_ContentPlaceHolder1_ddlcostcode').value;
  var ddldcostctr = document.getElementById('ctl00_ContentPlaceHolder1_ddlcostcode');
  
  var ddldetail = document.getElementById('ctl00_ContentPlaceHolder1_ddldccode').value;
  var ddldetailctr = document.getElementById('ctl00_ContentPlaceHolder1_ddldccode');
  
  var mbudget= document.getElementById('ctl00_ContentPlaceHolder1_txtBudgetM').value;
  var mbugetcntl= document.getElementById('ctl00_ContentPlaceHolder1_txtBudgetM');
  
  var mdate= document.getElementById('ctl00_ContentPlaceHolder1_txtDate1').value;
  var mdatecntl= document.getElementById('ctl00_ContentPlaceHolder1_txtDate1');
  
  if(ddldcostctr.selectedIndex==0)
  {
   window.alert("Select Cost Center");
   ddldcostctr.focus();
   return false;
  }
  
  if(ddldetailctr.selectedIndex==0)
  {
   window.alert("Select DCA");
   ddldetailctr.focus();
   return false;
  }
  
  if(mbudget=="")
  {
   window.alert("Enter Budget Amount");
   mbugetcntl.focus();
   return false;
  }
  
  if(mdate=="")
  {
   window.alert("Enter Date");
   mdatecntl.focus();
   return false;
  }
}
//frmAssignDCAMonthlyBudget Validation Ends


//frmUpdateMonthlyBudget Validation Starts

function updatemonthly()
{
 
  var ddlcost = document.getElementById('ctl00_ContentPlaceHolder1_ddlcost').value;
  var ddlcostctl = document.getElementById('ctl00_ContentPlaceHolder1_ddlcost');

  var ddldetailca = document.getElementById('ctl00_ContentPlaceHolder1_ddldetailca').value;
  var ddldetailcactr = document.getElementById('ctl00_ContentPlaceHolder1_ddldetailca');
  
  var rbtn1ad= document.getElementById('ctl00_ContentPlaceHolder1_rbtn1ad').value;
  var rbtn1adcntl= document.getElementById('ctl00_ContentPlaceHolder1_rbtn1ad');
  
  var rbtn1sub= document.getElementById('ctl00_ContentPlaceHolder1_rbtn1sub').value;
  var rbtn1subctl= document.getElementById('ctl00_ContentPlaceHolder1_rbtn1sub');
  
  var txtMbudget= document.getElementById('ctl00_ContentPlaceHolder1_txtMbudget').value;
  var txtMbudgetctl= document.getElementById('ctl00_ContentPlaceHolder1_txtMbudget');
  
  var txtdatem= document.getElementById('ctl00_ContentPlaceHolder1_txtdatem').value;
  var txtdatemcntl= document.getElementById('ctl00_ContentPlaceHolder1_txtdatem');
 
  if(ddlcostctl.selectedIndex==0)
  {
   window.alert("Select Cost Center");
   ddlcostctl.focus();
   return false;
  }
  
  if(ddldetailcactr.selectedIndex==0)
  {
   window.alert("Select DCA");
   ddldetailcactr.focus();
   return false;
  }
  
  if(rbtn1adcntl.checked==false && rbtn1subctl.checked==false)
  {
   window.alert("Select Updation Type");
   return false;
  }
  
  if(txtMbudget=="")
  {
   window.alert("Enter Budget");
   txtMbudgetctl.focus();
   return false;
  }
  
  if(txtdatem=="")
  {
   window.alert("Enter Date");
   txtdatemcntl.focus();
   return false;
  }
  
   
 
}
//  frmUpdateMonthlyBudget Validation Starts


//Add Contract Page Validation Starts



function Addcontract()
{
var txtProjname=document.getElementById('ctl00_ContentPlaceHolder1_txtProjname').value;
var txtProjnamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtProjname');

var txtClientname=document.getElementById('ctl00_ContentPlaceHolder1_txtClientname').value;
var txtClientnamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtClientname');

var txtCustomername=document.getElementById('ctl00_ContentPlaceHolder1_txtCostomername').value;
var txtCustomernamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtCostomername');

var txtdivision=document.getElementById('ctl00_ContentPlaceHolder1_txtDivision').value;
var txtdivisionctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtDivision');

var txtPMname=document.getElementById('ctl00_ContentPlaceHolder1_txtPMname').value;
var txtPMnamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPMname');

var txtContactno=document.getElementById('ctl00_ContentPlaceHolder1_txtContactno').value;
var txtContactnoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtContactno');

var txtSdate=document.getElementById('ctl00_ContentPlaceHolder1_txtStartdate').value;
var txtSdatectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtStartdate');

var txtEdate=document.getElementById('ctl00_ContentPlaceHolder1_txtEnddate').value;
var txtEdatectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtEnddate');

var txtccCode=document.getElementById('ctl00_ContentPlaceHolder1_txtCCcode').value;
var txtccCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtCCcode');

var txtjob=document.getElementById('ctl00_ContentPlaceHolder1_txtjob').value;
var txtjobctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtjob');

var txtPono=document.getElementById('ctl00_ContentPlaceHolder1_txtPO').value;
var txtPonoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPO');

//var txtPovalue=document.getElementById('ctl00_ContentPlaceHolder1_txtPOvalue').value;
//var txtPovaluectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPOvalue');

var txtPodate=document.getElementById('ctl00_ContentPlaceHolder1_txtPodate').value;
var txtPodatectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPodate');

var txtbasic=document.getElementById('ctl00_ContentPlaceHolder1_txtBasic').value;
var txtbasicctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtBasic');

var txttax=document.getElementById('ctl00_ContentPlaceHolder1_txtTax').value;
var txttaxctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtTax');

var txttotal=document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value;
var txttotalctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtTotal');

if(txtProjname=="")
{
window.alert("Enter Project Name");
txtProjnamectrl.focus();
return false;
}
if(txtdivision=="")
{
window.alert("Enter Division Name");
txtdivisionctrl.focus();
return false;
}
 if(txtClientname=="")
{
window.alert("Enter Client Name");
txtClientnamectrl.focus();
return false;
}
if(txtCustomername=="")
{
window.alert("Enter Customer Name");
txtCustomernamectrl.focus();
return false;
} 

if(txtPMname=="")
{
window.alert("Enter Project Manager Name");
txtPMnamectrl.focus();
return false;
}
var i;
 if(txtContactno=="")
{
window.alert("Enter Contact NO");
txtContactnoctrl.focus();
return false;
}
else
 for(i=0;i<txtContactno.length;i++) 
{ 
var temp=txtContactno.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txtContactnoctrl.focus();
 return false; 
} 
} 
if(txtContactno.length!=10) 
{
 window.alert("Mobile Number Must be 10 digits"); 
txtContactnoctrl.focus(); 
return false; 
}
 if(txtSdate=="")
{
window.alert("Enter Start Date");
txtSdatectrl.focus();
return false;
}
if(txtEdate=="")
{
window.alert("Enter End Date");
txtEdatectrl.focus();
return false;
}
 if(txtccCode=="")
{
window.alert("Enter Cost Center Code");
txtccCodectrl.focus();
return false;
}
if(txtjob=="")
{
window.alert("Enter Nature Of Job");
txtjobctrl.focus();
return false;
}
 if(txtPono=="")
{
window.alert("Enter Purchase Order No");
txtPonoctrl.focus();
return false;
}
// if(txtPovalue=="")
//{
//window.alert("Enter Purchase Order Value");
//txtPovaluectrl.focus();
//return false;
//}
//else
//for(i=0;i<txtPovalue.length;i++) 
//{ 
//var temp=txtPovalue.substring(i,i+1);
// if(!(temp>="0" && temp<="9")) 
//{ 
//window.alert(" Enter numeric values"); 
//txtPovaluectrl.focus();
// return false; 
//} 
//}
if(txtPodate=="")
{
window.alert("Enter Purchase Order Date");
txtPodatectrl.focus();
return false;
}
if(txtbasic=="")
{
window.alert("Enter Basic Value");
txtbasicctrl.focus();
return false;
}
else
for(i=0;i<txtbasic.length;i++) 
{ 
var temp=txtbasic.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txtbasicctrl.focus();
 return false; 
} 
}
 if(txttax=="")
{
window.alert("Enter Service Tax value");
txttaxctrl.focus();
return false;
}
else
for(i=0;i<txttax.length;i++) 
{ 
var temp=txttax.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txttaxctrl.focus();
 return false; 
} 
}

 if(txttotal=="")
{
window.alert("Enter Total value");
txttotalctrl.focus();
return false;
}
else
for(i=0;i<txttotal.length;i++) 
{ 
var temp=txttotal.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txttotalctrl.focus();
 return false; 
} 
}
}
//Add Contract Page Validation Ends

//Contract Amended Page Validation Starts

function POAmd()
{

var txtPO=document.getElementById('ctl00_ContentPlaceHolder1_txtPono').value;
var txtPOctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPono');

var txtAmdPono=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdPoNo').value;
var txtAmdPonoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdPoNo');

//var txtAmdVal=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdValue').value;
//var txtAmdValctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdValue');

var txtAmddate=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdDate').value;
var txtAmddatectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdDate');

var txtbasic=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdBasic').value;
var txtbasicctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdBasic');

var txttax=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdtax').value;
var txttaxctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAmdtax');

var txttotal=document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value;
var txttotalctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtTotal');


if(txtPO=="")
{
window.alert("Enter  PO No");
txtPOctrl.focus();
return false;
}
if(txtAmdPono=="")
{
window.alert("Enter Amended PO No");
txtAmdPonoctrl.focus();
return false;
}

if(txtAmddate=="")
{
window.alert("Enter Amended Date");
txtAmddatectrl.focus();
return false;
}
if(txtbasic=="")
{
window.alert("Enter Amended Basic Value");
txtbasicctrl.focus();
return false;
}
else
for(i=0;i<txtbasic.length;i++) 
{ 
var temp=txtbasic.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 

txtbasicctrl.focus();
 return false; 
} 
}
if(txttax=="")
{
window.alert("Enter Amended Tax Value");
txttaxctrl.focus();
return false;
}
else
for(i=0;i<txttax.length;i++) 
{ 
var temp=txttax.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txttaxctrl.focus();
 return false; 
} 
}
if(txttotal=="")
{
window.alert("Enter Total value");
txttotalctrl.focus();
return false;
}
else
for(i=0;i<txttotal.length;i++) 
{ 
var temp=txttotal.substring(i,i+1);
 if(!(temp>="0" && temp<="9")) 
{ 
window.alert(" Enter numeric values"); 
txttotalctrl.focus();
 return false; 
} 
}
}

//Contract Apeended page Validation Ends


//Total Page Validation Starts


function pototal()
{
var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtPono').value;
var txtctrl = document.getElementById('ctl00_ContentPlaceHolder1_txtPono');

if(txt=="")
{
window.alert("Insert PO Number");
return false;
txtctrl.focus();

}
}
// Total page validation ends
//Add validation for viewcontract starts
function txtbox()
{
var txt = document.getElementById('ctl00_ContentPlaceHolder1_TextBox1').value;
var txtctrl = document.getElementById('ctl00_ContentPlaceHolder1_TextBox1');

if(txt=="")
{
window.alert("Insert PO Number");
return false;
txtctrl.focus();

}
}
//Add validation for viewcontract ends


//Validation fof AddIt form

function addit()
{
  var txtitCode=document.getElementById('ctl00_ContentPlaceHolder1_txtitCode').value;
  var txtitCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtitCode');
  
  var txtithead=document.getElementById('ctl00_ContentPlaceHolder1_txtithead').value;
  var txtitheadctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtithead');
  
 
  
  if(txtitCode=="")
  {
   alert("Enter IT Code");
   txtitCodectrl.focus();
   return false;
  }
  if(txtithead=="")
  {
   alert("Enter IT Head");
   txtitheadctrl.focus();
   return false;
  }
  
  
}
function updateit()
{
  var TextBox1=document.getElementById('ctl00_ContentPlaceHolder1_TextBox1').value;
  var TextBox1=document.getElementById('ctl00_ContentPlaceHolder1_TextBox1');
  
  var ddlitcode = document.getElementById('ctl00_ContentPlaceHolder1_ddlitcode').value;
  var ddlitcodectr = document.getElementById('ctl00_ContentPlaceHolder1_ddlitcode');

 if(ddlitcodectr.selectedIndex==0)
  {
   alert("Select IT Code");
   return false;
  }
  if(TextBox1=="")
  {
   alert("Enter IT Head Name");
   TextBox1.focus();
   return false;
  }
}

//Add New Vendor Page Valiadation Starts
function Addvendor()
{

var txtMs=document.getElementById('ctl00_ContentPlaceHolder1_txtMs').value;
var txtMsctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtMs');

var txtSp=document.getElementById('ctl00_ContentPlaceHolder1_txtSp').value;
var txtSpctrl=document.getElementById('ctl00_ContentPlaceHolder1_Sp');

//var txtvc=document.getElementById('ctl00_ContentPlaceHolder1_txtVCode').value;
//var txtvcctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtVCode');

var txtVName=document.getElementById('ctl00_ContentPlaceHolder1_txtVName').value;
var txtVNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtVName');

var txtAdr=document.getElementById('ctl00_ContentPlaceHolder1_txtAddress').value;
var txtAdrctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAddress');

var rbtnSP=document.getElementById('ctl00_ContentPlaceHolder1_rbtnSP').value;
var rbtnSPctrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtnSP');

var rbtnMS=document.getElementById('ctl00_ContentPlaceHolder1_rbtnMS').value;
var rbtnMSctrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtnMS');

var txtPNo=document.getElementById('ctl00_ContentPlaceHolder1_txtpan').value;
var txtPNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtpan');

var txtSTNo=document.getElementById('ctl00_ContentPlaceHolder1_txttax').value;
var txtSTNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txttax');

var txtpf=document.getElementById('ctl00_ContentPlaceHolder1_txtpf').value;
var txtpfctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtpf');

var txtVNo=document.getElementById('ctl00_ContentPlaceHolder1_txtVat').value;
var txtVNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtVat');

var txtTNo=document.getElementById('ctl00_ContentPlaceHolder1_txtTin').value;
var txtTNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtTin');

var txtCNo=document.getElementById('ctl00_ContentPlaceHolder1_txtcst').value;
var txtCNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtcst');





// if(rbtnSPctrl.checked == false && rbtnMSctrl.checked==false)
//{
//window.alert("Select Vendor Type");
//return false;
//rbtnSPctrl.focus();
//}
//else if(rbtnMSctrl.checked == true)
//{
//if(txtCNo=="" && txtTNo=="" && txtVNo=="")
//{
//window.alert("Please Enter Tin or Vat or CST ");
////txtCNoctrl.focus();
//return false;
//txtTNoctrl.focus();
////txtVNoctrl.focus();

//}
//}
//else if(txtvc=="")
//{
//window.alert("Enter Vendor Code");
//txtvcctrl.focus();
//return false;
//}
//else if(rbtnSPctrl.checked == true)
//{
//if(txtPNo=="" && txtSTNo=="" && txtpf=="")
//{
//window.alert("Please Enter Pan or Tax No or PF No ");
////txtCNoctrl.focus();
//return false;
//txtSTNoctrl.focus();
////txtVNoctrl.focus();

//}
//}
// if(txtVName=="")
//{
//window.alert("Enter Vendor Name");
//txtVNamectrl.focus();
//return false;
//}
//else if(txtAdr=="")
//{
//window.alert("Enter Vendor Address");
//txtAdrctrl.focus();
//return false;
//}

//}
if(rbtnSPctrl.checked == false && rbtnMSctrl.checked == false)
{
window.alert("Select Vendor Type");
rbtnSPctrl.focus();
return false;
}
//else if(txtvc=="")
//{
//window.alert("Enter Vendor Code");
//txtvcctrl.focus();
//return false;
//}
else if(rbtnMSctrl.checked == true)
{
if(txtMs=="")
{
window.alert("Enter Vendor Code");
txtMsctrl.focus();
return false;
}
if(txtCNo=="" && txtTNo=="" && txtVNo=="")
{
window.alert("Enter Tin or Vat or CST ");
txtTNoctrl.focus();
return false;
//txtVNoctrl.focus();

}

}
else if(rbtnSPctrl.checked == true)
{
if(txtSp=="")
{
window.alert("Enter Vendor Code");
txtSpctrl.focus();
return false;
}
if(txtPNo=="" && txtSTNo=="" && txtpf=="")
{
window.alert("Enter Pan or Tax No or PF No ");
txtSTNoctrl.focus();
return false;

}
//else if(txtSp=="")
//{
//window.alert("Enter Vendor Code");
//txtSpctrl.focus();
//return false;
//}
}
if(txtVName=="")
{
window.alert("Enter Vendor Name");
txtVNamectrl.focus();
return false;
}
else if(txtAdr=="")
{
window.alert("Enter Vendor Address");
txtAdrctrl.focus();
return false;
}


}


//Add Vendor Page Validation Ends

//update vendor validation starts

function Updatevendor()
{
var ddlVCode=document.getElementById('ctl00_ContentPlaceHolder1_ddlSP').value;
var ddlVCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_ddlSP');

var ddlVCode1=document.getElementById('ctl00_ContentPlaceHolder1_ddlMS').value;
var ddlVCode1ctrl=document.getElementById('ctl00_ContentPlaceHolder1_ddlMS');


var rbtnSP=document.getElementById('ctl00_ContentPlaceHolder1_rbtnSP').value;
var rbtnSPctrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtnSP');

var rbtnMS=document.getElementById('ctl00_ContentPlaceHolder1_rbtnMS').value;
var rbtnMSctrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtnMS');

var txtPNo=document.getElementById('ctl00_ContentPlaceHolder1_txtpan').value;
var txtPNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtpan');

var txtSTNo=document.getElementById('ctl00_ContentPlaceHolder1_txttax').value;
var txtSTNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txttax');

var txtpf=document.getElementById('ctl00_ContentPlaceHolder1_txtpf').value;
var txtpfctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtpf');

var txtVNo=document.getElementById('ctl00_ContentPlaceHolder1_txtVat').value;
var txtVNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtVat');

var txtTNo=document.getElementById('ctl00_ContentPlaceHolder1_txtTin').value;
var txtTNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtTin');

var txtCNo=document.getElementById('ctl00_ContentPlaceHolder1_txtcst').value;
var txtCNoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtcst');

var txtVName=document.getElementById('ctl00_ContentPlaceHolder1_txtVName').value;
var txtVNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtVName');

var txtAdr=document.getElementById('ctl00_ContentPlaceHolder1_txtAddress').value;
var txtAdrctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAddress');



 if(rbtnSPctrl.checked == false && rbtnMSctrl.checked == false)
{
window.alert("Select Vendor Type");
return false;
rbtnSPctrl.focus();
}
else if(rbtnMSctrl.checked == true)
{

if(ddlVCode1ctrl.selectedIndex==0)
{
window.alert("Select Vendor Code");
ddlVCode1ctrl.focus();
return false;
}
if(txtCNo=="" && txtTNo=="" && txtVNo=="")
{
window.alert("Please Enter Tin or Vat or CST ");
//txtCNoctrl.focus();
return false;
txtTNoctrl.focus();
//txtVNoctrl.focus();

}
}
else if(rbtnSPctrl.checked == true)
{

if(ddlVCodectrl.selectedIndex==0)
{
window.alert("Select Vendor Code");
ddlVCodectrl.focus();
return false;
}
if(txtPNo=="" && txtSTNo=="" && txtpf=="")
{
window.alert("Please Enter Pan or Tax No or PF No ");
//txtCNoctrl.focus();
return false;
txtSTNoctrl.focus();
//txtVNoctrl.focus();

}
}
if(txtVName=="")
{
window.alert("Enter Vendor Name");
txtVNamectrl.focus();
return false;
}
else if(txtAdr=="")
{
window.alert("Enter Vendor Address");
txtAdrctrl.focus();
return false;
}
}

//update vendor validation ends



  


//Add New Sub DCA Page Validation Starts
function Subdca()
{

var ddldca = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode').value;
var ddldcactrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode');

var txtsubdca=document.getElementById('ctl00_ContentPlaceHolder1_txtSubDCACode').value;
var txtsubdcactrl=document.getElementById('ctl00_ContentPlaceHolder1_txtSubDCACode');

var txtsubdcaName=document.getElementById('ctl00_ContentPlaceHolder1_txtsubdcaName').value;
var txtsubdcaNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtsubdcaName');

if(ddldcactrl.selectedIndex == 0)
{
window.alert("Please Select DCA code");
ddldcactrl.focus();
return false;
}
else if(txtsubdca=="")
{
window.alert("Enter Sub Code");
txtsubdcactrl.focus();
return false;
}
else if(txtsubdcaName=="")
{
window.alert("Enter Sub DCA Name");
txtsubdcaNamectrl.focus();
return false;
}
}

//Add New Sub DCA page Validation Ends

//Update Sub DCA Page Validation Starrs
function Update()
{
var ddldca = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode').value;
var ddldcactrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode');

var ddlsubdca=document.getElementById('ctl00_ContentPlaceHolder1_ddlSubDCACode').value;
var ddlsubdcactrl=document.getElementById('ctl00_ContentPlaceHolder1_ddlSubDCACode');

var txtsubdcaName=document.getElementById('ctl00_ContentPlaceHolder1_txtsubDCAName').value;
var txtsubdcaNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtsubDCAName');

if(ddldcactrl.selectedIndex == 0)
{
window.alert("Please Select DCA code");
ddldcactrl.focus();
return false;
}
else if(ddlsubdcactrl.selectedIndex == 0)
{
window.alert("Please Select SubDCA Code");
ddlsubdcactrl.focus();
return false;
}
else if(txtsubdcaName=="")
{
window.alert("Enter Sub DCA Name");
txtsubdcaNamectrl.focus();
return false;
}
}
//Update Sub DCA Page Validation Ends

//Add Client Page Validation Starts

function Addclient()
{
var txtCName=document.getElementById('ctl00_ContentPlaceHolder1_txtClientName').value;
var txtCNamectrl = document.getElementById('ctl00_ContentPlaceHolder1_txtClientName');

var txtClientId = document.getElementById('ctl00_ContentPlaceHolder1_txtClientid').value;
var txtClientIdctrl = document.getElementById('ctl00_ContentPlaceHolder1_txtClientid');

var ddlcategory= document.getElementById('ctl00_ContentPlaceHolder1_ddlcategory').value;
var ddlcategotyctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlcategory');


var txtPersonname=document.getElementById('ctl00_ContentPlaceHolder1_txtPersonname').value;
var txtPersonnamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPersonname');

var txtPsnphoneno=document.getElementById('ctl00_ContentPlaceHolder1_txtPsnphoneno').value;
var txtPsnphonenoctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtPsnphoneno');


var txtAddress=document.getElementById('ctl00_ContentPlaceHolder1_txtAddress').value;
var txtAddressctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtAddress');

if (ddlcategotyctrl.selectedIndex == 0) {
    window.alert("Please Select Category");
    ddlcategotyctrl.focus();
    return false;
}
else if (txtClientId == "") {
    window.alert("Enter Client ID");
    txtClientIdctrl.focus();
    return false;
}
else if (txtCName == "") {
    window.alert("Enter Client Name");
    txtCNamectrl.focus();
    return false;
}
else if (txtPersonname == "") {
    window.alert("Enter Person Name");
    txtPersonnamectrl.focus();
    return false;
}
else if (txtPsnphoneno == "") {
    window.alert("Enter Person Phone No.");
    txtPsnphonenoctrl.focus();
    return false;
    }


else if (txtAddress == "") {

    window.alert("Enter Address");
    txtAddressctrl.focus();
    return false;
}

}
//Add Client Page validation Ends

//Update Client validation starts
function updateclient()
{
 var ddlClient = document.getElementById('ctl00_ContentPlaceHolder1_ddlClient').value;
 var ddlClientctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlClient');
 
 if(ddlClientctrl.selectedIndex==0)
 {
  window.alert("Select Client Name");
  ddlClientctrl.focus();
  return false;
 } 

}
//Update Client validation ends

//view Client Page Validation Starts
function client()
{
var ddlcl = document.getElementById('ctl00_ContentPlaceHolder1_ddlClient').value;
var ddlclctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlClient');

if(ddlclctrl.selectedIndex == 0)
{
window.alert("Select Client Name");
return false;
ddlclctrl.focus();

}
}
// view Client Page Validation ends

//Members Page validation Starts
function Members()
{
var ddlme = document.getElementById('ctl00_ContentPlaceHolder1_ddlmeber').value;
var ddlclctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlmeber');

if(ddlclctrl.selectedIndex == 0)
{
window.alert("Select User");
return false;
ddlclctrl.focus();

}
}

function Submit()
{
//var ddlcc = document.getElementById('ctl00_ContentPlaceHolder1_ddlccCode').value;
//var ddlccctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlccCode');

var ddlro = document.getElementById('ctl00_ContentPlaceHolder1_ddlrole').value;
var ddlroctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlrole');

//if(ddlccctrl.selectedIndex == 0)
//{
//window.alert("Select Cost Center");
//return false;
//ddlccctrl.focus();

//}
 if(ddlroctrl.selectedIndex == 0)
{
window.alert("Select Role");
return false;
ddlroctrl.focus();
}
}

//Members Page Validation Ends

//Update Members Page Validation Starts

function Update()
{

var ddlcc = document.getElementById('ctl00_ContentPlaceHolder1_ddlccCode').value;
var ddlccctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlccCode');

var ddlro = document.getElementById('ctl00_ContentPlaceHolder1_ddlrole').value;
var ddlroctrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlrole');

if(ddlccctrl.selectedIndex == 0)
{
window.alert("Select Cost Center");
return false;
ddlccctrl.focus();

}
else if(ddlroctrl.selectedIndex == 0)
{
window.alert("Select Role");
return false;
ddlroctrl.focus();
}
}
//update members page validation ends

//cashflow validation starts

function cashflow()
{

var rCrbtn = document.getElementById('ctl00_ContentPlaceHolder1_rbtncredit').value;
var rCrbtnCtrl = document.getElementById('ctl00_ContentPlaceHolder1_rbtncredit');

var rDtbtn = document.getElementById('ctl00_ContentPlaceHolder1_rbtndebit').value;
var rDtbtnCtrl = document.getElementById('ctl00_ContentPlaceHolder1_rbtndebit');

var rinbtn = document.getElementById('ctl00_ContentPlaceHolder1_rbtninvoice').value;
var rinbtnCtrl = document.getElementById('ctl00_ContentPlaceHolder1_rbtninvoice');

var date = document.getElementById('ctl00_ContentPlaceHolder1_txtdt').value;
var dateCtrl = document.getElementById('ctl00_ContentPlaceHolder1_txtdt');

var rbtnVend = document.getElementById('ctl00_ContentPlaceHolder1_rbtnvendor').value;
var rbtnVendCtrl = document.getElementById('ctl00_ContentPlaceHolder1_rbtnvendor');

var rbtnGen = document.getElementById('ctl00_ContentPlaceHolder1_rbtngen').value;
var rbtnGenCtrl =document.getElementById('ctl00_ContentPlaceHolder1_rbtngen');

var ddlVend = document.getElementById('ctl00_ContentPlaceHolder1_ddlvendor').value;
var ddlVendCtrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlvendor');

var ddldcaH = document.getElementById('ctl00_ContentPlaceHolder1_ddldetailhead').value;
var ddldcaHCtrl = document.getElementById('ctl00_ContentPlaceHolder1_ddldetailhead');

//var ddlsubdetail = document.getElementById('ctl00_ContentPlaceHolder1_ddlsubdetail').value;
//var ddlsubdetailCtrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlsubdetail');

//var name = document.getElementById('ctl00_ContentPlaceHolder1_txtname').value;
//var nameCtrl = document.getElementById('ctl00_ContentPlaceHolder1_txtname');

var desc = document.getElementById('ctl00_ContentPlaceHolder1_txtdesc').value;
var descCtrl = document.getElementById('ctl00_ContentPlaceHolder1_txtdesc');

var amt = document.getElementById('ctl00_ContentPlaceHolder1_txtamt').value;
var amtCtrl = document.getElementById('ctl00_ContentPlaceHolder1_txtamt');

if(!rCrbtnCtrl.checked)
{
if(!rDtbtnCtrl.checked)
{
if(!rinbtnCtrl.checked)
{
alert("Select Type of Transaction");
rCrbtnCtrl.focus();
return false;
}
}
}
if(date == "")
{
alert("Select Date");
dateCtrl.focus();
return false;
}
if(rbtnVendCtrl.checked == false && rbtnGenCtrl.checked == false)
{

alert("Select Payment Category");
rbtnVendCtrl.focus();
return false;

}
if(rbtnVendCtrl.checked == true)
{
if(ddlVendCtrl.selectedIndex == 0)
{
alert("Select Vendor");
ddlVendCtrl.focus();
return false;
}
}
if(ddldcaHCtrl.selectedIndex == 0)
{
alert("Select Dca Code");
ddldcaHCtrl.focus();
return false;
}
//if(ddlsubdetailCtrl.selectedIndex == 0)
//{
//alert("Select Sub Dca Code");
//ddlsubdetailCtrl.focus();
//return false;
//}
//if(name == "")
//{
//alert("Enter Vendor Name");
//nameCtrl.focus();
//return false;
//}
if(desc == "")
{
alert("Enter Transaction Details and Purpose");
descCtrl.focus();
return false;
}

if(amt == "")
{
if(rCrbtnCtrl.checked == true)// &&
{
alert("Enter Transaction Credit Amount");
amtCtrl.focus();
return false;
}
else if(rDtbtnCtrl.checked == true)
{
alert("Enter Transaction Debit Amount");
amtCtrl.focus();
return false;
}

}
}
//cashflow validation ends

//change password validation starts
function changepwd()
{
 var txtold=document.getElementById('ctl00_ContentPlaceHolder1_txtold').value;
var txtoldctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtold');

var txtnew=document.getElementById('ctl00_ContentPlaceHolder1_txtnew').value;
var txtnewctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtnew');

var txtconfirm=document.getElementById('ctl00_ContentPlaceHolder1_txtconfirm').value;
var txtconfirmctrl=document.getElementById('ctl00_ContentPlaceHolder1_txtconfirm');



if(txtold=="")
{
 alert("Enter Old Password");
 txtoldctrl.focus();
 return false;
}
if(txtnew=="")
{
 alert("Enter New Password");
 txtnewctrl.focus();
 return false;
}
if(txtconfirm=="")
{
 alert("Enter Confirm Password");
 txtconfirmctrl.focus();
 return false;
}

if(txtnew!=txtconfirm)
{
 alert("New Password and Confirm Password Did Not Match");
 txtnewctrl.focus();
 return false;
}

}

//chage password validation ends

//view costcenter page validation starts
function viewccval()
{
  var ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddl').value;
  var ddlctl = document.getElementById('ctl00_ContentPlaceHolder1_ddl');
  
  if(ddlctl.selectedIndex==0)
  {
   alert("Select Cost Center");
   ddlctl.focus();
   return false;
   }
 

}
//view cost center page validation ends

// opening balance validatin starts

function validateOB()
{


//var cc = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode').value;
//var ccCtrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlCCcode')
var obAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtOBamt').value;
var obAmtCtrl = document.getElementById('ctl00_ContentPlaceHolder1_txtOBamt')
var date = document.getElementById('ctl00_ContentPlaceHolder1_txtDate').value;
var dateCtrl = document.getElementById('ctl00_ContentPlaceHolder1_txtDate')

//if(ccCtrl.selectedIndex == 0)
//{
//alert("Select CC Code");
//ccCtrl.focus();
//return false;
//}

//if(obAmt=="")
//{
//alert("Enter Opening Balance");
//obAmtCtrl.focus();
//return false;
//}
//if(date == "")
//{
//alert("Select Date");
//dateCtrl.focus();
//return false;
//}


}
//opening balance validatin ends

//View Costcenter Budget validations starts
function viewccbudget()
{
 var ddlcccode = document.getElementById('ctl00_ContentPlaceHolder1_ddlcccode').value;
 var ddlcccodectl = document.getElementById('ctl00_ContentPlaceHolder1_ddlcccode');
 
 if(ddlcccodectl.selectedIndex==0)
 {
  alert("Select Cost Center");
  ddlcccodectl.focus();
  return false;
 }
  
}
//View Costcenter Budget Validation ends
//Add New DCA Page Validation Starts

function Adddca()
{

var rbtndca=document.getElementById('ctl00_ContentPlaceHolder1_rbtndca').value;
var rbtndcactrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtndca');

var rbtsubdca=document.getElementById('ctl00_ContentPlaceHolder1_rbtnsubdca').value;
var rbtnsubdcactrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtnsubdca');

var txtdcaCode=document.getElementById('ctl00_ContentPlaceHolder1_txtdcaCode').value;
var txtdcaCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtdcaCode');

var txtdcaName=document.getElementById('ctl00_ContentPlaceHolder1_txtDcaName').value;
var txtdcaNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtDcaName');

var ddldcaCode=document.getElementById('ctl00_ContentPlaceHolder1_ddldca').value;
var ddldcaCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_ddldca');


var txtsubdca = document.getElementById('ctl00_ContentPlaceHolder1_txtsubdca').value;
var txtsubdcactrl = document.getElementById('ctl00_ContentPlaceHolder1_txtsubdca');

var txtsubdcaname = document.getElementById('ctl00_ContentPlaceHolder1_txtsdname').value;
var txtsubdcanamectrl = document.getElementById('ctl00_ContentPlaceHolder1_txtsdname');


var ddlitcode = document.getElementById('ctl00_ContentPlaceHolder1_ddlit').value;
var ddlitcodectrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlit');


if(rbtndcactrl.checked == false && rbtnsubdcactrl.checked == false)
{
window.alert("Select Type");
return false;
rbtndcactrl.focus();
}
else if(rbtndcactrl.checked == true)
{
if(txtdcaCode=="")
{
window.alert("Enter DCA Code");
txtdcaCodectrl.focus();
return false;
}

else if(txtdcaName=="")
{
window.alert("Enter DCA Name");
txtdcaNamectrl.focus();
return false;
}

}
else if(rbtnsubdcactrl.checked == true)
{
 if(ddldcaCodectrl.selectedIndex==0)
{
window.alert("Select DCA Code");
ddldcaCodectrl.focus();
return false;
}
else if(txtsubdca=="")
{
window.alert("Enter Sub DCA Code");
txtsubdcactrl.focus();
return false;
}

else if(txtsubdcaname=="")
{
window.alert("Enter Sub DCA Name");
txtsubdcanamectrl.focus();
return false;
}
else if(ddlitcodectrl.selectedIndex==0)
{
window.alert("Select IT Code");
ddlitcodectrl.focus();
return false;
}
}
}



//Add Update DCA Page Validation Starts

function Updatedca()
{

var rbtndca=document.getElementById('ctl00_ContentPlaceHolder1_rbtndca').value;
var rbtndcactrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtndca');

var rbtsubdca=document.getElementById('ctl00_ContentPlaceHolder1_rbtnsubdca').value;
var rbtnsubdcactrl=document.getElementById('ctl00_ContentPlaceHolder1_rbtnsubdca');


var txtdcaName=document.getElementById('ctl00_ContentPlaceHolder1_txtDcaName').value;
var txtdcaNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtDcaName');

var ddldcaCode=document.getElementById('ctl00_ContentPlaceHolder1_ddldca').value;
var ddldcaCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_ddldca');


//var txtsubdca = document.getElementById('ctl00_ContentPlaceHolder1_txtsubdca').value;
//var txtsubdcactrl = document.getElementById('ctl00_ContentPlaceHolder1_txtsubdca');

var ddlsubdcaCode=document.getElementById('ctl00_ContentPlaceHolder1_ddlsdcode').value;
var ddlsubdcaCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_ddlsdcode');


var txtsubdcaname = document.getElementById('ctl00_ContentPlaceHolder1_txtsdname').value;
var txtsubdcanamectrl = document.getElementById('ctl00_ContentPlaceHolder1_txtsdname');


//var ddlitcode = document.getElementById('ctl00_ContentPlaceHolder1_ddlit').value;
//var ddlitcodectrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlit');



if(rbtndcactrl.checked == false && rbtnsubdcactrl.checked == false)
{
window.alert("Select Type");
return false;
rbtndcactrl.focus();
}
else if(rbtndcactrl.checked == true)
{
 if(ddldcaCodectrl.selectedIndex==0)
{
window.alert("Select DCA Code");
ddldcaCodectrl.focus();
return false;
}

//else if(txtdcaName=="")
//{
//window.alert("Enter DCA Name");
//txtdcaNamectrl.focus();
//return false;
//}

}
else if(rbtnsubdcactrl.checked == true)
{
 if(ddldcaCodectrl.selectedIndex==0)
{
window.alert("Select DCA Code");
ddldcaCodectrl.focus();
return false;
}
else if(ddlsubdcaCodectrl.selectedIndex==0)
{
window.alert("Select Sub DCA Code");
ddlsubdcaCodectrl.focus();
return false;
}
//else if(txtsubdcaname=="")
//{
//window.alert("Enter Sub DCA Name");
//txtsubdcanamectrl.focus();
//return false;
//}
//else if(ddlitcodectrl.selectedIndex==0)
//{
//window.alert("Select IT Code");
//ddlitcodectrl.focus();
//return false;
//}
}
}
//Update DCA Page Validation ends



//View DCA Page Validation Starts

function viewdcaval()
{
 var ddlDCAcode=document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode').value;
 var ddlDCAcodectl=document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode');
 
 if(ddlDCAcodectl.selectedIndex==0)
 {
  alert("Select DCA");
  ddlDCAcodectl.focus();
  return false;
 }
}
//View DCA Pge Validation ends

//Add Update subDCA Page Validation Starts

function Update()
{
var ddldcaCode=document.getElementById('ctl00_ContentPlaceHolder1_ddldca').value;
var ddldcaCodectrl=document.getElementById('ctl00_ContentPlaceHolder1_ddldca');
  
var txtdcaName=document.getElementById('ctl00_ContentPlaceHolder1_txtDcaName').value;
var txtdcaNamectrl=document.getElementById('ctl00_ContentPlaceHolder1_txtDcaName'); 


if(ddldcaCodectrl.selectedIndex == 0)
{
window.alert("Please Select DCA code");
ddldcaCodectrl.focus();
return false;
}

else if(txtdcaName=="")
{
window.alert("Enter DCA Name");
txtdcaNamectrl.focus();
return false;
}
}
//Update subDCA Page Validation ends

function updatesub()
{
 var ddldca = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode').value;
 var ddldcactrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlDCAcode');
 
 var ddlSubDCACode = document.getElementById('ctl00_ContentPlaceHolder1_ddlSubDCACode').value;
 var ddlSubDCACodectrl = document.getElementById('ctl00_ContentPlaceHolder1_ddlSubDCACode');
 
 if(ddldcactrl.selectedIndex==0)
 {
  alert("Select DCA Code");
  ddldcactrl.focus();
  return false;
 }
 
 if(ddlSubDCACodectrl.selectedIndex==0)
 {
  alert("Select Sub DCA Code");
  ddlSubDCACodectrl.focus();
  return false;
 }
}
//Approve Registration
    function Registration()
    {
   
       var status= document.getElementById("ddlgen").value;
       var statusctrl=document.getElementById("ddlgen");
       
       var userid=document.getElementById("txtuserid").value;
       var useridctrl=document.getElementById("txtuserid");
       
       var pwd=document.getElementById("txtpwd").value;
       var pwdctrl=document.getElementById("txtpwd");
       
       var cpwd=document.getElementById("txtcpwd").value;
       var cpwdctrl=document.getElementById("txtcpwd");
       
       var role=document.getElementById("ddlrole").value;
       var rolectrl=document.getElementById("ddlrole");
       
       
       var cc=document.getElementById("ddlcccode").value;
       var ccctrl=document.getElementById("ddlcccode");
       
       if(statusctrl.selectedIndex == 1 || statusctrl.selectedIndex == 3)
       {
           if(userid=="")
           {
           window.alert("Please Enter User ID");
           useridctrl.focus();
           return false;
           }
           else if(pwd=="")
           {
           window.alert("Please Enter Password");
           pwdctrl.focus();
           return false;
           }
           else if(cpwd=="")
           {
           window.alert("Please Enter Confirm Password");
           cpwdctrl.focus();
           return false;
           }
           else if(pwd!=cpwd)
           {
           window.alert("Password and Confirm Password do not match");
           cpwdctrl.focus();
           return false;

           }
           else if(rolectrl.selectedIndex ==0)
           {
           window.alert("Please Select Role");
           rolectrl.focus();
           return false;
           }
           else if(rolectrl.selectedIndex == 1 && ccctrl.selectedIndex == 0)
           {
           window.alert("Please Select CostCenter");
           ccctrl.focus();
           return false;
           
           }
        
       }
       else if(statusctrl.selectedIndex == 2)
       {
//           if(rolectrl.selectedIndex ==0)
//           {
//           window.alert("Please Select Role");
//           rolectrl.focus();
//           return false;
//           }
           if(rolectrl.selectedIndex == 1 && ccctrl.selectedIndex == 0)
           {
           window.alert("Please Select CostCenter");
           ccctrl.focus();
           return false;
           
           }
         
       }
         else if(statusctrl.selectedIndex == 3)
           {
          
           window.alert("Please Select Status");
           statusctrl.focus();
           return false;
           }
           
         
      
      
    
    }