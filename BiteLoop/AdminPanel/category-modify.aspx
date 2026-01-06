<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="category-modify.aspx.cs" Inherits="AdminPanel_Category_Modify" EnableEventValidation="false" ValidateRequest="false" %>

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
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Category Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmCategory" action="category-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxTitle.ClientID %>">Title</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxTitle" runat="server" name="tbxTitle" maxlength="25" />
                        </div>                        
                        <div class="form-group hide">
                            <label>URL</label>
                            <input type="text" class="form-control" id="tbxURL" runat="server" name="tbxURL" maxlength="250" />
                        </div>
                        <div class="form-group">
                            <label for="exampleInputFile">Image</label>
                            <input type="file" id="fupdImage" runat="server" name="fupdImage" />
                            <b>Note:</b>Image size approx.(50px Width x 50px Height) 
                            <br />
                            <b>Format :</b> .JPG, .JPEG, .PNG, .BMP, .GIF                                         
                        </div>
                        <div class="form-group">
                            <img id="imgImage" src="" alt="" runat="server" visible="false" />
                            <img src="<%=Config.VirtualDir %>adminpanel/images/delete.png" alt="Remove" id="imgCategory" title="Remove" onclick="RemoveCategoryImage(this)" style="cursor: pointer; display: none;" />
                            <input type="hidden" runat="server" id="hdnCategoryFile" name="hdnCategoryFile" />
                            <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                        </div>
                        
                        <div class="pull-right">
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm(this);">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='category-list.aspx';">Cancel</button>
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
             if ($('#<%=hdnImage.ClientID%>').val() == '') {
                 $('#imgCategory').hide();
             }
             else {
                 $('#imgCategory').show();
             }
         });
         var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {            
             var ErrMsg = '';

             if (jQuery.trim($('#<%=tbxTitle.ClientID %>').val()) == '') {
                 ErrMsg += '<br />- Title';
             }

             var fupdImage = '<%= fupdImage.ClientID %>';
             if (document.getElementById(fupdImage).value != '') {
                 if (!isImage(document.getElementById(fupdImage))) {
                     ErrMsg = ErrMsg + '<br> - ' + 'Please upload only JPG, .JPEG, .PNG, .BMP, .GIF image files.';
                     document.getElementById(fupdImage).value = '';
                 }
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
        function RemoveCategoryImage(imgCategory) {
            if (confirm('Are you sure you want to remove image?')) {
                $(imgCategory).hide();
                $('#<%=imgImage.ClientID %>').hide();
                $('#<%=hdnImage.ClientID %>').val('');
            }
        }
    </script>
</asp:Content>

