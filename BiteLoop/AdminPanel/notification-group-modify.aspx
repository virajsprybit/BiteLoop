<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="notification-group-modify.aspx.cs" Inherits="AdminPanel_Notification_Group_Modify" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register Src="~/AdminPanel/includes/editor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <%--<link rel="stylesheet" type="text/css" media="all" href="css/datepicker.css" />--%>
    <style>
        .category {
            height: 260px;
            overflow-x: hidden;
            overflow-y: auto;
            display: block;
            border: 1px solid #DEDEDE;
        }

            .category td {
                padding: 10px 0px 0px 10px;
            }

        .brand-highlight, .brand-highlight td {
            background: none !important;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="js/datetimepicker/jquery.datetimepicker.css" />
    <%--<script src="js/jquery.dataTables.min.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Notification Group Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmCMS" action="notification-group-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= txtTitle.ClientID %>">Group Name</label><span class="red">*</span>
                            <input type="text" id="txtTitle" class="form-control" runat="server"  name="txtTitle" maxlength="500"/>                            
                        </div>
                        <div class="clearfix"></div>                       
                        <br />
                        <div class="col-lg-4">
                            <div>
                                <asp:Repeater ID="rptUsers" runat="server">
                                    <HeaderTemplate>
                                        <label>Users</label><span class="red">*</span>
                                        <table width="90%" class="category tblCategory" id="tblCategory">
                                            <tr>
                                                <td style="border-bottom: 1px dashed #ced1d7">
                                                    <input type="checkbox" class="chkUserALL" style="margin-bottom: 12px;" />
                                                </td>
                                                <td style="border-bottom: 1px dashed #ced1d7; width: 100%; padding-top: 5px; padding-bottom: 4px; line-height: 31px;" valign="top">
                                                    <b>ALL</b>
                                                    <input type="text" id="txtCategorySearch" onkeyup="CategoryFilter()" autocomplete="off" placeholder="Search.." class="form-control pull-right" style="width: 80%; border: 1px solid #DEDEDE !important; margin-right: 4px;">
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="20">
                                                <input type="checkbox" class="clsUsers" id="chk<%#Eval("ID") %>" value="<%#Eval("ID") %>" /></td>
                                            <td><%# "<b>" + Eval("Name") + "</b>" + " - " + Eval("Email") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <tr>
                                            <td colspan="2">
                                                <br />
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div>
                                <asp:Repeater ID="rptVendors" runat="server">
                                    <HeaderTemplate>
                                        <label>Vendors</label><span class="red">*</span>
                                        <table width="90%" class="category tblVendors" id="tblVendors">
                                            <tr>
                                                <td style="border-bottom: 1px dashed #ced1d7">
                                                    <input type="checkbox" class="chkVendorALL" style="margin-bottom: 12px;" />
                                                </td>
                                                <td style="border-bottom: 1px dashed #ced1d7; width: 100%; padding-top: 5px; padding-bottom: 4px; line-height: 31px;" valign="top">
                                                    <b>ALL</b>
                                                    <input type="text" id="txtVendorSearch" onkeyup="VendorFilter()" autocomplete="off" placeholder="Search.." class="form-control pull-right" style="width: 80%; border: 1px solid #DEDEDE !important; margin-right: 4px;">
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="20">
                                                <input type="checkbox" class="clsVendors" id="chk<%#Eval("ID") %>" value="<%#Eval("ID") %>" /></td>
                                            <td><%# "<b>" + Eval("Name") + "</b>" %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div>
                                <asp:Repeater ID="rptSalesAdmin" runat="server">
                                    <HeaderTemplate>
                                        <label>Sales Admin</label><span class="red">*</span>

                                        <table width="90%" class="category tblSalesAdmin" id="tblSalesAdmin">
                                            <tr>
                                                <td style="border-bottom: 1px dashed #ced1d7">
                                                    <input type="checkbox" class="chkDSalesAdminALL" style="margin-bottom: 12px;" />
                                                </td>
                                                <td style="border-bottom: 1px dashed #ced1d7; width: 100%; padding-top: 5px; padding-bottom: 4px; line-height: 31px;" valign="top">
                                                    <b>ALL</b>
                                                    <input type="text" id="txtSalesAdminSearch" onkeyup="SalesAdminFilter()" autocomplete="off" placeholder="Search.." class="form-control pull-right" style="width: 80%; border: 1px solid #DEDEDE !important; margin-right: 4px;">
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="20">
                                                <input type="checkbox" class="clsSalesAdmin" id="chk<%#Eval("ID") %>" value="<%#Eval("ID") %>" /></td>
                                            <td><%# "<b>" + Eval("FullName") + "</b>" %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <br />
                        <br />
                        <div class="col-lg-12">
                            <div class="pull-left">
                                <input type="hidden" id="hdnUsers" name="hdnUsers" runat="server" class="hdnUsers" />
                                <input type="hidden" id="hdnVendors" name="hdnVendors" runat="server" class="hdnVendors" />
                                <input type="hidden" id="hdnSalesAdmin" name="hdnSalesAdmin" runat="server" class="hdnSalesAdmin" />
                                <button type="submit" class="btn btn-primary btnSave" runat="server" id="btnSave" onclick="ValidateForm(this);">Save Information</button>
                                <button type="button" class="btn btn-default" onclick="window.location='notifications-group-list.aspx';">Cancel</button>
                            </div>
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <script type="text/javascript">
        //function SetDatePicker() {
        //    $('#txtDate').datepicker({
        //        changeMonth: true,
        //        changeYear: true,
        //        buttonText: "Select Date",
        //        onSelect: function (selected) {
        //        }
        //    });
        //}
        $(document).ready(function () {
            //  SetDatePicker();

           
            $('.chkUserALL').change(function () {
                if ($(this).prop('checked')) {
                    $('.clsUsers').each(function () {                        
                        if ($(this).parent().parent().attr('style') != 'display: none;') {
                            $(this).prop('checked', true);
                        }
                    });
                    //$('.clsUsers').prop('checked', true);
                }
                else {
                    $('.clsUsers').prop('checked', false);
                }
            });

            $('.chkVendorALL').change(function () {
                if ($(this).prop('checked')) {
                    $('.clsVendors').each(function () {
                        if ($(this).parent().parent().attr('style') != 'display: none;') {
                            $(this).prop('checked', true);
                        }
                    });
                    //$('.clsVendors').prop('checked', true);
                }
                else {
                    $('.clsVendors').prop('checked', false);
                }
            });

            $('.chkDSalesAdminALL').change(function () {
                if ($(this).prop('checked')) {
                    $('.clsSalesAdmin').each(function () {
                        if ($(this).parent().parent().attr('style') != 'display: none;') {
                            $(this).prop('checked', true);
                        }
                    });
                   // $('.clsSalesAdmin').prop('checked', true);
                }
                else {
                    $('.clsSalesAdmin').prop('checked', false);
                }
            });

            
            // Vendors
            var strVendors = '<%=strVendors %>'
            if (strVendors != '') {
                var strVendorsSplit = strVendors.split(',');
                for (var i = 0; i < strVendorsSplit.length ; i++) {
                    $('.clsVendors').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(strVendorsSplit[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }

            // SalesAdmin
            var strSalesAdmin = '<%=strSalesAdmin %>'
            if (strSalesAdmin != '') {
                var strSalesAdminSplit = strSalesAdmin.split(',');
                for (var i = 0; i < strSalesAdminSplit.length ; i++) {
                    $('.clsSalesAdmin').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(strSalesAdminSplit[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }

            // Users
            var strUsers = '<%=strUsers %>'
            if (strUsers != '') {
                var strUsersSplit = strUsers.split(',');
                for (var i = 0; i < strUsersSplit.length ; i++) {
                    $('.clsUsers').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(strUsersSplit[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }
        });

        var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {
            var ErrMsg = '';

            if (jQuery.trim($('#<%=txtTitle.ClientID %>').val()) == '') {
                ErrMsg += '<br />- Group Name';
            }
         
            //Users
            var Users = '';

            $('.clsUsers').each(function () {
                if ($(this).prop('checked')) {
                    Users = Users + jQuery.trim($(this).val()) + ',';
                }
            });
            if (Users.length > 0) {
                Users = Users.substring(0, Users.length - 1);

            }
            $('.hdnUsers').val(Users);
            //Users


            //Vendors
            var Vendors = '';

            $('.clsVendors').each(function () {
                if ($(this).prop('checked')) {
                    Vendors = Vendors + jQuery.trim($(this).val()) + ',';
                }
            });
            if (Vendors.length > 0) {
                Vendors = Vendors.substring(0, Vendors.length - 1);
            }
            $('.hdnVendors').val(Vendors);
            //Vendors



            //SalesAdmin
            var SalesAdmin = '';

            $('.clsSalesAdmin').each(function () {
                if ($(this).prop('checked')) {
                    SalesAdmin = SalesAdmin + jQuery.trim($(this).val()) + ',';
                }
            });
            if (SalesAdmin.length > 0) {
                SalesAdmin = SalesAdmin.substring(0, SalesAdmin.length - 1);
            }
            $('.hdnSalesAdmin').val(SalesAdmin);
            //SalesAdmin


            if (SalesAdmin == '' && Vendors == '' && Users == '') {
                ErrMsg += '<br />- Users / Vendors / SalesAdmin';
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
    
    <script>
        
        function CategoryFilter() {
            // Declare variables 
            var input, filter, table, tr, td, i;
            input = document.getElementById("txtCategorySearch");
            //filter = "<B>" + input.value.toUpperCase();
			filter = input.value.toUpperCase();
            table = document.getElementById("tblCategory");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[1];
                if (td) {
					if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    //if (td.innerHTML.toUpperCase().startsWith(filter)) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                    tr[0].style.display = "";
                }
            }
        }

        function VendorFilter() {
            // Declare variables 
            var input, filter, table, tr, td, i;
            input = document.getElementById("txtVendorSearch");
           //filter = "<B>" + input.value.toUpperCase();
			filter = input.value.toUpperCase();
            table = document.getElementById("tblVendors");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[1];
                if (td) {
					if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
//                    if (td.innerHTML.toUpperCase().startsWith(filter)) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                    tr[0].style.display = "";
                }
            }
        }
        function SalesAdminFilter() {
            // Declare variables 
            var input, filter, table, tr, td, i;
            input = document.getElementById("txtSalesAdminSearch");
           //filter = "<B>" + input.value.toUpperCase();
			filter = input.value.toUpperCase();
            table = document.getElementById("tblSalesAdmin");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[1];
                if (td) {
					if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    //if (td.innerHTML.toUpperCase().startsWith(filter)) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                    tr[0].style.display = "";
                }
            }
        }
        </script>
</asp:Content>

