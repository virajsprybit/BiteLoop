<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="state-holiday.aspx.cs" Inherits="AdminPanel_State_holiday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link href='calendar/jquery-ui-1.9.2.custom.css' rel='stylesheet' />
    <link href='calendar/fullcalendar.css' rel='stylesheet' />

    <link href='calendar/fullcalendar.print.css' rel='stylesheet' media='print' />
    <script src='calendar/moment.min.js' type="text/javascript"></script>
    <script src='calendar/jquery.min.js' type="text/javascript"></script>
    <script src='calendar/fullcalendar.min.js' type="text/javascript"></script>
    <style>
        #calendar {
            max-width: 1280px;
            margin: 0 auto;
            border: 1px solid #e5e5e5;
            padding: 10px;
        }

        .btnevent {
            background: #8a8a8a;
            color: white;
            font-weight: bold;
            border: none;
            padding: 8px;
            cursor: pointer;
        }

        .btneventselected {
            border: 2px solid red;
        }

        .btnevent:hover {
            background: #9b9a9a;
        }

        .holiday {
            background-color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">State Holiday</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div width="100%" style="text-align: center">
                         <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="center" style="background-color:#f3f3f5;padding-top:10px;">
                                    <table width="30%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="background-color:#f3f3f5"><label>State: </label>
                                            </td>
                                            <td style="background-color:#f3f3f5">
                                                <select id="ddlState" name="ddlState" runat="server" onchange="StateChange()" class="ddlState form-control" style="border:1px solid #d0c9c9 !important"></select>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </div>
                <div class="panel-body well-lg">
                    
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="dataentry">
                        <tr style="display: none">
                            <td align="center">
                                <input type="button" id="btnSaturdaySundayLeave" name="btnSaturdaySundayLeave" value="Add Saturday/Sunday Holiday" class="btnevent" onclick="SetLeaveTitle('1', this)" />
                                &nbsp;&nbsp;&nbsp;<input type="button" id="btnOtherLeave" name="btnOtherLeave" value="Add Other Holiday" onclick="SetLeaveTitle('0', this)" class="btnevent btneventselected" />
                                <input type="text" id="txtLeaveTitle" name="txtLeaveTitle" readonly="readonly" style="display: none" /><br />
                                <input type="hidden" id="hdnState" name="hdnState" value="0" class="hdnState" />
                                <input type="hidden" id="hdnSelectedYear" name="hdnSelectedYear" value="<%=CurrentYear%>" class="hdnSelectedYear" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <div id='calendar'></div>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.hdnState').val(jQuery.trim($('.ddlState').val()));           
            GetEvents();
        });
        function GetEvents() {
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: ''
                },
                businessHours: true, // display business hours
                editable: false,
                defaultDate: '<%=CurrentYear%>-01-01',
                defaultView: 'year',
                yearColumns: 2,
                theme: true,
                eventRender: function (event, date, element, monthView) {
                    $('.fc-day[data-date="' + moment(event.start).format('YYYY-MM-DD') + '"]').css('background', 'rgb(136, 226, 136)');
                },
                eventAfterAllRender: function (view) {
                    $('.fc-title').each(function () {
                        $(this).attr('title', $(this).text());
                    });
                },
                dayClick: function (date, allDay, jsEvent, view) {                
                    var title = jQuery.trim($('#txtLeaveTitle').val());
                    if ($(this).css('background-color') == 'rgb(136, 226, 136)') {
                        if (confirm('Are you sure you want to remove selected Holiday?')) {
                            title = 'Deleted';
                        }
                    }
                    else {
                        if (title == '')
                            title = prompt('Enter Holiday Title:');
                    }

                    
                    if (jQuery.trim(title) != '') {

                        $.ajax({
                            url: 'state-holiday.aspx',
                            data: 'savestateholiday=y&title=' + title + '&stateid=' + jQuery.trim($('.hdnState').val()) + '&start=' + moment(date._d).format('DD/MMM/YYYY HH:mm'),
                            type: "POST",
                            success: function (json) {
                                $('#calendar').fullCalendar('refetchEvents');
                            }
                        });

                        if (jQuery.trim($(this).css('background-color')) == jQuery.trim('rgb(136, 226, 136)')) {
                            $(this).css('background-color', '');
                        }
                        else {
                            $(this).css('background-color', 'rgb(136, 226, 136)');
                        }
                    }

                },
                events: 'state-holiday.aspx?getEvents=y&stateid=' + jQuery.trim($('.hdnState').val()),
                viewRender: function (view, element) {
                    var view = $('#calendar').fullCalendar('getView');
                    var d = new Date();
                    var CurrentYear = d.getFullYear();
                    var prevButton = $('.fc-prev-button');
                },
            });

            $('.fc-next-button').click(function () {                
                $('.hdnSelectedYear').val($('.fc-toolbar h2').text());
            });

        }
        function StateChange() {
            $('.hdnState').val(jQuery.trim($('.ddlState').val()));
            //GetEvents();
       
            $('#calendar').fullCalendar('destroy');
            GetEvents();
            var SelectedDate = $('.hdnSelectedYear').val() + '-01-01';
            $('#calendar').fullCalendar('gotoDate', SelectedDate);
            

        }
        function SetLeaveTitle(type, ele) {
            //$('.btnevent').removeClass('btneventselected');
            //$(ele).addClass('btneventselected');
            //if (type == 1)
            //    $('#txtLeaveTitle').val('Holiday');
            //else
            //    $('#txtLeaveTitle').val('Holiday');
        }
    </script>
</asp:Content>

