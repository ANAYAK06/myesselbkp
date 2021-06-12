<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetProfitandLossDetailedview.aspx.cs"
    Inherits="GetProfitandLossDetailedview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style id="P&amp;L_Format_20676_Styles">
<!--table
	{mso-displayed-decimal-separator:"\.";
	mso-displayed-thousand-separator:"\,";}
.xl1520676
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6320676
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6420676
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#D8E4BC;
	mso-pattern:black none;
	white-space:nowrap;}
.xl6520676
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:11.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#D8E4BC;
	mso-pattern:black none;
	white-space:nowrap;}
.xl6620676
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:14.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#C4D79B;
	mso-pattern:black none;
	white-space:nowrap;}
.xl6720676
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:black;
	font-size:14.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Calibri, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:right;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#C4D79B;
	mso-pattern:black none;
	white-space:nowrap;}
-->
</style>
<style>


td {
    display: table-cell;
    vertical-align: inherit;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="P&amp;L_Format_20676" align="center" x:publishsource="Excel">
        <table border="0" cellpadding="0" cellspacing="0" width="665" style='border-collapse: collapse;
            table-layout: fixed; width: 499pt'>
          <%--  <col width="551" style='mso-width-source: userset; mso-width-alt: 20150; width: 413pt'>
            <col width="114" style='mso-width-source: userset; mso-width-alt: 4169; width: 86pt'>--%>
            <tr height="25" style='height: 18.75pt'>
                <td height="25" class="xl6620676" width="551" style='height: 18.75pt; width: 413pt' colspan="2">
                    Sub Group Name
                </td>
                <td class="xl6720676" width="114" style='border-left: none; width: 86pt'>
                    Rs.
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:TreeView ID="tvresult" runat="server" Target="_blank" >
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </div>
    </form>
   <%-- <script>
        $('.table-striped tr').each(function () {
            $(this).children('td').eq(3).remove();
        });
    </script>--%>
</body>

</html>
