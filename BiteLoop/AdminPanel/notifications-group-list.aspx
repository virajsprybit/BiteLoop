<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="notifications-group-list.aspx.cs" Inherits="AdminPanel_Notifications_Groups_List" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/adminpanel/includes/ListPagePagging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        .label-danger {
            background-color: #ed4e2a;

            background-image: none !important;
            font-size:12px;
        }
        .label-success{
            background-color: #3cc051;
            background-image: none !important;
            font-size:12px;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Notification Group List</a></li>
            </ol>
        </div>
    </div>
    <div class="alert hide" id="divMsg" runat="server"></div>
    <div class="panel">
        <div class="panel panel-default" style="display:none">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4><i class="icon-th-large"></i>Search</h4>
                </div>
                <div class="tools pull-right">
                    <a data-target="#collapseThree" data-toggle="collapse" href="javascript:;"><i class="icon-chevron-down text-transparent"></i></a>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body panel-collapse collapse collapse" id="collapseThree">
                <form id="frmSearch" action="notifications-group-list.aspx" onsubmit="SubmitForm();$('#hrefExport').attr('href','notifications-Group-list.aspx?ysnExport=1&txtName='+$('#txtName').val()+ '&txtEmail='+$('#txtEmail').val());return false;">
                    <div class="form-group">
                        <label for="txtName">Name</label>
                        <input type="text" class="form-control" name="txtName" id="txtName" maxlength="50" placeholder="Enter Group Name" />
                        <input type="text" id="Text1" name="Text1" maxlength="50" class="hide" />
                    </div>                    
                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />

                    <div class="panel-footer">
                        <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrl();" style="margin-right: 6px; float: left">Search</button>
                        <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearControls();" value="Reset">Reset</button>                     
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Notification Group List</h4>
            </div>

            <div class="pull-right padding-right"><a class="btn btn-info" href="notification-group-modify.aspx">Add New Group</a></div>
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
                                    <select class="drp-border" onchange="javascript:PerformAction(this);">
                                        <option value="Action">Select Action</option>
                                       <%-- <option value="active">Activate</option>
                                        <option value="inactive">Deactivate</option>--%>
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
                                        <th>Group Name</th>                                        
                                        <th style="text-align:center; width:120px">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <span class="check">
                                                        <input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td><%# Common.FixLengthString(Convert.ToString(((System.Data.DataRowView)Container.DataItem)["Title"]),500)%></td>                                                
                                                <td class="actions">                                                    
                                                    <a href="notification-group-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="false">
                                        <td colspan="6" align="center" style="text-align: center;">
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
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'notifications-group-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'txtName::Name@txtEmail::Email';      
    </script>
</asp:Content>


