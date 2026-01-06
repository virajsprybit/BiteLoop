<%@ Control Language="C#" AutoEventWireup="true" CodeFile="vendoreditor.ascx.cs" Inherits="include_vendoreditor" %>
<%@ Import Namespace="Utility" %>
 <script src="<%=Config.VirtualDir %>ckeditor/ckeditor.js" type="text/javascript"></script>
 
 <textarea id="tareaContent" cols="2" rows="5" runat="server" class="required" ></textarea>
 <script type="text/javascript">
 
     function FillTextArea() {
        var editor_data = CKEDITOR.instances.<%= tareaContent.ClientID %>.getData();
        editor_data = CKEDITOR.instances.<%= tareaContent.ClientID %>.getData();
        $('#<%= tareaContent.ClientID %>').html(editor_data);
        return editor_data;
    }
    $(document).ready(function(){
        $('#<%= tareaContent.ClientID %>').attr('rel','<%= LabelName %>');
        var CKurl = '<%=Config.VirtualDir %>ckfinder/ckfinder.html'; 
       
        var editor = CKEDITOR.replace('<%=tareaContent.ClientID %>',
        {
            fullPage: false,
            filebrowserBrowseUrl: CKurl,
            toolbar : [	
              { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline'] },
             // { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList'] },
               { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source'] },
              
            ]
        });
      
    });
</script>