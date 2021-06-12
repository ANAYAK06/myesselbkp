<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs"
    Inherits="ChangePassword" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="view" cellpadding="0" cellspacing="0" style="border: none; height: 500px;">
        <tbody>
            <tr align="center">
                <td style="padding: 150px 35px 5px 35px; min-width: 100px;" valign="top" width="450px">
                    <form name="loginform" id="loginform" style="padding-bottom: 5px; min-width: 100px;
                    width: 350px;">
                    <fieldset class="box" style="width: 400px">
                        <legend style="padding: 4px; font-weight: bold;">Change Password </legend>
                        <div class="box2" style="padding: 5px 5px 20px 5px">
                            <table width="100%" align="center" cellspacing="2px" cellpadding="0" style="border: none;">
                                <tbody>
                                    <tr>
                                        <td class="label" width="20px">
                                            <label for="user">
                                                Old Password:</label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:TextBox ID="txtold" runat="server" CssClass="db_user_pass" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <label for="password">
                                                New Password:</label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:TextBox ID="txtnew" runat="server" CssClass="db_user_pass" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <label for="password">
                                                Confirm Password:</label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:TextBox ID="txtconfirm" runat="server" CssClass="db_user_pass" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td class="db_login_buttons">
                                            <%-- <button type="submit" class="static_boxes">
                                                Login</button>--%>
                                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="static_boxes" 
                                                onclick="btnsubmit_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="item item-char">
                                            <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </fieldset>
                    </form>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
