<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="TaskStatus.aspx.cs" Inherits="TaskStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();
        }
        function showpopup() {
            $find('mdlindent').show();

        }
         
    </script>
    <script type="text/javascript">
        function validate() {
            var type = document.getElementById("<%=ddltype.ClientID %>").value;
            var SupportUser = document.getElementById("<%=ddlnames.ClientID %>").value;
            var staus = document.getElementById("<%=ddlfillstatus.ClientID %>").value;
            var month = document.getElementById("<%=txtdate1.ClientID %>").value;
            var year = document.getElementById("<%=txtdate2.ClientID %>").value;

            if (type == "Category") {
                window.alert("Select Category");
                document.getElementById("<%=ddltype.ClientID %>").focus();
                return false;
            }
            if (SupportUser == "SupportUser") {
                window.alert("Select SupportUser");
                document.getElementById("<%=ddlnames.ClientID %>").focus();
                return false;
            }
            if (staus == "Status") {
                window.alert("Select Status");
                document.getElementById("<%=ddlfillstatus.ClientID %>").focus();
                return false;
            }
            if (month == "" ) {
                window.alert("Enter From Date");
                document.getElementById("<%=txtdate1.ClientID %>").focus();
                return false;
            }
            if (year == "") {
                window.alert("Enter To Date");
                document.getElementById("<%=txtdate2.ClientID %>").focus();
                return false;
            }
        }
        function validates() {

            var status = document.getElementById("<%=ddlstatus.ClientID %>").value;

            if (status == "Status") {
                var objs = new Array("<%=ddlstatus.ClientID %>", "<%=txttime.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }

        }
      
    </script>

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr align="center">
            <td align="center">
                <table class="estbl" width="400px">
                    <tr>
                        <th valign="top" align="center" colspan="6">
                            <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Tasks Status"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Category" Width="120px" CssClass="esddown">
                                <asp:ListItem Value="Category">Category</asp:ListItem>
                                <asp:ListItem Value="1">CR</asp:ListItem>
                                <asp:ListItem Value="2">ISSUE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="">
                            <asp:DropDownList ID="ddlnames" runat="server" ToolTip="Name" Width="140px" CssClass="esddown">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="">
                            <asp:DropDownList ID="ddlfillstatus" runat="server" ToolTip="Status" Width="140px"
                                CssClass="esddown">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            <span class="filter_item">
                                <asp:TextBox ID="txtdate1" CssClass="estbox" runat="server" ToolTip="Date1"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate1"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtdate1">
                                </cc1:CalendarExtender>
                            </span>
                        </td>
                        <td align="center">
                            <span class="filter_item">
                                <asp:TextBox ID="txtdate2" CssClass="estbox" runat="server" ToolTip="Date2"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate2"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtdate2">
                                </cc1:CalendarExtender>
                            </span>
                        </td>
                       <%-- <td align="center">
                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="120px" runat="server">
                            </asp:DropDownList>
                        </td>--%>
                        <td align="center">
                            <asp:Button ID="btnok" runat="server" Text="OK" OnClientClick="javascript:return validate();"
                                OnClick="btnok_Click" />
                        </td>
                    </tr>
                </table>
                <table width="900px">
                    <tr align="center">
                        <td>
                            <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                        GridLines="None" EmptyDataText="No Data Avaliable" DataKeyNames="Cr_IssueNo"
                                        HorizontalAlign="Center" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        CssClass="mGrid" OnRowEditing="GridView1_RowEditing" OnRowDataBound="GridView1_RowDataBound"
                                        OnRowCreated="GridView1_RowCreated">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="false" />
                                            <asp:TemplateField ItemStyle-Width="70" HeaderText="NO" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" Style="cursor: hand;" runat="server" Width="70" Text='<%# Bind("NO") %>'></asp:Label>
                                                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="Panel1"
                                                        TargetControlID="lblno" DynamicContextKey='<%# Eval("NO") %>' DynamicControlID="Panel1"
                                                        DynamicServiceMethod="GetDynamicContent" Position="Bottom">
                                                    </cc1:PopupControlExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="assignto" HeaderText="AssignTo" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px" />
                                            <%--<asp:BoundField DataField="brdtime" HeaderText="BRD Time" ItemStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tdtime" HeaderText="TD Time" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                                           --%>
                                            <asp:BoundField DataField="developmenttime" HeaderText="Hours" ItemStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="comment" HeaderText="Comment" ItemStyle-Width="150px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="status_name" HeaderText="Status" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px" />
                                            <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="10px" ShowEditButton="true"
                                                EditImageUrl="~/images/iconset-b-edit.gif" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false"
                                        GridLines="Both" EmptyDataText="No Data Avaliable" DataKeyNames="Cr_IssueNo"
                                        HorizontalAlign="Center" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        CssClass="mGrid">
                                        <Columns>
                                            <asp:BoundField DataField="Cr_IssueNo" HeaderText="No" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="date" HeaderText="Approved Date" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="developmenttime" HeaderText="Hours" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Estmationverify_date" HeaderText="Estimation Approvedate"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="QualityTransferdate" HeaderText="Quality Transferdate"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="QualityApproveddate" HeaderText="Quality Approveddate"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="ProductionTrasferdate" HeaderText="Production Transferdate"
                                                ItemStyle-HorizontalAlign="Center" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="Panel1" runat="server">
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="poppo" BehaviorID="mdlindent" runat="server" TargetControlID="btnModalPopUp"
                                        PopupControlID="pnlraisepo" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                    <asp:Panel ID="pnlraisepo" runat="server" Style="display: none;">
                                        <table id="tbledit" width="350px" border="0" align="center" runat="server" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td width="13" valign="bottom">
                                                    <img alt="" src="images/leftc.jpg" />
                                                </td>
                                                <td class="pop_head" align="left">
                                                    <div class="popclose">
                                                        <img width="20" alt="" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png" />
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
                                                <td height="120" valign="top" class="popcontent">
                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 230px;">
                                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table style="vertical-align: middle;" width="200px" align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="vertical-align: middle;" align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table id="tblestimate" width="100%" runat="server">
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="NO"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lnlno" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Status"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:DropDownList ID="ddlstatus" runat="server" Width="150px" ToolTip="Task Status">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlstatus"
                                                                                                        runat="server" ErrorMessage="Required" InitialValue="Status"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="time" runat="server">
                                                                                                <td>
                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Issue Time"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txttime" runat="server" Width="150px" ToolTip="Issue Time"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="Remarks"></asp:Label>
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
                                                                                                <td align="center" id="Td1" runat="server" colspan="2">
                                                                                                    <asp:Button ID="btnstatus" runat="server" Text="Change Status" CssClass="button"
                                                                                                        OnClientClick="javascript:return validates();" OnClick="btnstatus_Click" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;">
            </td>
            <td align="left" colspan="2">
                <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
