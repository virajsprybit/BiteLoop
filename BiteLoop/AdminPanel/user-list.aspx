<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="user-list.aspx.cs" Inherits="AdminPanel_User_List" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/adminpanel/includes/ListPagePagging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link rel="stylesheet" href="css/select2.min.css" />
    <script src="js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Users List</a></li>
            </ol>
        </div>
    </div>
    <div class="alert hide" id="divMsg" runat="server"></div>
    <div class="panel">
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
                <form id="frmSearch" action="user-list.aspx" onsubmit="SubmitForm();$('#hrefExport').attr('href','user-list.aspx?ysnExport=1&txtName='+$('#txtName').val()+ '&txtEmail='+$('#txtEmail').val()+ '&txtPostcode='+$('#txtPostcode').val()+ '&ddlSuburb='+$('.ddlSuburb').val()+ '&ddlState='+$('.ddlState').val());return false;">

                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtName">Name/User ID</label>
                            <input type="text" class="form-control" name="txtName" id="txtName" maxlength="50" placeholder="Enter Name/User ID" />
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
                            <label for="ddlState">State</label>
                            <select id="ddlState" name="ddlState" runat="server" class="form-control ddlState">
                            </select>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group divSuburb">
                            <div id="divSuburb" runat="server">
                                <label for="ddlSuburb">Suburb</label>
                                <select id="ddlSuburb" name="ddlSuburb" runat="server" class="form-control ddlSuburb">
                                    <option value="">--All Suburb--</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtPostcode">Postcode</label>
                            <input type="text" class="form-control" name="txtPostcode" id="txtPostcode" maxlength="10" placeholder="Enter Postcode" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />

                    <div class="panel-footer">
                        <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrl();" style="margin-right: 6px; float: left">Search</button>
                        <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearControls();ResetSuburb();" value="Reset">Reset</button>
                        <div class="pull-right"><a class="btn btn-info" id="hrefExport" href="user-list.aspx?ysnExport=1">Export Users</a></div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel" style="overflow:auto; overflow-y:hidden">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Users List</h4>
            </div>

            <div class="clearfix"></div>
        </div>
        <div class="panel-body" style="overflow:auto; overflow-y:hidden">
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
                                            <input class="checked" type="checkbox" onclick="CbxAll(this); GetSelRecord();" id="cbxAll" /></span></th>
                                        <th width="100">User ID</th>
                                        <th>User Name</th>
                                        <th>Email Address</th>
                                        <%--<th>Gender</th>--%>
                                        <th>Mobile</th>
                                        <th>Registered Date</th>
                                        <th>Social Media</th>
                                        <%--<th>Saved Business</th>--%>
                                        <%--<th>Last Location</th>--%>
                                        <%--<th>State</th>--%>
                                        <%--<th>State</th>--%>
                                        <th>Postcode</th>
                                        <th>Last Login</th>
                                        <%--<th style="text-align: center; width: 50px;">Reward Points</th>--%>
<%--                                        <th style="text-align: center; width: 50px;">Meals Saved</th>
                                        <th style="text-align: center; width: 50px;">$ Saved</th>--%>
                                        <th style="text-align: center; width: 50px;">Status</th>
                                        <th style="text-align: center; width: 50px;">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <span class="check">
                                                        <input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td>USR-<%# ((System.Data.DataRowView)Container.DataItem)["UserUniqueID"]%></td>
                                                <td><a style="color:blue" href="user-orders-report.aspx?uid=<%#Eval("ID") %>" target="_blank"><%# Convert.ToString(Eval("Name")) + " " + Convert.ToString(Eval("LastName")) %></a></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["Email"]%></td>
                                                <%--<td><%# ((System.Data.DataRowView)Container.DataItem)["Gender"]%></td--%>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["Mobile"]%></td>
                                                <td><%# Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedDate"]).ToString("dd/MMM/yyyy")%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["IsFacebookGoogleApple"] == DBNull.Value ? "NO": (
                                                        Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["IsFacebookGoogleApple"]) == 1 ? "Facebook" :
                                                        Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["IsFacebookGoogleApple"]) == 2 ? "Google" :
                                                        Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["IsFacebookGoogleApple"]) == 3 ? "Apple" :
                                                        "NO")%>
                                                </td>
                                                <%--<td><%#((System.Data.DataRowView)Container.DataItem)["FavouriteBusiness"]%></td>--%>
                                                <%--<td><%# GetLocationDetails(Convert.ToString( ((System.Data.DataRowView)Container.DataItem)["LastLoginLocation"]))%></td>--%>
                                                <%--<td><%#Eval("State") %></td--%>
                                                <td><%# Convert.ToString(Eval("Postcode")) == "" ? "N/A" :Eval("Postcode") %></td>
                                                <%--<td><%#Convert.ToString(Eval("LastLoginDate")) != string.Empty ? Convert.ToDateTime(Eval("LastLoginDate")).ToString("dd/MMM/yyyy HH:mm") + "<br/>" + GetLocationDetails(Convert.ToString(Eval("LastLoginLocation"))) :"" %></td>--%>
                                                <td style="text-align: center;">
                                                    <a class="external lastlogin<%#Eval("ID") %>" data-toggle="modal" data-target='<%# "#lastLoginModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/lastloginpopupform.aspx?type=userlastlogin&id=<%#((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Last Login">
                                                        <img src="images/location.png" width="16" />
                                                    </a>
                                                    <div class="modal fade" style="z-index: 99999 !important;" id='lastLoginModal<%# Eval("Id") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>
                                                </td>
                                               <%-- <td style="text-align: center;">
                                                    <a class="external reward<%#Eval("ID") %>" data-toggle="modal" data-target='<%# "#myRewardsModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/rewardspopupform.aspx?type=userreward&id=<%#((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Modify Rewards">
                                                        <%#Convert.ToString(Eval("RewardsPoints")).Replace(".00","") %> 
                                                    </a>
                                                    <div class="modal fade" style="z-index: 99999 !important;" id='myRewardsModal<%# Eval("Id") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>
                                                </td>--%>
                                                <%--<td style="text-align: center;">
                                                    <a class="external meals<%#Eval("ID") %>" data-toggle="modal" data-target='<%# "#mealsModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/mealpopupform.aspx?type=usermeals&id=<%#((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Total Meals Purchased">
                                                        <%#Convert.ToString(Eval("TotalMeals")).Replace(".00","") %> 
                                                    </a>
                                                    <div class="modal fade" style="z-index: 99999 !important;" id='mealsModal<%# Eval("Id") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <a class="external yousaved<%#Eval("ID") %>" data-toggle="modal" data-target='<%# "#yousavedModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/yousavedpopupform.aspx?type=yousaved&id=<%#((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="$ saved">
                                                        <%#Convert.ToString(Eval("YouSaved")).Replace(".00","") %> 
                                                    </a>
                                                    <div class="modal fade" style="z-index: 99999 !important;" id='yousavedModal<%# Eval("Id") %>' tabindex="-1" role="dialog" aria-hidden="true"></div>
                                                </td>--%>
                                                <td class="actions">
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'status','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                        <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i>
                                                    </a>
                                                </td>
                                                <td class="actions">
                                                    <a style="margin-right:8px;"class="external" data-toggle="modal" data-target='<%# "#myModal" +Eval("ID").ToString() %>' href="<%=Config.VirtualDir%>adminpanel/popupform.aspx?type=user&id=<%#((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="View"><i class="icon-search"></i></a>
                                                    <a href="user-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a></td>
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

    <script type="text/javascript">
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'user-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'txtName::Name@txtEmail::Email@CPHContent_ddlSuburb::Suburb@CPHContent_ddlState::State@txtPostcode::Postcode';

        var UID = parseInt('<%= UID%>');
        if (UID > 0) {
            $('#txtName').val('<%=UserName%>');          

            $('.divSearchBox').removeClass('collapse');
            $('.divSearchBox').addClass('in');
        }

        function Export() {
            window.location.href = 'user-list.aspx?ysnExport=1&txtName' + $('#txtName').val() + '=&txtEmail=' + $('#txtEmail').val() + '&txtPostcode=' + $('#txtPostcode').val() + '&ddlSuburb=' + $('.ddlSuburb').val() + '&ddlState=' + $('.ddlState').val();
        }

        $(document).ready(function () {

            $('.ddlState').change(function () {
                if ($(this).val() != '') {
                    $.ajax({
                        url: 'user-list.aspx',
                        type: 'POST',
                        data: { 'statecode': $(this).val(), 'bindsuburb': 'y' },
                        success: function (resp) {
                            $('.divSuburb').html(resp);
                            $('#<%=ddlSuburb.ClientID%>').select2();
                        }
                    });
                }
                else {
                    $('.ddlSuburb').html('<option value="">--All Suburb--</option>');
                }
            });

        });
        function ResetSuburb() {
            $('.ddlSuburb').html('<option value="">--All Suburb--</option>');
        }
    </script>
</asp:Content>


