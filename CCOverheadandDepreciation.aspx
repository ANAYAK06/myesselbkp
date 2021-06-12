<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CCOverheadandDepreciation.aspx.cs"
    Inherits="CCOverheadandDepreciation" EnableEventValidation="false" Title="Untitled Page" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript">
        function PFfootervalidation() {
            sumgrid = document.getElementById("<%=GridView3.ClientID %>");
            var CCCode = sumgrid.rows[sumgrid.rows.length - 1].cells(0).children(0).selectedIndex;
            var OverHead = sumgrid.rows[sumgrid.rows.length - 1].cells(2).children(0).value;
            var Dep = sumgrid.rows[sumgrid.rows.length - 1].cells(3).children(0).value;

            if (CCCode == 0) {
                alert("Select CC Code");
                return false;
            }
            else if (OverHead == "") {
                alert("Enter Over Head Percentage");
                return false;
            }
            else if (Dep == "") {
                alert("Enter Depreciation Value");
                return false;

            }


        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>CC OverHead Percentage and
                                Depreciation Details <a class="help" href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="box-a list-a">
                                                                        <div class="inner">
                                                                            <table id="Table1" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td class="grid-content">
                                                                                            <table class="grid" width="100%" align="center">
                                                                                                <asp:GridView ID="GridView3" BorderColor="White" Width="650px" runat="server" AutoGenerateColumns="false"
                                                                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                    DataKeyNames="id" OnRowEditing="GridView3_RowEditing" OnRowCancelingEdit="GridView3_RowCancelingEdit"
                                                                                                    OnRowUpdating="GridView3_RowUpdating" OnRowDeleting="GridView3_RowDeleting" ShowFooter="true"
                                                                                                    OnRowDataBound="GridView3_RowDataBound">
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="id" Visible="false" />
                                                                                                        <asp:TemplateField HeaderText="CC Code">
                                                                                                            <ItemTemplate>
                                                                                                                <%#Eval("cc_code")%>
                                                                                                            </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:DropDownList ID="ddlfooterCCcode" runat="server" ToolTip="Cost Center" Width="125px"
                                                                                                                    CssClass="esddown" onchange="SetDynamicKey('dp2',this.value);">
                                                                                                                </asp:DropDownList>
                                                                                                                <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddlfooterCCcode"
                                                                                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="CCOverHead"
                                                                                                                    PromptText="Select Cost Center">
                                                                                                                </cc1:CascadingDropDown>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="CC Name" ItemStyle-Wrap="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblccname" runat="server" Text='<%#Eval("cc_name")%>'></asp:Label>
                                                                                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" runat="server" TargetControlID="lblccname"
                                                                                                                    ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx" ServiceMethod="GetCCName">
                                                                                                                </cc1:DynamicPopulateExtender>
                                                                                                            </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Label ID="lblfccname" runat="server" Text=""></asp:Label>
                                                                                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp2" runat="server"
                                                                                                                    TargetControlID="lblfccname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                                                    ServiceMethod="GetCCName">
                                                                                                                </cc1:DynamicPopulateExtender>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="OverHead Percentage">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbloverhead" runat="server" Text='<%#Eval("overhead")%>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <EditItemTemplate>
                                                                                                                <asp:TextBox ID="txtohead" runat="server" CssClass="estbox" Width="100px" Text='<%#Eval("overhead")%>'></asp:TextBox>
                                                                                                            </EditItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtoverhead" runat="server" CssClass="estbox" Width="100px"></asp:TextBox>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Depreciation Value">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbldepreciation" runat="server" Text='<%#Eval("depreciationvalue")%>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <EditItemTemplate>
                                                                                                                <asp:TextBox ID="txtdep" runat="server" CssClass="estbox" Width="100px" Text='<%#Eval("depreciationvalue")%>'></asp:TextBox>
                                                                                                            </EditItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtdepreciation" runat="server" CssClass="estbox" Width="100px"></asp:TextBox>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:CommandField ShowEditButton="true" ItemStyle-Wrap="false" ShowCancelButton="true"
                                                                                                            ShowDeleteButton="true" ControlStyle-Width="50px" />
                                                                                                        <asp:TemplateField>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClientClick="javascript:return PFfootervalidation()"
                                                                                                                    OnClick="btnInsert_Click" />
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
