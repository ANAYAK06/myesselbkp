<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLedgerFromPandL.aspx.cs"
    Inherits="ViewLedgerFromPandL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/62gvam81_styles.css" rel="stylesheet" type="text/css" />
   
     <script src="Java_Script/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="Java_Script/jquery.dataTables.min.js" type="text/javascript"></script>
    <link href="Css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style>
        /* unvisited link */
        a:link
        {
            color: blue;
        }
        
        /* visited link */
        a:visited
        {
            color: green;
        }
        
        /* mouse over link */
        a:hover
        {
            color: hotpink;
        }
        
        /* selected link */
        a:active
        {
            color: blue;
        }
    </style>
    <style type="text/css">
        #tooltip
        {
            position: absolute;
            z-index: 1000;
            border: 1px solid #111;
            padding: 5px;
            opacity: 0.85;
        }
        #tooltip h3, #tooltip div
        {
            margin: 0;
        }
    </style>
   <script type="text/javascript">

       $(document).ready(function () {

           var table = $('#example').DataTable({
               "lengthMenu": [[100, 200, 300, -1], [100, 200, 300, "All"]]
           });
           $('#example tbody').on('click', 'td.details-control', function (e) {
               var tr = $(this).closest('tr');
               var row = table.row(tr);
               if (row.child.isShown()) {
                   // This row is already open - close it
                   row.child.hide();
                   $(tr.find("img")).attr('src', 'images/details_open.png');
                   tr.removeClass('shown');
               }
               else {
                   // Open this row
                   var Ledgertype = $("#ctl00_ContentPlaceHolder1_hf").val();
                   var LedgerName = $("#ctl00_ContentPlaceHolder1_hfledgername").val();
                   $.ajax({

                       type: "POST",
                       url: "ViewLedgerFromPandL.aspx/GetTransactiondetails",

                       data: "{'Ledgertype':'" + Ledgertype + "','LedgerName':'" + LedgerName + "' ,'RecordedLedgerName': '" + $(tr.find(".details-control")).data('hidden') + "' }",
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       async: false,

                       success: function (response) {

                           //  $("#div").append('');

                           //$("#div").html(response.d);
                           row.child(response.d).show();
                           $(tr.find("img")).attr('src', 'images/details_close.png');
                           tr.addClass('shown');

                       },
                       failure: function (response) {
                           alert(response.d);
                       }
                   });


               }
           });

       });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr id="trexcel" runat="server">
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
                <asp:ImageButton ID="btnpdf" runat="server" ImageUrl="~/images/pdf1.png" OnClick="btnpdf_Click" />
                <asp:ImageButton ID="btnword" runat="server" ImageUrl="~/images/word.png" OnClick="btnword_Click" />
            </td>
        </tr>
    </table>
    <div id="htmlledger" runat="server">
    </div>
    <div id="Div1" runat="server">
    </div>
    <asp:HiddenField ID="hf" runat="server" />
    <asp:HiddenField ID="hfledgername" runat="server" />
    <div id="div" style="display: none; border-collapse: collapse">
    </div>
    </form>
</body>
</html>
