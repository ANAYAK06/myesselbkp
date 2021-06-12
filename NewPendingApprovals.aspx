<%@ Page Title="PendingApprovals" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="NewPendingApprovals.aspx.cs" Inherits="NewPendingApprovals" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .accordionContent
        {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 90%;
        }
        .accordionHeaderSelected
        {
            background-color: #5078B3;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 90%;
        }
        .accordionHeader
        {
            background-color: #2E4D7B;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 90%;
        }
        .href
        {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td style="width: 720px;">
                <cc1:Accordion ID="UserAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250"
                    FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None" Width="720px">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">INDENT'S:</a><asp:Label ID="indenttotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="Pnlindents" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblIndents" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Indent No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    Indent Date
                                                </td>
                                                <td>
                                                    Indent Cost
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="indenttbody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane2" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">PO'S:</a><asp:Label ID="pototal" runat="server" Text=""
                                    ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlpos" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblpos" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    PO No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    Indent No
                                                </td>
                                                <td>
                                                    PO DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="Pobody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane3" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">MRR'S :</a><asp:Label ID="Mrrtotal" runat="server" Text=""
                                    ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlmrr" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblmrr" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    MRR No
                                                </td>
                                                <td>
                                                    PO NO
                                                </td>
                                                <td>
                                                    PO DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="Mrrbody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane4" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">SUPPLIER INVOICE'S :</a><asp:Label ID="invtotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlsinvoice" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblsinvoice" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Invoice No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    Total
                                                </td>
                                                <td>
                                                    PO DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="invoicebody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane5" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">TRANSFER OF MATERIAL'S:</a><asp:Label ID="transfertotal"
                                    runat="server" Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnltransfer" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblstransfer" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Reference No
                                                </td>
                                                <td>
                                                    Transfer Out
                                                </td>
                                                <td>
                                                    Transfer In
                                                </td>
                                                <td>
                                                    DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="transferbody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane6" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">ISSUE OF MATERIAL'S:</a><asp:Label ID="issuetotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlissue" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblissue" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Reference No
                                                </td>
                                                <td>
                                                    Transfer Out
                                                </td>
                                                <td>
                                                    Transfer In
                                                </td>
                                                <td>
                                                    DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="Issuebody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane7" runat="server" Width="720px">
                            <Header>
                                <a href="#" class="href">SPPO'S:</a><asp:Label ID="sppototal" runat="server" Text=""
                                    ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlsppo" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblsppo" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    PO No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    PO COST
                                                </td>
                                                <td>
                                                    SPPO DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="SPPObody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane8" runat="server">
                            <Header>
                                <a href="#" class="href">AMEND SPPO'S:</a><asp:Label ID="AMSPPOtotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlamndsppo" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblamendsppo" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    PO No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    AMEND VALUE
                                                </td>
                                                <td>
                                                    AMEND DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="ASPPObody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane9" runat="server">
                            <Header>
                                <a href="#" class="href">CLOSE SPPO'S:</a><asp:Label ID="clsppototal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlcsppo" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblclsppo" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    PO No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    PO COST
                                                </td>
                                                <td>
                                                    SPPO DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="ClSPPObody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane10" runat="server">
                            <Header>
                                <a href="#" class="href">CLIENT PO'S:</a><asp:Label ID="clientpototal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlClpo" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblclpo" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    PO No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    PO COST
                                                </td>
                                                <td>
                                                    PO DATE
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="ClientPObody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane11" runat="server">
                            <Header>
                                <a href="#" class="href">STOCK UPDATION'S:</a><asp:Label ID="stocktotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlstock" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblstock" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Invoiceno No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    DATE
                                                </td>
                                                <td>
                                                    Description
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="stockbody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane12" runat="server">
                            <Header>
                                <a href="#" class="href">ITEM CODE APPROVAL'S:</a><asp:Label ID="itemtotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlitem" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblitem" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Item Code
                                                </td>
                                                <td>
                                                    Item Name
                                                </td>
                                                <td>
                                                    Basic Price
                                                </td>
                                                <td>
                                                    DATE
                                                </td>
                                                <td>
                                                    Specification
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="itemcodebody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane13" runat="server">
                            <Header>
                                <a href="#" class="href">SCRAP ITEMS APPROVAL'S:</a><asp:Label ID="scraptotal" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlscrapitems" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblscrap" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Invoice No
                                                </td>
                                                <td>
                                                    Invoice Date
                                                </td>
                                                <td>
                                                    Party Name
                                                </td>
                                                <td>
                                                    Remarks
                                                </td>
                                                <td>
                                                    Amount
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="Scrapbody" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane14" runat="server">
                            <Header>
                                <a href="#" class="href">LOST/DAMAGED ITEMS APPROVAL'S:</a><asp:Label ID="losttotal"
                                    runat="server" Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnllost" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tbllost" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Reference No
                                                </td>
                                                <td>
                                                    Date
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="Tlostdamage" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane15" runat="server">
                            <Header>
                                <a href="#" class="href">STORE CLOSE APPROVAL'S:</a><asp:Label ID="storeclosetotal"
                                    runat="server" Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="pnlstoreclose" runat="server">
                                    <table class="estbl" width="720px">
                                        <thead id="tblstore" runat="server">
                                            <tr>
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Date
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    Remarks
                                                </td>
                                                <td>
                                                    Close Type
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="TStoreclose" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordSpInv4rAppr" runat="server">
                            <Header>
                                <a href="#" class="href">SP Invoice For Approval :</a><asp:Label ID="lblSpInv4rApproval"
                                    runat="server" Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="Panel6" runat="server">
                                    <table class="estbl" width="720px" id="Table6">
                                        <thead id="Thead13" runat="server" style="font-weight: bold">
                                            <tr align="left">
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Invoice No
                                                </td>
                                                <td>
                                                    CC Code
                                                </td>
                                                <td>
                                                    DCA Code
                                                </td>
                                                <td>
                                                    Date
                                                </td>
                                                <td>
                                                    Net Amount
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="TbodySpInv4rAppr" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordAssetSale" runat="server">
                            <Header>
                                <a href="#" class="href">Asset Sale Approval :</a><asp:Label ID="lblassetsale" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table class="estbl" width="720px" id="Table1">
                                        <thead id="Thead1" runat="server" style="font-weight: bold">
                                            <tr align="left">
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Request No
                                                </td>
                                                <td>
                                                    Item Code
                                                </td>
                                                <td>
                                                    Buyer Name
                                                </td>
                                                <td>
                                                    Date
                                                </td>
                                                <td>
                                                    Selling Amount
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="TbodyAccordAssetSale" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordHSNCode" runat="server">
                            <Header>
                                <a href="#" class="href">HSN/SAC Code Approval :</a><asp:Label ID="lblhsnapproval" runat="server"
                                    Text="" ForeColor="GreenYellow">&nbsp;&nbsp;&nbsp;</asp:Label></Header>
                            <Content>
                                <asp:Panel ID="Panel2" runat="server">
                                    <table class="estbl" width="720px" id="Table1">
                                        <thead id="Thead2" runat="server" style="font-weight: bold">
                                            <tr align="left">
                                                <td>
                                                    View
                                                </td>
                                                <td>
                                                    Category
                                                </td>
                                                <td>
                                                    HSN/SAC Code
                                                </td>
                                                <td>
                                                    Remarks
                                                </td>
                                           
                                            </tr>
                                        </thead>
                                        <tbody id="Tbodyhsnsaccode" runat="server">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </td>
        </tr>
    </table>
</asp:Content>
