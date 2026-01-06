<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="AdminPanel_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        .panel-body h5 {
            margin-top: 10px !important;
            font-size: 15px !important;
        }

        .boxessquare .panel-body {
            height: 118px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <!--Page Content-->
    <!-- Stats & Updates Row 1-->
    <div class="row no-padding boxessquare">
        <div class="col-md-12">
            <h3><%= SelectedState == "" ? "All" : SelectedState %></h3>
        </div>

        <%if (1 == 1)
          { %>
        <!--row 1, col 1-->
        <div class="col-md-4 col-sm-6">

            <div class="panel panel-body-only panel-secondary-solid">
                <div class="panel-body">
                    <div class="pull-left">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-copy"></i></div>
                        </div>
                    </div>
                    <span class="big-text"><%=SoldQty %></span>
                    <div class="stat text-transparent">
                        <h5>Total Sold Items (Today)</h5>
                    </div>
                </div>
            </div>
        </div>
        <!--/ row 1, col 1-->
        <!--row 1, col 2-->
        <div class="col-md-4 col-sm-6">
            <div class="panel panel-body-only panel-info-solid">
                <div class="panel-body">
                    <div class="pull-left" style="height: 67px;">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-money"></i></div>
                        </div>
                    </div>
                    <span id="new-orders" class="big-text">$<%=TotalAmountBeforeBMH %></span><div class="stat text-transparent">
                        <h5>Total Amount after Commission (Today)</h5>
                    </div>
                </div>
            </div>
        </div>
        <!--/ row 1, col 2-->
        <div class="col-md-4 col-sm-6">
            <div class="panel panel-body-only panel-warning-solid">
                <div class="panel-body">
                    <div class="pull-left" style="height: 67px;">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-money"></i></div>
                        </div>
                    </div>
                    <span id="Span3" class="big-text">$<%=BMH %></span><div class="stat text-transparent">
                        <h5>Commission Revenue (Today)</h5>
                    </div>
                </div>
            </div>
        </div>

        <!--row 1, col 4-->


        <!--/ row 1, col 4-->
        <!--row 1, col 3-->
        <div class="col-md-4 col-sm-6">
            <div class="panel panel-body-only panel-primary-blue">
                <div class="panel-body">
                    <div class="pull-left" style="height: 67px;">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-money"></i></div>
                        </div>
                    </div>
                    <span class="big-text">$<%=GrandTotal %></span><div class="stat text-transparent">
                        <h5>Gross Sales at Store Level (Today)</h5>
                    </div>
                </div>
            </div>
        </div>

        <!--/ row 1, col 3-->

        <div class="col-md-4 col-sm-6">
            <div class="panel panel-body-only panel-primary-color1">
                <div class="panel-body">
                    <div class="pull-left" style="height: 67px;">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-list"></i></div>
                        </div>
                    </div>
                    <span id="Span1" class="big-text"><%=BusinessSignedIn %></span><div class="stat text-transparent">
                        <h5>Total Business Signed In (Today)</h5>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div class="panel panel-body-only panel-primary-color2">
                <div class="panel-body">
                    <div class="pull-left" style="height: 67px;">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-list"></i></div>
                        </div>
                    </div>
                    <span id="Span2" class="big-text"><%=UserSignedIn %></span><div class="stat text-transparent">
                        <h5>Total Users Signed In (Today)</h5>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="col-md-3 col-sm-6">
            <div class="panel panel-body-only panel-primary-color3">
                <div class="panel-body">
                    <div class="pull-left">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-money"></i></div>
                        </div>
                    </div>
                    <span id="Span2" class="big-text">$<%=Donation %></span><div class="stat text-transparent">
                        <h5>Total Donation Received (Today)</h5>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="panel panel-body-only panel-primary-color4">
                <div class="panel-body">
                    <div class="pull-left">
                        <div class="badge-circle daily-stat-left">
                            <div class="big-text"><i class="icon-money"></i></div>
                        </div>
                    </div>
                    <span id="Span4" class="big-text">$<%=TransactionFee %></span><div class="stat text-transparent">
                        <h5>Total Transaction Fee (Today)</h5>
                    </div>
                </div>
            </div>
        </div>--%>
        <%} %>
    </div>
    <!--/ row-->
    <!--/ Stats & Updates Row 1-->



    <div class="row">
        <!-- Sparkline -->
        <div class="col-sm-9" runat="server" visible="true">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4><i class="icon-align-left icon-2x"></i>Graph </h4>
                    </div>
                    <div class="pull-right" style="display: ">
                        <select id="ddlYearYearly" name="ddlYearYearly" class="ddlYearYearly drp-border" runat="server" onchange="RefreshYearlyReport(this)" autocomplete="off"></select>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="panel-body no-padding1">
                    <canvas id="canvasReports"></canvas>
                </div>
            </div>
        </div>


        <div class="col-md-3 col-sm-6" runat="server" visible="true">
            <div class="panel panel-body-only panel-primary-solid1">
                <div class="panel-body">
                    <span class="big-text spnTotalAmount" style="color: #808080;">$<%=ReportDisplayGrandTotal%></span><div class="stat text-transparent">
                        <h5>Total Amount Before BiteLoop Commission<span class="spnYear"></span></h5>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-3 col-sm-6" runat="server" visible="true">
            <div class="panel panel-body-only panel-primary-solid1">
                <div class="panel-body">
                    <span class="big-text spnGrandTotal" style="color: #808080;">$<%=ReportDisplayTotal  %></span><div class="stat text-transparent">
                        <h5>Total Amount after BiteLoop Commission<span class="spnYear"></span></h5>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6" runat="server" visible="true">
            <div class="panel panel-body-only panel-primary-solid1">
                <div class="panel-body">
                    <span class="big-text spnBMH" style="color: #808080;">$<%=ReportDisplayBMH %></span><div class="stat text-transparent">
                        <h5>Total BiteLoop Commission<span class="spnYear"></span></h5>
                    </div>
                </div>
            </div>
        </div>
        <%--   <div class="col-md-3 col-sm-6">
            <div class="panel panel-body-only panel-primary-solid1">
                <div class="panel-body">
                    <span class="big-text spnBMH" style="color: #808080;">$<%=ReportDisplayDonation%></span><div class="stat text-transparent">
                        <h5>Total Donation Received<span class="spnYear"></span></h5>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <div class="clearfix"></div>

    <div class="row">
        <!-- Sparkline -->
        <%--<div class="col-sm-12">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4 style="padding-left:0px;">State Wide Today's Report </h4>
                    </div>
                    <table class="dataTable table table-striped table-hover table-bordered custom-check">
                        <thead>
                            <tr>
                               
                                <th  style="text-align: center;width:100px">State</th>
                                <th style="text-align: center">Total Sold Items</th>
                                <th style="text-align: center">Total Amount after Commission </th>
                                <th style="text-align: center">Commission Revenue</th>
                                <th style="text-align: center">Gross Sales at Store Level</th>
                                <th style="text-align: center">Total Business Signed In</th>
                                <th style="text-align: center">Total Users Signed In</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptRecord">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center"><%#Convert.ToString(Eval("StateCode"))=="" ? "0" : Convert.ToString(Eval("StateCode")).Replace(".00","") %></td>
                                        <td style="text-align: center"><%#Convert.ToString(Eval("SoldQty")) =="" ? "0" : Convert.ToString(Eval("SoldQty")).Replace(".00","")  %></td>
                                        <td style="text-align: center"><%#Convert.ToString(Eval("TotalAmountBeforeBMH")) =="" ? "0" : Convert.ToString(Eval("TotalAmountBeforeBMH")).Replace(".00","") %></td>
                                        <td style="text-align: center"><%#Convert.ToString(Eval("BMH")) =="" ? "0" : Convert.ToString(Eval("BMH")).Replace(".00","") %></td>
                                        <td style="text-align: center"><%#Convert.ToString(Eval("GrandTotal")) =="" ? "0" : Convert.ToString(Eval("GrandTotal")).Replace(".00","") %></td>
                                        <td style="text-align: center"><%#Eval("BusinessRegistered") %></td>
                                        <td style="text-align: center"><%#Eval("RegisteredUsers") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr runat="server" id="trNoRecords" visible="false">
                                <td colspan="4" align="center" style="text-align: center;">
                                    <b>No Records Exists.</b>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>--%>
    </div>
    <!-- Row 2 -->
    <div class="row">
        <!-- Sparkline -->
        <div class="col-sm-3" style="display: none">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4><i class="icon-align-left icon-2x"></i>Recent Activity </h4>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body no-padding">
                    <!--chart options-->
                    <table id="charts" class="table table-responsive">
                        <tbody>
                            <tr>
                                <td><i class="icon-star"></i>&nbsp;&nbsp;Today&nbsp;&nbsp;</td>
                                <td><%= DateTime.Now.ToLongDateString() %></td>
                            </tr>
                            <tr>
                                <td><i class="icon-map-marker"></i>&nbsp;&nbsp;Last Login&nbsp;&nbsp;</td>
                                <td><%=strDate %></td>
                            </tr>
                            <tr>
                                <td><i class="icon-user"></i>&nbsp;&nbsp;From IP&nbsp;&nbsp;</td>
                                <td><%=strIPAddress %></td>
                            </tr>
                        </tbody>
                    </table>
                    <!-- / chart options -->
                </div>

            </div>
        </div>

        <!-- / sparkline -->

        <!--  large sparkline charts -->
        <div class="col-sm-8" style="display: none">
            <div id="profile-main-widget" class="col-md-12 no-padding">
                <div class="panel text-center">
                    <div class="panel-body bgimg-clouds">
                        <div class="col-sm-12  no-padding text-center">
                            <div class="tools pull-right icons-medium text-transparent hide">
                                <a href="#"><i class="icon-camera text-transparent"></i></a>
                                <a href="#"><i class="icon-cog text-transparent"></i></a>
                                <a href="#"><i class="icon-pencil text-transparent"></i></a>
                            </div>
                            <div class="clearfix"></div>
                            <!-- user -->
                            <ul class="list-inline list-unstyled">
                                <li>
                                    <div class="avatar text-center avatar-lg animated flipInX">
                                        <img src="img/avatar-lg.jpg" alt="admin" class="img-circle" width="135" />
                                    </div>
                                </li>
                                <li class="align-center-vert">
                                    <div class="leftarrowdiv-white">
                                        <div class="well-lg transparent">
                                            <ul class="list-unstyled">
                                                <li class="no-padding-left">
                                                    <h2>Welcome back <%=UserName %>!</h2>
                                                </li>
                                                <li class="no-padding-left">
                                                    <h5 class="text-transparent">----------------------- ----------------------</h5>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <br />
                        <br />
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /large sparkline charts -->
    </div>
    <!-- / row 2 -->
    <div class="row hide">
        <!-- Sparkline -->
        <div class="col-sm-4">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4><i class="icon-align-left"></i>Counts</h4>
                    </div>
                    <%--<div class="tools pull-right">
                        <a href="#"><i class="icon-refresh text-transparent"></i></a>
                        <a href="javascript:;" class="close-panel" data-dismiss="panel" aria-hidden="true"><i class="icon-remove text-transparent"></i></a>
                    </div>--%>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body no-padding">
                    <!--chart options-->
                    <table id="charts" class="table table-responsive">
                        <tbody>
                            <tr>
                                <td>Contant Pages</td>
                                <td><span class="responsive-hide badge">103</span></td>
                            </tr>
                            <tr>
                                <td>Banners</td>
                                <td>3</td>
                            </tr>
                            <tr>
                                <td>Contact Enquiry</td>
                                <td>3</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                    <!-- / chart options -->
                </div>
            </div>
        </div>
        <!-- / sparkline -->
        <!--  large sparkline charts -->
        <div class="col-sm-8">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4><i class="icon-bar-chart"></i>Unique Visitors  </h4>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body text-center gray-dark-bg" style="overflow: hidden;">
                    <!-- sparkline -->
                    <span id="bar-ex2"></span>
                    <!-- / sparkline -->
                </div>

            </div>
        </div>
        <!-- /large sparkline charts -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="assets/js/jquery.sparkline.js"></script>
    <script type="text/javascript">
        $("#bar-ex2").sparkline([15, 6, 7, 2, 0, 22, 12, 25, 6, 7, 9, 9, 5, 3, 2, 2, 15, 6, 7, 2, 1, 8, 8, 5, 2, 1, 2, 1], {
            type: 'bar',
            barWidth: 15,
            width: '',
            height: '200',
            barColor: '#ec9a5d',
            negBarColor: '#ff7f00'
        });
    </script>
    <script src="js/chart/Chart.bundle.js"></script>
    <script src="js/chart/utils.js"></script>


    <script type="text/javascript">
        $('.spnYear').html('<b> (' + jQuery.trim($('.ddlYearYearly').val()) + ')</b>');


        //https://colorlib.com/polygon/gentelella/chartjs.html
        //view-source:http://www.chartjs.org/samples/latest/charts/bar/vertical.html

        var datapoints = [<%=ReportTotal%>];
        var dataBMH = [<%=ReportBMH%>];
        var dataGrandTotal = [<%=ReportGrandTotal%>];
        var dataTotalQty = [<%=ReportTotalQty%>];
        var dataTotalTotalSignedIns = [<%=ReportTotalSignedIns%>];
        var dataTotalDonation = [<%=ReportTotalDonation%>];


        var config = {
            type: 'bar',
            data: {
                labels: [<%=ReportMonths%>],
                datasets: [
                     {
                         label: "Grand Total",
                         data: dataGrandTotal,
                         borderColor: window.chartColors.purple,
                         backgroundColor: window.chartColors.purple,
                         fill: true

                     },
                     {
                         label: "Total Amount After Commission",
                         data: datapoints,
                         borderColor: "rgb(12,144,177)",
                         backgroundColor: "rgb(12,144,177)",
                         fill: true

                     },
                    {
                        label: "BiteLoop Commission",
                        data: dataBMH,
                        borderColor: "rgb(242,186,79)",
                        backgroundColor: "rgb(242,186,79)",
                        fill: true

                    },
                    {
                        label: "Total Sold Items",
                        data: dataTotalQty,
                        borderColor: "rgb(159,62,105)",
                        backgroundColor: "rgb(159,62,105)",
                        fill: true

                    },
                     {
                         label: "Total Sign Ins",
                         data: dataTotalTotalSignedIns,
                         borderColor: "rgb(244,146,221)",
                         backgroundColor: "rgb(244,146,221)",
                         fill: true

                     }
                     //,
                     //{
                     //    label: "Total Donation Received",
                     //    data: dataTotalDonation,
                     //    borderColor: "rgb(120,209,102)",
                     //    backgroundColor: "rgb(120,209,102)",
                     //    fill: true

                     //}

                ]
            },
            options: {
                responsive: true,
                title: {
                    display: false,
                    text: 'Total Amount of this month'
                },
                tooltips: {
                    mode: 'index'
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,

                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Total Amount After Commission'
                        },
                        ticks: {
                            suggestedMin: 0,
                            suggestedMax: 100,
                        }
                    }]
                }
            }
        };
        // Photocopy DONE
        window.onload = function () {

            // Photocopy DONE
            // var ctx = document.getElementById("canvasReports").getContext("2d");
            var ctx = document.getElementById("canvasReports");
            window.myLine = new Chart(ctx, config);
            // Photocopy DONE


        };

        function RefreshYearlyReport(ele) {
            $.ajax({
                url: 'dashboard.aspx',
                data: { 'year': jQuery.trim($(ele).val()), 'yearlyreport': 'y', 'state': '<%=SelectedState%>' },
                type: 'POST',
                success: function (resp) {

                    BindYearlyReport(resp.split('^^^')[0], resp.split('^^^')[1], resp.split('^^^')[2], resp.split('^^^')[3], resp.split('^^^')[7], resp.split('^^^')[8], resp.split('^^^')[9]);
                    $('.spnTotalAmount').text('$' + resp.split('^^^')[6]);
                    $('.spnBMH').text('$' + resp.split('^^^')[4]);
                    $('.spnGrandTotal').text('$' + resp.split('^^^')[5]);


                    $('.spnYear').html('<b> (' + jQuery.trim($(ele).val()) + ')</b>');
                }
            });
        }
        function BindYearlyReport(ReportGrandTotal, ReportMonths, ReportBMH, ReportTotal, ReportQty, ReportTotalSignedIns, ReportTotalDonation) {

            for (var index = 0; index < 100; ++index) {
                config.data.labels.splice(0, 1);
            }
            config.data.datasets.splice(0, 7);

            //----------------------

            //Grand Total
            var newDatasetViewGrandTotal =
            {
                backgroundColor: window.chartColors.purple,
                borderColor: window.chartColors.purple,
                label: "Grand Total",
                fill: true,
                data: []
            };


            var ReportGrandTotalsplit = ReportGrandTotal.split(',');
            for (var index = 0; index < ReportGrandTotalsplit.length; ++index) {
                newDatasetViewGrandTotal.data.push(ReportGrandTotalsplit[index].replace('"', '').replace('"', ''));
            }
            config.data.datasets.push(newDatasetViewGrandTotal);
            //BMH   


            var newDatasetView =
            {
                backgroundColor: "rgb(12,144,177)",
                borderColor: "rgb(12,144,177)",
                label: "Total Amount After Commission",
                fill: true,
                data: []
            };

            //Total Amount
            var ReportTotalsplit = ReportTotal.split(',');
            for (var index = 0; index < ReportTotalsplit.length; ++index) {
                newDatasetView.data.push(ReportTotalsplit[index].replace('"', '').replace('"', ''));
            }
            //Total Amount
            config.data.datasets.push(newDatasetView);



            //BMH
            var newDatasetViewBMH =
            {
                backgroundColor: "rgb(242,186,79)",
                borderColor: "rgb(242,186,79)",
                label: "BiteLoop Commission",
                fill: true,
                data: []
            };

            var ReportBMHsplit = ReportBMH.split(',');
            for (var index = 0; index < ReportBMHsplit.length; ++index) {
                newDatasetViewBMH.data.push(ReportBMHsplit[index].replace('"', '').replace('"', ''));
            }

            config.data.datasets.push(newDatasetViewBMH);
            //BMH           

            //Qty            

            var newDatasetViewQty =
            {
                backgroundColor: "rgb(159,62,105)",
                borderColor: "rgb(159,62,105)",
                label: "Total Sold Items",
                fill: true,
                data: []
            };

            var ReportQtysplit = ReportQty.split(',');
            for (var index = 0; index < ReportQtysplit.length; ++index) {
                newDatasetViewQty.data.push(ReportQtysplit[index].replace('"', '').replace('"', ''));
            }

            config.data.datasets.push(newDatasetViewQty);
            //Qty 


            //SignedIns            

            var newDatasetViewSignedIns =
            {
                backgroundColor: "rgb(244,146,221)",
                borderColor: "rgb(244,146,221)",
                label: "Total Sign Ins",
                fill: true,
                data: []
            };

            var ReportTotalSignedInssplit = ReportTotalSignedIns.split(',');
            for (var index = 0; index < ReportTotalSignedInssplit.length; ++index) {
                newDatasetViewSignedIns.data.push(ReportTotalSignedInssplit[index].replace('"', '').replace('"', ''));
            }

            config.data.datasets.push(newDatasetViewSignedIns);
            //SignedIns           


            //Donations            

            //var newDatasetViewDonation =
            //                   {
            //                       backgroundColor: "rgb(120,209,102)",
            //                       borderColor: "rgb(120,209,102)",
            //                       label: "Total Donation Received",
            //                       fill: true,
            //                       data: []
            //                   };

            var ReportTotalDonationsplit = ReportTotalDonation.split(',');
            //for (var index = 0; index < ReportTotalDonationsplit.length; ++index) {
            //    newDatasetViewDonation.data.push(ReportTotalDonationsplit[index].replace('"', '').replace('"', ''));
            //}

            //config.data.datasets.push(newDatasetViewDonation);
            //Donations






            var ReportMonthssplit = ReportMonths.split(',');
            for (var index = 0; index < ReportMonthssplit.length; ++index) {
                config.data.labels.push(ReportMonthssplit[index].replace('"', '').replace('"', ''));
            }

            window.myLine.update();

        }
    </script>
</asp:Content>

