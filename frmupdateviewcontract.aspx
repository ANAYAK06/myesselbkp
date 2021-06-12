<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmupdateviewcontract.aspx.cs"
    Inherits="frmupdateviewcontract" EnableEventValidation="false" Title="Update Veiw Contract - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr valign="top">
                        <%--<th valign="top"  colspan="4" align="center" >
    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Update View Contract"></asp:Label>
    </th>--%>
                        <td align="center">
                            <table class="estbl" width="700px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" colspan="4" align="center">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Project Information"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td valign="middle" colspan="4">
                                        <asp:DropDownList ID="ddlpo" runat="server" CssClass="esddown" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlpo"
                                            ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="Polist"
                                            PromptText="Select PO">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Project Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpname" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Client Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtclientname" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Division"></asp:Label>
                                    </td>
                                    <td colspan="1">
                                        <asp:TextBox ID="txtdivision" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                    </td>
                                    <td colspan="1">
                                        <asp:TextBox ID="txtcc" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Start Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtstartdate" runat="server" onkeydown="return DateReadonly();"
                                            MaxLength="50"></asp:TextBox><img onclick="scwShow(document.getElementById('<%=txtstartdate.ClientID %>'),this);"
                                                alt="" src="images/cal.gif" style="left: 3px; position: relative; top: 1px; width: 15px;
                                                height: 15px;" id="cldrDob" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="End Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtenddate" runat="server" onkeydown="return DateReadonly();" MaxLength="50"></asp:TextBox><img
                                            onclick="scwShow(document.getElementById('<%=txtenddate.ClientID %>'),this);"
                                            alt="" src="images/cal.gif" style="left: 3px; position: relative; top: 1px; width: 15px;
                                            height: 15px;" id="Img1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Nature Of Job"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtnoj" runat="server" MaxLength="50" Width="220px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Project Manager Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpmname" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="Project Manager Contact No"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpmcno" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="Customer Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcustname" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top" colspan="4" align="center">
                                        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnupdate" CssClass="esbtn" runat="server" Text="Update" OnClick="btnupdate_Click" />
                                        <asp:Button ID="btnclear" runat="server" CssClass="esbtn" Text="Clear" OnClick="btnclear_Click" />
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
