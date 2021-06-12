<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAddIt.aspx.cs"
    Inherits="Admin_frmAddIt" EnableEventValidation="false" Title="Add IT - Essel Projects Pvt. Ltd." %>

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
                        <td align="center">
                            <div>
                                <table class="estbl" width="600px">
                                    <tr style="border: 1px solid #000">
                                        <th colspan="2">
                                            <asp:Label ID="itform" CssClass="esfmhead" runat="server" Text="Add IT Code Form"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblitcode" CssClass="eslbl" runat="server" Text="IT Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtitCode" CssClass="estbox" runat="server" Style="margin: 0px 0px 0px -120px;"
                                                onkeyup="CCcodeChecker(this.value);" MaxLength="50"></asp:TextBox><span class="starSpan">*</span><span
                                                    id="spanAvailability"></span>
                                            <asp:DropDownList ID="ddlitcode" CssClass="esddown" runat="server" onchange="ITHnamechecker(this.value);"
                                                Style="margin: 0px 0px 0px -130px;">
                                            </asp:DropDownList>
                                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlitcode"
                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="itcode">
                                            </cc1:CascadingDropDown>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblithead" CssClass="eslbl" runat="server" Text="IT Head"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtithead" CssClass="estbox" onkeyup="ITHeadchecker(this.value);"
                                                Style="margin: 0px 0px 0px -105px;" runat="server" MaxLength="50"></asp:TextBox><span
                                                    class="starSpan">*</span><span id="span1"></span>
                                            <asp:TextBox ID="TextBox1" CssClass="estbox" Style="margin: 0px 0px 0px -140px;"
                                                runat="server"></asp:TextBox>
                                            <span id="span2"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAddIT" CssClass="esbtn" runat="server" Text="Add IT" OnClick="btnAddIT_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="update" CssClass="esbtn" runat="server" Text="Update" Style="visibility: hidden"
                                                OnClick="update_Click" />
                                            <input id="btnUpdCancel" class="esbtn" runat="server" type="button" value="Clear"
                                                onclick="javascript:return display()" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="esbtn" Text="Cancel" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                    <%-- <tr class="estr">
        <td colspan="2">
            <asp:Label ID="lblAlert" CssClass="eslblalert" runat="server" Text=""></asp:Label></td>
        </tr> --%>
                                </table>
                                <div style="margin: -120px 0px 0px 370px; top: 303px; left: 231px; height: 24px;"
                                    class="">
                                    <asp:LinkButton ID="LinkButton1" CssClass="eslkbtn" runat="server" OnClientClick="javascript:return display(true);">Edit</asp:LinkButton>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <script type="text/javascript">
 function display(disp)
 {
    var panel2=document.getElementById('ctl00_ContentPlaceHolder1_panel2');
    var update = document.getElementById('ctl00_ContentPlaceHolder1_update');
    var btnaddcc=document.getElementById('ctl00_ContentPlaceHolder1_btnAddIT');
    var panel1 = document.getElementById('ctl00_ContentPlaceHolder1_panel1');

if(disp)
{ 
    document.getElementById('ctl00_ContentPlaceHolder1_ddlitcode').style.visibility="visible";
    document.getElementById('ctl00_ContentPlaceHolder1_update').style.visibility="visible";
    document.getElementById('ctl00_ContentPlaceHolder1_btnAddIT').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_txtitCode').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_btnUpdCancel').style.visibility="visible";
    document.getElementById('ctl00_ContentPlaceHolder1_btnCancel').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_txtithead').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_TextBox1').style.visibility="visible";


}
else
{   
    document.getElementById('ctl00_ContentPlaceHolder1_txtitCode').value="";
    document.getElementById('ctl00_ContentPlaceHolder1_txtithead').value="";
        
    document.getElementById('ctl00_ContentPlaceHolder1_TextBox1').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_ddlitcode').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_update').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_btnAddIT').style.visibility="visible";
    document.getElementById('ctl00_ContentPlaceHolder1_txtitCode').style.visibility="visible";
    document.getElementById('ctl00_ContentPlaceHolder1_btnUpdCancel').style.visibility="hidden";
    document.getElementById('ctl00_ContentPlaceHolder1_btnCancel').style.visibility="visible";
    document.getElementById('ctl00_ContentPlaceHolder1_txtithead').style.visibility="visible";

}
return false;
}
    </script>

    <script type="text/javascript">
            var CCcodeCheckerTimer;
            var spanAvailability = $get("spanAvailability");

            function CCcodeChecker(cccode) 
            {
                clearTimeout(CCcodeCheckerTimer);
                if (cccode.length == 0)
                    spanAvailability.innerHTML = "";
                else
                {
                    spanAvailability.innerHTML = "<span style='color: #ccc;'>checking...</span>";
                    CCcodeCheckerTimer = setTimeout("checkcc('" +cccode+ "');", 750);
                }
            }

            function checkcc(cccode) 
            {
                // initiate the ajax pagemethod call
                // upon completion, the OnSucceded callback will be executed
                PageMethods.IsITCodeAvailable(cccode, OnSucceeded);
            }

            // Callback function invoked on successful completion of the page method.
            function OnSucceeded(result, userContext, methodName) 
            {
                if (methodName == "IsITCodeAvailable")
                {
                    var btnAddIT = document.getElementById('ctl00_ContentPlaceHolder1_btnAddIT');
                    

                     if (result == "IT Code Already Exists")
                    {
                        spanAvailability.innerHTML ="<font color='red'>"+ result+"</font>";
                        btnAddIT.disabled =true;
                    }

                    else
                    {
                       spanAvailability.innerHTML ="<font color='green'>"+result+" is Available</font>";
                       btnAddIT.disabled =false;
                    }
                     
                }
            }




    </script>

    <script type="text/javascript">
            var ItcodecheckerTimer;
            var span1 = $get("span1");

            function ITHeadchecker(cccode) 
            {
                clearTimeout(ItcodecheckerTimer);
                if (cccode.length == 0)
                    span1.innerHTML = "";
                else
                {
                    span1.innerHTML = "<span style='color: #ccc;'>checking...</span>";
                    ItcodecheckerTimer = setTimeout("checkcc1('" +cccode+ "');", 750);
                }
            }

            function checkcc1(cccode) 
            {
                // initiate the ajax pagemethod call
                // upon completion, the OnSucceded callback will be executed
                PageMethods.IsITHeadAvailable(cccode, OnSucceed);
            }

            // Callback function invoked on successful completion of the page method.
            function OnSucceed(result, userContext, methodName) 
            {
                if (methodName == "IsITHeadAvailable")
                {
                     var btnAddIT = document.getElementById('ctl00_ContentPlaceHolder1_btnAddIT');
                    //var txtithead = document.getElementById('ctl00_ContentPlaceHolder1_txtithead');

                     if (result == "IT Head Already Exists")
                    {
                        span1.innerHTML = "<font color='red'>"+ result+"</font>"
                        btnAddIT.disabled =true;
                    }

                    else
                    {
                       span1.innerHTML ="<font color='green'>"+result+" is Available</font>";
                       btnAddIT.disabled =false;
                    }
//                     if (result == true)
//                    {
//                        txtithead.value=result;
//                    }

//                    else
//                    {
//                      txtithead.value=result;
//                    }
                     
                }
            }


    </script>

    <script type="text/javascript">
            var ItheadcheckerTimer;
            var span2 = $get("span2");

            function ITHnamechecker(cccode) 
            {
                clearTimeout(ItheadcheckerTimer);
                if (cccode.length == 0)
                    span2.innerHTML = "";
                else
                {
                    //span2.innerHTML = "<span style='color: #ccc;'>checking...</span>";
                    ItheadcheckerTimer = setTimeout("checkcc12('" +cccode+ "');", 750);
                }
            }

            function checkcc12(cccode) 
            {
                // initiate the ajax pagemethod call
                // upon completion, the OnSucceded callback will be executed
                PageMethods.IsITHnameAvailable(cccode, OnSuccee);
            }

            // Callback function invoked on successful completion of the page method.
            function OnSuccee(result, userContext, methodName) 
            {
                if (methodName == "IsITHnameAvailable")
                {
                     
                    var txtithead = document.getElementById('ctl00_ContentPlaceHolder1_TextBox1');

                    
                     if (result == true)
                    {
                        txtithead.value=result;
                    }

                    else
                    {
                      txtithead.value=result;
                    }
                     
                }
            }


    </script>

</asp:Content>
