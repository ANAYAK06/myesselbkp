<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmPayBill.aspx.cs"
    Inherits="Admin_frmPayBill" EnableEventValidation="false" Title="Pay Bill- Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function SelectAll() {
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");

            var originalValue = 0;
            for (var rowCount = 1; rowCount <= GridView1.rows.length - 1; rowCount++) {
                if (GridView1.rows(rowCount).cells(5).children(0) != null) {


                    if (GridView1.rows(rowCount).cells(5).children(0).checked == true) {
                        //                var bankname=GridView1.rows(rowCount).cells(3).innerText;
                        //                if(bankname==bank)
                        //                {
                        //                if(GridView1.rows(rowCount).cells(3).innerText!=GridView1.rows(rowCount-1).cells(3).innerText)
                        //                {
                        //                var value=GridView1.rows(rowCount).cells(4).innerText.replace(/,/g,"");
                        //                alert("You are Wrongly Selected");
                        //                GridView1.rows(rowCount).cells(5).children(0).checked=false;
                        //                return false;

                        //                }
                        //                }
                        //                else
                        //                {
                        var value = GridView1.rows(rowCount).cells(4).innerText.replace(/,/g, "");
                        //document.getElementById("<%=lblbank.ClientID %>").innerHTML = GridView1.rows(rowCount).cells(3).innerText;
                        //                }
                        if (value != "") {
                            originalValue += parseInt(value);
                        }
                    }
                }
            }
            document.getElementById('<%= txtamt.ClientID%>').value = originalValue;
        }
    </script>
    <script language="javascript" type="text/javascript">

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

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
        function validate() {
            var objs = new Array("<%=ddlmonth.ClientID %>", "<%=ddlyear.ClientID %>", "<%=ddlbank.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
        function validation() {
            var objs = new Array("<%=txtdate.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=ddlcheque.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;

        }
      
    </script>
    <script type="text/javascript">
        function pay() {

            var payment = document.getElementById("<%=ddlpayment.ClientID %>");
            var cheque = document.getElementById("<%=ddlcheque.ClientID %>");
            var rtgs = document.getElementById("<%=txtcheque.ClientID %>");
            if (payment.selectedIndex != 1) {
                //                window.alert("Invalid");
                //                document.getElementById("<%=ddlpayment.ClientID %>").selectedIndex = 1;
                cheque.style.display = 'none';
                rtgs.style.display = 'block';
            }
            else if (payment.selectedIndex == 1) {

                cheque.style.display = 'block';
                rtgs.style.display = 'none';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="Menu1" runat="server" />
            </td>
            <td>
                <asp:Panel runat="server" ID="viewreportpanel">
                    <table>
                        <tr>
                            <td align="center">
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
                                        <table class="estbl">
                                            <tr>
                                                <th valign="top" style="background-color: #8B8A8A;" align="center">
                                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Pay Bill"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="paytype" runat="server" class="innertab">
                                                        <tr>
                                                            <td colspan="4">
                                                                <table>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="Month">
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
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="year"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlbank" runat="server" ToolTip="Bank" CssClass="esddown" Width="100px">
                                                                            </asp:DropDownList>
                                                                            <span class="starSpan">*</span>
                                                                            <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlbank"
                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                                                PromptText="Select">
                                                                            </cc1:CascadingDropDown>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="Button1" runat="server" CssClass="" Text="go" ToolTip="Go" Height="20px"
                                                                                align="center" OnClientClick="javascript:return validate()" OnClick="Button1_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="None"
                                                                    Font-Size="Small" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="id">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                                                        <asp:BoundField DataField="DCA_Code" HeaderText="DCA Code" />
                                                                        <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                                                        <asp:BoundField DataField="Debit" HeaderText="Amount" />
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                                            </HeaderTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr id="paymentdetails" runat="server">
                                                            <td>
                                                                <table align="center" class="estbl" width="100%" runat="server">
                                                                    <tr>
                                                                        <th align="center" colspan="4">
                                                                            Payment Details
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="bank" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblbank" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                                Width="120px"></asp:TextBox><span class="starSpan">*</span>
                                                                            <img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                                                src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="ModeofPay" runat="server">
                                                                        <td>
                                                                            Mode Of Pay:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlpayment" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                                                                Width="100" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlpayment"
                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                                                PromptText="Select">
                                                                            </cc1:CascadingDropDown>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="100px"></asp:TextBox><span
                                                                                class="starSpan">*</span>
                                                                            <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                                                Width="100">
                                                                                <asp:ListItem Value="0" Text="select"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Remarks:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Remarks" Width="200px"
                                                                                TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                                                        </td>
                                                                        <td>
                                                                            Amount:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="200px"></asp:TextBox><span
                                                                                class="starSpan">*</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="btn" runat="server">
                                                <td align="center">
                                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                        Text="Submit" OnClientClick="javascript:return validation()" OnClick="btnsubmit_Click" />&nbsp
                                                    <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
