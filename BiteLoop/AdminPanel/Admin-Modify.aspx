<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Admin-Modify.aspx.cs" Inherits="AdminPanel_Admin_Modify" ValidateRequest="false"%>

<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
        <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Admin Modify</a></li>
            </ol>
        </div>
    </div>
   <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">

                    <form id="frmAdmin" action="admin-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                        class="nice custom" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>

                        <div class="col-lg-6">
                            <div class="form-group hide">
                                <label for="<%= ddlMemberType.ClientID %>">User Type</label><span class="red">*</span>
                                <select runat="server" id="ddlMemberType" class="drp-border-full">
                                    <%--<option value="0">Select User Type</option>--%>
                                    <option value="2">Team Manager</option>
                                    <%--<option value="3">Agent</option>--%>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="<%=tbxFirstName.ClientID%>">First Name</label>
                                <input type="text" class="form-control" id="tbxFirstName" runat="server" name="tbxFirstName" maxlength="50" />
                            </div>
                            <div class="form-group">
                                <label for="<%=tbxLastName.ClientID%>">Last Name</label>
                                <input type="text" class="form-control" id="tbxLastName" runat="server" name="tbxLastName" maxlength="50" />
                            </div>
                         
                            <div class="form-group">
                                <label for="<%=tbxEmail.ClientID%>">Email Address</label>
                                <input id="tbxEmail" runat="server" name="tbxEmail" type="text" size="30" maxlength="100" class="form-control" autocomplete="off" />
                                <input type="hidden" id="hdnpwd" runat="server" name="hdnpwd" />
                            </div>
                               <div class="form-group">
                                <label for="<%=tbxUserName.ClientID%>">
                                    User Name</label><span class="red">*</span>
                                <input id="tbxUserName" runat="server" name="tbxUserName"  type="text" class="form-control" maxlength="50" />
                            </div>
                            <div class="form-group" id="tbPassword" runat="server" visible="false">
                                <label for="<%=tbxPassword.ClientID%>">Password</label><span class="red">*</span>
                                <div id="divPwd" runat="server">
                                    <input id="tbxPassword" runat="server" name="tbxPassword" type="password" autocomplete="off" class="form-control" maxlength="20" /><b>Note :</b> Password must be at least 6 characters long
                            <a href="javascript:;" style="display: none;" id="hrefCancel" onclick="ShowHideCtrl('<%= divlblPwd.ClientID %>','<%= divPwd.ClientID %>');$('#ctl00_CPHContent_tbxPassword').attr('value','');">Cancel</a>
                                </div>
                                <div id="divlblPwd" runat="server" visible="false">
                                    <label id="lblPassword" runat="server"></label>
                                    <a href="javascript:;" onclick="ShowHideCtrl('<%= divPwd.ClientID %>','<%= divlblPwd.ClientID %>');$('#hrefCancel').show();">Edit</a>
                                </div>
                            </div>
                            <div class="form-group" runat="server" id="trConfirmPwd">
                                <label for="<%=tbxConfPassword.ClientID%>">Confirm Password</label><span class="red">*</span>
                                <input name="tbxConfPassword" runat="server" id="tbxConfPassword" maxlength="20" class="form-control" size="30" type="Password" autocomplete="off" />
                            </div>
                            <div class="form-group hide">
                                <label>Phone Number</label>
                                <input id="tbxphone" name="tbxphone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                            </div>
                            <div class="formRow">
                                <label for="<%= fupdImage.ClientID %>">
                                    Avtar</label>
                                <input id="fupdImage" runat="server" name="fupdImage" type="file" size="30" maxlength="500"
                                    class="input-text large " />
                                <b>Note:</b>Image size approx.(200px Width x 200px Height)
                            </div>
                            <div class="formRow">
                                <img id="imgImage" src="" alt="" runat="server" visible="false" />
                                <img src="<%=Config.VirtualDir %>images/delete.png" alt="Remove" id="imgCategory"
                                    title="Remove" onclick="RemoveCategoryImage(this)" style="cursor: pointer; display: none;" />
                                <input type="hidden" runat="server" id="hdnCategoryFile" name="hdnCategoryFile" />
                                <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                            </div>
                        </div>
                        <%--<div class="col-lg-6">
                            <label>Module Access</label><br />
                            <asp:Repeater ID="rptPages" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-4">
                                        <input type="checkbox" class="chkPages" id="<%#Eval("ID") %>"  value="<%#Eval("ID") %>"/>&nbsp;&nbsp;<%#Eval("PageName") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>--%>
                        <div class="clearfix"></div>
                        <div class="pull-right">
                            <input type="hidden" id="hdnPages" name="hdnPages" class="hdnPages"  runat="server"/>
                            <%--<button type="button" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm()">Save Information</button>--%>
                            <input class="btn btn-primary" runat="server" id="btnSave" type="submit" value="Save Information"
                                name="btnSave" />
                            <button type="button" class="btn btn-default" onclick="window.location='admin-list.aspx';">Cancel</button>
                        </div>
                        <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
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
            if (parseInt('<%=ID%>') == 0) {
                $('#<%=tbxEmail.ClientID %>').val('');
                $('#<%=tbxPassword.ClientID %>').val('');
                $('#<%=tbxConfPassword.ClientID %>').val('');
                $('#<%=tbxUserName.ClientID %>').val('');
            }
            $('#<%=tbxFirstName.ClientID %>').focus();

            var Pages = jQuery.trim('<%=strPages%>');
            if (Pages != '') {
                var Pages = Pages.split(',');
                for (var i = 0; i < Pages.length; i++) {
                    $('.chkPages').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(Pages[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }


        });
        function ValidateForm() {
            var ErrMsg = '';
            if (jQuery.trim($('#<%=tbxFirstName.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'First Name';
            }
            if (jQuery.trim($('#<%=tbxLastName.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Last Name';
            }
            if (jQuery.trim($('#<%=tbxEmail.ClientID %>').val()) != '') {
                if (!isValidEmailAddress(jQuery.trim($('#<%=tbxEmail.ClientID %>').val()))) {
                    ErrMsg += '<br/> - ' + 'Email address is not valid.';
                }
            }
            if (jQuery.trim($('#<%=tbxUserName.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'User Name';
            }

            if (parseInt('<%=ID%>') == 0) {
                if (jQuery.trim($('#<%=tbxPassword.ClientID %>').val()) == '') {
                    ErrMsg += '<br/> - ' + 'Password';
                }
                if (jQuery.trim($('#<%=tbxConfPassword.ClientID %>').val()) == '') {
                    ErrMsg += '<br/> - ' + 'Confirm Password';
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
            }
            var fupdImage = '<%= fupdImage.ClientID %>';
            if (document.getElementById(fupdImage).value != '') {
                if (!isImage(document.getElementById(fupdImage))) {
                    ErrMsg = ErrMsg + '<br> - Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF files.';
                    document.getElementById(fupdImage).value = '';
                }
            }


            if (ErrMsg.length != 0) {
                var HeaderText = '<b>Please correct the following error(s):</b>';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                return false;
            }

            else {

                var PageNames = '';
                $('.chkPages').each(function () {
                    if ($(this).prop('checked')) {
                        PageNames = PageNames + $(this).val() + ',';
                    }
                });

                if (PageNames.length > 0) {
                    PageNames = PageNames.substring(0, PageNames.length - 1);
                    $('#<%=hdnPages.ClientID%>').val(PageNames);
                }


                $('#' + divMsg).hide();
                return true;
            }
            return true;
        }
    </script>



</asp:Content>

