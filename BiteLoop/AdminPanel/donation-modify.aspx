<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="donation-modify.aspx.cs" Inherits="AdminPanel_Donation_Modify" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Import Namespace="Utility" %>
<%@ Register Src="~/AdminPanel/includes/editor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-6">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmFaq" action="donation-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxTitle.ClientID %>">Amount</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxTitle" runat="server" name="tbxTitle" maxlength="50" />
                        </div>                        
                        <div class="pull-right">
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" onclick="return ValidateForm();">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='donations-list.aspx';">Cancel</button>
                            <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
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

        function ValidateForm() {
            $('#<%= hdnContent.ClientID %>').val(FillTextArea());
            var ErrMsg = '';
            if (jQuery.trim($('#<%=tbxTitle.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - Amount';
            }

            if (ErrMsg.length != 0) {
                ScrollTop();
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                return false;
            }
            else {
                $('#<%=divMsg.ClientID %>').hide(); return true;
            }
            
        }    </script>
</asp:Content>

