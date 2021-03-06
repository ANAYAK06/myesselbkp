// *****************************************************************************
//                  Cross-Browser Javascript pop-up calendar.
//
// Author  : Anthony Garrett
//
// Credits : I wrote this from scratch myself but I couldn't have done it
//           without the superb "JavaScript The Definitive Guide" by David
//           Flanagan (Pub. O'Reilly ISBN 0-596-00048-0).  I also recognise
//           a contribution from my experience with PopCalendar 4.1 by
//           Liming(Victor) Weng.
//
// Rights  : Feel free to copy and change this as you like except that I
//           regard it as polite to leave the first twenty-one lines as is.
//
// Contact : Sorry, I can't offer support for this but if you find a problem
//           (or just want to tell me how useful you find it), please send
//           me an email at scwfeedback@tarrget.info (Note the two Rs in
//           tarrget).  I will try to fix problems quickly but this is a
//           spare time thing for me.
//
// *****************************************************************************
//
// Features: Easily customised
//                  (output date format, colours, language, year range and
//                   week start day)
//           Accepts a date as input
//                  (see comments below for formats).
//           Cross-browser code tested against;
//                  Internet Explorer 6.0.28     Mozilla  1.7.1
//                  Opera             7.52+      Firefox  0.9.1+
//                  Konqueror         3.4.0      Flock    0.4.9
//
// How to add the Calendar to your page:
//           This script needs to be defined for your page so, immediately after
//           the BODY tag add the following line;
//
//                  <script type='Text/JavaScript' src='scw.js'></script>
//
// How to use the Calendar once it is defined for your page:
//           Simply choose an event to trigger the calendar (like an onClick or
//           an onMouseOver) and an element to work on (for the calendar to take
//           its initial date from and write its output date to) then write it
//           like this;
//                      <<event>>="scwShow(<<element>>,this);"
//
//              e.g. onClick="scwShow(document.getElementById('myElement'),this);"
//              or   onMouseOver="scwShow(this,this);"
//
// *****************************************************************************
//
// Version   Date        By               Description
// =======   ====        ===============  ===========
//   1.0     2004-08-02  Anthony Garrett  Initial release
//   1.1     2005-10-17  Anthony Garrett  Added requested feature to allow
//                                        a click anywhere on the calling page
//                                        to cancel the calendar.
//                                        Added "How to" paragraphs to
//                                        documentation (above).
//                                        Corrected bug that misread numeric
//                                        seed months as one less than entered.
//   1.2     2005-10-26  Anthony Garrett  Allow start of week to be any day.
//   2.0     2005-11-03  Anthony Garrett  Add an IFRAME behind the calendar to
//                                        deal with IE SELECT boxes.
//                                        Renamed all exposed variables and
//                                        functions but kept showCal as entry
//                                        point for backward compatibility.
//                                        Added classes to all HTML elements
//                                        and moved all style attributes to
//                                        inline stlye sheet in customisation
//                                        section.
//   2.1     2005-11-10  Anthony Garrett  Fixed a bug which causes the calendar
//                                        not to display in Firefox when the
//                                        event trigger element's parent was
//                                        not the data element's parent.
//                                        NOTE: This has forced me to add a
//                                              second interface parameter!
//   2.2     2005-11-17  Anthony Garrett  Added input date validation.
//                                        Added input date highlighting (thanks
//                                        to Brad Allan for that suggestion).
//                                        Added optional strict date processing
//                                        (e.g. making 31-Sep an error instead
//                                        of returning 1-Oct). Improved the
//                                        calendar positioning so that CSS
//                                        positioning using DIVs is handled
//                                        correctly.
//   2.3     2005-11-23  Anthony Garrett  Corrected input validation for US
//                                        and other date formats.  Added
//                                        examples for US date processing.
//   2.4     2005-12-10  Anthony Garrett  Added feature to allow disabling
//                                        of specific days of the week, dates
//                                        and date ranges.  Made it optional
//                                        that clicking on the calendar itself
//                                        causes the pop-up to be hidden.
//                                        Thanks to Felix Joussein for the
//                                        feedback.
//   2.41    2005-12-12  Anthony Garrett  Small fix for "Today" display when
//                                        there are no disabled dates.
//   2.42    2005-12-15  Anthony Garrett  Fixed bug where calendar exceeded end
//                                        of range by one month.
//   2.5     2005-12-23  Anthony Garrett  Implemented optional disabling of
//                                        displayed out of range days which
//                                        can occur at the start of the very
//                                        first month and end of the very last.
//                                        Tidied up cursor on calendar
//                                        (thanks to Lou LaRocca for that
//                                        suggestion and others under
//                                        consideration).
//                                        Replaced all browser sniffing with
//                                        more reliable techniques.
//                                        Tightened up and improved access
//                                        to month display with week start
//                                        days other than Zero (Sunday).
//   2.6     2006-01-04  Anthony Garrett  Fixed bug where "disabled" today's
//                                        date could still be used.
//                                        Modified to work with Konqueror.
//                                        Simplified calendar positioning (and
//                                        made it more robust) - Thanks to
//                                        Brad Rubenstein for that suggestion.
//
// *****************************************************************************

// This date is used throughout to determine today's date.

    var scwDateNow = new Date(Date.parse(new Date().toDateString()));

//------------------------------------------------------------------------------
// Customisation section
//------------------------------------------------------------------------------

    // Set the bounds for the calendar here...
    // If you want the year to roll forward you can use something like this...
    //      var scwBaseYear = scwDateNow.getFullYear()-5;
    // alternatively, hard code a date like this...
    //      var scwBaseYear = 1990;

    var scwBaseYear        = scwDateNow.getFullYear()-65;

    // How many years do want to be valid and to show in the drop-down list?

    var scwDropDownYears   = 80;

    // All language dependent changes can be made here...

    var scwToday               = 'Today:',
        scwInvalidDateMsg      = 'The entered date is invalid.\n',
        scwOutOfRangeMsg       = 'The entered date is out of range.',
        scwDoesNotExistMsg     = 'The entered date does not exist.',
        scwInvalidAlert        = ['Invalid date (',') ignored.'],
        scwDateDisablingError  = ['Error ',' is not a Date object.'],
        scwRangeDisablingError = ['Error ',' should consist of two elements.'],
        scwArrMonthNames       = ['Jan','Feb','Mar','Apr','May','Jun',
                                  'Jul','Aug','Sep','Oct','Nov','Dec'],
        scwArrWeekInits        = ['S','M','T','W','T','F','S'];

    // Note:  Always start the scwArrWeekInits array with your string for
    //        Sunday whatever scwWeekStart (below) is set to.

    // scwWeekStart determines the start of the week in the display
    // Set it to: 0 (Zero) for Sunday, 1 (One) for Monday etc..

    var scwWeekStart       =    0;

    // Set the allowed date delimiters here...
    // E.g. To set the rising slash, hyphen, full-stop (aka stop or point)
    //      and comma as delimiters use
    //              var scwArrDelimiters   = ['/','-','.',','];

    var scwArrDelimiters   = ['/','-','.',','];

    // scwZindex controls how the pop-up calendar interacts with the rest
    // of the page.  It is usually adequate to leave it as 1 (One) but I
    // have made it available here to help anyone who needs to alter the
    // level in order to ensure that the calendar displays correctly in
    // relation to all other elements on the page.

    var scwZindex          = 1;

    // Personally I like the fact that entering 31-Sep-2005 displays
    // 1-Oct-2005, however you may want that to be an error.  If so,
    // set scwBlnStrict = true.  That will cause an error message to
    // display and the selected month is displayed without a selected
    // day. Thanks to Brad Allan for his feedback prompting this feature.

    var scwBlnStrict       = false;

    // If you wish to disable any displayed day, e.g. Every Monday
    // you can do it by setting the following array.  The array elements
    // match the displayed cells.
    //
    // You could put something like the following in your calling page
    // to disable all weekend days;
    //
    //  for (var i=0;i<scwEnabledDay.length;i++)
    //      {if (i%7==0 || i%7==6)  // Disable all weekend days
    //          {scwEnabledDay[i] = false;}
    //      }

    var scwEnabledDay      = [true, true, true, true, true, true, true,
                              true, true, true, true, true, true, true,
                              true, true, true, true, true, true, true,
                              true, true, true, true, true, true, true,
                              true, true, true, true, true, true, true,
                              true, true, true, true, true, true, true];

    // You can disable any specific date by creating an element of the
    // array scwDisabledDates as a date object with the value you want
    // to disable.  Date ranges can be disabled by placing an array
    // of two values (Start and End) into an element of this array.

    var scwDisabledDates   = new Array();

    // e.g. To disable 10-Dec-2005:
    //          scwDisabledDates[0] = new Date(2005,11,10);
    //
    //      or a range from 2004-Dec-25 to 2005-Jan-01:
    //          scwDisabledDates[1] = [new Date(2004,11,25),new Date(2005,0,1)];
    //
    // Remember that Javascript months are Zero-based.

    // The disabling by date and date range does prevent the current day
    // from being selected.  Disabling days of the week does not so you can set
    // the scwActiveToday value to false to prevent selection.

    var scwActiveToday = true;

    // Dates that are out of the specified range can be displayed at the start
    // of the very first month and end of the very last.  Set
    // scwOutOfRangeDisable to  true  to disable these dates (or  false  to
    // allow their selection).

    var scwOutOfRangeDisable = true;

    // Closing the calendar by clicking on it (rather than elsewhere on the
    // main page) can be inconvenient.  The scwClickToHide boolean value
    // controls this feature.

    var scwClickToHide = false;

    // Blend the colours into your page here...

    var scwBackground           = '#B0ADAD;'//'#AA5120';//'#ECA659';    // Calendar background
    var scwHeadText             = '#fffff';    // Colour of week headings

    // If you want to "turn off" any of the highlighting then just
    // set the highlight colours to the same as the un-higlighted colours.

    // Today string
    var scwTodayText            = '#FFFFFF',
        scwTodayHighlight       = '#010101';

    // Active Cell
    var scwHighlightText        = '#000000',
        scwHighlightBackground  = '#8B8A8A';

    // Weekend Days
    var scwWeekendText          = '#9E0404',
        scwWeekendBackground    = '#E9E6E6';

    // Days out of current month
    var scwExMonthText          = '#ffffff',
        scwExMonthBackground    = '#CCCCCC';

    // Current month's weekdays
    var scwCellText             = '#000000',
        scwCellBackground       = '#CCCCCC';

    // Input date
    var scwInDateText           = '#FF0000',
        scwInDateBackground     = '#FFCCCC';

    // Disabled days
    var scwDisabledDayText       = '#993333',
        scwDisabledDayBackground = '#999999';


    // I have made every effort to isolate the pop-up script from any
    // CSS defined on the main page but if you have anything set that
    // affects the pop-up (or you may want to change the way it looks)
    // then you can address it here.
    //
    // The classes are;
    //      scw         Overall
    //      scwHead     The Selection buttons/drop-downs
    //      scwWeek     The Day Initials (Column Headings)
    //      scwCells    The Individual days
    //      scwFoot     The "Today" selector

    document.writeln("<style>");
    document.writeln(   '.scw       {padding:1px;vertical-align:middle;}');
    document.writeln(   'iframe.scw {position:absolute;z-index:' + scwZindex    +
                                    ';top:0px;left:0px;visibility:hidden;'      +
                                    'width:1px;height:1px;}');
    document.writeln(   'table.scw  {padding:0px;visibility:hidden;'            +
                                    'position:absolute;width:200px;'            +
                                    'top:0px;left:0px;z-index:' + (scwZindex+1) +
                                    ';text-align:center;cursor:default;'        +
                                    'padding:1px;vertical-align:middle;'        +
                                    'background-color:' + scwBackground         +
                                    ';border:ridge 2px;font-size:10pt;'         +
                                    'font-family:Arial,Helvetica,Sans-Serif;'   +
                                    'font-weight:bold;}');
    document.writeln(   'td.scwHead     {padding:0px 0px;text-align:center;}');
    document.writeln(   'select.scwHead {margin:3px 1px;}');
    document.writeln(   'input.scwHead  {height:22px;width:22px;'               +
                                        'vertical-align:middle;'                +
                                        'text-align:center;margin:2px 1px;'     +
                                        'font-size:10pt;font-family:fixedSys;'  +
                                        'font-weight:bold;}');
    document.writeln(   'tr.scwWeek     {text-align:center;font-weight:bold;'   +
                                        'color:' + scwHeadText + ';}');
    document.writeln(   'td.scwWeek     {padding:0px;}');
    document.writeln(   'table.scwCells {text-align:right;font-size:8pt;'       +
                                        'width:96%;font-family:'                +
                                        'Arial,Helvetica,Sans-Serif;}');
    document.writeln(   'td.scwCells {padding:3px;vertical-align:middle;'       +
                                     'width:16px;height:16px;font-weight:bold;' +
                                     'color:' + scwCellText                     +
                                     ';background-color:' + scwCellBackground   +
                                     '}');
    document.writeln(   'td.scwFoot  {padding:0px;text-align:center;'           +
                                     'font-weight:normal;color:'                +
                                      scwTodayText + ';}');
    document.writeln(   'td.scwClear  {padding:0px;text-align:left;'           +
                                     'font-weight:normal;color:'                +
                                      ';}');
    document.writeln("</style>");

    // You can modify the input, display and output date formats in the
    // following three functions;

    function scwInputFormat(scwArrInput,scwEleValue)
        {var scwArrSeed = new Array();

         scwBlnFullInputDate = false;

         switch (scwArrInput.length)

            {case 1:
                {// Year only entry
                 scwArrSeed[0] = parseInt(scwArrInput[0],10);   // Year
                 scwArrSeed[1] = '6';                           // Month
                 scwArrSeed[2] = 1;                             // Day
                 break;
                }
             case 2:
                {// Year and Month entry
                 scwArrSeed[0] = parseInt(scwArrInput[1],10);   // Year
                 scwArrSeed[1] = scwArrInput[0];                // Month
                 scwArrSeed[2] = 1;                             // Day
                 break;
                }
             case 3:
                {// Day Month and Year entry
                 scwArrSeed[0] = parseInt(scwArrInput[2],10);   // Year
                 scwArrSeed[1] = scwArrInput[1];                // Month
                 scwArrSeed[2] = parseInt(scwArrInput[0],10);   // Day

                 // for Month, Day and Year entry use...
//                    scwArrSeed[0] = parseInt(scwArrInput[2],10);  // Year
//                    scwArrSeed[1] = scwArrInput[0];               // Month
//                    scwArrSeed[2] = parseInt(scwArrInput[1],10);  // Day

                 scwBlnFullInputDate = true;
                 break;
                }
             default:
                {// A stuff-up has led to more than three elements in the date.
                 scwArrSeed[0] = 0;     // Year
                 scwArrSeed[1] = 0;     // Month
                 scwArrSeed[2] = 0;     // Day
                }
            }
/*
         // Apply validation and report failures

         if (scwExpValYear.exec(scwArrSeed[0])  == null ||
             scwExpValMonth.exec(scwArrSeed[1]) == null ||
             scwExpValDay.exec(scwArrSeed[2])   == null)
             {alert(scwInvalidDateMsg  +
                    scwInvalidAlert[0] + scwEleValue + scwInvalidAlert[1]);
              scwBlnFullInputDate = false;
              scwArrSeed[0] = scwBaseYear + Math.floor(scwDropDownYears/2); // Year
              scwArrSeed[1] = '6';     // Month
              scwArrSeed[2] = 1;       // Day
             }
			 */

         // Return the  Year    in scwArrSeed[0]
         //             Month   in scwArrSeed[1]
         //             Day     in scwArrSeed[2]

         return scwArrSeed;
        }

    function scwDisplayFormat(scwDisplayDate)
        {// The format of the display of today's date at the foot of the
         // calendar...
         // Day Month and Year display

         document.write(scwDisplayDate.getDate()                    + '-' +
                        scwArrMonthNames[scwDisplayDate.getMonth()] + '-' +
                        scwDisplayDate.getFullYear()
                       );

         // for Month, Day and Year output use...
//         document.write(scwArrMonthNames[scwDisplayDate.getMonth()] + '/' +
//                        scwDisplayDate.getDate()                    + '/' +
//                        scwDisplayDate.getFullYear()
//                       );
        }

//Clear
    function scwClearSetOutput()
        {// Numeric months are held internally as 0 to 11 in this script so
         // the correct numeric month output should be in the form
         //                         (scwOutputDate.getMonth()+1)
         // e.g.
         //    scwTargetEle.value = ((scwOutputDate.getDate()<10)?'0':'')  +
         //                         scwOutputDate.getDate()             + '-' +
         //                         ((scwOutputDate.getMonth()<9)?'0':'')  +
         //                         (scwOutputDate.getMonth()+1)        + '-' +
         //                         scwOutputDate.getFullYear();

         // The best way I can think of (and that's no guarantee it's the very best way)
         // to display a reliable two digit year is to replace;
         // scwOutputDate.getFullYear() with
         // ((scwOutputDate.getFullYear()%100<10)?'0':'') + scwOutputDate.getFullYear()%100

         // Day Month and Year output
         scwTargetEle.value =   ((scwOutputDate.getDate()<10)?'0':'')        +
                                  scwOutputDate.getDate()                    + '-' +
                                  scwArrMonthNames[scwOutputDate.getMonth()] + '-' +
                                  scwOutputDate.getFullYear();

         // for Month, Day and Year output use...
        scwTargetEle.value =   "";
         scwTargetEle.focus();
         scwHide();
        }

//End Clear

    function scwSetOutput(scwOutputDate)
        {// Numeric months are held internally as 0 to 11 in this script so
         // the correct numeric month output should be in the form
         //                         (scwOutputDate.getMonth()+1)
         // e.g.
         //    scwTargetEle.value = ((scwOutputDate.getDate()<10)?'0':'')  +
         //                         scwOutputDate.getDate()             + '-' +
         //                         ((scwOutputDate.getMonth()<9)?'0':'')  +
         //                         (scwOutputDate.getMonth()+1)        + '-' +
         //                         scwOutputDate.getFullYear();

         // The best way I can think of (and that's no guarantee it's the very best way)
         // to display a reliable two digit year is to replace;
         // scwOutputDate.getFullYear() with
         // ((scwOutputDate.getFullYear()%100<10)?'0':'') + scwOutputDate.getFullYear()%100

         // Day Month and Year output
         scwTargetEle.value =   ((scwOutputDate.getDate()<10)?'0':'')        +
                                  scwOutputDate.getDate()                    + '-' +
                                  scwArrMonthNames[scwOutputDate.getMonth()] + '-' +
                                  scwOutputDate.getFullYear();

         // for Month, Day and Year output use...
//         scwTargetEle.value =   (((scwOutputDate.getMonth()+1)<10)?'0':'')   +
//							    (scwOutputDate.getMonth()+1)				   + '/' +
//                              ((scwOutputDate.getDate()<10)?'0':'')        +
//                                scwOutputDate.getDate()                    + '/' +
//                                scwOutputDate.getFullYear();
         scwTargetEle.select();
         scwHide();
        }

//------------------------------------------------------------------------------
// End of customisation section
//------------------------------------------------------------------------------

    var scwTargetEle,
        scwSaveText,
        scwSaveBackground,
        scwMonthSum         = 0,
        scwBlnFullInputDate = false,
        scwStartDate        = new Date(),
        scwSeedDate         = new Date(),
        scwWeekStart        = scwWeekStart%7;

    // "Escape" all the user defined date delimiters -
    // several delimiters will need it and it does no harm for the others.

    var scwExpDelimiters    = new RegExp('[\\'+scwArrDelimiters.join('\\')+']','g');

    // These regular expression validate the input date format to the
    // following rules;
    //
    // Format:              Day   1-31 (optional zero on single digits)
    //                      Month 1-12 (optional zero on single digits)
    //                            or case insensitive name
    //                      Year  Two or four digits

    // Months names and Delimiters are as defined above

    var scwExpValDay    = /^(0?[1-9]|[1-2]\d|3[0-1])$/,
        scwExpValMonth  = new RegExp("^(0?[1-9]|1[0-2]|"        +
                                     scwArrMonthNames.join("|") +
                                     ")$","i"),
        scwExpValYear   = /^(\d{2}|\d{4})$/;

    function showCal(scwEle,scwSourceEle)    {scwShow(scwEle,scwSourceEle);}
    function scwShow(scwEle,scwSourceEle)
        {//*********************************************************************
         //   If no value is preset then the seed date is
         //      Today (when today is in range) OR
         //      The middle of the date range.

         scwSeedDate = scwDateNow;

         // Strip space characters from start and end of date input
         scwEle.value = scwEle.value.replace(/^\s+/,'').replace(/\s+$/,'');

         if (scwEle.value.length==0)
            {// If no value is entered and today is within the range,
             // use today's date, otherwise use the middle of the valid range.

             scwBlnFullInputDate=false;

             if ((new Date(scwBaseYear+scwDropDownYears-1,11,31))<scwSeedDate ||
                 (new Date(scwBaseYear,0,1))                     >scwSeedDate
                )
                {scwSeedDate = new Date(scwBaseYear +
                                        Math.floor(scwDropDownYears / 2), 5, 1);
                }
            }
         else
            {// Parse the string into an array using the allowed delimiters
             scwArrSeedDate =
                 scwInputFormat(scwEle.value.split(scwExpDelimiters),
                                scwEle.value);

             // So now we have the Year, Month and Day in an array.

             //   If the year is two digits then the routine assumes a year
             //   belongs in the 21st Century unless it is less than 50 in which
             //   case it assumes the 20th Century is intended.

             if (scwArrSeedDate[0]<100)
                {scwArrSeedDate[0]= scwArrSeedDate[0] +
                                    parseInt((scwArrSeedDate[0]>50)?1900:2000,
                                             10);
                }

             // Check whether the month is in digits or an abbreviation

             if (scwArrSeedDate[1].search(/\d+/)!=0)
                {month = scwArrMonthNames.join('|').toUpperCase().
                            search(scwArrSeedDate[1].substr(0,3).toUpperCase());
                 scwArrSeedDate[1] = Math.floor(month/4)+1;
                }

             scwSeedDate = new Date(scwArrSeedDate[0],
                                    scwArrSeedDate[1]-1,
                                    scwArrSeedDate[2]);
            }

         // Test that we have arrived at a valid date

         if (isNaN(scwSeedDate))
            {alert( scwInvalidDateMsg +
                    scwInvalidAlert[0] + scwEle.value +
                    scwInvalidAlert[1]);
             scwSeedDate = new Date(scwBaseYear +
                    Math.floor(scwDropDownYears/2),5,1);
             scwBlnFullInputDate=false;
            }
         else
            {// Test that the date is within range,
             // if not then set date to a sensible date in range.

             if ((new Date(scwBaseYear,0,1)) > scwSeedDate)
                {if (scwBlnStrict) alert(scwOutOfRangeMsg);
                 scwSeedDate = new Date(scwBaseYear,0,1);
                 scwBlnFullInputDate=false;
                }
             else
                {if ((new Date(scwBaseYear+scwDropDownYears-1,11,31))<
                      scwSeedDate)
                    {if (scwBlnStrict) alert(scwOutOfRangeMsg);
                     scwSeedDate = new Date(scwBaseYear +
                                            Math.floor(scwDropDownYears)-1,
                                                       11,1);
                     scwBlnFullInputDate=false;
                    }
                 else
                    {if (scwBlnStrict && scwBlnFullInputDate &&
                          (scwSeedDate.getDate()      != scwArrSeedDate[2] ||
                           (scwSeedDate.getMonth()+1) != scwArrSeedDate[1] ||
                           scwSeedDate.getFullYear()  != scwArrSeedDate[0]
                          )
                        )
                        {alert(scwDoesNotExistMsg);
                         scwSeedDate = new Date(scwSeedDate.getFullYear(),
                                                scwSeedDate.getMonth()-1,1);
                         scwBlnFullInputDate=false;
                        }
                    }
                }
            }

         // Test the disabled dates for validity
         // Give error message if not valid.

         for (var i=0;i<scwDisabledDates.length;i++)
            {if (!((typeof scwDisabledDates[i]      == 'object') &&
                   (scwDisabledDates[i].constructor == Date)))
                {if ((typeof scwDisabledDates[i]      == 'object') &&
                     (scwDisabledDates[i].constructor == Array))
                    {var scwPass = true;

                     if (scwDisabledDates[i].length !=2)
                        {alert(scwRangeDisablingError[0] + scwDisabledDates[i] +
                               scwRangeDisablingError[1]);
                         scwPass = false;
                        }
                     else
                        {for (var j=0;j<scwDisabledDates[i].length;j++)
                            {if (!((typeof scwDisabledDates[i][j]      == 'object') &&
                                   (scwDisabledDates[i][j].constructor == Date)))
                                {alert(scwDateDisablingError[0] + scwDisabledDates[i][j] +
                                       scwDateDisablingError[1]);
                                 scwPass = false;
                                }
                            }
                        }

                     if (scwPass && (scwDisabledDates[i][0] > scwDisabledDates[i][1]))
                        {scwDisabledDates[i].reverse();}
                    }
                 else
                    {alert(scwDateDisablingError[0] + scwDisabledDates[i] +
                           scwDateDisablingError[1]);}
                }
            }

         // Calculate the number of months that the entered (or
         // defaulted) month is after the start of the allowed
         // date range.

         scwMonthSum =  12*(scwSeedDate.getFullYear()-scwBaseYear)+
                            scwSeedDate.getMonth();

         // Set the drop down boxes.

         document.getElementById('scwYears').options.selectedIndex =
            Math.floor(scwMonthSum/12);
         document.getElementById('scwMonths').options.selectedIndex=
            (scwMonthSum%12);

         // Position the calendar box

/* The modification to the outlineWidth covers the
   Opera 9.0 Beta (Build 8031) implementation of positioning.
   <!-- bug-191157@bugs.opera.com -->
   http://www.w3.org/TR/REC-CSS2/ui.html#dynamic-outlines
*/
/*       var scwCurrentStyle =
                (scwEle.currentStyle)
                    ?scwEle.currentStyle
                    :document.defaultView.getComputedStyle(scwEle,null);

         if (scwCurrentStyle.outlineWidth)
            {var scwOutlineWidth =
                parseInt(scwCurrentStyle.outlineWidth,10) +
                         ((scwCurrentStyle.outlineWidth.
                                substring(parseInt(scwCurrentStyle.
                                                    outlineWidth,10)).length>0)
                            ?scwCurrentStyle.outlineWidth.
                                substring(parseInt(scwCurrentStyle.
                                                    outlineWidth,10))
                            :'px');
             scwEle.style.outlineWidth='0px';
            }
*/
         var offsetTop =parseInt(scwEle.offsetTop ,10) +
                        parseInt(scwEle.offsetHeight,10),
             offsetLeft=parseInt(scwEle.offsetLeft,10);

//       if (scwCurrentStyle.outlineWidth) scwEle.style.outlineWidth=scwOutlineWidth;

         scwTargetEle=scwEle;

         do {scwEle=scwEle.offsetParent;
             offsetTop +=parseInt(scwEle.offsetTop,10);
             offsetLeft+=parseInt(scwEle.offsetLeft,10);
            }
         while (scwEle.tagName!='BODY');

         document.getElementById('scw').style.top =offsetTop +'px';
         document.getElementById('scw').style.left=offsetLeft+'px';

         if (document.getElementById('scwIframe'))
            {document.getElementById('scwIframe').style.top=offsetTop +'px';
             document.getElementById('scwIframe').style.left=offsetLeft+'px';
             document.getElementById('scwIframe').style.width=
                (document.getElementById('scw').offsetWidth-2)+'px';
             document.getElementById('scwIframe').style.height=
                (document.getElementById('scw').offsetHeight-2)+'px';
             document.getElementById('scwIframe').style.visibility='visible';
            }

         // Display the month

         scwShowMonth(0);

         // Show it on the page

         document.getElementById('scw').style.visibility='visible';

         scwCancelPropagation(scwSourceEle);
        }

    function scwCellOutput(scwEvt)
        {var scwEle = eventTrigger(scwEvt),
             scwOutputDate = new Date(scwStartDate);
         if (scwEle.nodeType==3) scwEle=scwEle.parentNode;

         scwOutputDate.setDate(scwStartDate.getDate() +
                                 parseInt(scwEle.id.substr(8),10));

         scwSetOutput(scwOutputDate);
        }

//Clear function
    function scwClearOutput()
      {scwClearSetOutput();}
    
    function scwFootOutput()
        {scwSetOutput(scwDateNow);}

    function scwCancelPropagation(scwSourceEle)
        {if (typeof event=='undefined')
                {scwSourceEle.parentNode.
                    addEventListener("click",scwStopPropagation,false);
                }
         else   {event.cancelBubble = true;}
        }

    function scwStopPropagation(scwEvt)
        {if (typeof event=='undefined')
              scwEvt.stopPropagation();
         else scwEvt.cancelBubble = true;
        }

    function scwHighlight(e)
        {var scwEle = eventTrigger(e);

         if (scwEle.nodeType==3) scwEle=scwEle.parentNode;

         scwSaveText        =scwEle.style.color;
         scwSaveBackground  =scwEle.style.backgroundColor;

         scwEle.style.color             =scwHighlightText;
         scwEle.style.backgroundColor   =scwHighlightBackground;

         return true;
        }

    function scwUnhighlight(e)
        {var scwEle = eventTrigger(e);

         if (scwEle.nodeType==3) scwEle =scwEle.parentNode;

         scwEle.style.backgroundColor   =scwSaveBackground;
         scwEle.style.color             =scwSaveText;

         return true;
        }

    function eventTrigger(e)
        {if (!e) e = event;
         return e.target||e.srcElement;
        }

    function scwCancel(e)
        {if (scwClickToHide) scwHide();
         scwStopPropagation(e);
        }

    function scwHide()
        {document.getElementById('scw').style.visibility='hidden';
         if (document.getElementById('scwIframe'))
            {document.getElementById('scwIframe').style.visibility='hidden';}
        }

    function scwClearOver()
        {document.getElementById('scwClear').style.color=scwTodayHighlight;
         document.getElementById('scwClear').style.fontWeight='bold';
        }

    function scwClearOut()
        {document.getElementById('scwClear').style.color=scwTodayText;
         document.getElementById('scwClear').style.fontWeight='normal';
        }


    function scwFootOver()
        {document.getElementById('scwFoot').style.color=scwTodayHighlight;
         document.getElementById('scwFoot').style.fontWeight='bold';
        }

    function scwFootOut()
        {document.getElementById('scwFoot').style.color=scwTodayText;
         document.getElementById('scwFoot').style.fontWeight='normal';
        }

    function scwShowMonth(scwBias)
        {// Set the selectable Month and Year
         // May be called: from the left and right arrows
         //                  (shift month -1 and +1 respectively)
         //                from the month selection list
         //                from the year selection list
         //                from the showCal routine
         //                  (which initiates the display).

         var scwShowDate  = new Date(Date.parse(new Date().toDateString()));

         scwSelYears  = document.getElementById('scwYears');
         scwSelMonths = document.getElementById('scwMonths');

         if (scwSelYears.options.selectedIndex>-1)
            {scwMonthSum=12*(scwSelYears.options.selectedIndex)+scwBias;
             if (scwSelMonths.options.selectedIndex>-1)
                {scwMonthSum+=scwSelMonths.options.selectedIndex;}
            }
         else
            {if (scwSelMonths.options.selectedIndex>-1)
                {scwMonthSum+=scwSelMonths.options.selectedIndex;}
            }

         scwShowDate.setFullYear(scwBaseYear + Math.floor(scwMonthSum/12),
                                 (scwMonthSum%12),
                                 1);

         if ((12*parseInt((scwShowDate.getFullYear()-scwBaseYear),10)) +
             parseInt(scwShowDate.getMonth(),10) < (12*scwDropDownYears)    &&
             (12*parseInt((scwShowDate.getFullYear()-scwBaseYear),10)) +
             parseInt(scwShowDate.getMonth(),10) > -1)
            {scwSelYears.options.selectedIndex=Math.floor(scwMonthSum/12);
             scwSelMonths.options.selectedIndex=(scwMonthSum%12);

             scwCurMonth = scwShowDate.getMonth();

             scwShowDate.setDate((((scwShowDate.getDay()-scwWeekStart)<0)?-6:1)+
                                 scwWeekStart-scwShowDate.getDay());

             scwStartDate = new Date(scwShowDate);

//Clear

             var scwClear = document.getElementById('scwClear');

             if (scwDisabledDates.length==0)
                {if (scwActiveToday)
                    {scwClear.onclick     =scwClearOutput;
                     scwClear.onmouseover =scwClearOver;
                     scwClear.onmouseout  =scwClearOut;
                     scwClear.style.cursor=(document.getElementById('scwIframe'))
                                            ?'hand':'pointer';
                    }
                 else
                    {scwClear.onclick     =null;
                     if (document.addEventListener)
                            {scwClear.addEventListener('click',scwStopPropagation, false);}
                     else   {scwClear.attachEvent('onclick',scwStopPropagation);}
                     scwClear.onmouseover =null;
                     scwClear.onmouseout  =null;
                     scwClear.style.cursor='default';
                    }
                }
             else
                {for (var k=0;k<scwDisabledDates.length;k++)
                    {if (!scwActiveToday ||
                         ((typeof scwDisabledDates[k] == 'object')                      &&
                             (((scwDisabledDates[k].constructor == Date)                &&
                               scwDateNow.valueOf() == scwDisabledDates[k].valueOf()
                              ) ||
                              ((scwDisabledDates[k].constructor == Array)               &&
                               scwDateNow.valueOf() >= scwDisabledDates[k][0].valueOf() &&
                               scwDateNow.valueOf() <= scwDisabledDates[k][1].valueOf()
                              )
                             )
                         )
                        )
                        {scwClear.onclick     =null;
                         if (document.addEventListener)
                                {scwClear.addEventListener('click',scwStopPropagation, false);}
                         else   {scwClear.attachEvent('onclick',scwStopPropagation);}
                         scwClear.onmouseover =null;
                         scwClear.onmouseout  =null;
                         scwClear.style.cursor='default';
                         break;
                        }
                     else
                        {scwClear.onclick     =scwClearOutput;
                         scwClear.onmouseover =scwClearOver;
                         scwClear.onmouseout  =scwClearOut;
                         scwClear.style.cursor=(document.getElementById('scwIframe'))
                                                ?'hand':'pointer';
                        }
                    }
                }



//End Clear




             var scwFoot = document.getElementById('scwFoot');

             if (scwDisabledDates.length==0)
                {if (scwActiveToday)
                    {scwFoot.onclick     =scwFootOutput;
                     scwFoot.onmouseover =scwFootOver;
                     scwFoot.onmouseout  =scwFootOut;
                     scwFoot.style.cursor=(document.getElementById('scwIframe'))
                                            ?'hand':'pointer';
                    }
                 else
                    {scwFoot.onclick     =null;
                     if (document.addEventListener)
                            {scwFoot.addEventListener('click',scwStopPropagation, false);}
                     else   {scwFoot.attachEvent('onclick',scwStopPropagation);}
                     scwFoot.onmouseover =null;
                     scwFoot.onmouseout  =null;
                     scwFoot.style.cursor='default';
                    }
                }
             else
                {for (var k=0;k<scwDisabledDates.length;k++)
                    {if (!scwActiveToday ||
                         ((typeof scwDisabledDates[k] == 'object')                      &&
                             (((scwDisabledDates[k].constructor == Date)                &&
                               scwDateNow.valueOf() == scwDisabledDates[k].valueOf()
                              ) ||
                              ((scwDisabledDates[k].constructor == Array)               &&
                               scwDateNow.valueOf() >= scwDisabledDates[k][0].valueOf() &&
                               scwDateNow.valueOf() <= scwDisabledDates[k][1].valueOf()
                              )
                             )
                         )
                        )
                        {scwFoot.onclick     =null;
                         if (document.addEventListener)
                                {scwFoot.addEventListener('click',scwStopPropagation, false);}
                         else   {scwFoot.attachEvent('onclick',scwStopPropagation);}
                         scwFoot.onmouseover =null;
                         scwFoot.onmouseout  =null;
                         scwFoot.style.cursor='default';
                         break;
                        }
                     else
                        {scwFoot.onclick     =scwFootOutput;
                         scwFoot.onmouseover =scwFootOver;
                         scwFoot.onmouseout  =scwFootOut;
                         scwFoot.style.cursor=(document.getElementById('scwIframe'))
                                                ?'hand':'pointer';
                        }
                    }
                }

             // Treewalk to display the dates.
             // I tried to use getElementsByName but IE refused to cooperate
             // so I resorted to this method which works for all tested
             // browsers.

             var scwCells = document.getElementById('scwCells');
             for (i=0;i<scwCells.childNodes.length;i++)
                {var scwRows = scwCells.childNodes[i];
                 if (scwRows.nodeType==1 && scwRows.tagName=='TR')
                    {for (j=0;j<scwRows.childNodes.length;j++)
                        {var scwCols = scwRows.childNodes[j];
                         if (scwCols.nodeType==1 && scwCols.tagName=='TD')
                            {scwRows.childNodes[j].innerHTML=
                                    scwShowDate.getDate();

                             var scwCellStyle=scwRows.childNodes[j].style,
                                 scwDisabled = (scwOutOfRangeDisable &&
                                                 (scwShowDate < (new Date(scwBaseYear,0,1)) ||
                                                  scwShowDate > (new Date(scwBaseYear+scwDropDownYears-1,11,31))
                                                 ))?true:false;

                             for (var k=0;k<scwDisabledDates.length;k++)
                                {if ((typeof scwDisabledDates[k]      == 'object')  &&
                                     (scwDisabledDates[k].constructor == Date)      &&
                                     scwShowDate.valueOf() == scwDisabledDates[k].valueOf())
                                    {scwDisabled = true;}
                                 else
                                    {if ((typeof scwDisabledDates[k]      == 'object') &&
                                         (scwDisabledDates[k].constructor == Array)    &&
                                         scwShowDate.valueOf() >= scwDisabledDates[k][0].valueOf() &&
                                         scwShowDate.valueOf() <= scwDisabledDates[k][1].valueOf())
                                        {scwDisabled = true;}
                                    }
                                }

                             if (scwDisabled || !scwEnabledDay[j+(7*((i*scwCells.childNodes.length)/6))])
                                {scwRows.childNodes[j].onclick     =null;
                                 scwRows.childNodes[j].onmouseover =null;
                                 scwRows.childNodes[j].onmouseout  =null;
                                 scwRows.childNodes[j].style.cursor='default';
                                 scwCellStyle.color=scwDisabledDayText;
                                 scwCellStyle.backgroundColor=
                                     scwDisabledDayBackground;
                                }
                             else
                                {scwRows.childNodes[j].onclick      =scwCellOutput;
                                 scwRows.childNodes[j].onmouseover  =scwHighlight;
                                 scwRows.childNodes[j].onmouseout   =scwUnhighlight;
                                 scwRows.childNodes[j].style.cursor =(document.getElementById('scwIframe'))
                                                                        ?'hand':'pointer';

                                 if (scwShowDate.getMonth()!=scwCurMonth)
                                    {scwCellStyle.color=scwExMonthText;
                                     scwCellStyle.backgroundColor=
                                         scwExMonthBackground;
                                    }
                                 else if (scwBlnFullInputDate &&
                                          scwShowDate.toDateString()==
                                          scwSeedDate.toDateString())
                                    {scwCellStyle.color=scwInDateText;
                                     scwCellStyle.backgroundColor=
                                         scwInDateBackground;
                                    }
                                 else if (scwShowDate.getDay()%6==0)
                                    {scwCellStyle.color=scwWeekendText;
                                     scwCellStyle.backgroundColor=
                                         scwWeekendBackground;
                                    }
                                 else
                                    {scwCellStyle.color=scwCellText;
                                     scwCellStyle.backgroundColor=
                                         scwCellBackground;
                                    }
                                }

                             scwShowDate.setDate(scwShowDate.getDate()+1);
                            }
                        }
                    }
                }
            }
        }
    document.write(
     "<!--[if IE]>" +
        "<iframe class='scw' " +
                "id='scwIframe' name='scwIframe' " +
                "frameborder='0'>" +
        "</iframe>" +
     "<![endif]-->" +
     "<table id='scw' class='scw'>" +
       "<tr class='scw'>" +
         "<td class='scw'>" +
           "<table class='scwHead' id='scwHead' " +
                    "cellspacing='0' cellpadding='0' width='100%'>" +
            "<tr class='scwHead'>" +
                "<td class='scwHead'>" +
                    "<input class='scwHead' type='button' value='<' " +
                            "onclick='scwShowMonth(-1);'  /></td>" +
                 "<td class='scwHead'>" +
                    "<select id='scwMonths' class='scwHead' " +
                            "onChange='scwShowMonth(0);'>");

    for (i=0;i<scwArrMonthNames.length;i++)
        document.write(   "<option>" + scwArrMonthNames[i] + "</option>");

    document.write("   </select>" +
                 "</td>" +
                 "<td class='scwHead'>" +
                    "<select id='scwYears' class='scwHead' " +
                            "onChange='scwShowMonth(0);'>");

    for (i=0;i<scwDropDownYears;i++)
        document.write(   "<option>" + (scwBaseYear+i) + "</option>");

    document.write(   "</select>" +
                 "</td>" +
                 "<td class='scwHead'>" +
                    "<input class='scwHead' type='button' value='>' " +
                            "onclick='scwShowMonth(1);' /></td>" +
                "</tr>" +
              "</table>" +
            "</td>" +
          "</tr>" +
          "<tr class='scw'>" +
            "<td class='scw'>" +
              "<table class='scwCells' align='center'>" +
                "<thead class='scwWeek'>" +
                  "<tr  class='scwWeek'>");

    for (i=0;i<scwArrWeekInits.length;i++)
        document.write( "<td class='scwWeek' id='scwWeekInit" + i + "'>" +
                          scwArrWeekInits[(i+scwWeekStart)%scwArrWeekInits.length] +
                        "</td>");

    document.write("</tr>" +
                "</thead>" +
                "<tbody class='scwCells' id='scwCells'>");

    for (i=0;i<6;i++)
        {document.write(
                    "<tr class='scwCells'>");
         for (j=0;j<7;j++)
            {document.write(
                        "<td class='scwCells' id='scwCell_" + (j+(i*7)) +
                        "'></td>");
            }

         document.write(
                    "</tr>");
        }

    document.write(
                "</tbody>");

    if ((new Date(scwBaseYear + scwDropDownYears, 11, 32)) > scwDateNow &&
        (new Date(scwBaseYear, 0, 0))                      < scwDateNow)
        {document.write(
                  "<tfoot class='scwFoot'>" +
                    "<tr class='scwFoot'>" +
                      "<td class='scwFoot' id='scwFoot' colspan='7'>" + scwToday + " ");

         scwDisplayFormat(scwDateNow);
         document.write(
                        "</td>" +
                     "</tr>" +
                   "</tfoot>");
        //Clear function
        document.write(
                  "<tfoot class='scwClear'>" +
                    "<tr class='scwClear'>" +
                      "<td class='scwClear' id='scwClear' colspan='7'> Clear ");
         document.write(
                        "</td>" +
                     "</tr>" +
                   "</tfoot>");
        
        }

    document.write(
              "</table>" +
            "</td>" +
          "</tr>" +
        "</table>");

    if (document.addEventListener)
        {document.addEventListener('click',scwHide, false);
         document.getElementById('scw').addEventListener('click',scwCancel,false);
         document.getElementById('scwHead').addEventListener('click',scwStopPropagation,false);
         document.getElementById('scwCells').addEventListener('click',scwStopPropagation,false);
        }
    else
        {document.attachEvent('onclick',scwHide);
         document.getElementById('scw').attachEvent('onclick',scwCancel);
         document.getElementById('scwHead').attachEvent('onclick',scwStopPropagation);
         document.getElementById('scwCells').attachEvent('onclick',scwStopPropagation);
        }

// End of Calendar

//dont allow
function DateReadonly()
{
return false;
}