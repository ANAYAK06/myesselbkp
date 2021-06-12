//.............Client ID
var ClientID="";
//.............Client ID Ends

//............Required Field Validations for Any Type of Control with message
function CheckInput(obj,msg)
{
    switch(obj.type.toString())
    {
        case "text":
            if(obj.value.length<1||obj.value=="")
            {
              alert(msg+" Required");
//              if(obj.disable==false)
                obj.focus();
              return false;
            }
            break;
        case "select-one":
            if(obj.selectedIndex==0)
            {
              alert("Select "+msg);
//              if(obj.disable==false)
                obj.focus();
              return false;
            }
            break;
        case "file":
            if(obj.value.length<1||obj.value=="")
            {
              alert(msg+" Required");
//              if(obj.disable==false)
                obj.focus();
              return false;
            }
            break;
        case "textarea":
            if(obj.value.length<1||obj.value=="")
            {
              alert(msg+" Required");
//              if(obj.disable==false)
                obj.focus();
              return false;
            }
            break;
    }    
    return true;
}
//............Required Field Validations for Group of Any Type of Controls objects with messages
function CheckInputs(objs,msgs)
{
var i=0;
for(i=0;i<objs.length;i++)
{
if(!CheckInputs(document.getElementById(objs[i]),msgs[i]))
return false;
}
}
//............Required Field Validations for Group of Any Type of Controls objects and messages
function CheckInputs(objs)
{
var i=0;
for(i=0;i<objs.length;i++)
{
var ob=document.getElementById(objs[i]);
if(ob!=null)
{
if(!CheckInput(ob,ob.title))
return false;  
}    
}
return true;
}
//............Max Length Validations takes message and length as parameters
function CheckLength(cnt,len)
{
if(cnt.value.length>len)
{
  alert(cnt.title+" must be less than "+len+" characters");
  cnt.focus();
  return false;
}
}
function imposeMaxLength(Object, MaxLen)
{
  return (Object.value.length < MaxLen);
}
//............Min Length Validations takes message and length as parameters
function CheckMinLength(cnt,len)
{
if(cnt.value.length<len)
{
  alert(cnt.title+" must be greater than "+len+" characters");
  cnt.focus();
  return false;
}
}

//................Select/Clear All Check Boxes in Check Box List
function CheckBoxList(cntl,state)
{
var models = cntl.getElementsByTagName("input");
for(var i = 0; i < models.length; i++)
{
	if(models[i].type == "checkbox")
	{
		models[i].checked = state;
	}
}
}

/*.................ALlow Numerics only.........................................................*/
function IsNumeric(evt) 
{
  var theEvent = evt || window.event;
  var key = theEvent.keyCode || theEvent.which;
  key = String.fromCharCode( key );
  var regex = /[0-9]|\./;
  if( !regex.test(key) ) {
    theEvent.returnValue = false;
    theEvent.preventDefault();
  }
}



/*..........................Functions Required for Grid View......................................................*/

//................Select/Clear All Check Boxes in Gridview and keep Traking of selection
function GridParent(stat,cntl)
{
gridview=document.getElementById(cntl);  //document.getElementById('ctl00_ContentPlaceHolder1_'+cntl);
for(var i = 1;i < gridview.rows.length; i++)
{
gridview.rows[i].cells[0].children[0].checked = stat;
} 
}

//.........................Watch Selection on child status changed
function GridChild(cntl)
{
gridview=document.getElementById(cntl);
if(CountChecked(cntl)==gridview.rows.length-2)
    gridview.rows[0].cells[0].children[0].checked=true;
else
    gridview.rows[0].cells[0].children[0].checked=false;
}

//............Count Checked Check Boxes
function CountChecked(cntl)
{
    gridview=document.getElementById(cntl);
    var checkBoxCount = 0;  
    for(i=1; i<gridview.rows.length-1;i++) 
    {
        if(gridview.rows[i].cells[0].children[0].checked ==true) checkBoxCount++; 
    }    
    return parseInt(checkBoxCount);
}

/*......................... End Functions Required for Grid View......................................................*/

/*.........................Required field validator for Radio Button list or CheckBoxList............................*/
function ChceckRBL(cntl)
{
    var rbl=document.getElementById(cntl);
    var models = rbl.getElementsByTagName("input");
    var checkBoxCount = 0;  
    for(var i = 0; i < models.length; i++)
    {
	    if(models[i].checked ==true) checkBoxCount++;	    
    }    
    if(checkBoxCount==0)
    {
        alert("Select "+rbl.title);
        return false;
    }
    return true;
}

function SelectedIndex(cntl)
{
    var rbl=document.getElementById(cntl);
    var models = rbl.getElementsByTagName("input");
    var checkBoxCount = 0;  
    var i;
    for(var i = 0; i < models.length; i++)
    {
	    if(models[i].checked ==true) break;	    
    }
    return i;
}
/*.....................End Required field validator for Radio Button list or CheckBoxList............................*/


/*......................... Delete Confirmation................................................................*/
function CofirmDelete(cat)
{
return confirm('Are you sure you are going to Delete '+cat+'?');
}
/*......................... End Delete Confirmation................................................................*/

/*.........................Functions for Ajax Controls............................................*/

//Set Dynamic key for dynamic populate extender
function SetDynamicKey(bid,value)
{ 
    var behavior = $find(bid);    
    if(behavior) 
    { 
        behavior.populate(value); 
    } 
} 

//Set Context Key for Cascading DropDown
function SetContextKey(bid,value)
{ 
    var behavior = $find(bid);    
    if(behavior) 
    { 
        behavior.set_contextKey(value); 
    } 
}
/*.........................End Functions for Ajax Controls.......................................*/



/*.........................Zoom In Zomm out.....................................................*/
//Specify affected tags. Add or remove from list:
var tgs = new Array( 'div','td','tr', 'p', 'span', 'font');
    
//Specify spectrum of different font sizes:
//var szs = new Array('xx-small','x-small','smaller','small','medium','large','larger','x-large','xx-large' );
var szs = new Array('xx-small','x-small','small','medium','large','x-large','xx-large' );
var startSz = 2;
function ts( trgt,inc ) 
{
    if (!document.getElementById) return
    var d = document,cEl = null,sz = startSz,i,j,cTags;
    sz += inc;
    if ( sz < 0 ) sz = 0;
    if ( sz > 6 ) sz = 6;
    startSz = sz;
    if ( !( cEl = d.getElementById( trgt ) ) ) cEl = d.getElementsByTagName( trgt )[ 0 ];
    cEl.style.fontSize = szs[ sz ];
    for ( i = 0 ; i < tgs.length ; i++ ) {
        cTags = cEl.getElementsByTagName( tgs[ i ] );
        for ( j = 0 ; j < cTags.length ; j++ ) cTags[ j ].style.fontSize = szs[ sz ];
    }
}
/*.........................End Zoom In Zomm out.....................................................*/
/*........................Numeric Validations starts.......................................................

function IsNumeric1(evt)  {
  GridView=document.getElementById("<%=Vengrid.ClientID %>");
   for ( var rowCount = 1; rowCount < GridView.rows.length-1; rowCount++ ) 
   {
     
//      var rqty=GridView.rows(rowCount).cells(10).children[0].value;
   var theEvent = evt|| window.event;
  var key = theEvent.keyCode || theEvent.which;
  key = String.fromCharCode( key );
  var regex = /[0-9]|\./;
  if( !regex.test(key) ) {
    theEvent.returnValue = false;
//    theEvent.preventDefault();
      }
   }
   /*........................Numeric Validations ends.......................................................*/
