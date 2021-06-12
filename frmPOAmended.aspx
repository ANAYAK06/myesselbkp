<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmPOAmended.aspx.cs"
    Inherits="Admin_frmPOAmended" EnableEventValidation="false" Title="PO Amended Information - Essel Projects Pvt.Ltd." %>
<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <accountmenu:menu id="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr valign="top">
                        <td align="center">
                            <table class="estbl" width="600px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" colspan="4" align="center">
                                        <asp:Label ID="Label19" runat="server" Text="PO Amended Information" CssClass="eslbl"></asp:Label>
                                        <div style="margin: -10px -190px 00px 350px">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="eslkbtn" OnClientClick="javascript:return display(true);">Edit</asp:LinkButton>
                                        </div>
                                    </th>
                                </tr>
                                <tr height="40px">
                                    <td style="width: 117px">
                                        <asp:Label ID="Label3" runat="server" Text="PO No" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlpo" runat="server" Width="150px" CssClass="esddown">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlpo"
                                            ServicePath="cascadingDCA.asmx" Category="dds" LoadingText="Please Wait"
                                            ServiceMethod="PoNo" PromptText="Select PO">
                                        </cc1:CascadingDropDown>
                                        <span id="spanAvailability" class="esspan"></span>

                                        

                                    </td>
                                    <td style="width: 110px">
                                        <asp:Label ID="Label4" runat="server" Text="Amended Date" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox ID="txtAmdDate" runat="server" Width="110px" onkeydown="return DateReadonly();"
                                            CssClass="estbox"></asp:TextBox><img onclick="scwShow(document.getElementById('<%=txtAmdDate.ClientID %>'),this);"
                                                alt="" src="images/cal.gif" style="left: 3px; position: relative; width: 15px;
                                                height: 15px;" id="cldrDob1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 117px">
                                        <asp:Label ID="Label20" runat="server" Text="Amended PO No" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td valign="top" align="left" style="width: 178px">
                                        <asp:TextBox ID="txtAmdPoNo" runat="server" CssClass="estbox" Width="44px"></asp:TextBox><span
                                            class="starSpan">*</span>
                                        <div style="margin: -20px 00px -30px 00px">
                                            <asp:DropDownList ID="ddlamdpo" runat="server" Width="150px" CssClass="esddown" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlamdpo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlamdpo"
                                                ServicePath="cascadingDCA.asmx" Category="amdpono" LoadingText="Please Wait"
                                                ServiceMethod="amdpo" PromptText="Select Amd PO" ParentControlID="ddlpo">
                                            </cc1:CascadingDropDown>
                                        </div>
                                        <%--<span class="starSpan" >*</span>--%>
                                        <span id="span" class="esspan"></span>
                                        <%--<script type="text/javascript">
                  var AmdpoCheckerTimer;
                  var span = $get("span");
                 function AmdpoChecker(amdpo_no) 
                  {
                clearTimeout(AmdpoCheckerTimer);
                if (amdpo_no.length == 0 )
                    span.innerHTML = "";
                else
                {
                    span.innerHTML = "<span style='color: #ccc;'>Matching....</span>";
                    var po=document.getElementById("ctl00_ContentPlaceHolder1_txtpono").value;
                    AmdpoCheckerTimer = setTimeout("checkAmdpoUsage('" + po + "','"+ amdpo_no + "');", 1050);
                }
            }
                function checkAmdpoUsage(po_no,amdpo_no) 
                  {
                // initiate the ajax pagemethod call
                // upon completion, the OnSucceded callback will be executed
                    PageMethods.IsAmdAvailable(po_no,amdpo_no,OnSucceeded);
                  }
//                  // Callback function invoked on successful completion of the page method.
            function OnSucceeded(result, userContext, methodName) 
            {
                if (methodName == "IsAmdAvailable")
                {
                 var btnAsn=document.getElementById('ctl00_ContentPlaceHolder1_btnPOAmended');
               
                     if(result ==true)
                    {
                       span.innerHTML = result;
                        btnAsn.disabled = true;
                    }

                    else
                    {
                        span.innerHTML = result;
                         btnAsn.disabled = false;
               }
               }
                if (methodName == "IsPOAvailable")
                {
               
                     
                     if(result ==true)
                    {
                        spanAvailability.innerHTML = result;
                       
                       
                    }
                    else
                    {
                       spanAvailability.innerHTML = result;
                         
                    }
                    }
                    }
                         

                 
                 
                 </script>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Basic" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmdBasic" runat="server" Width="120px" CssClass="estbox"></asp:TextBox>
                                        <span class="starSpan">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 117px">
                                        <asp:Label ID="Label24" runat="server" Text="Service Tax" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td style="width: 178px" align="left">
                                        <asp:TextBox ID="txtAmdtax" runat="server" CssClass="estbox"></asp:TextBox>
                                        <span class="starSpan">*</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Total" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal" runat="server" Width="120px" CssClass="estbox"></asp:TextBox>
                                        <span class="starSpan">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Button ID="btnPOAmended" runat="server" Text="Amend" OnClick="btnPOAmended_Click"
                                            CssClass="esbtn" />&nbsp;
                                        <asp:Button ID="update" runat="server" CssClass="esbtn" Text="Update" Style="visibility: hidden"
                                            OnClick="update_Click" />&nbsp;
                                        <input id="btnUpdCancel" runat="server" style="visibility: hidden" type="button"
                                            class="esbtn" value="Clear" onclick="javascript:return display()" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                                            CssClass="esbtn" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
function display(disp)
 {
     var btnPOAmended = document.getElementById('ctl00_ContentPlaceHolder1_btnPOAmended');
     var btnCancel = document.getElementById('ctl00_ContentPlaceHolder1_btnCancel');
     var LinkButton1 = document.getElementById('ctl00_ContentPlaceHolder1_LinkButton1');
    {
    if(disp)
        {
         document.getElementById('ctl00_ContentPlaceHolder1_LinkButton1').style.visibility="hidden";
         document.getElementById('ctl00_ContentPlaceHolder1_btnPOAmended').style.visibility="hidden";
         document.getElementById('ctl00_ContentPlaceHolder1_btnCancel').style.visibility="hidden";
         document.getElementById('ctl00_ContentPlaceHolder1_btnUpdCancel').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_update').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_ddlpo').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_ddlamdpo').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_txtAmdPoNo').style.visibility="hidden";


        }
        else
        {
         document.getElementById('ctl00_ContentPlaceHolder1_btnPOAmended').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_btnCancel').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_btnUpdCancel').style.visibility="hidden";
         document.getElementById('ctl00_ContentPlaceHolder1_update').style.visibility="hidden";
         document.getElementById('ctl00_ContentPlaceHolder1_LinkButton1').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_ddlpo').style.visibility="visible";
         document.getElementById('ctl00_ContentPlaceHolder1_ddlamdpo').style.visibility="hidden";
         document.getElementById('ctl00_ContentPlaceHolder1_txtAmdPoNo').style.visibility="visible";


         

         
           document.getElementById('ctl00_ContentPlaceHolder1_ddlpo').value="";
           document.getElementById('ctl00_ContentPlaceHolder1_ddlamdpo').value="";
           document.getElementById('ctl00_ContentPlaceHolder1_txtAmdDate').value="";
           document.getElementById('ctl00_ContentPlaceHolder1_txtAmdPoNo').value="";
           document.getElementById('ctl00_ContentPlaceHolder1_txtAmdBasic').value="";
           document.getElementById('ctl00_ContentPlaceHolder1_txtAmdtax').value="";
           document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value="";

        }
        return false;
    }
 }
    </script>
    <script type="text/javascript">
            
               var POCheckerTimer;
            var spanAvailability = $get("spanAvailability");

            function POChecker(po_no) 
            {
                clearTimeout(POCheckerTimer);
                if (po_no.length == 0 )
                    spanAvailability.innerHTML = "";
                else
                {
                   spanAvailability.innerHTML = "<span style='color: #ccc;'>Matching....</span>";
                    POCheckerTimer = setTimeout("checkponoUsage('" + po_no + "');", 100);
                   
                }
            }

            function checkponoUsage(po_no) 
            {
                // initiate the ajax pagemethod call
                // upon completion, the OnSucceded callback will be executed
                PageMethods.IsPOAvailable(po_no, OnSucceeded);
            }

                                        </script>
</asp:Content>
