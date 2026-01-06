<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="vendor-list.aspx.cs" Inherits="AdminPanel_Vendor_List" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/adminpanel/includes/ListPagePagging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link rel="stylesheet" href="css/select2.min.css" />
    <script src="js/select2.min.js"></script>
    <style>
        .Status-Active {
            color: green;
            font-weight: bold;
        }

        .Status-InActive {
            color: grey;
            font-weight: bold;
        }

        .Status-Cancelled {
            color: red;
            font-weight: bold;
        }

        .Status-UnApproved {
            color: Blue;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Vendor List</a></li>
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
                <form id="frmSearch" action="vendor-list.aspx" onsubmit="SubmitForm();$('#hrefExport').attr('href','vendor-list.aspx?ysnExport=1&txtName='+$('#txtName').val()+ '&txtEmail='+$('#txtEmail').val()+ '&ddlStatus='+$('#ddlStatus').val()+ '&ddlState='+$('#<%=ddlState.ClientID%>').val()+ '&ddlSuburb='+$('#<%=ddlSuburb.ClientID%>').val()+ '&txtPostCode='+$('#txtPostCode').val());return false;">
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtName">Name/Vendor ID</label>
                            <input type="text" class="form-control" name="txtName" id="txtName" maxlength="50" placeholder="Enter Name/Vendor ID" />
                            <input type="text" id="Text1" name="Text1" maxlength="50" class="hide" />
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtEmail">Email Address</label>
                            <input type="text" class="form-control" name="txtEmail" id="txtEmail" maxlength="50" placeholder="Enter Email Address" />
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="ddlStatus">Status</label>
                            <select id="ddlStatus" name="ddlStatus" class="form-control">
                                <option value="-1">--Select--</option>
                                <option value="1">Active</option>
                                <option value="0">Inactive</option>
                                <option value="2">Cancelled</option>
                                <option value="3">Pending</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="ddlState">State</label>
                            <select id="ddlState" name="ddlState" class="form-control" runat="server" onchange="GetStateSuburb(this.value)">
                            </select>

                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="ddlSuburb">Suburb</label>
                            <select id="ddlSuburb" name="ddlSuburb" class="drp-border  form-control" style="width: 198px" runat="server">
                                <option value="">--All Suburb--</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtPostCode">Postcode</label>
                            <input type="text" class="form-control" name="txtPostCode" id="txtPostCode" maxlength="50" placeholder="Enter Postcode" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />

                    <div class="panel-footer">
                        <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrl();" style="margin-right: 6px; float: left">Search</button>
                        <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearControls();ResetSuburb()" value="Reset">Reset</button>
                        <div class="pull-right"><a class="btn btn-info" id="hrefExport" href="vendor-list.aspx?ysnExport=1">Export Vendors</a></div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel" style="overflow: auto; overflow-y: auto">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Vendor List</h4>
            </div>
            <%--<div class="pull-right padding-right"><a class="btn btn-info" href="vendor-modify.aspx">Add New Vendor</a></div>--%>
            <div class="clearfix"></div>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <div role="grid" class="dataTables_wrapper form-inline">
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="dataTables_length">
                                <label id="divRecSelect"></label>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="dataTables_filter">
                                <label>
                                    <select class="drp-border" onchange="javascript:PerformActionBusiness(this);">
                                        <option value="Action">Select Action</option>
                                        <option value="active">Activate</option>
                                        <option value="inactive">Inactivate</option>
                                        <option value="cancel">Cancel</option>
                                        <option value="remove">Delete</option>
                                    </select>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="DivRender">
                        <div id="divList" runat="server">
                            <table class="dataTable table table-striped table-hover table-bordered custom-check">
                                <thead>
                                    <tr>
                                        <th class="check-header"><span class="check">
                                            <input class="checked" type="checkbox" onclick="CbxAll(this); GetSelRecord();" id="cbxAll" /></span></th>
                                        <th width="100">Vendor ID</th>
                                        <th>Vendor Name</th>
                                        <th>Email Address</th>
                                        <th>Full Name</th>
                                        <th>ABN</th>
                                        <th>Business Phone</th>
                                        <th>Location</th>
                                        <th>Registered Date</th>
                                        <th>Last Active Date Time</th>
                                        <%--<th style="text-align: left; width: 100px">Store Manager</th>--%>
                                        <%--<th style="text-align: left; width: 100px">Android Link</th>--%>
                                        <%--<th style="text-align: left; width: 100px">IOS Link</th>--%>
                                        <%--<th style="text-align: center">Note</th>--%>
                                       <%-- <th style="text-align: center; width: 50px;">BMH Commission Rate(%)</th>--%>
                                        <%--<th style="text-align: center; width: 100px">Holiday</th>--%>
                                        <th style="text-align: center">Status</th>
                                        <th style="text-align: center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <span class="check">
                                                        <input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td>VEN-<%# ((System.Data.DataRowView)Container.DataItem)["VendorUniqueID"]%></td>
                                                <td><a style="color: blue" href="user-orders-report.aspx?vid=<%#Eval("ID") %>" target="_blank"><%# ((System.Data.DataRowView)Container.DataItem)["Name"]%></a></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["EmailAddress"]%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["FullName"]%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["ABN"]%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["BusinessPhone"]%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["location"]%></td>
                                                <td><%#Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedOn"]).ToString("dd/MMM/yyyy")%></td>
                                                <td><%#Convert.ToString(Eval("LastLoginDateTime")) != string.Empty ? Convert.ToDateTime(Eval("LastLoginDateTime")).ToString("dd/MMM/yyyy hh:mm tt") : "" %></td>
                                                <%--<td style="text-align: left"><%# ((System.Data.DataRowView)Container.DataItem)["PersonIncharge"]%></td>--%>
                                                <%--<td style="text-align: center">
                                                    <button class="btnAndroidCopyClipBoard" id="btnAndroidCopyClipBoard<%#Eval("ID") %>" data-clipboard-text="<%= Config.WebSiteUrl %>BMHBusinessDetails/<%#Eval("ID") %>" style="border: 0px; background-color: transparent">
                                                        <img src="images/CopyClipBoard.png" height="30" title="Android Link" />
                                                    </button>
                                                </td>--%>
                                                <%--<td style="text-align: center">
                                                    <button class="btnIOSCopyClipBoard" id="btnIOSCopyClipBoard<%#Eval("ID")  %>" data-clipboard-text="com.app.BringMeHomeConsumer://BMHBusinessDetails/<%#Eval("ID")  %>" style="border: 0px; background-color: transparent">
                                                        <img src="images/CopyClipBoard.png" height="30" title="IOS Link" />
                                                    </button>
                                                </td>--%>
                                                <%--<td style="text-align: center"><%# ((System.Data.DataRowView)Container.DataItem)["Commission"]%></td>--%>
                                                <%--<td style="text-align: center"><%# Convert.ToString( ((System.Data.DataRowView)Container.DataItem)["BMHCommissionRate"]).Replace(".00","")%></td>--%>
                                                <%--<td style="text-align: center"><a href="vendor-holiday.aspx?v=<%# Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(Eval("ID"))) %>">Add</a> </td>--%>
                                               <%-- <td class="actions">--%>
                                                    <%--<a href="javascript:;" onclick="changerecord(this,'status','<%# ((system.data.datarowview) container.dataitem)["id"] %>')" title='<%# convert.toboolean(((system.data.datarowview)container.dataitem)["status"])==true ? "activate" : "deactivate"%>'>--%>
                                                    <%--<i title="<%# (convert.toint16(eval("status")) == 1) ? "active" : (convert.toint16(eval("status")) == 0) ? "inactive" : (convert.toint16(eval("status")) == 2) ? "cancelled" : "un approved" %>"
                                                        class="<%# convert.toint16(eval("status")) ==1 ? "icon-ok" : convert.toint16(eval("status")) ==0 ? "icon-remove" :  convert.toint16(eval("status")) ==3 ? "icon-thumbs-down" : "icon-ban-circle" %>" <%# (convert.toint16(eval("status")) == 2) || (convert.toint16(eval("status")) == 4) ? "style='color:red'" : "" %>></i>--%>
                                                    <%--<span <%#(Convert.ToInt16(Eval("Status")) == 1) ? "class='Status-Active'" : (Convert.ToInt16(Eval("Status")) == 0) ? "class='Status-InActive'" : (Convert.ToInt16(Eval("Status")) == 2) ? "class='Status-Cancelled'" : "class='Status-UnApproved'" %>>
                                                        <%#(Convert.ToInt16(Eval("Status")) == 1) ? "Active" : (Convert.ToInt16(Eval("Status")) == 0) ? "Inactive" : (Convert.ToInt16(Eval("Status")) == 2) ? "Cancelled" : "Pending" %>--%>

                                                        <%--</a>--%>
                                                <%--</td>--%>
                                                <td class="actions">
                                                <a href="javascript:;" onclick="ChangeRecord(this,'status','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i>
                                                </a>
                                                </td>
                                                <td class="actions">
                                                    <a style="margin-right:8px;" href="vendor-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a class="external" data-toggle="modal" data-target='<%# "#myModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/popupform.aspx?type=vendor&id=<%#((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="View"><i class="icon-search"></i></a>
                                                    <%--<a href="javascript:;" onclick="ChangeRecordBusiness(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a></td>--%>
                                                <div class="modal fade" id='myModal<%# Eval("Id") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="false">
                                        <td colspan="17" align="center" style="text-align: center;">
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

    <script src="js/clipboard.min.js"></script>
    <script type="text/javascript">

        // Copy To Clipboard -----------------------------

        $(window).load(function () {
            //Android 
            var clipboardAndroid = new ClipboardJS('.btnAndroidCopyClipBoard');

            clipboardAndroid.on('success', function (e) {
                console.log(e.trigger)
                setTooltip('Copied!', e.trigger);
                hideTooltip(e.trigger);
                e.clearSelection();
            });

            //IOS
            var clipboardIOS = new ClipboardJS('.btnIOSCopyClipBoard');

            clipboardIOS.on('success', function (e) {
                setTooltip('Copied!', e.trigger);
                hideTooltip(e.trigger);
                e.clearSelection();
            });

        });

        $('.btnIOSCopyClipBoard').tooltip({
            trigger: 'click',
            placement: 'bottom'
        });

        $('.btnAndroidCopyClipBoard').tooltip({
            trigger: 'click',
            placement: 'bottom'
        });


        function setTooltip(message, elem) {
            $('#' + elem.id).tooltip('hide')
              .attr('data-original-title', message)
              .tooltip('show');
        }

        function hideTooltip(elem) {
            setTimeout(function () {
                $('#' + elem.id).tooltip('hide');
            }, 1000);
        }

        // --------------------------------------


        $('#<%=ddlSuburb.ClientID%>').select2();
        var VID = parseInt('<%= VID%>');
        if (VID > 0) {
            $('#txtName').val('<%=VendorName%>');

            $('.divSearchBox').removeClass('collapse');
            $('.divSearchBox').addClass('in');
        }
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'vendor-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'txtName::Name@txtEmail::Email@ddlStatus::Status@CPHContent_ddlState::Location@txtPostCode::PostCode';

        function Export() {
            window.location.href = 'vendor-list.aspx?ysnExport=1&txtName=' + $('#txtName').val() + '&txtEmail=' + $('#txtEmail').val();
        }
        function ChangeRecordBusiness(ctrl, process, ID) {
            var type = '';
            $('#chk' + ID).prop('checked', true);
            GetSelRecord();
            if (process == 'status') {
                if ($(ctrl).attr('title') == 'Activate') {
                    type = 'inactive';
                }
                else {
                    type = 'active';
                }
            }
            if (process == 'remove') {
                type = 'remove';
                if (!confirm("This will delete all the sales data as well. Are you sure you want to delete selected record?")) {
                    $('#chk' + ID).prop('checked', false);
                    return;
                }
            }

            $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
            if (PageUrl.indexOf('?') == -1)
                PageUrl = PageUrl + '?';
            else
                PageUrl = PageUrl + '&';
            $.ajax(
                {
                    url: PageUrl + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&type=' + type + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
                    success: function (response) { $('#' + RspCtrl).html(response); $.hideprogress(); $('.custom-check input').ezMark(); ScrollTop(); }
                });
        }
        function PerformActionBusiness(Ctrl) {

            //alert($(Ctrl).val());

            if ($(Ctrl).val() != 'Action') {
                var Type = $(Ctrl).val();
                OperationBusiness(divMsg, Type);
                $(Ctrl).val('Action');
            }
        }


        function OperationBusiness(ctrlMsg, type) {

            if ($('#hdnID').val() == '0' || $('#hdnID').val().length == 0) {
                DisplMsg(ctrlMsg, 'Please select at least one record.', 'alert-message error');
                ScrollTop();
            }
            else {
                var typename = '';
                if (type == 'remove') {
                    typename = "delete";
                }
                else if (type == 'active') {
                    typename = "activate";
                }
                else if (type == 'inactive') {
                    typename = "deactivate";
                }
                var messagetitle = '';
                if (type == 'remove') {
                    messagetitle = "This will delete all the sales data as well. Are you sure you want to delete selected record?"
                }
                else {
                    messagetitle = "Are you sure you want to " + typename + " selected record(s)?"
                }

                if (confirm(messagetitle)) {
                    DisplMsg(ctrlMsg, '', '');
                    $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
                    if (PageUrl.indexOf('?') == -1)
                        PageUrl = PageUrl + '?';
                    else
                        PageUrl = PageUrl + '&';
                    $.ajax(
                        {
                            url: PageUrl + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&type=' + type + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
                            success: function (response) { $('#' + RspCtrl).html(response); $.hideprogress(); $('.custom-check input').ezMark(); ScrollTop(); }
                        });
                }
            }
        }
        function GetStateSuburb(state) {
            $('#<%=ddlSuburb.ClientID%>').html('<option value="">--All Suburb--</option>');
            if (state != "") {
                $.ajax({
                    url: PageUrl + "/" + 'BindSuburb',
                    dataType: 'json',
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    data: '{stateCode: "' + state + '" }',
                    success: function (data) {
                        $('#<%=ddlSuburb.ClientID%>').empty();
                        $('#<%=ddlSuburb.ClientID%>').prepend("<option value='' selected='selected' >--All Suburb--</option>");
                        $.each(data.d, function (i, item) {
                            $('#<%=ddlSuburb.ClientID%>').append($('<option>').text(item.Suburb).attr('value', item.Suburb));
                        });
                    }
                });
                }
                else {
                    $('#<%=ddlSuburb.ClientID%>').html('<option value="">--All Suburb--</option>');
            }
        }


        function ResetSuburb() {
            $('#<%=ddlSuburb.ClientID%>').html('<option value="">--All Suburb--</option>');
            $('#ddlStatus').val('-1');
        }
    </script>
</asp:Content>


