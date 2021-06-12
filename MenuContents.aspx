<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="MenuContents.aspx.cs"
    Inherits="MenuContents" Title="Essel Home - Essel Projects Pvt. Ltd." %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript">
function PuchaseHome()
{
location.href("PurchaseHome.aspx");
}
function WarehouseHome()
{
location.href("WareHouseHome.aspx");

}
function AccountingHome()
{
location.href("AccountHome.aspx");
}
function HRHome()
{
    location.href("HR/HRHome.aspx");
}

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr >
            <td id="primary" class="first-page-primary" style="width: 50px; background-color: #A0A1A1;">
                <div class="wrap" style="padding: 10px; width: 291px;">
                    <ul class="sections-a">
                        <li class="web_dashboard" id="405"><span class="wrap"><a href="PurchaseHome.aspx">
                            <table width="100%" height="100%" cellspacing="0" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td align="center" style="height: 100px;">
                                            <img src="images/Puchase1.png" alt="" style="display: inline;" />
                                            <%--  <img class="hover" src="images/Puchase1.png" alt="" style="display: none;" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span onclick="Javascript:PuchaseHome();"><strong>Purchases</strong> </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a></span></li>
                    </ul>
                </div>
            </td>
            <td id="primary1" class="first-page-primary" style="width: 50px; background-color: #A0A1A1;">
                <div class="wrap" style="padding: 10px;">
                    <ul class="sections-a">
                        <li class="web_dashboard" id="315"><span class="wrap"><a href="WareHouseHome.aspx"
                            target="_top">
                            <table width="100%" height="100%" cellspacing="0" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td align="center" style="height: 100px;">
                                            <img src="images/WareHouse1.png" alt="" style="display: inline;">
                                            <%-- <img class="hover" src="images/WareHouse1.png" alt="" style="display: none;">--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span  onclick="Javascript:WarehouseHome();"><strong>Warehouse</strong> </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a></span></li>
                    </ul>
                </div>
            </td>
            <td id="primary2" class="first-page-primary" style="width: 50px; background-color: #A0A1A1;">
                <div class="wrap" style="padding: 10px;">
                    <ul class="sections-a">
                        <li class="web_dashboard" id="269"><span class="wrap"><a href="ProjectHome.aspx"
                            target="_top">
                            <table width="100%" height="100%" cellspacing="0" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td align="center" style="height: 100px;">
                                            <img src="images/Project1.png" alt="" style="display: inline;">
                                            <%--    <img class="hover" src="images/Project1.png" alt="" style="display: none;">--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span ><strong>Project</strong> </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a></span></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr width="900px">
            <td id="primary3" class="first-page-primary" style="width: 50px; background-color: #A0A1A1;">
                <div class="wrap" style="padding: 10px;">
                    <ul class="sections-a">
                        <li class="web_dashboard" id=""><span class="wrap"><a href="AccountHome.aspx" target="_top">
                            <table width="100%" height="100%" cellspacing="0" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td align="center" style="height: 100px;">
                                            <img src="images/Account.png" alt="" style="display: inline;" />
                                            <%--  <img class="hover" src="images/Account.png" alt="" style="display: none;" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span  onclick="Javascript:AccountingHome();"><strong>Accounting</strong> </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a></span></li>
                    </ul>
                </div>
            </td>
            <td id="primary4" class="first-page-primary" style="width: 50px; background-color: #A0A1A1;">
                <div class="wrap" style="padding: 10px;">
                    <ul class="sections-a">
                        <li class="web_dashboard" id="153"><span class="wrap"><a href="HR/HRHome.aspx" target="_top">
                            <table width="100%" height="100%" cellspacing="0" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td align="center" style="height: 100px;">
                                            <img src="images/HumanResource1.png" alt="" style="display: inline;" />
                                            <%-- <img class="hover" src="images/HumanResource1.png" style="display: none;" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span  onclick="Javascript:HRHome();"><strong>Human Resources</strong> </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a></span></li>
                    </ul>
                </div>
            </td>
            <td id="primary5" class="first-page-primary" style="width: 50px; background-color: #A0A1A1;">
                <div class="wrap" style="padding: 10px;">
                    <ul class="sections-a">
                        <li class="web_dashboard" id="94"><span class="wrap"><a href="" target="_top">
                            <table width="100%" height="100%" cellspacing="0" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td align="center" style="height: 100px;">
                                            <img src="images/Tools1.png" style="display: inline;" />
                                            <%--<img class="hover" src="images/Tools1.png" alt="" style="display: none;" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span><strong>Tools</strong> </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a></span></li>
                    </ul>
                </div>
            </td>
            <td style="background-color: #A0A1A1;">
            </td>
        </tr>
    </table>
</asp:Content>
