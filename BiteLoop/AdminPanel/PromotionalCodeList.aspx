<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="PromotionalCodeList.aspx.cs" Inherits="PromotionalCodeList" %>

<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>
<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link rel="stylesheet" href="assets/colorbox/colorbox.css" />
    <script type="text/javascript" src="assets/colorbox/jquery.colorbox.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Promotional Code List</a></li>
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
            <div class="panel-body panel-collapse collapse collapse" id="collapseThree">
                <form id="frmSearch" onsubmit="SubmitForm();return false;" action="PromotionalCodelist.aspx">

                    <div class="form-group col-md-3">
                        <label for="tbxCouponCode">Coupon Code</label>
                        <input type="text" class="form-control" id="tbxCouponCode" name="tbxCouponCode" maxlength="50" placeholder="Enter Coupon Code" />
                    </div>

                    <div class="clearfix"></div>
                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage%>" name="hdnRecPerPage" />
                    <div class="panel-footer">
                        <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrl();">Search</button>
                        <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearControls();" value="Reset">Reset</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Promotional Code List</h4>
            </div>
            <div class="pull-right padding-right"><a class="btn btn-info" href="PromotionalCode.aspx">Add New Code</a></div>
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
                                        <option value="active">Activate</option>
                                        <option value="inactive">Deactivate</option>
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
                                            <input class="checked" type="checkbox" onclick="CbxAllUSer(this); GetSelRecordUser();" id="cbxAll" /></span></th>
                                        <th>Coupon Code</th>
                                        <th>Start Date</th>
                                        <th>End Date</th>
                                        <th>Discount</th>
                                        <th class="actions">Users</th>
                                        <th class="actions">Status</th>
                                        <th class="actions">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <span class="check">
                                                        <input class="checked chkUSer" type="checkbox" onclick="SetCbxBoxUser(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["CouponCode"]%></td>
                                                <td><%#Convert.ToDateTime(Eval("CouponStartTime")).ToString("dd/MMM/yyyy") %></td>
                                                <td><%#Convert.ToDateTime(Eval("CouponEndTime")).ToString("dd/MMM/yyyy") %></td>
                                                <td><%#Convert.ToString(Eval("DiscountType")) == "1" ? "$" + Convert.ToString(Eval("DiscountAmount")) :  Convert.ToString(Eval("DiscountAmount")) + "%" %></td>
                                                 <td class="actions">
                                                    


                                                      
                                                    <a class="external" data-toggle="modal" data-target='<%# "#myModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/promocode-popupform.aspx?type=user&code=<%#((System.Data.DataRowView) Container.DataItem)["CouponCode"] %>">
                                                        <%#Convert.ToString(Eval("UserCount")) %>

                                                    </a>
                                                    
                                                <div class="modal fade" id='myModal<%# Eval("Id") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>




                                                 </td>
                                                <td class="actions">
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'status','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                        <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i>
                                                    </a>
                                                </td>
                                                <td class="actions">
                                                    <a href="PromotionalCode.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="false">
                                        <td colspan="15" align="center" style="text-align: center;">
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
        var PageUrl = 'promotionalcodelist.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'tbxCouponCode::CouponCode';
        //$(document).ready(function () {
        //    $(".iframe").colorbox({ iframe: true, width: "600", height: "502", closeButton: false });
        //});
    </script>
</asp:Content>


