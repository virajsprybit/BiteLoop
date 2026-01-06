<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="contactus-subject-modify.aspx.cs" Inherits="AdminPanel_ContactusSubject_Modify" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register Src="~/AdminPanel/includes/editor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Contact Us Subject Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-6">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmFoodItems" action="contactus-subject-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxTitle.ClientID %>">Subject</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxTitle" runat="server" name="tbxTitle" maxlength="25" />
                        </div>                        
                        <div class="pull-right">
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm(this);">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='contactus-subject-list.aspx';">Cancel</button>
                        </div>

                    </form>
                    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                </div>

            </div>
            <!-- / panel  -->
        </div>
    </div>
    <!-- / row 2 -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=tbxTitle.ClientID %>').focus();
         });
         var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {            
             var ErrMsg = '';

             if (jQuery.trim($('#<%=tbxTitle.ClientID %>').val()) == '') {
                 ErrMsg += '<br />- Subject';
             }


             if (ErrMsg.length != 0) {
                 var HeaderText = 'Please correct the following error(s):';
                 DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                 ScrollTop();
                 return false;
             }
             else {
                 $('#<%=divMsg.ClientID %>').hide();
                return true;
            }
            return true;
        }    
    </script>
</asp:Content>

