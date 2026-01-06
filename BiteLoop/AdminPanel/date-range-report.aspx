<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="date-range-report.aspx.cs" Inherits="AdminPanel_DateRange_List" %>

<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link href="js/DateRangePicker/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="js/DateRangePicker/ui.daterangepicker.css" rel="stylesheet" type="text/css" media="screen" />
      <link rel="stylesheet" href="css/select2.min.css" />
    <script src="js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Vendor Report</a></li>
            </ol>
        </div>
    </div>
    <div class="alert hide" id="divMsg" runat="server"></div>
    <div class="panel">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4><i class="icon-th-large"></i>Search</h4>
                </div>
                <div class="tools pull-right">
                    <a data-target="#collapseThree" data-toggle="collapse" href="javascript:;"><i class="icon-chevron-down text-transparent"></i></a>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body panel-collapse collapse1 collapse1" id="collapseThree">
                <form>
                    <div class="col-xs-1" style="width: 80px !important;margin-top: 4px;">
                        <label>Date Range</label>
                    </div>
                    <div class="col-xs-3">                        
                        <div class="divdate form-group">

                            <input type="text" value="" id="tbxDate" runat="server" name="tbxDate" class="form-control" style="width: 173px !important; height: 25px; padding: 5px;"
                                readonly="readonly" />
                        </div>
                        <%--<input type="button" value="Clear" onclick="javascript: $('#<%=tbxDate.ClientID %>').val('');" />--%>
                    </div>
                    <div class="col-xs-5">
                        <div class="form-group">
                            <label>Vendor&nbsp;&nbsp;&nbsp;</label>
                            <select id="ddlBusiness" name="ddlBusiness" class="drp-border" runat="server" style="width: 320px"></select>                            
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />
                    <div class="panel-footer">
                        <button type="button" class="btn btn-info" onclick="page=1;return SearchOrerControl();">Search</button>
                        <button type="button" id="btnReset" class="btn btn-default" onclick="page=1;ClearOrderControls();" value="Reset">Reset</button>
                         <div class="pull-right"><a class="btn btn-info" id="hrefExport" href="javascript:;" onclick="Export()">Export Vendor Report</a></div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Vendor Report</h4>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <div role="grid" class="dataTables_wrapper form-inline">
                    <div class="row">
                        <div class="col-xs-6">
                        </div>
                        <div class="col-xs-6" style="display: none">
                        </div>
                    </div>
                    <div id="DivRender">
                        <div id="divList" runat="server">
                            <table class="dataTable table table-striped table-hover table-bordered custom-check">
                                <thead>
                                    <tr>                                       
                                        <th>Vendor ID</th>
                                          <th>Vendor</th>
                                        <th>Total Posted Items</th>
                                       <th>Not Sold Items</th>
                                        <th>Sold Items</th>
                                        <th>Total Amount Before BiteLoop Commison</th>
                                        <th>BiteLoop Commission</th>
                                        <th>Amount After Commission</th>                                       
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                 <td>VEN-<%#((System.Data.DataRowView)Container.DataItem)["VendorUniqueID"]%></td>
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["BusinessName"]%></td>
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["TotalPosted"]%></td>
                                                <td><%# Convert.ToInt32(Eval("TotalPosted")) - Convert.ToInt32(Eval("SoldQty"))  %></td>                                            
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["SoldQty"]%></td>                                            
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["TotalAmountBeforeBMH"]%></td>                                            
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["BMH"]%></td>                                            
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["GrandTotal"]%></td>                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="true">
                                        <td colspan="20" align="center" style="text-align: center;">
                                            <b>No Records Exists.</b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
<%--                            <Ctrl:Paging runat="server" ID="CtrlPage1" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        $('#<%=ddlBusiness.ClientID%>').select2();
    </script>
    <script src="js/DateRangePicker/jQuery.js"></script>
    <script src="js/DateRangePicker/daterangepicker.jQuery.js"></script>
    <script src="js/DateRangePicker/jquery-ui-1.7.1.custom.min.js"></script>
    <script type="text/javascript">
        $('#<%=tbxDate.ClientID %>').daterangepicker({ arrows: true });
        $('.ui-daterangepicker-arrows').css('width', '220px');
        $('.ui-daterangepicker-arrows').css('padding', '4px');


        $('.ui-daterangepicker-prev').css('top', '7px');
        $('.ui-daterangepicker-next').css('top', '7px');
        $('#<%=tbxDate.ClientID %>').click(function () {
            // alert('test');
            $('.ui-daterangepickercontain').css('top', parseInt($('.divdate').offset().top + 25) + 'px');
            $('.ui-daterangepickercontain').css('left', parseInt($('.divdate').offset().left) + 'px');
            $('.ui-daterangepickercontain').css('z-index', '100000');
        });

        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'date-range-report.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = "<%= divMsg.ClientID %>";
        var SearchControl = 'CPHContent_ddlBusiness::ddlBusiness';

        function ClearOrderControls() {
            $('#<%=tbxDate.ClientID %>').val('');
            $('#<%=ddlBusiness.ClientID %>').val('0');

            $('.dataTable tbody').html("<tr id='CPHContent_trNoRecords'><td colspan='20' style='text-align: center;' align='center'><b>No Records Exists.</b></td></tr>");
        }

        function SearchOrerControl() {
            if ($('#<%=tbxDate.ClientID %>').val() == '') {
                DisplMsg(divMsg, 'Please select date range.', 'alert-message error');
            }
            else {
                $('#' + divMsg).hide();


//                $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
                $.ajax({
                    url: PageUrl,
                    data: {'searchorder':'y', 'date' : $('#<%=tbxDate.ClientID %>').val(), 'businessid' : $('#<%=ddlBusiness.ClientID %>').val()},
                    type: 'POST',
                    success: function (json) {
                        $('#' + RspCtrl).html(json);// $.hideprogress();
                        ScrollTop();
                    }
                });

            }

            
        }
        function Export() {
            window.location.href = 'date-range-report.aspx?ysnExport=1&date=' + $('#<%=tbxDate.ClientID%>').val() + '&businessid=' + $('#<%=ddlBusiness.ClientID%>').val();
           }
    </script>
</asp:Content>

