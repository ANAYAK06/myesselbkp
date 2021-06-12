<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyCreditPayment.aspx.cs" Inherits="VerifyCreditPayment" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         function checkNumeric(event) {
             var kCode = event.keyCode || event.charCode; // for cross browser check

             //FF and Safari use e.charCode, while IE use e.keyCode that returns the ASCII value 
             if ((kCode > 57 || kCode < 48) && (kCode != 46 && kCode != 45)) {
                 //code for IE
                 if (window.ActiveXObject) {
                     event.keyCode = 0
                     return false;
                 }
                 else {
                     event.charCode = 0
                 }
             }
         }
    </script>
      <script type="text/javascript">
          function checkadvance() {
              var totaldeduct = document.getElementById("<%=hftotaldeduct.ClientID %>").value;
              var totalcredit = document.getElementById("<%=hftotalcredit.ClientID %>").value;
              var advance = document.getElementById("<%=txtadvance.ClientID %>").value;           

              var total = parseFloat(totaldeduct) + parseFloat(advance)
              if (totalcredit < total) {
                  alert("You are excessing the Recieved Advance");
                  document.getElementById("<%=txtadvance.ClientID %>").focus();
                  document.getElementById("<%=txtadvance.ClientID %>").value = "";
                  return false;
              }
          }
          function validate() {


              var objs = new Array("<%=txtindt.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>", "<%=txttax.ClientID %>"
                        , "<%=txtex.ClientID %>", "<%=txtfre.ClientID %>", "<%=txtins.ClientID %>", "<%=txted.ClientID %>", "<%=txthed.ClientID %>", "<%=txttotal.ClientID %>"
                        , "<%=txtretention.ClientID %>", "<%=txttds.ClientID %>", "<%=txtwct.ClientID %>", "<%=txtadvance.ClientID %>", "<%=txthold.ClientID %>", "<%=txtother.ClientID %>"
                        , "<%=txtnettax.ClientID %>", "<%=txtNetED.ClientID %>", "<%=txtNetHED.ClientID %>"
                        , "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
              if (!CheckInputs(objs)) {
                  return false;
              }
              var netamount = document.getElementById("<%= txtamt.ClientID%>").value;
              if (netamount < 0) {
                  alert("You are excessing the Invoice Amount");
                  return false;
              }
              var GridView = document.getElementById("<%=gridInvcredit.ClientID %>");
              if (GridView != null) {
                  for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {

                      if (GridView.rows(rowCount).cells(10).children(0).checked == true) {
                          var str1 = GridView.rows(rowCount).cells(11).children[0].value;

                          var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
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
                              alert("You are not able to make payment to before invoice date");
                              document.getElementById("<%=txtdate.ClientID %>").focus();
                              return false;
                          }
                          if (GridView.rows(rowCount).cells(12).children[0].value == "1" || GridView.rows(rowCount).cells(12).children[0].value == "2" || GridView.rows(rowCount).cells(12).children[0].value == "2A") {
                              alert("Invoice is not approved:" + GridView.rows(rowCount).cells(0).innerHTML);
                              return false;
                          }
                      }
                  }
              }

              var bank = document.getElementById("<%=ddlfrom.ClientID %>").value;
              var response = confirm("Do you want to Continue with the " + bank);
              document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
              if (response) {
                  return true;
              }
              else {
                  return false;
              }
          }
  
       </script>
       <script language="javascript">
           function Total() {
               var originalValue = 0;
               var originalValue1 = 0;
               var originalValue2 = 0;
               var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
               var tax = document.getElementById("<%=txttax.ClientID %>").value;
               var hftype = document.getElementById("<%=hf2.ClientID %>").value;

               var ed = document.getElementById("<%=txted.ClientID %>").value;
               var hed = document.getElementById("<%=txthed.ClientID %>").value;
               var tds = document.getElementById("<%=txttds.ClientID %>").value;
               var ret = document.getElementById("<%=txtretention.ClientID %>").value;
               var WCT = document.getElementById("<%=txtwct.ClientID %>").value;
               var adv = document.getElementById("<%=txtadvance.ClientID %>").value;
               var hold = document.getElementById("<%=txthold.ClientID %>").value;
               var other = document.getElementById("<%=txtother.ClientID %>").value;

               if (document.getElementById("<%=hftypeofpay.ClientID %>").value == "Trading Supply" || document.getElementById("<%=hftypeofpay.ClientID %>").value == "Manufacturing") {
                   var ex = document.getElementById("<%=txtex.ClientID %>").value;
                   var fre = document.getElementById("<%=txtfre.ClientID %>").value;
                   var ins = document.getElementById("<%=txtins.ClientID %>").value;
                   var nex = document.getElementById("<%=txtnetex.ClientID %>").value;
                   var nfre = document.getElementById("<%=txtnetfre.ClientID %>").value;
                   var nins = document.getElementById("<%=txtnetins.ClientID %>").value;
                   var ntax = document.getElementById("<%=txtnettax.ClientID %>").value;
                   var ned = document.getElementById("<%=txtNetED.ClientID %>").value;
                   var nhed = document.getElementById("<%=txtNetHED.ClientID %>").value;
                   if (basic == "") {
                       basic = 0;
                   }
                   if (tax == "") {
                       tax = 0;
                   }
                   if (ex == "") {
                       ex = 0;
                   }
                   if (fre == "") {
                       fre = 0;
                   }
                   if (ins == "") {
                       ins = 0;
                   }
                   if (ed == "") {
                       ed = 0;
                   }
                   if (hed == "") {
                       hed = 0;
                   }
                   if (tds == "") {
                       tds = 0;
                   }
                   if (ret == "") {
                       ret = 0;
                   }
                   if (WCT == "") {
                       WCT = 0;
                   }
                   if (adv == "") {
                       adv = 0;
                   }
                   if (hold == "") {
                       hold = 0;
                   }
                   if (other == "") {
                       other = 0;
                   }
                   if (nex == "") {
                       nex = 0;
                   }
                   if (nfre == "") {
                       nfre = 0;
                   }
                   if (nins == "") {
                       nins = 0;
                   }
                   var taxbal = 0;
                   var edbal = 0;
                   var hedbal = 0;
                   var exbal = 0;
                   var frebal = 0;
                   var insbal = 0;
                   if ((tax != ntax) && ntax != "") {
                       taxbal = ntax - tax;
                   }
                   if ((ed != ned) && ned != "") {
                       edbal = ned - ed;
                   }
                   if ((hed != nhed) && nhed != "") {
                       hedbal = nhed - hed;
                   }
                   if ((ex != nex) && nex != "") {
                       exbal = nex - ex;
                   }
                   if ((fre != nfre) && nfre != "") {
                       frebal = nfre - fre;
                   }
                   if ((ins != nins) && nins != "") {
                       insbal = nins - ins;
                   }
                   originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(ex) + parseFloat(fre) + parseFloat(ins)));
                   originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(ex) + parseFloat(fre) + parseFloat(ins)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(WCT) + parseFloat(adv) + parseFloat(hold) + parseFloat(other)) + (parseFloat(taxbal) + parseFloat(edbal) + parseFloat(hedbal) + parseFloat(exbal) + parseFloat(frebal) + parseFloat(insbal))));

                   var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                   var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                   document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                   document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;

               }
               else if (hftype == "VAT/Material Supply") {
                   var ex = document.getElementById("<%=txtex.ClientID %>").value;
                   var fre = document.getElementById("<%=txtfre.ClientID %>").value;
                   var ins = document.getElementById("<%=txtins.ClientID %>").value;
                   var nex = document.getElementById("<%=txtnettax.ClientID %>").value;
                   var nfre = document.getElementById("<%=txtnetfre.ClientID %>").value;
                   var nins = document.getElementById("<%=txtnetins.ClientID %>").value;
                   if (basic == "") {
                       basic = 0;
                   }
                   if (tax == "") {
                       tax = 0;
                   }
                   if (ex == "") {
                       ex = 0;
                   }
                   if (fre == "") {
                       fre = 0;
                   }
                   if (ins == "") {
                       ins = 0;
                   }
                   if (ed == "") {
                       ed = 0;
                   }
                   if (hed == "") {
                       hed = 0;
                   }
                   if (tds == "") {
                       tds = 0;
                   }
                   if (ret == "") {
                       ret = 0;
                   }
                   if (WCT == "") {
                       WCT = 0;
                   }
                   if (adv == "") {
                       adv = 0;
                   }
                   if (hold == "") {
                       hold = 0;
                   }
                   if (other == "") {
                       other = 0;
                   }
                   if (nex == "") {
                       nex = 0;
                   }
                   if (nfre == "") {
                       nfre = 0;
                   }
                   if (nins == "") {
                       nins = 0;
                   }
                   var taxbal = 0;
                   var frebal = 0;
                   var insbal = 0;
                   if ((tax != nex) && nex != "") {
                       taxbal = nex - tax;
                   }

                   if ((fre != nfre) && nfre != "") {
                       frebal = nfre - fre;
                   }
                   if ((ins != nins) && nins != "") {
                       insbal = nins - ins;
                   }
                   originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(fre) + parseFloat(ins)));
                   originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(fre) + parseFloat(ins)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(WCT) + parseFloat(adv) + parseFloat(hold) + parseFloat(other)) + (parseFloat(taxbal) + parseFloat(frebal) + parseFloat(insbal))));

                   var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                   var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                   document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                   document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
               }
               else {
                   var ntax = document.getElementById("<%=txtnetex.ClientID %>").value;
                   var ned = document.getElementById("<%=txtNetED.ClientID %>").value;
                   var nhed = document.getElementById("<%=txtNetHED.ClientID %>").value;
                   if (basic == "") {
                       basic = 0;
                   }
                   if (tax == "") {
                       tax = 0;
                   }

                   if (ed == "") {
                       ed = 0;
                   }
                   if (hed == "") {
                       hed = 0;
                   }
                   if (tds == "") {
                       tds = 0;
                   }
                   if (ret == "") {
                       ret = 0;
                   }
                   if (WCT == "") {
                       WCT = 0;
                   }
                   if (adv == "") {
                       adv = 0;
                   }
                   if (hold == "") {
                       hold = 0;
                   }
                   if (other == "") {
                       other = 0;
                   }
                   var taxbal = 0;
                   var edbal = 0;
                   var hedbal = 0;
                   if ((tax != ntax) && ntax != "") {
                       taxbal = ntax - tax;
                   }
                   if ((ed != ned) && ned != "") {
                       edbal = ned - ed;
                   }
                   if ((hed != nhed) && nhed != "") {
                       hedbal = nhed - hed;
                   }
                   originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)));
                   originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(WCT) + parseFloat(adv) + parseFloat(hold) + parseFloat(other)) + (parseFloat(taxbal) + parseFloat(edbal) + parseFloat(hedbal))));

                   var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                   var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                   document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                   document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
               }
           }
           
         </script>
     <style type="text/css">
        .style9
        {
            width: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
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
                        <table class="estbl" width="660px">
                            <tr>
                                <td>
                                    <h2 align="left">
                                        Invoice Credit Form</h2>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="grid-content">
                                    <table id="Table1" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                        style="background: none;" width="100%">
                                        <asp:GridView ID="gridInvcredit" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="InvoiceNo,ID,Invoice_Id"
                                            EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                            PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%" OnRowDeleting="gridInvcredit_RowDeleting" OnSelectedIndexChanged="gridInvcredit_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                    ShowSelectButton="true" />
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                <asp:BoundField DataField="po_no" HeaderText="PO NO" />
                                                <asp:BoundField DataField="CC_code" HeaderText="CC CODE" />
                                                <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                                <asp:BoundField DataField="Amount" DataFormatString="{0:0.00}" HeaderText="Credit" />
                                               <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                        ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                            </Columns>
                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                            <PagerStyle CssClass="grid pagerbar" />
                                            <HeaderStyle CssClass="grid-header" />
                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                        </asp:GridView>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdetail" runat="server">
                                <td>
                                    <table class="estbl" width="660px">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblclient" runat="server" CssClass="eslbl" Text="Client ID:" Width="100px"></asp:Label>
                                            </td>
                                            <td width="100px">
                                                <asp:TextBox ID="txtclientid" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="Client id"
                                                    Width="100px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblclientname" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblsubclient" runat="server" CssClass="eslbl" Text="Subclient ID:"
                                                    Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtsubclient" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="Subclient id"
                                                    Width="100px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblsubclientname" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblcccode" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcccode" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="CC Code"
                                                    Width="100px"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblccname" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblpono" runat="server" CssClass="eslbl" Text="PO NO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hftypeofpay" runat="server" />
                                                <asp:TextBox ID="txtpono" runat="server" CssClass="estbox" ReadOnly="true" ToolTip="PO NO"
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                            <td class="style9" id="tdinv1" runat="server" colspan="1">
                                                <asp:Label ID="lblinv" runat="server" CssClass="eslbl" Text="Invoice No:" Width="100px"></asp:Label>
                                            </td>
                                            <td style="width: 125px" id="tdinv" runat="server">
                                                <asp:HiddenField ID="hf2" runat="server" />
                                                <asp:TextBox ID="txtinvoice" Width="100px" CssClass="estbox" ReadOnly="true" runat="server"></asp:TextBox>
                                            </td>
                                            <td id="tdinv2" runat="server">
                                                <asp:Label ID="lblInvdate" runat="server" CssClass="eslbl" Text="Invoice Date:" Width="100px"></asp:Label>
                                            </td>
                                            <td id="tdinvdate" runat="server">
                                                <asp:TextBox ID="txtindt" CssClass="estbox" runat="server" ReadOnly="true" ToolTip="Invoice Date"
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trbasic" runat="server">
                                            <td>
                                                <asp:Label ID="lblrano" CssClass="eslbl" runat="server" Text="RA No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtra" CssClass="estbox" runat="server" ToolTip="RA No:" Width="100px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="lblBasic" CssClass="eslbl" runat="server" Text="Basic Value:" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                    Width="100px" onkeyup="Total();"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltax" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttax" CssClass="estbox" runat="server" ToolTip="Tax" Width="100px"
                                                    onkeyup="Total();"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="ex" runat="server">
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtex" CssClass="estbox" runat="server" ToolTip="Excise duty" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text=" EDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txted" CssClass="estbox" runat="server" ToolTip="EDCess" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="HEDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txthed" CssClass="estbox" runat="server" ToolTip="HEDCess" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="trfreigt">
                                            <td class="style9">
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Freight"></asp:Label>
                                            </td>
                                            <td colspan="0" rowspan="1" width="75px">
                                                <asp:TextBox ID="txtfre" CssClass="estbox" runat="server" ToolTip="Freight" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Insurance"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtins" CssClass="estbox" runat="server" ToolTip="Insurance" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltotal" runat="server" CssClass="eslbl" Text="Total:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttotal" CssClass="estbox" runat="server" ToolTip="Total" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="6" align="center">
                                                Deductions
                                            </th>
                                        </tr>
                                        <tr id="trtds" runat="server">
                                            <td class="style9">
                                                <asp:Label ID="lbltds" runat="server" CssClass="eslbl" Text="TDS:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttds" CssClass="estbox" runat="server" ToolTip="TDS" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblretention" runat="server" CssClass="eslbl" Text="Retention:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtretention" CssClass="estbox" runat="server" ToolTip="Retention"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbledc" runat="server" CssClass="eslbl" Text="WCT:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtwct" CssClass="estbox" runat="server" ToolTip="WCT" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradv" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Advance:"></asp:Label>
                                            </td>
                                            <td>
                                              <asp:HiddenField ID="hftotalcredit" runat="server" />
                                                <asp:HiddenField ID="hftotaldeduct" runat="server" />
                                                
                                                <asp:TextBox ID="txtadvance" CssClass="estbox" runat="server" ToolTip="Advance" Width="100px"
                                                    onkeyup="Total();" onblur="checkadvance();" onkeypress='javascript:return checkNumeric(event);' ></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Hold:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txthold" CssClass="estbox" runat="server" ToolTip="Hold" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Any Other:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtother" CssClass="estbox" runat="server" ToolTip="Any Other" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="NET" runat="server">
                                            <th colspan="6" align="center">
                                                Net Receipt Against Taxes
                                            </th>
                                        </tr>
                                        <tr id="netex" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetex" CssClass="estbox" runat="server" ToolTip="Net Excise duty"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="EDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetED" CssClass="estbox" runat="server" ToolTip="Net Receipt EDCess"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="HEDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetHED" CssClass="estbox" runat="server" ToolTip="Net Receipt HEDCess"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="NETDetails" runat="server">
                                            <td>
                                                <asp:Label ID="lblnettax" CssClass="eslbl" runat="server" Text="Sales Tax:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnettax" CssClass="estbox" runat="server" ToolTip="Net Receipt Tax"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Freight"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetfre" CssClass="estbox" runat="server" ToolTip="Net Freight"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Insurance"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetins" CssClass="estbox" runat="server" ToolTip="Net Insurance"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="center" colspan="6">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="bank" runat="server">
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                            </td>
                                            <td colspan="5" align="left">
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" AutoPostBack="true"
                                                    Width="150px">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlpayment" AutoPostBack="true" runat="server" ToolTip="Mode Of Pay"
                                                    CssClass="esddown" Width="70px">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>&nbsp&nbsp
                                                <asp:Label ID="lbldate" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="90px"></asp:TextBox><span class="starSpan">*</span>
                                                <img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:" ></asp:Label>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="100px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="250px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="Amount:"></asp:Label>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="100px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="660px">
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                    Text="Submit" OnClientClick="javascript:return validate()" 
                                                    onclick="btnsubmit_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
