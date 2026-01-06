<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="vendor-holiday-List.aspx.cs" Inherits="AdminPanel_Vendor_holiday_List" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link rel="stylesheet" href="css/select2.min.css" />
    <script src="js/select2.min.js"></script>
    <style>
        .publickholidays .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

        .switch input {
            opacity: 0;
            width: 0;
            height: 0;
        }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #41d641;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }


        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .brand-highlight, .brand-highlight td {
            background-color: transparent !important;
        }

        .table-striped td {
            vertical-align: middle !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Vendor Holiday</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel well-lg" style="min-height:300px">
                <div width="100%" style="text-align: center">
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="center" style="background-color: #f3f3f5; padding-top: 10px;">
                                <table width="60%" cellspacing="0" cellpadding="5">
                                    <tr>
                                        
                                        <td style="background-color: #f3f3f5">
                                            <label>State:</label>
                                        </td>
                                        <td style="background-color: #f3f3f5">
                                            <select id="ddlState" name="ddlState" runat="server" class="ddlState form-control" style="border: 1px solid #d0c9c9 !important" onchange="BindBusinesses()"></select>                                            
                                        </td>
                                        <td style="background-color: #f3f3f5">
                                            <label>Vendor:</label>
                                        </td>
                                        <td style="background-color: #f3f3f5" class="divBusinesses">
                                            <div id="divBusinesses" runat="server">
                                            <select id="ddlVendor" name="ddlVendor" runat="server" class="ddlVendor form-control" style="border: 1px solid #d0c9c9 !important; width:290px"></select>
                                            <input type="hidden" class="hdnVendorID" name="hdnVendorID" id="hdnVendorID" value="0" />
                                                </div>
                                        </td>
                                        <td>
                                            <input type="button" class="btn btn-info" value="View Holiday" onclick="ViewHoliday()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="panel-body well-lg divVendorHolidays" style="display: none">
                    <div id="divVendorHolidays" runat="server">
                        <div class="col-lg-6">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i><span id="spnHolidayTitle">Public Holidays</span></h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg publickholidays">



                                    <asp:Repeater ID="rptPublicHolidays" runat="server">
                                        <HeaderTemplate>
                                            <table width="100%" style="background-color: transparent" class="table table-striped table-hover table-bordered" cellspacing="0" cellpadding="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <h5><%#Eval("Title") %></h5>
                                                </td>
                                                <td width="120" align="center">
                                                    <h5 class="spnStatus"><%#Convert.ToString(Eval("OnOff")) == "1" ? "Open" : "Closed" %></h5>
                                                </td>
                                                <td width="120" align="center">
                                                    <input type="hidden" value="<%#Eval("StateHolidayID") %>" class="hdnStateHoliday" />
                                                    <label class="switch">
                                                        <input type="checkbox" rel="<%#Eval("StateHolidayID") %>" class="pubicHoliday" <%#Convert.ToString(Eval("OnOff")) == "1" ? "checked" : "" %> onclick="ChangeStatus(this)" />
                                                        <span class="slider round"></span>
                                                    </label>
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                        <FooterTemplate></table></FooterTemplate>
                                    </asp:Repeater>


                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Custom Holidays</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="alert hide" id="divMsg" runat="server"></div>
                                    <div class="col-xs-12">
                                        <label>Title&nbsp;&nbsp;&nbsp;</label>
                                        <input type="text" class="form-control txtTitle" id="txtTitle" name="txtTitle" style="border: 1px solid #d0c9c9 !important" maxlength="100" />
                                    </div>
                                    <br />
                                    <div class="col-xs-4">
                                        <div class="form-group">
                                            <label>Start Date&nbsp;&nbsp;&nbsp;</label>
                                            <input type="text" class="form-control txtStartDate" id="txtStartDate" name="txtStartDate" style="border: 1px solid #d0c9c9 !important" data-date-format="dd/M/yyyy" autocomplete="off" readonly="true" />
                                        </div>
                                    </div>
                                    <div class="col-xs-4">
                                        <div class="form-group">
                                            <label>End Date&nbsp;&nbsp;&nbsp;</label>
                                            <input type="text" class="form-control txtEndDate" id="txtEndDate" name="txtEndDate" style="border: 1px solid #d0c9c9 !important" data-date-format="dd/M/yyyy" autocomplete="off" readonly="true" />
                                        </div>
                                    </div>
                                    <div class="col-xs-4">
                                        <div class="form-group">
                                            <label style="display: block">&nbsp;</label>
                                            <input type="hidden" class="hdnCustomID" value="0" />
                                            <button type="button" class="btn btn-primary" onclick="SaveCustomHoliday();">Save</button>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-xs-12 divCustomHolidays">
                                        <div id="divCustomHolidays" runat="server">
                                            <asp:Repeater ID="rptCustomHolidays" runat="server">
                                                <HeaderTemplate>
                                                    <table width="100%" style="background-color: transparent" class="table table-striped table-hover table-bordered" cellspacing="0" cellpadding="0">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <label><%#Eval("Title") %></label>
                                                        </td>
                                                        <td width="200">
                                                            <%# Convert.ToDateTime(Eval("HolidayFromDate")).ToString("dd/MMM/yyyy") %> - <%# Convert.ToDateTime(Eval("HolidayToDate")).ToString("dd/MMM/yyyy") %>
                                                        </td>
                                                        <td width="50" align="center">
                                                            <a href="javascript:;" onclick="EditCustomHoliday('<%#Eval("ID") %>')" title="Edit"><i class="icon-pencil"></i></a>
                                                            <input type="hidden" id="hdnHolidayID<%#Eval("ID") %>" name="hdnHolidayID<%#Eval("ID") %>" value="<%#Eval("ID") %>" />
                                                            <input type="hidden" id="FromDate<%#Eval("ID") %>" name="FromDate<%#Eval("ID") %>" value="<%# Convert.ToDateTime(Eval("HolidayFromDate")).ToString("dd/MMM/yyyy") %>" />
                                                            <input type="hidden" id="ToDate<%#Eval("ID") %>" name="ToDate<%#Eval("ID") %>" value="<%#Convert.ToDateTime(Eval("HolidayToDate")).ToString("dd/MMM/yyyy")  %>" />
                                                            <input type="hidden" id="Title<%#Eval("ID") %>" name="Title<%#Eval("ID") %>" value="<%#Eval("Title") %>" />
                                                        </td>
                                                        <td width="50" align="center">
                                                            <a href="javascript:;" onclick="DeleteCustomHoliday('<%#Eval("ID") %>')" title='Delete'><i class="icon-trash"></i></a>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <FooterTemplate></table></FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <link href="js/BootstrapDatepicker/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="js/BootstrapDatepicker/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
        });
        $('#<%=ddlState.ClientID%>').select2();
        $('#<%=ddlVendor.ClientID%>').select2();
        function ViewHoliday() {
            var BusinessID = jQuery.trim($('.ddlVendor').val());
            $('.hdnVendorID').val(BusinessID);
            if (BusinessID != 0) {

                $.ajax(
                  {
                      url: 'vendor-holiday-List.aspx',
                      data: { 'BindHolidays': 'Y', 'BusinessID': BusinessID },
                      type: 'POST',
                      success: function (resp) {

                          $('.divVendorHolidays').html(resp);
                          $('.divVendorHolidays').show();

                          $('#spnHolidayTitle').html("Public Holidays - " + $('#<%=ddlState.ClientID%>').val());

                          $(".txtStartDate").datepicker({
                              format: 'dd/M/yyyy',
                          }).on('changeDate', function (e) {
                              $(this).datepicker('hide');
                          });


                          $(".txtEndDate").datepicker({
                              format: 'dd/M/yyyy',
                          }).on('changeDate', function (e) {
                              $(this).datepicker('hide');
                          });
                      }
                  });
            }
            else {
                alert('Please select Vendor');
            }

        }

        function ChangeStatus(ele) {
            var OnOff = 0;
            var StateHolidayID = jQuery.trim($(ele).closest('tr').find('.hdnStateHoliday').val());
            var BusinessID = jQuery.trim($('.hdnVendorID').val());

            if ($(ele).prop('checked')) {
                $(ele).closest('tr').find('.spnStatus').html('Open');
                OnOff = 1;
            }
            else {
                $(ele).closest('tr').find('.spnStatus').html('Closed');
                OnOff = 0;
            }
            $.ajax(
               {
                   url: 'vendor-holiday-List.aspx',
                   data: { 'SavePublicHoliday': 'Y', 'OnOff': OnOff, 'StateHolidayID': StateHolidayID, 'BusinessID': BusinessID },
                   type: 'POST',
                   success: function (resp) {
                   }
               });


        }
        function DeleteCustomHoliday(ID) {
            var BusinessID = jQuery.trim($('.hdnVendorID').val());
            if (confirm("Are you sure you want to delete selected Holiday?")) {
                $.ajax(
            {
                url: 'vendor-holiday-List.aspx',
                data: { 'DeleteCustomHoliday': 'Y', 'BusinessID': BusinessID, 'ID': ID },
                type: 'POST',
                success: function (resp) {
                    $('.hdnCustomID').val('0');
                    $('.txtTitle').val('');
                    $('.txtStartDate').val('');
                    $('.txtEndDate').val('');
                    $('.divCustomHolidays').html(resp);
                }
            });

            }
        }
        function EditCustomHoliday(ID) {
            $('.hdnCustomID').val($('#hdnHolidayID' + ID).val());
            $(".txtStartDate").datepicker("setDate", $('#FromDate' + ID).val());
            $(".txtEndDate").datepicker("setDate", $('#ToDate' + ID).val());
            $('.txtTitle').val($('#Title' + ID).val());
            $('.txtTitle').focus();

        }
        function SaveCustomHoliday() {
            var divMsg = "<%= divMsg.ClientID %>";
            var BusinessID = jQuery.trim($('.hdnVendorID').val());

            if (BusinessID != "0") {

                var ErrorMsg = '';

                if (jQuery.trim($('.txtTitle').val()) == '') {
                    ErrorMsg += ' - Title is required.' + '<br/>';
                }
                if (jQuery.trim($('.txtStartDate').val()) == '') {
                    ErrorMsg += ' - Start Date is required.' + '<br/>';
                }
                if (jQuery.trim($('.txtEndDate').val()) == '') {
                    ErrorMsg += ' - End Date is required.' + '<br/>';
                }

                if (jQuery.trim($('.txtStartDate').val()) != '' && jQuery.trim($('.txtEndDate').val()) != '') {
                    var startDate = new Date(jQuery.trim($('.txtStartDate').val()));
                    var endDate = new Date(jQuery.trim($('.txtEndDate').val()));

                    if ((Date.parse(startDate) > Date.parse(endDate))) {
                        ErrorMsg += ' - Start Date must be less than End Date.'; + '<br/>';

                    }
                }
                if (ErrorMsg != '') {
                    DisplMsg(divMsg, ErrorMsg, 'alert-message error');
                }
                else {

                    $.ajax(
                       {
                           url: 'vendor-holiday-List.aspx',
                           data: { 'SaveCustomHoliday': 'Y', 'BusinessID': BusinessID, 'Title': jQuery.trim($('.txtTitle').val()), 'FromDate': jQuery.trim($(".txtStartDate").val()), 'ToDate': jQuery.trim($(".txtEndDate").val()), 'ID': jQuery.trim($('.hdnCustomID').val()) },
                           type: 'POST',
                           success: function (resp) {
                               $('.hdnCustomID').val('0');
                               $('.txtTitle').val('');
                               $('.txtStartDate').val('');
                               $('.txtEndDate').val('');
                               $('.divCustomHolidays').html(resp);
                           }
                       });
                }
            }
        }
        function BindBusinesses() {
            $('.divVendorHolidays').hide();
            $.ajax(
                    {
                        url: 'vendor-holiday-List.aspx',
                        data: { 'BindBusiness': 'Y', 'state': $('#<%=ddlState.ClientID%>').val()},
                        type: 'POST',
                        success: function (resp) {
                            $('.divBusinesses').html(resp);
                            $('#<%=ddlVendor.ClientID%>').select2();

                        }
                    });
        }
    </script>
</asp:Content>

