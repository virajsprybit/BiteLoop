<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="user-orders-report.aspx.cs" Inherits="AdminPanel_UserOrders_List" %>

<%@ Import Namespace="Utility" %>
<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        .brand-highlight {
            background-color: transparent !important;
        }

        .promoamount {
            background-color: #fe9d0f !important;
            padding: 0px 8px 1px 8px;
            font-weight: bold;
            margin-top: 38px;
        }
    </style>
    <link rel="stylesheet" href="css/select2.min.css" />
    <script src="js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">User Order Report</a></li>
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
            <div class="panel-body panel-collapse collapse collapse divSearchBox" id="collapseThree">
                <form id="frmSearch" onsubmit="SubmitForm();return false;" action="user-orders-report.aspx">
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>Order ID&nbsp;&nbsp;&nbsp;</label>
                            <input type="text" class="form-control" id="txtOrderID" name="txtOrderID" runat="server" />
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>Start Date&nbsp;&nbsp;&nbsp;</label>
                            <input type="text" class="form-control" id="txtStartDate" name="txtStartDate" runat="server" data-date-format="mm/dd/yyyy" autocomplete="off" readonly="true" />
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>End Date&nbsp;&nbsp;&nbsp;</label>
                            <input type="text" class="form-control" id="txtEndDate" name="txtEndDate" runat="server" data-date-format="mm/dd/yyyy" autocomplete="off" readonly="true" />
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="ddlState">State</label>
                            <select id="ddlState" name="ddlState" runat="server" class="form-control ddlState">
                            </select>
                        </div>
                    </div>

                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>User&nbsp;&nbsp;&nbsp;</label><br />
                            <select id="ddlUsers" name="ddlUsers" class="drp-border form-control" runat="server" style="width: 198px"></select>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>Vendor&nbsp;&nbsp;&nbsp;</label><br />
                            <select id="ddlBusiness" name="ddlBusiness" class="drp-border form-control" runat="server" style="width: 198px"></select>

                            <input type="text" class="form-control" name="txtTitle" id="txtTitle" maxlength="50" placeholder="Enter Title" style="display: none" />
                        </div>
                    </div>

                    <div class="clearfix"></div>


                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />
                    <div class="panel-footer">
                        <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrlOrders();">Search</button>
                        <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearDropdowns();ClearControlsOrders();" value="Reset">Reset</button>
                        <div class="pull-right"><a class="btn btn-info" id="hrefExport" href="javascript:;" onclick="Export()">Export Order Report</a></div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel" style="overflow: auto">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>User Order Report</h4>
            </div>
            <div class="pull-right">
    <button type="button"
            class="btn btn-primary"
            id="btnSave"
            onclick="ValidateForm()"
            disabled>
        Submit
    </button>
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
                                        <th class="check-header">
    <span class="check">
        <input class="checked"
               type="checkbox"
               id="cbxAll"
               onclick="toggleSubmit(this); CbxAll(this); GetSelRecord();" />
    </span>
</th>
                                        <th width="100">Order ID</th>
                                        <th>Order Date</th>
                                        <th>User ID</th>
                                        <th>User Name</th>
                                        <th>Vendor ID</th>
                                        <th>Vendor(Purchase From)</th>
                                        <th>State</th>
                                        <%--   <th>Purchase Location</th>--%>
                                        <th>User Email</th>
                                        <th style="text-align: center">Unit Price</th>
                                        <%--<th style="text-align: center">Discount</th>--%>
                                        <th style="text-align: center">Discounted Price</th>
                                        <th style="text-align: center">Qty</th>
                                        <%--<th style="text-align: center">Redeem Amount</th>--%>
                                       <%-- <th style="text-align: center">Promo Code</th>--%>
                                        <th style="text-align: center">Total Price</th>
                                        <%--<th style="text-align: center">Donation</th>
                                        <th style="text-align: center">Transaction Fee</th>--%>
                                        <%--<th style="text-align: center">BMH Commission</th>--%>
                                        <th style="text-align: center">Grand Total</th>
                                        <th style="text-align: center">BiteLoop Fee</th>
                                        <th style="text-align: center">Payout Value</th>
                                        <th style="text-align: center">Payout Status</th>

                                        <th style="text-align: center">Order Status</th>
                                        <%--<th width="30" style="text-align: center">Refunded?</th>--%>
                                        <%--<th width="30" style="text-align: center">Refund</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>

                                                <td class="check">
                                                <span class="check">
                                                <input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["OrderID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["OrderID"] %>" />
                                                </span></td>
                                                <td>ORD-<%#((System.Data.DataRowView)Container.DataItem)["OrderID"]%></td>
                                                <td><%# Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedOn"]).ToString("dd/MMM/yyyy HH:mm tt")%></td>
                                                <td>USR-<%#((System.Data.DataRowView)Container.DataItem)["UserUniqueID"]%></td>
                                                <%--<td><%#((System.Data.DataRowView)Container.DataItem)["UserName"]%></td>--%>
                                                <td><a style="color: blue" href="user-list.aspx?uid=<%#Eval("UserID") %>" target="_blank"><%# Convert.ToString(Eval("UserName")) %></a></td>
                                                <td>VEN-<%#((System.Data.DataRowView)Container.DataItem)["VendorUniqueID"]%></td>
                                                <td><a style="color: blue" target="_blank" href="vendor-list.aspx?vid=<%#Eval("BusinessID") %>"><%#((System.Data.DataRowView)Container.DataItem)["BusinessName"]%></a> </td>
                                                <td><%#Eval("State") %></td>
                                                <%--<td><%#((System.Data.DataRowView)Container.DataItem)["PurchaseLocation"]%></td>--%>
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["Email"]%></td>
                                                <td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["OriginalPrice"]%></td>
                                                <%--<td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["Discount"]%></td>--%>
                                                <td style="text-align: center">$<%# Convert.ToDouble(Eval("Discount")).ToString("f2")  %></td>
                                                <td style="text-align: center"><%#((System.Data.DataRowView)Container.DataItem)["Qty"]%></td>
                                                <%--<td style="text-align: center"><%#((System.Data.DataRowView)Container.DataItem)["RewardsAmount"]%></td>--%>
                                                <%--<td style="text-align: center">
                                                    <%#Convert.ToString(Eval("PromoCode")) == string.Empty ? " - " : "<b>" + Convert.ToString(Eval("PromoCode")) + "</b><br><span class='promoamount'>$" + Convert.ToString(Eval("APPPromocodeDiscountAmount")) + "</span>"%>
                                                </td>--%>


                                                <td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["ItemPrice"]%></td>
                                                <%--<td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["Donation"]%></td>
                                                <td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["TransactionFee"]%></td>--%>
                                                <%--<td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["BringMeHomeFee"]%></td>--%>
                                                <td style="text-align: center">$<%#((System.Data.DataRowView)Container.DataItem)["GrandTotal"]%></td>
                                                <td style="text-align: center">$<%#(Convert.ToDouble(((System.Data.DataRowView)Container.DataItem)["ItemPrice"]) * 0.15).ToString("0.00")%></td>
                                                <td style="text-align: center">$<%#(Convert.ToDouble(((System.Data.DataRowView)Container.DataItem)["ItemPrice"]) - Convert.ToDouble(((System.Data.DataRowView)Container.DataItem)["ItemPrice"]) * 0.15).ToString("0.00")%></td>
                                                <td style="text-align: center"><%#"Unpaid"%></td>
                                                <td style="text-align: center"><%# Convert.ToString(Eval("OrderStatusID")).ToLower() == "1" ? "Pending" : 
                                                                                   Convert.ToString(Eval("OrderStatusID")).ToLower() == "2" ? "Confirmed" :
                                                                                   Convert.ToString(Eval("OrderStatusID")).ToLower() == "3" ? "Ready To Collect" :
                                                                                   Convert.ToString(Eval("OrderStatusID")).ToLower() == "4" ? "Completed":
                                                                                   "Cancelled"%></td>
                                                <%-- <td style="text-align: center">
                                                    <input type="checkbox" onclick="ChangeOrderStatus(this)" id="chkOrderID<%#Eval("OrderID") %>" value="<%#Eval("OrderID") %>" <%#Convert.ToString(Eval("OrderStatus")) != string.Empty ? "checked='checked'" : "" %> /></td>--%>
                                               <%-- <td align="center">
                                                    <a <%# Convert.ToString(Eval("OrderStatus")) =="" && Convert.ToString(Eval("StripeChargeID")) != "" ? "" : "style='display:none'" %> class="external btn btn-primary reward<%#Eval("OrderID") %>" data-toggle="modal" data-target='<%# "#myRefundModal" +Eval("OrderID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/refundpopupform.aspx?type=userrefund&id=<%# Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString( ((System.Data.DataRowView) Container.DataItem)["OrderID"] ))%>" title="Refund">Refund
                                                    </a>
                                                    <div class="modal fade" data-backdrop="static" data-keyboard="false" style="z-index: 99999 !important;" id='myRefundModal<%# Eval("OrderID") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>

                                                    <%# Convert.ToString(Eval("OrderStatus")) =="" && Convert.ToString(Eval("StripeChargeID")) != "" ? "" : Convert.ToString(Eval("OrderStatus")) !="" ? "<a href='"+ Config.VirtualDir + "adminpanel/refunddetailspopupform.aspx?type=userrefund&id="+ Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString( ((System.Data.DataRowView) Container.DataItem)["OrderID"] )) + "' class='external' data-toggle='modal' data-target='#myRefundDetailsModal" +Eval("OrderID").ToString() + "'>" + Convert.ToString(Eval("OrderStatus")) + "</a>" : " - "  %>
                                                    <div class="modal fade" data-backdrop="static" data-keyboard="false" style="z-index: 99999 !important;" id='myRefundDetailsModal<%# Eval("OrderID") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>
                                                </td>--%>


                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="false">
                                        <td colspan="20" align="center" style="text-align: center;">
                                            <b>No Records Exists.</b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <Ctrl:Paging runat="server" ID="CtrlPage1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        $('#<%=ddlUsers.ClientID%>').select2();
        $('#<%=ddlBusiness.ClientID%>').select2();
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'user-orders-report.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = "<%= divMsg.ClientID %>";
        var SearchControl = 'CPHContent_ddlBusiness::ddlBusiness';

    </script>
    <link href="assets/plugins/datepicker/css/datepicker.css" rel="stylesheet" />
    <script src="assets/plugins/datepicker/js/bootstrap-datepicker.js"></script>
    <script>
        function toggleSubmit(cb) {
            document.getElementById("btnSave").disabled = !cb.checked;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            var VID = parseInt('<%= VID%>');
            if (VID > 0) {
                $('#<%=ddlBusiness.ClientID%>').val(VID);
                $('#<%=ddlBusiness.ClientID%>').select2();

                $('.divSearchBox').removeClass('collapse');
                $('.divSearchBox').addClass('in');
            }
            var UID = parseInt('<%= UID%>');
            if (UID > 0) {
                $('#<%=ddlUsers.ClientID%>').val(UID);
                $('#<%=ddlUsers.ClientID%>').select2();

                $('.divSearchBox').removeClass('collapse');
                $('.divSearchBox').addClass('in');
            }

            $('.brand-highlight').removeClass('brand-highlight');
            $("#<%=txtStartDate.ClientID%>").datepicker({
                format: 'mm/dd/yyyy',
            }).on('changeDate', function (e) {
                $(this).datepicker('hide');
            });


            $("#<%=txtEndDate.ClientID%>").datepicker({
                format: 'mm/dd/yyyy',
            }).on('changeDate', function (e) {
                $(this).datepicker('hide');
            });

        });
        function ValidateCtrlOrders() {
            if (jQuery.trim($('#<%= txtStartDate.ClientID %>').val()) != '' && jQuery.trim($('#<%= txtEndDate.ClientID %>').val()) != '') {
                var startDate = new Date(jQuery.trim($('#<%= txtStartDate.ClientID %>').val()));
                var endDate = new Date(jQuery.trim($('#<%= txtEndDate.ClientID %>').val()));

                if ((Date.parse(startDate) >= Date.parse(endDate))) {

                    DisplMsg(divMsg, 'Start Date must be less than End Date.', 'alert-message error');
                    return false;
                }
            }
            return true;
        }
        function ClearControlsOrders() {
            $('#<%= txtStartDate.ClientID %>').val('');
            $('#<%= txtEndDate.ClientID %>').val('');
            $('#<%= ddlUsers.ClientID %>').val('0');
            $('#<%= ddlBusiness.ClientID %>').val('0');

        }

        function Export() {
            window.location.href = 'user-orders-report.aspx?ysnExport=1&ctl00$CPHContent$txtStartDate=' + $('#<%=txtStartDate.ClientID%>').val() + '&ctl00$CPHContent$txtEndDate=' + $('#<%=txtEndDate.ClientID%>').val() + '&ctl00$CPHContent$ddlUsers=' + $('#<%=ddlUsers.ClientID%>').val() + '&ctl00$CPHContent$ddlBusiness=' + $('#<%=ddlBusiness.ClientID%>').val() + '&ctl00$CPHContent$ddlState=' + $('#<%=ddlState.ClientID%>').val();
        }

        function ChangeOrderStatus(ele) {

            var SelectedStatus = '';
            if ($(ele).prop('checked')) {
                SelectedStatus = 'Refunded';
            }
            $.ajax({
                url: 'user-orders-report.aspx',
                data: { 'OrderStatus': jQuery.trim(SelectedStatus), 'OrderID': jQuery.trim($(ele).val()), 'changeorderstatus': 'y' },
                type: 'POST',
                success: function (resp) {
                    DisplMsg(divMsg, 'Order Status has been changed successfully.', 'alert-message success');

                    //$('#' + $(ele).attr('id')).prop('checked', false);
                    //$('#' + $(ele).attr('id')).parent().removeClass('ez-checked')
                    $('#' + $(ele).attr('id')).parent().parent().parent().removeClass('brand-highlight');
                    ScrollTop();
                }
            });
        }

        function RefreshOrderReport() {
            $.ajax({
                url: 'user-orders-report.aspx',
                data: { 'page': page, 'hdnRecPerPage': jQuery.trim($('#hdnRecPerPage').val()), 'sorttype': SortType, 'sortcol': SortColumn },
                type: 'POST',
                success: function (resp) {
                    $('#DivRender').html(resp);
                }
            });
        }
        function ClearDropdowns() {
            $('#<%=txtOrderID.ClientID%>').val('');
            $('#<%=ddlState.ClientID%>').val('');
            $('#<%=ddlUsers.ClientID%>').val('0');
            $('#<%=ddlBusiness.ClientID%>').val('0');
            $('#<%=ddlUsers.ClientID%>').select2();
            $('#<%=ddlBusiness.ClientID%>').select2();
        }
    </script>
</asp:Content>

