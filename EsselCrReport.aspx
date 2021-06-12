<%@ Page Title="View CR Details" Language="C#"  MasterPageFile="~/Essel.master" AutoEventWireup="true" 
    CodeFile="EsselCrReport.aspx.cs" Inherits="EsselCrReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddltype.ClientID %>");
            var month = document.getElementById("<%=ddlMonth.ClientID %>").value;
            var year = document.getElementById("<%=ddlyear.ClientID %>").value;
            if (!CheckInputs(objs)) {
                return false;
            }


            else if ((month != "Select Month") && (year == "Any Year")) {
                window.alert("Select Year");

                return false;
            }
        }

        function valid() {

            var objs = new Array("<%=txtbrd.ClientID %>", "<%=txttd.ClientID %>", "<%=txtdevelopment.ClientID %>", "<%=ddleststatus.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=Button1.ClientID %>").style.display = 'none';
            return true;
            
        }
        function validates() {
            var type = document.getElementById("<%=ddltype.ClientID %>").value;
            var status = document.getElementById("<%=ddlstatus.ClientID %>").value;
            var status1 = document.getElementById("<%=ddlfillstatus.ClientID %>").value;
            //  var str = status.options[status.selectedIndex].value;

            if (type == "1" && status1 == "status") {
                var objs = new Array("<%=ddlfillstatus.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            if (type == "2" && status == "2") {
                var objs = new Array("<%=ddlusername.ClientID %>", "<%=ddlfillstatus.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
        
    </script>

    <script type="text/javascript">
        function validation() {

            var objs = new Array("<%=ddlcrtype.ClientID %>", "<%=txtdesc.ClientID %>", "<%=ddlpriority.ClientID %>", "<%=ddlsetstatus.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnupdate.ClientID %>").style.display = 'none';
            return true;

        }
        function deleteSpecialChar(txtdesc) {
            if (txtdesc.value != '' && txtdesc.value.match(/^[\w ]+$/) == null) {
                txtdesc.value = txtdesc.value.replace(/[\W]/g, '');
            }
        }
        function deleteSpecial(txtcomment) {
            if (txtcomment.value != '' && txtcomment.value.match(/^[\w ]+$/) == null) {
                txtcomment.value = txtcomment.value.replace(/[\W]/g, '');
            }
        }
    </script>

    <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();


        }
        function showpopup() {
            $find('mdlindent').show();

        }
       
      
    </script>

    <script type="text/javascript">
        function td() {
            var lbl = document.getElementById("<%=lbleno.ClientID %>").innerHTML;
            var add = "http://quality.esselprojects.com/uploads/TD-" + lbl + ".docx";

            //var add = "http://localhost:1076/Essel(17-07-12)/uploads/TD-" + lbl + ".docx";
            window.open(add, 'td', 'width=780,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
            return false;
        }
        function brd() {
            var lbl = document.getElementById("<%=lbleno.ClientID %>").innerHTML;
            var add = "http://quality.esselprojects.com/uploads/BRD-" + lbl + ".docx";

            //var add = "http://localhost:1076/Essel(17-07-12)/uploads/BRD-" + lbl + ".docx.htm";
            window.open(add, 'brd', 'width=780,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
            return false;
        }
    </script>

    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upd" runat="server">
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
            <table width="900px" runat="server">
                <tr align="center">
                    <td align="center">
                        <table class="estbl" width="400px">
                            <tr>
                                <th valign="top" align="center" colspan="2">
                                    <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="ESSEL CR'S REPORT"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbltype" CssClass="eslbl" runat="server" Text="Category"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" ToolTip="Category"
                                        Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">CR</asp:ListItem>
                                        <asp:ListItem Value="2">ISSUE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblstatus" CssClass="eslbl" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlstatus" runat="server" ToolTip="Status" Width="150px" CssClass="esddown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="bllmonth" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                </td>
                                <td align="center">
                                    <span class="filter_item">
                                        <asp:DropDownList ID="ddlMonth" Width="150px" CssClass="esddown" runat="server">
                                            <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                            <asp:ListItem Value="4">Apr</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">Aug</asp:ListItem>
                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="150px" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trprint" runat="server">
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnview" runat="server" CssClass="esbtn" Style="font-size: small"
                                        Text="View" OnClientClick="javascript:return validate()" OnClick="btnview_Click" />
                                    <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                        OnClick="btnExcel_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="900px" id="tblprint" runat="server">
                            <tr id="tr1print" runat="server">
                                <td>
                                    <%--<asp:Panel ID="Panel1" runat="server">--%>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" ShowFooter="true"
                                        GridLines="None" EmptyDataText="No Data Avaliable" HorizontalAlign="Center" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" DataKeyNames="id" OnRowEditing="GridView1_RowEditing"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="" ShowEditButton="true" EditImageUrl="~/images/iconset-b-edit.gif"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="id" Visible="false" />
                                            <asp:BoundField DataField="CR/ISSUE No" HeaderText="CR/ISSUE No" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="crtype" HeaderText="CRType" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Hours">
                                                <ItemTemplate >
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Hours") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="priority" HeaderText="Priority" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-Width="230px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="comment" HeaderText="Comment" ItemStyle-Width="135px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                            <asp:CommandField ButtonType="Image" HeaderText="" ShowEditButton="true" EditImageUrl="~/images/iconset-b-edit.gif"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                    <%--</asp:Panel>--%>
                                    <cc1:ModalPopupExtender ID="poppo" BehaviorID="mdlindent" runat="server" TargetControlID="btnModalPopUp"
                                        PopupControlID="pnlraisepo" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                    <asp:Panel ID="pnlraisepo" runat="server" Style="display: none;">
                                        <table id="tbledit" width="550px" border="0" align="center" runat="server" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td width="13" valign="bottom">
                                                    <img alt="" src="images/leftc.jpg" />
                                                </td>
                                                <td class="pop_head" align="left">
                                                    <div class="popclose">
                                                        <img alt="" width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png" />
                                                    </div>
                                                    <asp:Label ID="lblviewpo" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td width="13" valign="bottom">
                                                    <img alt="" src="images/rightc.jpg" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFFF">
                                                    &nbsp;
                                                </td>
                                                <td height="180" valign="top" class="popcontent">
                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 320px;">
                                                        <asp:UpdatePanel ID="upslots" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table style="vertical-align: middle;" width="400px" align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="vertical-align: middle;" align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table id="tblpopup" width="100%" runat="server">
                                                                                            <tr>
                                                                                                <td style="width: 50px">
                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="CR/ISSUE NO:-"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:Label ID="lblno" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 100px">
                                                                                                    <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Category"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:DropDownList ID="ddlcrtype" runat="server" ToolTip="Type" Width="130px" CssClass="esddown">
                                                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">CR</asp:ListItem>
                                                                                                        <asp:ListItem Value="2">ISSUE</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Raised By:-"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:Label ID="lblraised" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" ToolTip="Description"
                                                                                                        Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Priority"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:DropDownList ID="ddlpriority" runat="server" Width="130px" ToolTip="Priority">
                                                                                                        <asp:ListItem Value="0">Select Priority</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">Priority 1(Critical)</asp:ListItem>
                                                                                                        <asp:ListItem Value="2">Priority 2(High)</asp:ListItem>
                                                                                                        <asp:ListItem Value="3">Priority 3(Medium)</asp:ListItem>
                                                                                                        <asp:ListItem Value="4">Priority 4(Low)</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Comment"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Status"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:DropDownList ID="ddlsetstatus" runat="server" Width="130px" ToolTip="Status">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td align="center" id="btn" runat="server" colspan="2">
                                                                                                    <asp:Button ID="btnupdate" runat="server" Text="Submit" CssClass="button" OnClientClick="javascript:return validation();"
                                                                                                        OnClick="btnupdate_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table id="tblestimate" width="100%" runat="server">
                                                                                            <tr>
                                                                                                <td style="width: 50px">
                                                                                                    <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="CR/ISSUE NO:-"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:Label ID="lbleno" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 100px">
                                                                                                    <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Category"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:Label ID="lbletype" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="BRD Time"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:TextBox ID="txtbrd" runat="server" Enabled="false" ToolTip="BRD Time"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="h1" runat="server" />
                                                                                                    <asp:Button ID="btnbrd" runat="server" Text="BRD FILE" OnClick="btnbrd_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="TD Time"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:TextBox ID="txttd" runat="server" Enabled="false" ToolTip="TD Time"></asp:TextBox>
                                                                                                    <asp:Button ID="btntd" runat="server" Text="TD FILE" OnClick="btntd_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="Development Time"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:TextBox ID="txtdevelopment" runat="server" Enabled="false" ToolTip="Development Time"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:TextBox ID="lbledesc" runat="server" Enabled="false" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="comment"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtecomment" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trestchange" runat="server">
                                                                                                <td>
                                                                                                    <asp:Label ID="Label20" runat="server" CssClass="eslbl" Text="Status"></asp:Label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:DropDownList ID="ddleststatus" runat="server" Width="130px" ToolTip="Status">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td colspan="2">
                                                                                                    <%--<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>--%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center" id="trok" runat="server">
                                                                                                <td align="center" id="Td1" runat="server" colspan="2">
                                                                                                    <asp:Button ID="Button1" runat="server" Text="Ok" CssClass="button" OnClientClick="javascript:return valid();"
                                                                                                        OnClick="Button1_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table id="tblstatus" width="100%" runat="server">
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 100px">
                                                                                                    <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Category"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:Label ID="lblCategoryno" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="truser" runat="server">
                                                                                                <td>
                                                                                                    <asp:Label ID="Label19" runat="server" CssClass="eslbl" Text="SupportUser"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlusername" runat="server" Width="150px" ValidationGroup="user"
                                                                                                        ToolTip="SupportUser">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlusername"
                                                                                                        runat="server" ErrorMessage="Required" ValidationGroup="user"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Status"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlfillstatus" ValidationGroup="fs" ToolTip="Status" runat="server"
                                                                                                        Width="150px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlfillstatus"
                                                                                                        runat="server" ErrorMessage="Required" ValidationGroup="fs"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="Remarks"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td align="center" id="Td2" runat="server" colspan="2">
                                                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="button" OnClientClick="javascript:return validates();"
                                                                                                        OnClick="btnsubmit_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btntd" />
                                                                <asp:PostBackTrigger ControlID="btnbrd" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                                <td bgcolor="#FFFFFF">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
