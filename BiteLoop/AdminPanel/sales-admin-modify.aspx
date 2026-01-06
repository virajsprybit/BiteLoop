<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="sales-admin-modify.aspx.cs" Inherits="AdminPanel_SalesAdmin_Modify" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
        <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Sales Admin Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmAdmin" action="sales-admin-modify.aspx" onsubmit="ValidateForm();return false;">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>
                                                
                        <div class="form-group">
                            <label for="<%=tbxFirstName.ClientID%>">Full Name</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxFirstName" runat="server" name="tbxFirstName" maxlength="50" />
                        </div>                      
                        <div class="form-group">
                            <label for="<%=tbxEmail.ClientID%>">Email Address</label><span class="red">*</span>
                            <input id="tbxEmail" runat="server" name="tbxEmail" type="text" size="30" maxlength="100" class="form-control" />
                            <input type="hidden" id="hdnpwd" runat="server" name="hdnpwd" />
                        </div>
                        <div class="form-group" id="tbPassword" runat="server">
                            <label for="<%=tbxPassword.ClientID%>">Password</label><span class="red">*</span>
                            <div id="divPwd" runat="server">
                                <input id="tbxPassword" runat="server" name="tbxPassword" type="text" class="form-control" maxlength="20" /><b>Note :</b> Password must be at least 6 characters long
                            <a href="javascript:;" style="display: none;" id="hrefCancel" onclick="ShowHideCtrl('<%= divlblPwd.ClientID %>','<%= divPwd.ClientID %>');$('#ctl00_CPHContent_tbxPassword').attr('value','');">&nbsp;&nbsp;&nbsp;<i class="icon-remove"></i></a>
                            </div>
                            <div id="divlblPwd" runat="server" visible="false">
                                <label id="lblPassword" runat="server"></label>
                                <a href="javascript:;" onclick="ShowHideCtrl('<%= divPwd.ClientID %>','<%= divlblPwd.ClientID %>');$('#hrefCancel').show();">&nbsp;&nbsp;&nbsp;<i class="icon-pencil"></i></a>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="trConfirmPwd">
                            <label for="<%=tbxConfPassword.ClientID%>">Confirm Password</label><span class="red">*</span>
                            <input name="tbxConfPassword" runat="server" id="tbxConfPassword" maxlength="20" class="form-control" size="30" type="Password" />
                        </div>
                        <div class="form-group">
                            <label>Mobile</label>
                            <input id="tbxphone" name="tbxphone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                        </div>
                        <div class="pull-right">
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='sales-admin-list.aspx';">Cancel</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        var divMsg = '<%= divMsg.ClientID %>';
        $(document).ready(function () {
            $('#<%=tbxFirstName.ClientID %>').focus();
        });
        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
        function ValidateForm() {
            var ErrMsg = '';           
            ErrMsg += DirValCtrl();
            if (jQuery.trim($('#<%=tbxEmail.ClientID %>').val()) != '') {
                if (!IsEmail(jQuery.trim($('#<%=tbxEmail.ClientID %>').val()))) {
                    ErrMsg += '<br/> - ' + 'Email address is not valid.';
                }
            }
            if (jQuery.trim($('#<%=tbxPassword.ClientID %>').val()) != '') {
                if (jQuery.trim($('#<%=tbxPassword.ClientID %>').val()).length < 6) {
                    ErrMsg += '<br/> - ' + 'Password must be at least 6 characters long.';
                }
            }
            if (jQuery.trim($('#<%=tbxPassword.ClientID %>').val()) != '' && jQuery.trim($('#<%=tbxConfPassword.ClientID %>').val()) != '') {
                if (jQuery.trim($('#<%=tbxPassword.ClientID %>').val()) != jQuery.trim($('#<%=tbxConfPassword.ClientID %>').val())) {
                    ErrMsg += '<br/> - ' + 'Password and Confirm Password must be same.';
                }
            }
            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
            }

            else {
                $('#' + divMsg).hide();
                $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
                $.ajax(
                {
                    url: 'sales-admin-modify.aspx?id=<%= ID %>', data: $('#frmAdmin').serialize(), type: 'POST',
                    success: function (resp) {
                        if (resp == 'duplicate') {
                            DisplMsg(divMsg, 'This email address already exists. So please try another email address.', 'alert-message error');
                        }
                        else {
                            DisplMsg(divMsg, 'Sales Admin information has been saved successfully.', 'alert-message success');
                            window.setTimeout("window.location.href='sales-admin-list.aspx'", 2000);
                        }
                    $.hideprogress();                  
                }
            });
        }
    }
    </script>



</asp:Content>

