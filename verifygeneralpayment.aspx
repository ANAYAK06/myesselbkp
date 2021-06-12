<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="verifygeneralpayment.aspx.cs"
    Inherits="verifygeneralpayment" Title="Verify General Payment" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

        function validate() {
            var objs = new Array("<%=ddlcccode.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtremarks.ClientID %>", "<%=txtamount.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById("<%=lbldca.ClientID %>").innerHTML == "DCA-Excise" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select Excise No");
                return false;
            }
            if (document.getElementById("<%=lbldca.ClientID %>").innerHTML == "DCA-VAT" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select Vat No");
                return false;
            }
            if (document.getElementById("<%=lbldca.ClientID %>").innerHTML == "DCA-SRTX" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select Service Tax No");
                return false;
            }
            if (document.getElementById("<%=lbldca.ClientID %>").innerHTML == "DCA-GST-CR" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select GST No");
                return false;
            }
            document.getElementById("<%=btnupdate.ClientID %>").style.display = 'none';
            return true;
        }
              
    </script>
    <script language="javascript">
        function numericFilter(txb) {
            txb.value = txb.value.replace(/[^\0-9]/ig, "");
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
                        <table width="700px">
                            <tr align="center">
                                <asp:Label ID="itform" CssClass="esfmhead" Width="550px" runat="server" Text="Verify General Invoice"></asp:Label>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="gridgeneral" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        DataKeyNames="Invoiceno" OnRowEditing="gridgeneral_RowEditing" EmptyDataText="There Is No Records"
                                        OnRowDeleting="gridgeneral_RowDeleting">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                EditImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:BoundField DataField="invoiceno" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="date" HeaderText="Invoice Date" />
                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                            <asp:BoundField DataField="subdca_code" HeaderText="SDCA Code" />
                                            <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-Width="150px" />
                                            <asp:BoundField DataField="amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="Mode_of_Pay" HeaderText="Payment Mode" />
                                            <asp:TemplateField HeaderText="Reject">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkreject" CommandName="Delete" runat="server"><img src="images/Delete.jpg" alt='Reject' /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                        <PagerStyle CssClass="grid pagerbar" />
                                        <HeaderStyle CssClass="grid-header" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table id="tblinvoice" runat="server" style="margin: 1em; border-collapse: collapse;"
                            width="700px">
                            <tr align="center">
                                <td>
                                    <table width="600px" runat="server" style="background-color: White">
                                        <tr>
                                            <th colspan="4" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small;
                                                background: #E3E4FA;" align="center">
                                                Self Invoice For General Payment
                                            </th>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td style="border: 1px #000000 solid; border-right-color: White;" align="center">
                                                <asp:Label ID="Label1" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Invoice No"></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtinvno" runat="server" Style="color: #000000; font-family: Tahoma;
                                                    text-decoration: none" Width="90%" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="border: 1px #000000 solid; border-right-color: White;" align="center">
                                                <asp:Label ID="Label3" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid;" align="center">
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="90%"
                                                    CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="newcostcode"
                                                    PromptText="Select Cost Center">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td style="border: 1px #000000 solid; border-right-color: White;" align="center">
                                                <asp:Label ID="Label5" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Dca:- "></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid;" align="center">
                                                <asp:Label ID="lbldca" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text=""></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid; border-right-color: White;" align="center">
                                                <asp:Label ID="Label7" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="SubDca:- "></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid;" align="center">
                                                <asp:Label ID="lblsubdca" runat="server" Style="color: Black; font-family: Arial;
                                                    font-weight: bold" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td style="border: 1px #000000 solid;" align="center" colspan="2">
                                                <asp:Label ID="lbldcaname" runat="server" Style="color: Black; font-family: Arial;
                                                    font-weight: bold"></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid; border-right-color: black;" align="center"
                                                colspan="2">
                                                <asp:Label ID="lblsdcaname" runat="server" Style="color: Black; font-family: Arial;
                                                    font-weight: bold"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="border: 1px #000000 solid; height: 50px;">
                                            <td style="padding: .3em; border: 1px #000000 solid; width: 150px;" align="center"
                                                valign="top" colspan="2">
                                                <asp:DropDownList ID="ddlno" CssClass="esddown" Width="150px" ToolTip="Excise" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label6" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Mode of Pay"></asp:Label>
                                            </td>
                                            <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                                                align="center">
                                                <asp:DropDownList ID="ddlmodeofpay" runat="server" ToolTip="Mode Of Pay" Width="130px"
                                                    CssClass="esddown">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                    <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="border: 1px #000000 solid;">
                                            <td style="border: 1px #000000 solid; border-right-color: White;" align="center">
                                                <asp:Label ID="Label2" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Party Name"></asp:Label>
                                            </td>
                                            <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                                                align="center">
                                                <asp:TextBox ID="txtname" runat="server" Width="90%" ToolTip="Name" Style="color: #000000;
                                                    font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                            </td>
                                            <td style="border: 1px #000000 solid; border-right-color: White;" align="center">
                                                <asp:Label ID="Label24" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Invoice Date"></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtdate" runat="server" ToolTip="Date" Style="color: #000000; font-family: Tahoma;
                                                    text-decoration: none" Width="90%"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr style="border: 1px #000000 solid;">
                                            <td align="right" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;">
                                                <asp:Label ID="Label4" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Remarks"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtremarks" runat="server" ToolTip="Remarks" Style="color: #000000;
                                                    font-family: Tahoma; text-decoration: none;" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                            </td>
                                            <td align="right" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;">
                                                <asp:Label ID="Label28" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Invoice Amount"></asp:Label>
                                            </td>
                                            <td style="border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtamount" runat="server" ToolTip="Amount" Style="color: #000000;
                                                    font-family: Tahoma; text-decoration: none;" Width="90%" onKeyUp="numericFilter(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Trgvusers" runat="server" style="border: 1px #000000 solid;">
                                            <td colspan="4">
                                                <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="none" Width="740px" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Status" ItemStyle-Wrap="false" />
                                                        <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                        <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                        <%--  <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="UserID" ItemStyle-Wrap="false" />--%>
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr style="border: 1px #000000 solid;">
                                            <td colspan="4" align="center" style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;">
                                                <asp:Button CssClass="esbtn" Style="font-size: small; height: 26px;" ID="btnupdate"
                                                    runat="server" Text="Update" OnClientClick="javascript:return validate()" OnClick="btnupdate_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnreset" runat="server" CssClass="esbtn" Style="font-size: small;
                                                    height: 26px;" Text="Back" OnClick="btnreset_Click" />
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
