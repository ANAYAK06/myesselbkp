<%@ Page Title="Pending Home" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="PendingHome.aspx.cs" Inherits="PendingHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .popup-div-background {
            position: absolute;
            top: 0;
            left: 0;
            background-color: #ccc;
            filter: alpha(opacity=90);
            opacity: 0.9; /* the following two line will make sure
             /* that the whole screen is covered by
                 /* this transparent layer */
            height: 100%;
            width: 100%;
            min-height: 100%;
            min-width: 100%;
        }
    </style>
    <style type="text/css">
        a.btn.btn-blue {
            background-color: #699DB6;
            border-color: rgba(0,0,0,0.3);
            text-shadow: 0 1px 0 rgba(0,0,0,0.5);
            color: #FFF;
        }

            a.btn.btn-blue:hover {
                background-color: #4F87A2;
                border-color: rgba(0,0,0,0.5);
            }

            a.btn.btn-blue:active {
                background-color: #3C677B;
                border-color: rgba(0,0,0,0.9);
            }

        .accordionContent {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 90%;
        }

        .accordionHeaderSelected {
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

        .accordionHeader {
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

        .href {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }

        body {
            margin: 0;
            padding: 0;
            text-align: center; /* !!! */
        }

        .centered {
            margin: 0 auto;
            text-align: left;
            width: 850px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered">
        <table width="900px">
            <tr valign="top">
                <td style="width: 100%;">
                    <cc1:Accordion ID="UserAccordion" runat="server" SelectedIndex="-1" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        FadeTransitions="true" SuppressHeaderPostbacks="false" TransitionDuration="250"
                        FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None" Width="100%">
                        <Panes>
                            <cc1:AccordionPane ID="AccordSupplier" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Supplier Invoice For Approval:</a><asp:Label ID="lblSupplier"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnksupplier" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/BasicPriceUpdationnewGST.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="PnlSupplier" runat="server">
                                        <table class="estbl" width="800px">
                                            <thead id="tblSupplier" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Mrr No
                                                    </td>
                                                    <td>PO NO
                                                    </td>
                                                    <td>Invoice No
                                                    </td>
                                                    <td>Invoice Date
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodySupplier" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordPayApproval" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Payment For Approvals:</a><asp:Label ID="lblPay4rApproval"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkPo4rApproval" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/Inbox2.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlpay" runat="server">
                                        <table class="estbl" width="800px">
                                            <thead id="tblpay" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>CC Code
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="Po4rApproval" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordTRApproval" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Term Loan Verification:</a><asp:Label ID="lblTRApproval"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkTR4rApproval" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyTermloanApproval.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <table class="estbl" width="800px">
                                            <thead id="Thead19" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>CC Code
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TR4rApprovals" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordSppoApproval" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">SPPO Approvals :</a><asp:Label ID="lblsppoapprov" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkSpSPPO" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/verifysppo.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlsppo" runat="server">
                                        <table class="estbl" width="800px">
                                            <thead id="tblsppo" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Po No
                                                    </td>
                                                    <td>VendorId
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                    <td>DCA Code
                                                    </td>
                                                    <td>SubDCA Code
                                                    </td>
                                                    <td>PO Value
                                                    </td>
                                                    <td>PO Date
                                                    </td>
                                                    <td>Remarks
                                                    </td>
                                                    <td>Vendor Name
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="SpSPPO" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordAmendSppoApproval" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Amend SPPO Approvals :</a><asp:Label ID="lblamndsppo" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkAmendSppo" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/AmendSPPO.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlAmendSppo" runat="server">
                                        <table class="estbl" width="800px" id="tblAmendSppo">
                                            <thead id="Thead1" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td colspan="2">PONO
                                                    </td>
                                                    <td>Amended Amount
                                                    </td>
                                                    <td>Amended Date
                                                    </td>
                                                    <td>Remarks
                                                    </td>
                                                    <td>Vendor Name
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyAmendSppo" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordBank" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Bank Cheque Books For Approval :</a><asp:Label ID="lblbankcheque"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkBankCheque" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/chequebook.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlBank" runat="server">
                                        <table class="estbl" width="800px" id="tblBank">
                                            <thead id="Thead3" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Issue Date
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                    <td>From
                                                    </td>
                                                    <td>To
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyBankCheque" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordGenInv" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Verify General Invoice :</a><asp:Label ID="lblGenInv" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkGenInvoice" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/verifygeneralpayment.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlGenInvoice" runat="server">
                                        <table class="estbl" width="800px" id="tblGenInvoice">
                                            <thead id="Thead4" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>InvoiceNo
                                                    </td>
                                                    <td>CCCode
                                                    </td>
                                                    <td>DCA Code
                                                    </td>
                                                    <td>Sub DCA Code
                                                    </td>
                                                    <td>Name
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                    <td>Mode Of Payment
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyGenInvoice" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordCostCenter" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Verify Cost Center :</a><asp:Label ID="lblccverify" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkCostCenter" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/frmAddCostCenter.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="PnlCostCenter" runat="server">
                                        <table class="estbl" width="800px" id="tblCostCenter">
                                            <thead id="Thead5" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>CC-Code
                                                    </td>
                                                    <td>CC-Name
                                                    </td>
                                                    <td>CC-InchargeName
                                                    </td>
                                                    <td>Address
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyCostCenter" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordCshTransfer" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Cash Transfer For Approval :</a><asp:Label ID="lblcshtransfer"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkCshTransfer" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/SrAccountantInbox.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlcshtrnsfr" runat="server">
                                        <table class="estbl" width="800px" id="tblcshtrnsfr">
                                            <thead id="Thead6" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Id
                                                    </td>
                                                    <td>Category
                                                    </td>
                                                    <td>Voucher Date
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                    <td>Debit
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyCshTransfer" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordAdvPay" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Verify Advance Payment :</a><asp:Label ID="lbladvpay" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkAdvPay" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyClientAdvanceReciept.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlAdvPay" runat="server">
                                        <table class="estbl" width="800px" id="tblAdvPay">
                                            <thead id="Thead7" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Po_No
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                    <td>Transaction No
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyAdvPay" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordcrdtPay" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Verify Credit Payment :</a><asp:Label ID="lblcrdtpay" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkCrdtPay" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyClientInvoicePayment.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlcrdtpay" runat="server">
                                        <table class="estbl" width="800px" id="tblcrdtpay">
                                            <thead id="Thead8" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Pono
                                                    </td>
                                                    <td>CC Codw
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Credit
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyCrdtPay" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordClosePo" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Close SPPO Approvals :</a><asp:Label ID="lblClosePO" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkClosePO" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/ClosePO.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table class="estbl" width="800px" id="Table1">
                                            <thead id="Thead9" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>CCCode
                                                    </td>
                                                    <td>cc name
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyClosePO" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordIndentApproval" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Indent For Approval :</a><asp:Label ID="lblIndentApprov"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkIndentApprov" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/Indent.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table class="estbl" width="800px" id="Table2">
                                            <thead id="Thead2" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Indent No
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                    <td>Indent Date
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                    <td>Indent Cost
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyIndentApprov" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordApproveDirectStock" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Approve Direct Stock Updation :</a><asp:Label ID="lblVerifyStockUpd"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkVStockUpdate" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/ViewStockUpdation.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <table class="estbl" width="800px" id="Table4">
                                            <thead id="Thead11" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Request No
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                    <td>Request Date
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodyVStockUpdate" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordSpInv4rAppr" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">SP Invoice For Approval :</a><asp:Label ID="lblSpInv4rApproval"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkSpInv4rAppr" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/InvoiceVerficationNew.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="Panel6" runat="server">
                                        <table class="estbl" width="800px" id="Table6">
                                            <thead id="Thead13" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>CC Code
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                    <td>DCA Code
                                                    </td>
                                                    <td>SUB DCA
                                                    </td>
                                                    <td>Invoice No
                                                    </td>
                                                    <td>Net Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="TbodySpInv4rAppr" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordSuppPO" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Supplier PO'S/DO'S:</a><asp:Label ID="lblpototal" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkPobody" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/ViewsVendorPo.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlpos" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="tblpos" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>PO NO
                                                    </td>
                                                    <td>po date
                                                    </td>
                                                    <td>Indent No
                                                    </td>
                                                    <td>Ref No.
                                                    </td>
                                                    <td>Ref Date
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                    <td>Remarks
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="Pobody" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="Accordother" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Other Receipts:</a><asp:Label ID="lblothers" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkothers" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyCreditPayments.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlothers" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead10" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Date
                                                    </td>
                                                    <td>CC Code
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Payment Type
                                                    </td>
                                                    <td>Descriprion
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyothers" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="Accordunsecured" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Unsecured Loans:</a><asp:Label ID="lblunsecured" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkunsecured" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyUnsecuredLoan.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlothersunsecured" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead12" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Date
                                                    </td>
                                                    <td>Loan no
                                                    </td>
                                                    <td>Name
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Descriprion
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyunsecured" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="Accordionsharecapital" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Share Capital:</a><asp:Label ID="lblsharecapital" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnksharecapital" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyShareCapital.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlsharecapital" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead14" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Date
                                                    </td>
                                                    <td>Name
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Descriprion
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodysharecapital" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordionopenFD" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Fixed Deposit:</a><asp:Label ID="lblfixedopen" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkfdopen" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyNewFD.aspx" ForeColor="White" Text="Click Here for New FD Approvals"></asp:HyperLink>&nbsp;&nbsp;&nbsp;<asp:HyperLink
                                                        runat="server" ID="hlnkfdinterest" class="btn btn-blue" Visible="false" NavigateUrl="~/VerifyFDInterest.aspx"
                                                        ForeColor="White" Text="Click Here for FD Interest Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlfdopen" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead15" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Date
                                                    </td>
                                                    <td>FDR
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Descriprion
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyfixedopen" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordFdclaim" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Fixed Deposit(Partially\Fully Closed):</a><asp:Label ID="lblfdclosed" runat="server"
                                            Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkfdclaim" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/VerifyClaimFD.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlfdclaim" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead16" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>Date
                                                    </td>
                                                    <td>FDR
                                                    </td>
                                                    <td>Bank Name
                                                    </td>
                                                    <td>Descriprion
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyfdclaim" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordClientrenthold" runat="server" Width="800px">
                                <Header>
                                    <div>
                                        <a href="#" class="href">Verify Client Retention and Hold:</a><asp:Label ID="lblretholdcount"
                                            runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                                <asp:HyperLink runat="server" ID="hlnkclretention" class="btn btn-blue" Visible="false"
                                                    NavigateUrl="~/CustomerRetentionVerification.aspx" ForeColor="White" Text="Click Here for Retention Approvals"></asp:HyperLink>&nbsp;&nbsp;&nbsp;<asp:HyperLink
                                                        runat="server" ID="hlnkclhold" class="btn btn-blue" Visible="false" NavigateUrl="~/CustomerHoldVerification.aspx"
                                                        ForeColor="White" Text="Click Here for Hold Approvals"></asp:HyperLink>
                                        </div>
                                    </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlrenthold" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead17" runat="server" style="font-weight: bold">
                                                <tr align="center">
                                                    <td>PaymentType
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                    <td>Bank_Name
                                                    </td>
                                                    <td>Description
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyretenhold" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordItemcode" runat="server">
                                <Header>
                                    <a href="#" class="href">Item Code Approval:</a><asp:Label ID="lblitemcodecount"
                                        runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                            <asp:HyperLink runat="server" ID="hlnkitemcodes" class="btn btn-blue" Visible="false"
                                                NavigateUrl="~/verifyitemcode.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlitem" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="tblitem" runat="server">
                                                <tr align="center">
                                                    <td>Item Code
                                                    </td>
                                                    <td>Item Name
                                                    </td>
                                                    <td>Basic Price
                                                    </td>
                                                    <td>Specification
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="itemcodebody" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="Accordassetsale" runat="server">
                                <Header>
                                    <a href="#" class="href">Asset Sale Approval:</a><asp:Label ID="lblassetsalecount"
                                        runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                            <asp:HyperLink runat="server" ID="hlnkassetsale" class="btn btn-blue" Visible="false"
                                                NavigateUrl="~/VerifyAssetSale.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlasset" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead18" runat="server">
                                                <tr align="center">
                                                    <td>Request No
                                                    </td>
                                                    <td>Item code
                                                    </td>
                                                    <td>Buyer Name
                                                    </td>
                                                    <td>Date
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="assetsalebody" runat="server">
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordionPanehsn" runat="server">
                                <Header>
                                    <a href="#" class="href">HSN/SAC Approval:</a><asp:Label ID="lblhsncount"
                                        runat="server" Text="">&nbsp;&nbsp;&nbsp;</asp:Label><div style="float: right;">
                                            <asp:HyperLink runat="server" ID="hlnkhsnapproval" class="btn btn-blue" Visible="false"
                                                NavigateUrl="~/VerifyHSNSACCode.aspx" ForeColor="White" Text="Click Here for Approvals"></asp:HyperLink>
                                        </div>
                                </Header>
                                <Content>
                                    <asp:Panel ID="pnlhsn" runat="server">
                                        <table class="estbl" width="720px">
                                            <thead id="Thead20" runat="server">
                                                <tr align="center">
                                                    <td>Category
                                                    </td>
                                                    <td>HSN/SAC Code
                                                    </td>
                                                    <td>Remarks
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="hsnbody" runat="server">
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
    </div>
</asp:Content>
