<%@ Page Title="Supplier/Service Provider Cash Payment" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VendorCashPayment.aspx.cs" Inherits="VendorCashPayment" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript">
       function checkDate(sender, args) {
           var today = new Date();
           var dd = today.getDate();
           var mm = today.getMonth() + 1; //January is 0!
           var yyyy = today.getFullYear();
           if (dd < 10) {
               dd = '0' + dd
           }

           if (mm < 10) {
               mm = '0' + mm
           }
           var month = new Array();
           month[0] = "Jan";
           month[1] = "Feb";
           month[2] = "Mar";
           month[3] = "Apr";
           month[4] = "May";
           month[5] = "Jun";
           month[6] = "Jul";
           month[7] = "Aug";
           month[8] = "Sep";
           month[9] = "Oct";
           month[10] = "Nov";
           month[11] = "Dec";
           var mmm = month[today.getMonth()];
           today = dd + '-' + mmm + '-' + yyyy;
           var str1 = document.getElementById("<%=txtdt.ClientID %>").value;
           var str2 = today;
           var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
           var dt1 = str1.substring(0, 2);
           var dt2 = str2.substring(0, 2);
           var yr1 = str1.substring(7, 11);
           var yr2 = str2.substring(7, 11);
           for (var i = 0; i < args.length; i++) {
               var month = str2.substring(3, 6);
               var month1 = str1.substring(3, 6);
               if (args[i] == month) {
                   var month = parseInt(i + 1);
                   var date2 = yr2 + "-" + month + "-" + dt2;

               }
               if (args[i] == month1) {
                   var month1 = parseInt(i + 1);
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
           //debugger;
           _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
           if (date4 > date3) {
               alert("Invalid Future Date Selection");
               document.getElementById("<%=txtdt.ClientID %>").value = "";

               return false;
           }
       }
       function checkDatepaid(sender, args) {
           var today = new Date();
           var dd = today.getDate();
           var mm = today.getMonth() + 1; //January is 0!
           var yyyy = today.getFullYear();
           if (dd < 10) {
               dd = '0' + dd
           }

           if (mm < 10) {
               mm = '0' + mm
           }
           var month = new Array();
           month[0] = "Jan";
           month[1] = "Feb";
           month[2] = "Mar";
           month[3] = "Apr";
           month[4] = "May";
           month[5] = "Jun";
           month[6] = "Jul";
           month[7] = "Aug";
           month[8] = "Sep";
           month[9] = "Oct";
           month[10] = "Nov";
           month[11] = "Dec";
           var mmm = month[today.getMonth()];
           today = dd + '-' + mmm + '-' + yyyy;
           var str1 = document.getElementById("<%=txtpaiddate.ClientID %>").value;
           var str2 = today;
           var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
           var dt1 = str1.substring(0, 2);
           var dt2 = str2.substring(0, 2);
           var yr1 = str1.substring(7, 11);
           var yr2 = str2.substring(7, 11);
           for (var i = 0; i < args.length; i++) {
               var month = str2.substring(3, 6);
               var month1 = str1.substring(3, 6);
               if (args[i] == month) {
                   var month = parseInt(i + 1);
                   var date2 = yr2 + "-" + month + "-" + dt2;

               }
               if (args[i] == month1) {
                   var month1 = parseInt(i + 1);
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
           //debugger;
           _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
           if (date4 > date3) {
               alert("Invalid Future Date Selection");
               document.getElementById("<%=txtpaiddate.ClientID %>").value = "";

               return false;
           }
           paiddatecheck();
       }
       function paiddatecheck() {
           debugger;
           var date = document.getElementById("<%=txtdt.ClientID %>").value;
           if (date == "") {
               alert("Please Select Date");
               document.getElementById("<%=txtpaiddate.ClientID %>").value = "";
               return false;
           }
           else {              
               var str1 = document.getElementById("<%=txtpaiddate.ClientID %>").value;
               var str2 = document.getElementById("<%=txtdt.ClientID %>").value;
               var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
               var dt1 = str1.substring(0, 2);
               var dt2 = str2.substring(0, 2);
               var yr1 = str1.substring(7, 11);
               var yr2 = str2.substring(7, 11);
               for (var i = 0; i < args.length; i++) {
                   var month = str2.substring(3, 6);
                   var month1 = str1.substring(3, 6);
                   if (args[i] == month) {
                       var month = parseInt(i + 1);
                       var date2 = yr2 + "-" + month + "-" + dt2;

                   }
                   if (args[i] == month1) {
                       var month1 = parseInt(i + 1);
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
               //debugger;
               _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
               if (date4 < date3) {
                   alert("Invalid Paid Date Selection");
                   document.getElementById("<%=txtpaiddate.ClientID %>").value = "";
                   return false;
               }
           }
       }

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
    </script>
     <script language="javascript" type="text/javascript">
         function SelectAllCheckboxes(spanChk) {
             var oItem = spanChk.children;
             var theBox = (spanChk.type == "checkbox") ?
                  spanChk : spanChk.children.item[0];
             xState = theBox.checked;
             elm = theBox.form.elements;
             for (i = 0; i < elm.length; i++)
                 if (elm[i].type == "checkbox" &&
                elm[i].id != theBox.id) {
                     if (elm[i].checked != xState)
                         elm[i].click();
                 }
         }
    </script>
     <script language="javascript">
         function closepopup() {
             $find('mdlreport').hide();
         }
         function showpopup() {
             $find('mdlreport').show();

         }
    </script>
    <script type="text/javascript" language="javascript">
        function SelectAll() {
            //debugger;
            var GridView2 = document.getElementById("<%=grd.ClientID %>");
            var service = document.getElementById("<%=ddlservice.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                if (service.selectedIndex == 1 || service.selectedIndex == 2) {
                    if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
                if (service.selectedIndex == 3 || service.selectedIndex == 4) {
                    if (GridView2.rows(rowCount).cells(10).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(10).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
            }
            //var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtamt.ClientID%>').value = originalValue;

        }
    </script>
    <script type="text/javascript" language="javascript">
        function Amounvalidation(amount) {
            //debugger;
            var GridView2 = document.getElementById("<%=grd.ClientID %>");
            var service = document.getElementById("<%=ddlservice.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                if (service.selectedIndex == 1 || service.selectedIndex == 2) {
                    if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
                if (service.selectedIndex == 3 || service.selectedIndex == 4) {
                    if (GridView2.rows(rowCount).cells(10).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(10).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
            }

            var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);

            if (parseFloat(originalValue) < parseFloat(document.getElementById('<%= txtamt.ClientID%>').value)) {
                window.alert("Invalid");
                document.getElementById('<%= txtamt.ClientID%>').value = "";
                return false;
            }
            if (parseFloat(document.getElementById('<%= txtamt.ClientID%>').value) == 0) {
                window.alert("Invalid");
                document.getElementById('<%= txtamt.ClientID%>').value = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function SelectAlls() {
            //debugger;
            var GridView2 = document.getElementById("<%=grds.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                    if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                        var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");

                        if (value != "") {
                            originalValue += parseFloat(value);

                        }
                    }
                }
            }
            document.getElementById('<%= txtamts.ClientID%>').value = originalValue;

        }
    </script>
    <script language="javascript">
        function closepopups() {
            $find('mdlreports').hide();
        }
        function showpopup() {
            $find('mdlreports').show();

        }
    </script>
     <script type="text/javascript" language="javascript">
         function Amounvalidations(amount) {
             //debugger;
             var GridView2 = document.getElementById("<%=grds.ClientID %>");
             var originalValue = 0;
             for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                 if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                     if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                         var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                         if (value != "") {
                             originalValue += parseFloat(value);
                         }
                     }
                 }
             }
             var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
             if (parseFloat(originalValue) < parseFloat(document.getElementById('<%= txtamts.ClientID%>').value)) {
                 window.alert("Invalid");
                 document.getElementById('<%= txtamts.ClientID%>').value = "";
                 return false;
             }
             if (parseFloat(document.getElementById('<%= txtamts.ClientID%>').value) == 0) {
                 window.alert("Invalid");
                 document.getElementById('<%= txtamts.ClientID%>').value = "";
                 return false;
             }
         }
    </script>
    <script type="text/javascript">
        function numericValidation(txtvalue) {

            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (!(document.getElementById(txtvalue.id).value)) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            else {
                var val = document.getElementById(txtvalue.id).value;
                if (charCode == 46 || (charCode > 31 && (charCode > 47 && charCode < 58))) {
                    var points = 0;
                    points = val.indexOf(".", points);
                    if (points >= 1 && charCode == 46) {
                        return false;
                    }
                    if (points == 1) {
                        var lastdigits = val.substring(val.indexOf(".") + 1, val.length);
                        if (lastdigits.length >= 2) {
                            alert("Two decimal places only allowed");
                            return false;
                        }
                    }
                    return true;
                }
                else {
                    alert("Only Numerics allowed");
                    return false;
                }
            }
        }
    </script>
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
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table class="estbl" width="745px">
                            <tr>
                                <td style="width: 150px">
                                    <asp:Label ID="lbltypetra" CssClass="eslbl" runat="server" Text="Mode of Transaction"></asp:Label>
                                </td>
                                <td style="width: 300px" colspan="2">
                                    <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Mode of Transction"
                                        RepeatDirection="Horizontal" runat="server" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="Debit">Self Debit</asp:ListItem>
                                        <asp:ListItem Value="Paid Against">Debit From Other CC</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td height="30">
                                    <asp:DropDownList ID="ddlcccode" runat="server" CssClass="esddown" ToolTip="CC Code"
                                        Width="175px" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged" AutoPostBack="true" >
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                        ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="newcostcodeP"
                                        PromptText="Select Other CC">
                                    </cc1:CascadingDropDown>
                                </td>
                            </tr>
                            <tr id="trdate" runat="server">
                                <td>
                                    <asp:Label ID="lbldate" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txtdt" runat="server" ToolTip="Date" onKeyDown="preventBackspace();"
                                        onpaste="return false;" onkeypress="return false;" CssClass="estbox"></asp:TextBox><span
                                            class="starSpan" style="cursor: not-allowed;">*</span>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdt"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtdt" OnClientDateSelectionChanged="checkDate" >                                       
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" CssClass="eslbl" Text="Paid Date"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txtpaiddate" onKeyDown="preventBackspace();" onpaste="return false;"
                                        onkeypress="return false;" runat="server" ToolTip="Paid Date" CssClass="estbox"></asp:TextBox><span
                                            class="starSpan">*</span>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtpaiddate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtpaiddate" OnClientDateSelectionChanged="checkDatepaid" >                                    
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr id="trpaymenttype" runat="server">
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Payment Category"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlvendortype" runat="server" CssClass="esddown" ToolTip="Payment Category"
                                        OnSelectedIndexChanged="ddlvendortype_SelectedIndexChanged" AutoPostBack="true"
                                        Width="300px">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Service Provider</asp:ListItem>
                                        <asp:ListItem>Supplier</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table id="tblserviceprovider" runat="server" class="estbl" width="745px">
                            <tr>
                                <th align="center">
                                    Service Provider Payment
                                </th>
                            </tr>
                            <tr id="paytype" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Payment Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlservice" Width="150px" runat="server" ToolTip="ServiceType"
                                                    CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlservice_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="vename" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                    Text="Vendor Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendor" CssClass="esddown" Width="300px" ToolTip="Vendor"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trselection" runat="server">
                                <td>
                                    <asp:Label ID="Label2" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                        Text="View Records of:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown" ToolTip="Month">
                                        <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="esddown" ToolTip="Year" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlpo" runat="server" ToolTip="Po" Width="105px" CssClass="esddown">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button1" runat="server" CssClass="esbtn" Text="Go" ToolTip="Go" Height="20px"
                                        Width="30px" align="center" OnClick="Button1_Click" />
                                </td>
                            </tr>
                            <tr id="trgrid" runat="server">
                                <td align="center" colspan="2">
                                    <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                        AutoGenerateColumns="False" Width="745px" CellPadding="4" ForeColor="#333333"
                                        GridLines="None" ShowFooter="true" Font-Size="Small" OnRowDataBound="grd_RowDataBound"
                                        DataKeyNames="InvoiceNo,po_no" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" InsertVisible="False"
                                                ReadOnly="True" FooterText="Total" />
                                            <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" HtmlEncode="false" />
                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                            <asp:BoundField DataField="Dca_code" HeaderText="DCA Code" />
                                            <asp:BoundField DataField="Vendor_id" HeaderText="Vendor ID" />
                                            <asp:BoundField DataField="BasicValue" HeaderText="Basic" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Retention" HeaderText="Retention" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Hold" HeaderText="Hold" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"  OnCheckedChanged="chkAll_CheckedChanged" />
                                                    <%--
                                                        --%>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="2px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="2px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("status")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <cc1:ModalPopupExtender ID="popview" BehaviorID="mdlreport" runat="server" TargetControlID="btnModalPopUp"
                                        PopupControlID="pnlreport" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                    <asp:Panel ID="pnlreport" runat="server" Style="display: none;">
                                        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                            height: 500px;">
                                            <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="Grdviewpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                        DataKeyNames="Dca_Code,Subdca_Code" ShowFooter="true" OnRowDataBound="Grdviewpopup_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" Visible="false" />
                                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                            <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-Width="100px" />
                                                            <asp:BoundField DataField="" HeaderText="Dca Code" />
                                                            <asp:BoundField DataField="" HeaderText="Subdca Code" />
                                                            <asp:BoundField DataField="RegdNo" HeaderText="Regd No" />
                                                            <asp:BoundField DataField="Tax_Type" HeaderText="Tax Type" />
                                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                                            <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="100px"
                                                                DataFormatString="{0:#,##,##,###.00}" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Total Amount" ItemStyle-Width="100px"
                                                                DataFormatString="{0:#,##,##,###.00}" />
                                                        </Columns>
                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                        <PagerStyle CssClass="grid pagerbar" />
                                                        <HeaderStyle CssClass="grid-header" />
                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                    </asp:GridView>
                                                    <button class="button-error pure-button" onclick="closepopup();">
                                                        Close</button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr id="trname" align="center" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtname" CssClass="estbox" runat="server" ToolTip="Name" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trpaymentdetails" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4sp" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="200px"
                                                    onkeyup="Amounvalidation(this.value);" onkeypress='return numericValidation(this);' ></asp:TextBox><span class="starSpan">*</span>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblsupplier" runat="server" class="estbl" width="745px">
                            <tr>
                                <th align="center">
                                    Supplier Payment
                                </th>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller" Text="Payment Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlservices" Width="150px" runat="server" ToolTip="ServiceType"
                                                    CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlservices_SelectedIndexChanged" >
                                                </asp:DropDownList> 
                                            </td>
                                            <td>
                                                <asp:Label ID="venames" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                    Text="Vendor Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendors" CssClass="esddown" Width="300px" ToolTip="Vendor"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvendors_SelectedIndexChanged" > 
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trselections" runat="server">
                                <td>
                                    <asp:Label ID="Label8" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller" Text="View Records of:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlmonths" runat="server" CssClass="esddown" ToolTip="Month">
                                        <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlyears" runat="server" CssClass="esddown" ToolTip="Year" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlpos" runat="server" ToolTip="Po" Width="120px" CssClass="esddown">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button1s" runat="server" CssClass="esbtn" Text="Go" ToolTip="Go" Height="20px"
                                        Width="30px" align="center" OnClick="Button1s_Click" />  <%----%>
                                </td>
                            </tr>
                            <tr id="trgrids" runat="server">
                                <td align="center" colspan="2">
                                    <asp:GridView ID="grds" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="InvoiceNo,po_no" Width="745px" CellPadding="4"
                                        ForeColor="#333333" GridLines="None"  ShowFooter="true"
                                        Font-Size="Small" OnRowDataBound="grds_RowDataBound" OnSelectedIndexChanged="grds_SelectedIndexChanged" > 
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" InsertVisible="False"
                                                ReadOnly="True" FooterText="Total" />
                                            <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" HtmlEncode="false" />
                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                            <asp:BoundField DataField="Dca_code" HeaderText="DCA Code" />
                                            <asp:BoundField DataField="Vendor_id" HeaderText="Vendor ID" />
                                            <asp:BoundField DataField="BasicValue" HeaderText="Basic" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAlls();"  />     <%-- --%>                                              
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAlls" runat="server" OnCheckedChanged="chkAlls_CheckedChanged" onclick="javascript:SelectAllCheckboxes(this);"  />    
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="2px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="2px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("status")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <cc1:ModalPopupExtender ID="popviews" BehaviorID="mdlreports" runat="server" TargetControlID="btnModalPopUps"
                                        PopupControlID="pnlreports" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                    <asp:Panel ID="pnlreports" runat="server" Style="display: none;">
                                        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                            height: 500px;">
                                            <asp:UpdatePanel ID="upindents" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="Grdviewpopups" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                        DataKeyNames="Dca_Code,Subdca_Code" 
                                                        ShowFooter="true" OnRowDataBound="Grdviewpopups_RowDataBound"> 
                                                        <Columns>
                                                            <asp:BoundField DataField="id" Visible="false" />
                                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                            <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-Width="100px" />
                                                            <asp:BoundField DataField="" HeaderText="Dca Code" />
                                                            <asp:BoundField DataField="" HeaderText="Subdca Code" />
                                                            <asp:BoundField DataField="RegdNo" HeaderText="Regd No" />
                                                            <asp:BoundField DataField="Tax_Type" HeaderText="Tax Type" />
                                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                                            <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="100px"
                                                                DataFormatString="{0:#,##,##,###.00}" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Total Amount" ItemStyle-Width="100px"
                                                                DataFormatString="{0:#,##,##,###.00}" />
                                                        </Columns>
                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                        <PagerStyle CssClass="grid pagerbar" />
                                                        <HeaderStyle CssClass="grid-header" />
                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                    </asp:GridView>
                                                    <button class="button-error pure-button" onclick="closepopups();" > <%----%>
                                                        Close</button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <asp:Button runat="server" ID="btnModalPopUps" Style="display: none" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr id="trnames" align="center" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnames" CssClass="estbox" runat="server" ToolTip="Name" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trpaymentdetailss" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="Table1">                                       
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdescs" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamts" CssClass="estbox" runat="server" ToolTip="Amount" Width="200px"
                                                   onkeyup="Amounvalidations(this.value);" onkeypress='return numericValidation(this);' ></asp:TextBox><span
                                                        class="starSpan">*</span> <%-- --%>
                                                <asp:HiddenField ID="hf1s" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblbtn" runat="server"  width="660px">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate()"  OnClick="btnsubmit_Click" />
                                    <%----%>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        function validate() {
            debugger;
            if (!ChceckRBL("<%=rbtntype.ClientID %>"))
                return false;
            var vendortype = document.getElementById("<%=ddlvendortype.ClientID %>");
            if (vendortype.selectedIndex == 1) {
                if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                    var objs = new Array("<%=ddlcccode.ClientID %>", "<%=txtdt.ClientID %>"
                        , "<%=txtpaiddate.ClientID %>", "<%=ddlvendortype.ClientID %>", "<%=ddlservice.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlpo.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
                else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                    var objs = new Array("<%=txtdt.ClientID %>"
                        , "<%=txtpaiddate.ClientID %>", "<%=ddlvendortype.ClientID %>", "<%=ddlservice.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlpo.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }

                var netamount = document.getElementById("<%= txtamt.ClientID%>").value;
                if (netamount < 0) {
                    alert("You are excessing the Invoice Amount");
                    return false;
                }
                //debugger;
                var GridView = document.getElementById("<%=grd.ClientID %>");
                if (GridView != null) {
                    var service = document.getElementById("<%=ddlservice.ClientID %>");
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (service.selectedIndex == 1) {
                            if (GridView.rows(rowCount).cells(9).children(0).checked == true) {
                                var str1 = GridView.rows(rowCount).cells(2).innerHTML;

                                var str2 = document.getElementById("<%=txtdt.ClientID %>").value;
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
                                var service = document.getElementById("<%=ddlservice.ClientID %>").value;
                                if (service != "Advance Payment") {
                                    if (parseFloat(_Diff) < 0) {
                                        alert("You are not able to make payment to before invoice date");
                                        document.getElementById("<%=txtdt.ClientID %>").focus();
                                        return false;
                                    }
                                }
//                                if (GridView.rows(rowCount).cells(11).children[0].value != "3" || GridView.rows(rowCount).cells(11).children[0].value != "4") {
//                                    alert("Invoice is not approved:" + GridView.rows(rowCount).cells(1).innerHTML);
//                                    return false;
//                                }
                            }
                        }
                        else if (service.selectedIndex == 2 || service.selectedIndex == 3) {
                            if (GridView.rows(rowCount).cells(10).children(0).checked == true) {
                                var str1 = GridView.rows(rowCount).cells(2).innerHTML;
                                var str2 = document.getElementById("<%=txtdt.ClientID %>").value;
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
                                    document.getElementById("<%=txtdt.ClientID %>").focus();
                                    return false;
                                }
//                                if (GridView.rows(rowCount).cells(12).children[0].value == "1" || GridView.rows(rowCount).cells(12).children[0].value == "2" || GridView.rows(rowCount).cells(12).children[0].value == "2A" || GridView.rows(rowCount).cells(12).children[0].value == "A1") {
//                                    alert("Invoice is not approved:" + GridView.rows(rowCount).cells(1).innerHTML);
//                                    return false;
//                                }
                            }
                        }
                    }
                }
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                return true;
            }
            else if (vendortype.selectedIndex == 2) {

                if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                    var objs = new Array("<%=ddlcccode.ClientID %>", "<%=txtdt.ClientID %>"
                        , "<%=txtpaiddate.ClientID %>", "<%=ddlvendortype.ClientID %>", "<%=ddlservices.ClientID %>", "<%=ddlvendors.ClientID %>", "<%=ddlpos.ClientID %>", "<%=txtnames.ClientID %>", "<%=txtdescs.ClientID %>", "<%=txtamts.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
                else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                    var objs = new Array("<%=txtdt.ClientID %>"
                        , "<%=txtpaiddate.ClientID %>", "<%=ddlvendortype.ClientID %>", "<%=ddlservices.ClientID %>", "<%=ddlvendors.ClientID %>", "<%=ddlpos.ClientID %>", "<%=txtnames.ClientID %>", "<%=txtdescs.ClientID %>", "<%=txtamts.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
                var netamount = document.getElementById("<%= txtamts.ClientID%>").value;
                if (netamount < 0) {
                    alert("You are excessing the Invoice Amount");
                    return false;
                }
                //debugger;
                var GridView = document.getElementById("<%=grds.ClientID %>");
                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (GridView.rows(rowCount).cells(9).children(0).checked == true) {
                            var str1 = GridView.rows(rowCount).cells(2).innerHTML;
                            var str2 = document.getElementById("<%=txtdt.ClientID %>").value;
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
                            var service = document.getElementById("<%=ddlservices.ClientID %>").value;
                            if (service != "Advance Payment") {
                                if (parseFloat(_Diff) < 0) {
                                    alert("You are not able to make payment to before invoice date");
                                    document.getElementById("<%=txtdt.ClientID %>").focus();
                                    return false;
                                }
                            }
                            if (GridView.rows(rowCount).cells(11).children[0].value == "1" || GridView.rows(rowCount).cells(11).children[0].value == "2" || GridView.rows(rowCount).cells(11).children[0].value == "2A") {
                                alert("Invoice is not approved:" + GridView.rows(rowCount).cells(1).innerHTML);
                                return false;
                            }
                        }
                    }
                }
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                return true;
            }
        }
    </script>
</asp:Content>
