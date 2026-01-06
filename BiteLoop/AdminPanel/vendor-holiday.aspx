<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="vendor-holiday.aspx.cs" Inherits="AdminPanel_vendor_holiday" %>

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
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Vendor Holiday</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <h3><%=BusinessName %></h3>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="dataentry">                        
                        <tr style="display:none">
                            <td align="center">
                                <input type="button" id="btnSaturdaySundayLeave" name="btnSaturdaySundayLeave" value="Add Saturday/Sunday Holiday" class="btnevent" onclick="SetLeaveTitle('1', this)" />
                                &nbsp;&nbsp;&nbsp;<input type="button" id="btnOtherLeave" name="btnOtherLeave" value="Add Other Holiday" onclick="SetLeaveTitle('0', this)" class="btnevent btneventselected" />
                                <input type="text" id="txtLeaveTitle" name="txtLeaveTitle" readonly="readonly" style="display: none" /><br />
                                <input type="hidden" id="hdnBusinessID" name="hdnBusinessID" class="hdnBusinessID" value="0" runat="server" />
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
                        title = 'Holiday';
                    }
                    else {
                        if (title == '')
                            title = prompt('Enter Holiday Title:');                        
                    }
                    if (jQuery.trim(title) != '') {
                        $.ajax({
                            url: 'vendor-holiday.aspx',
                            data: 'saveleave=y&title=' + title + '&businessid=' + jQuery.trim($('.hdnBusinessID').val()) + '&start=' + moment(date._d).format('DD/MMM/YYYY HH:mm'),
                            type: "POST",
                            success: function (json) {
                                $('#calendar').fullCalendar('refetchEvents');                                
                            }
                        });
                        if (jQuery.trim($(this).css('background-color')) == jQuery.trim('rgb(136, 226, 136)')) {
                            $(this).css('background-color', '');
                        }                        else {
                            $(this).css('background-color', 'rgb(136, 226, 136)');
                        }
                    }

                },
                events: 'vendor-holiday.aspx?getEvents=y&businessid=' + jQuery.trim($('.hdnBusinessID').val()),
                viewRender: function (view, element) {
                    var view = $('#calendar').fullCalendar('getView');
                    var d = new Date();
                    var CurrentYear = d.getFullYear();
                    var prevButton = $('.fc-prev-button');                   
                },
            });




        });
        function SetLeaveTitle(type, ele) {
            $('.btnevent').removeClass('btneventselected');
            $(ele).addClass('btneventselected');
            if (type == 1)
                $('#txtLeaveTitle').val('Holiday');
            else
                $('#txtLeaveTitle').val('Holiday');
        }
    </script>
</asp:Content>

