<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="PromotionalCode.aspx.cs" Inherits="PromotionalCode" %>

<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        #BusinessDatatable td, th {
            text-align: left;
        }

        #BusinessDatatable th:nth-child(1) {
            text-align: center !important;
            width: 30px !important;
            padding-right: 0px !important;
        }

        #BusinessDatatable td:nth-child(1) {
            text-align: center !important;
            width: 30px !important;
        }

        #BusinessDatatable th:nth-child(2) {
            text-align: left;
            padding-left: 0px;
        }

        #BusinessDatatable td:nth-child(2) {
            text-align: left;
            padding-left: 0px;
        }

        /*#BusinessDatatable tbody {
            height:400px;
            overflow:auto;
            overflow-x:hidden;
        }*/
        #UserDatatable td, th {
            text-align: left;
        }

        #UserDatatable th:nth-child(1) {
            text-align: center !important;
            width: 30px !important;
            padding-right: 0px !important;
        }

        #UserDatatable td:nth-child(1) {
            text-align: center !important;
            width: 30px !important;
        }

        #UserDatatable th:nth-child(2) {
            text-align: left;
            padding-left: 0px;
        }

        #UserDatatable td:nth-child(2) {
            text-align: left;
            padding-left: 0px;
        }

        .dataTables_wrapper.no-footer .dataTables_scrollBody {
            border-bottom: none !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            border-bottom: 1px solid #b5b3b3 !important;
        }

        #frmPromocode .form-control {
            border: 1px solid #e0e0e0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Promotional Code</a></li>
            </ol>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-61">
            <div class="panel1">
                <div class="panel-body1 well-lg1">
                    <div class="alert-message hide" id="divMsg" runat="server"></div>
                    <form id="frmPromocode" action="PromotionalCode.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <div class="pull-left">
                                            <h4><i class="icon-th-large"></i>Coupon Settings</h4>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="panel-body well-lg">

                                        <div class="form-group">
                                            <label for="<%=tbxCouponCode.ClientID %>">Coupon Code</label><span class="red">*</span>
                                            <input type="text" class="form-control" id="tbxCouponCode" runat="server" name="tbxCouponCode" maxlength="50" autocomplete="off" />
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=tbxMinimumAmount.ClientID %>">Minimum Order Amount</label><span class="red">*</span>
                                            <input type="text" class="form-control discount" id="tbxMinimumAmount" runat="server" name="tbxMinimumAmount" autocomplete="off" maxlength="50" style="width: 200px" />
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=ddlDiscountType.ClientID %>">Discount Type</label><span class="red">*</span>
                                            <select id="ddlDiscountType" name="ddlDiscountType" class="form-control" runat="server" style="width: 200px" onchange="SetDiscountLabel()">
                                                <option value="1">Flat</option>
                                                <option value="2">Percentage</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=tbxDiscountAmount.ClientID %>" class="lbldiscountamount">Discount Amount</label><span class="red">*</span>
                                            <input type="text" class="form-control discount" id="tbxDiscountAmount" autocomplete="off" runat="server" name="tbxDiscountAmount" maxlength="50" style="width: 200px" />
                                        </div>

                                        <div class="form-group">
                                            <label for="<%=tbxCouponStartTime.ClientID%>">
                                                Coupon Start Date<span class="red">*</span></label>
                                            <input id="tbxCouponStartTime" runat="server" name="tbxCouponStartTime" type="text" autocomplete="off" size="30" class="form-control tbxCouponStartTime" data-date-format="dd/M/yyyy" style="width: 200px" />
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=tbxCouponEndTime.ClientID%>">
                                                Coupon End Date<span class="red">*</span></label>
                                            <input id="tbxCouponEndTime" runat="server" name="tbxCouponEndTime" type="text" autocomplete="off" size="30" class="form-control tbxCouponEndTime" data-date-format="dd/M/yyyy" style="width: 200px" />
                                        </div>

                                        <div class="form-group">

                                            <label>Use</label><span class="red">*</span>
                                            <select id="ddlSingleUser" name="ddlSingleUse" runat="server" class="form-control" style="width: 200px">
                                                <option value="1">Single Time Use</option>
                                                <option value="0">Multiple Time Use</option>
                                            </select>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-4 hide">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <div class="pull-left">
                                            <h4><i class="icon-th-large"></i>Users</h4>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="panel-body well-lg">
                                        <table id="UserDatatable" class="display select" cellspacing="0" width="100%">
                                            <thead>
                                                <tr>
                                                    <th style="padding-right: 0px;">
                                                        <input type="checkbox" name="select_all" value="1" id="UserDatatable-select-all" />

                                                    </th>
                                                    <th style="padding-left: 0px;">Name</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <div class="pull-left">
                                            <h4><i class="icon-th-large"></i>Business</h4>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="panel-body well-lg">

                                        <div class="form-group1" style="display: inline; position: absolute; z-index: 999999">
                                            <label for="<%=ddlState.ClientID %>">State</label><span class="red">*</span>
                                            <select id="ddlState" name="ddlState" class="form-control ddlState" runat="server" style="width: 200px; display: inline-block;">
                                            </select>
                                        </div>

                                        <table id="BusinessDatatable" class="display select" cellspacing="0" width="100%">
                                            <thead>
                                                <tr>
                                                    <th style="padding-right: 0px;">
                                                        <input type="checkbox" name="select_all" value="1" id="BusinessDatatable-select-all" />

                                                    </th>
                                                    <th style="padding-left: 0px;">Name</th>
                                                    <th style="padding-left: 0px;">State</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel-body well-lg">
                                    <div class="pull-right">
                                        <div class="col-lg-">
                                            <input type="hidden" id="hdnBusiness" name="hdnBusiness" runat="server" value="" class="hdnBusiness" />
                                            <input type="hidden" id="hdnUsers" name="hdnUsers" runat="server" value="" class="hdnUsers" />
                                            <input class="btn btn-primary" runat="server" id="Submit1" type="submit" value="Save Information" name="btnSave" onclick="return ValidateForm()" />
                                            <button type="button" class="btn btn-default" onclick="window.location='promotionalcodelist.aspx';">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                    </form>



                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <link href="js/datepicker1/css/datepicker.css" rel="stylesheet" />
    <script src="js/datepicker1/js/bootstrap-datepicker.js"></script>

    <link href="js/datatable/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/datatable/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        var divMsg = '<%= divMsg.ClientID %>';
        $(document).ready(function () {
            SetDiscountLabel();
            $('.tbxCouponStartTime').datepicker().on('changeDate', function () {
                $(this).datepicker('hide');
                //$('.tbxCouponEndTime').datepicker('setStartDate', new Date($(this).val()));

                //if ($('.tbxCouponEndTime').val() != '') {

                //    if (new Date($('.tbxCouponEndTime').val()) < new Date($('.tbxCouponStartTime').val())) {
                //        $('.tbxCouponEndTime').val('');
                //    }
                //}

            });

            $('.tbxCouponEndTime').datepicker().on('changeDate', function () {
                $(this).datepicker('hide');
                //$('.tbxCouponStartTime').datepicker('setEndDate', new Date($(this).val()));
            });


            $(".discount").keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    return;
                }

                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            //var table;
            BindBusinessList();

            $('.ddlState').on('change', function () {
                //var sid = $('.ddlState').val();
                //table.ajax.url("<%=Config.WebSiteUrl%>webservice/CommonAPI.asmx/BindBusinessForPromocodeWithState?StateID=" + sid).load();
                BindBusinessList();
            });

            // Business -----------------------------------------------------

           <%-- // Users -----------------------------------------------------
            var table1 = $('#UserDatatable').DataTable({
                'ajax': {
                    'url': "<%=Config.WebSiteUrl%>webservice/CommonAPI.asmx/BindUsersForPromocode",
                },
                "scrollY": '400',
                "paging": false,
                "info": false,
                'columnDefs': [
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta) {

                            return '<input type="checkbox" class="chkUsers" name="" value="' + full.ID + '">';
                        }
                    },
                        {
                            'targets': 1,
                            'searchable': true,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, full, meta) {
                                return full.Name;
                            }
                        }
                ],
                'order': [[1, 'asc']],
                "initComplete": function (settings, json) {

                    //Users

                    var Users = $('#<%=hdnUsers.ClientID%>').val();
                    if (Users != '') {
                        var UsersSplit = Users.split(',');

                        $('.chkUsers').each(function () {

                            for (var i = 0; i < UsersSplit.length ; i++) {
                                if (jQuery.trim($(this).val()) == UsersSplit[i]) {
                                    $(this).prop('checked', true);
                                }
                            }
                        });
                    }
                    //Users

                }
            });--%>

            $('#UserDatatable-select-all').on('click', function () {
                var rows = table1.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });


            $('#UserDatatable tbody').on('change', 'input[type="checkbox"]', function () {

                if (!this.checked) {
                    var el = $('#UserDatatable-select-all').get(0);
                    if (el && el.checked && ('indeterminate' in el)) {
                        el.indeterminate = true;
                    }
                }
            });
            // User -----------------------------------------------------

        });
        function BindBusinessList() {
            var table = $('#BusinessDatatable').DataTable();
            table.destroy();
            // Business -----------------------------------------------------
            var _sid = $('.ddlState').val();
            table = $('#BusinessDatatable').DataTable({
                'ajax': {
                    'url': "<%=Config.WebSiteUrl%>webservice/CommonAPI.asmx/BindBusinessForPromocodeWithState?StateID=" + _sid,
                },
                "scrollY": '466',
                "paging": false,
                "info": false,
                'columnDefs': [
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta) {

                            return '<input type="checkbox" class="chkBusiness" name="" value="' + full.ID + '">';
                        }
                    },
                    {
                        'targets': 1,
                        'searchable': true,
                        'orderable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta) {
                            return full.Name;
                        }
                    },
                    {
                        'targets': 2,
                        'searchable': true,
                        'orderable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta) {
                            return full.StateCode;
                        }
                    }
                ],
                'order': [[1, 'asc']],
                "initComplete": function (settings, json) {

                    //Business

                    var Business = $('#<%=hdnBusiness.ClientID%>').val();
                   if (Business != '') {
                       var BusinessSplit = Business.split(',');

                       $('.chkBusiness').each(function () {

                           for (var i = 0; i < BusinessSplit.length; i++) {
                               if (jQuery.trim($(this).val()) == BusinessSplit[i]) {
                                   $(this).prop('checked', true);
                               }
                           }
                       });
                   }
                   //Business

               }
            });




               $('#BusinessDatatable-select-all').on('click', function () {
                   var rows = table.rows({ 'search': 'applied' }).nodes();
                   $('input[type="checkbox"]', rows).prop('checked', this.checked);
               });


               $('#BusinessDatatable tbody').on('change', 'input[type="checkbox"]', function () {

                   if (!this.checked) {
                       var el = $('#BusinessDatatable-select-all').get(0);
                       if (el && el.checked && ('indeterminate' in el)) {
                           el.indeterminate = true;
                       }
                   }
               });
               $('.dataTables_empty').html('No records found..');
           }
           function ValidateForm() {


               var ErrMsg = '';
               if (jQuery.trim($('#<%=tbxCouponCode.ClientID %>').val()) == '') {
                    ErrMsg += '<br/> - Coupon Code';
                }
                if (jQuery.trim($('#<%=tbxMinimumAmount.ClientID %>').val()) == '') {
                    ErrMsg += '<br/> - Minimum Order Amount';
                }

                if (jQuery.trim($('#<%=tbxDiscountAmount.ClientID %>').val()) == '') {
                    if ($('#<%=ddlDiscountType.ClientID%>').val() == '1') {
                    ErrMsg = ErrMsg + '<br> - Discount Amount';
                }
                else {
                    ErrMsg = ErrMsg + '<br> - Discount Percentage';
                }
            }
            if (jQuery.trim($('#<%=tbxCouponStartTime.ClientID %>').val()) == '') {
                    ErrMsg = ErrMsg + '<br> - Coupon Start Date';
                }
                if (jQuery.trim($('#<%=tbxCouponEndTime.ClientID %>').val()) == '') {
                    ErrMsg = ErrMsg + '<br> - Coupon End Date';
                }

               if (jQuery.trim($('#<%=tbxCouponStartTime.ClientID %>').val()) != '' && jQuery.trim($('#<%=tbxCouponEndTime.ClientID %>').val()) != '') {
                   if (new Date($('.tbxCouponEndTime').val()) < new Date($('.tbxCouponStartTime').val())) {
                       ErrMsg = ErrMsg + '<br> - Coupon Start Date must be less than End Date';
                   }
               }


                if (ErrMsg.length != 0) {
                    ScrollTop();
                    var HeaderText = 'Please correct the following error(s):';
                    DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                    return false;
                }
                else {

                    var SelectedBusiness = [];
                    $('[class="chkBusiness"]:checked').each(function (i, e) {
                        SelectedBusiness.push(e.value);
                    });

                    SelectedBusiness = SelectedBusiness.join(',');

                    var SelectedUsers = [];
                    //$('[class="chkUsers"]:checked').each(function (i, e) {
                    //    SelectedUsers.push(e.value);
                    //});

                    SelectedUsers = SelectedUsers.join(',');
                    $('.hdnBusiness').val(SelectedBusiness);
                    // $('.hdnUsers').val(SelectedUsers);

                    $('#<%=divMsg.ClientID %>').hide(); return true;
            }
                //return true;
        }

        function SetDiscountLabel() {
            if ($('#<%=ddlDiscountType.ClientID%>').val() == '1') {
                    $('.lbldiscountamount').html('Discount Amount');
                }
                else {
                    $('.lbldiscountamount').html('Discount Percentage');
                }
            }
    </script>
</asp:Content>
