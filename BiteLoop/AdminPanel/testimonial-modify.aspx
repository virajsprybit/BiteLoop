<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="testimonial-modify.aspx.cs" Inherits="AdminPanel_Testimonial_Modify" EnableEventValidation="false" ValidateRequest="false" %>

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
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Testimonial Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmTestimonials" action="testimonial-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= txtClientName.ClientID %>">Client Name</label><span class="red">*</span>
                            <input type="text" class="form-control" id="txtClientName" runat="server" name="txtClientName" maxlength="100" />
                        </div>
                        <div class="form-group">
                            <label for="<%= txtTitle.ClientID %>">Title</label><span class="red">*</span>
                            <input type="text" class="form-control" id="txtTitle" runat="server" name="txtTitle" maxlength="100" />
                        </div>
                        <div class="form-group">
                            <label for="<%= txtDescription.ClientID %>">Description</label><span class="red">*</span>                            
                            <textarea id="txtDescription" class="form-control" name="txtDescription" rows="3" cols="50" runat="server"></textarea>
                        </div>                        
                        <div class="form-group">
                            <label for="exampleInputFile">Image</label>
                            <input type="file" id="fupdImage" runat="server" name="fupdImage" />
                            <b>Note:</b>Image size approx.(80px Width x 80px Height) 
                            <br />
                            <b>Format :</b> .JPG, .JPEG, .PNG, .BMP, .GIF                                         
                        </div>
                        <div class="form-group">
                            <img id="imgImage" src="" alt="" runat="server" visible="false" />
                            <img src="<%=Config.VirtualDir %>adminpanel/images/delete.png" alt="Remove" id="imgTestimonials" title="Remove" onclick="RemoveTestimonialsImage(this)" style="cursor: pointer; display: none;" />
                            <input type="hidden" runat="server" id="hdnTestimonialsFile" name="hdnTestimonialsFile" />
                            <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                        </div>
                         <div class="form-group">
                            <label >Star Rating</label> <br />
                              <div class="col-lg-3" style="padding-left:0px;">
                            <select id="ddlRating" class="form-control" name="ddlRating" runat="server">
                                <option value="0">--Select--</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                            </select>
                                  </div>
                        </div> 
                        <div class="pull-right">
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm(this);">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='testimonials-list.aspx';">Cancel</button>
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
            $('#<%=txtClientName.ClientID %>').focus();
             if ($('#<%=hdnImage.ClientID%>').val() == '') {
                 $('#imgTestimonials').hide();
             }
             else {
                 $('#imgTestimonials').show();
             }
         });
         var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {            
             var ErrMsg = '';

             if (jQuery.trim($('#<%=txtClientName.ClientID %>').val()) == '') {
                 ErrMsg += '<br />- Client Name';
             }
             if (jQuery.trim($('#<%=txtTitle.ClientID %>').val()) == '') {
                 ErrMsg += '<br />- Title';
             }
            if (jQuery.trim($('#<%=txtDescription.ClientID %>').val()) == '') {
                ErrMsg += '<br />- Description';
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
                 //$("#frmTestimonials").attr("action", $("#frmTestimonials").attr("action") + "&SaveAndCont=" + tab);
                return true;
            }
            return true;
        }
        function RemoveTestimonialsImage(imgTestimonials) {
            if (confirm('Are you sure you want to remove image?')) {
                $(imgTestimonials).hide();
                $('#<%=imgImage.ClientID %>').hide();
                $('#<%=hdnImage.ClientID %>').val('');
            }
        }
    </script>
</asp:Content>

