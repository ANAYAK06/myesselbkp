<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="AssignTasks.aspx.cs" Inherits="AssignTasks" %>

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
        function valid() {

            var objs = new Array("<%=ddlnames.ClientID %>", "<%=txtremarks.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function validate() {
            var type = document.getElementById("<%=ddltype.ClientID %>").value;
            var status = document.getElementById("<%=ddlstatus.ClientID %>").value;
            var month = document.getElementById("<%=ddlMonth.ClientID %>").value;
            var year = document.getElementById("<%=ddlyear.ClientID %>").value;

            if (type == "Category") {
                window.alert("Select Category");
                document.getElementById("<%=ddltype.ClientID %>").focus();
                return false;
            }
            if (status == "Status") {
                window.alert("Select Status");
                document.getElementById("<%=ddlstatus.ClientID %>").focus();
                return false;
            }
            if (month != "Select Month" && year == "Select Year") {
                window.alert("Select Year");
                document.getElementById("<%=ddlyear.ClientID %>").focus();
                return false;
            }
        }
      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr align="center">
            <td align="center">
                <asp:UpdatePanel ID="upgrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="estbl" width="400px">
                            <tr>
                                <th valign="top" align="center">
                                    <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Assign Tasks"></asp:Label>
                                </th>
                            </tr>
                            <tr align="left">
                                <td>
                                    <table class="estbl">
                                        <tr>
                                            <td align="center">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Category" Width="120px" CssClass="esddown"
                                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    <asp:ListItem Value="Category">Category</asp:ListItem>
                                                    <asp:ListItem Value="1">CR</asp:ListItem>
                                                    <asp:ListItem Value="2">ISSUE</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <asp:DropDownList ID="ddlstatus" runat="server" ToolTip="Status" Width="140px" CssClass="esddown"
                                                    OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <span class="filter_item">
                                                    <asp:DropDownList ID="ddlMonth" Width="120px" CssClass="esddown" runat="server">
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
                                            <td align="center">
                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="120px" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="Button1" runat="server" Text="GO" OnClientClick="javascript:return validate();"
                                                    OnClick="Button1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="900px">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                        GridLines="None" EmptyDataText="No Data Avaliable" HorizontalAlign="Center" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" OnRowEditing="GridView1_RowEditing"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="no" HeaderText="NO" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Assignto" HeaderText="Assign To" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="brdtime" HeaderText="BRD Time" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tdtime" HeaderText="TD Time" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="developmenttime" HeaderText="Development Time" ItemStyle-HorizontalAlign="Center" />
                                         
                                            <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="comment" HeaderText="Comment" ItemStyle-Width="130px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" EditImageUrl="~/images/iconset-b-edit.gif"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
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
                                                <td height="180" valign="top" class="popcontent">
                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 300px;">
                                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table style="vertical-align: middle;" width="400px" align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="vertical-align: middle;" align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table id="tblestimate" width="100%" runat="server">
                                                                                            <tr>
                                                                                                <td style="width: 50px">
                                                                                                    <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="NO:-"></asp:Label>
                                                                                                </td>
                                                                                                <td align="center">
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
                                                                                                <td align="center">
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
                                                                                                <td align="center">
                                                                                                    <asp:Label ID="lblbrd" runat="server" CssClass="eslbl"></asp:Label>
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
                                                                                                <td align="center">
                                                                                                    <asp:Label ID="lbltd" runat="server" CssClass="eslbl"></asp:Label>
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
                                                                                                <td align="center">
                                                                                                    <asp:Label ID="lbldevelopmenttime" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Assign To"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlnames" runat="server" Width="150px" ToolTip="Assign To">
                                                                                                    </asp:DropDownList>
                                                                                                  
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
                                                                                                    <asp:TextBox ID="txtremarks" runat="server" ToolTip="Remarks" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 10px">
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td align="center" id="Td1" runat="server" colspan="2">
                                                                                                    <asp:Button ID="btnassign" runat="server" Text="Assign" CssClass="button" OnClientClick="javascript:return valid();" OnClick="btnassign_Click" />
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
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
