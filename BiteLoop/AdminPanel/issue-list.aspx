<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="issue-list.aspx.cs" Inherits="AdminPanel_Issue_List" ValidateRequest="false" EnableEventValidation="false" %>

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
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Issues List</a></li>
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
                <form id="frmSearch" runat="server" action="issue-list.aspx" onsubmit="SubmitForm();return false;">

                    <div class="col-xs-2">
                        <div class="form-group">
                            <label for="txtName">Name/Issue ID</label>
                            <input type="text" class="form-control" name="txtName" id="txtName" maxlength="50" placeholder="Enter Name/Issue ID" />
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
                    <asp:HiddenField ID="hdnID" runat="server" />
                    <asp:HiddenField ID="hdnType" runat="server" />
                    <div class="panel-footer">
                        <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrl();" style="margin-right: 6px; float: left">Search</button>
                        <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearControls();ResetSuburb();" value="Reset">Reset</button>
                        <div class="pull-right"><a class="btn btn-info" id="hrefExport" href="issue-list.aspx?ysnExport=1">Export Issues</a></div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="panel" style="overflow:auto; overflow-y:hidden">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Issues List</h4>
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
                    </div>
                    <div id="DivRender">
                        <div id="divList" runat="server">
                            <table class="dataTable table table-striped table-hover table-bordered custom-check">
                                <thead>
                                    <tr>
                                        <th class="check-header"><span class="check">
                                            <input class="checked" type="checkbox" onclick="CbxAll(this); GetSelRecord();" id="cbxAll" /></span></th>
                                        <th width="100">Issue ID</th>
                                        <th width="100">Case Number</th>
                                        <th width="100">Order ID</th>
                                        <th width="100">Description</th>
                                        <th width="160">Image</th>
                                        <th width="50">Registered Date</th>
                                        <th style="text-align: center; width: 50px;">Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <input type="checkbox"
                                                        id='chk<%# Eval("ID") %>'
                                                        name='<%# Eval("ID") %>' />
                                                </td>

                                                <td>USR-<%# Eval("ID") %></td>

                                                <td><%# Eval("CaseNumber") %></td>

                                                <td><%# Eval("OrderUniqueID") %></td>
                                                
                                                <td><%# Eval("IssueDescription") %></td>
                                               <%-- <td><%# Eval("Images") %></td>--%>
                                                <td><%# GetImages(Eval("Images").ToString()) %></td>

                                                <td>
                                                    <%# Convert.ToDateTime(Eval("CreatedOn")).ToString("dd/MMM/yyyy") %>
                                                </td>

                                                <td>
                                                    <select class="drp-border" onchange="PerformAction(this, '<%# Eval("ID") %>');">
                                                        <%# GetStatusOptions(Eval("Status")) %>
                                                    </select>
                                                </td>
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
        function PerformAction(sel) {
            alert(sel.value); 
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var msgDiv = $('#<%= divMsg.ClientID %>');
             if (msgDiv.text().trim() != "") {
                 msgDiv.show();
                 setTimeout(function () {
                     msgDiv.fadeOut('slow');
                 }, 10000);
             }
         });
    </script>


    <script type="text/javascript">
        var page = 1;
        var PageUrl = 'user-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'txtName::Name@txtEmail::Email@CPHContent_ddlSuburb::Suburb@CPHContent_ddlState::State@txtPostcode::Postcode';

        function Export() {
            window.location.href = 'user-list.aspx?ysnExport=1&txtName' + $('#txtName').val() + '=&txtEmail=' + $('#txtEmail').val() + '&txtPostcode=' + $('#txtPostcode').val() + '&ddlSuburb=' + $('.ddlSuburb').val() + '&ddlState=' + $('.ddlState').val();
        }

        function PerformAction(selectObj, recordID) {
            var selectedValue = selectObj.value;
            if (!selectedValue) return; 
            document.getElementById('<%= hdnID.ClientID %>').value = recordID;
            document.getElementById('<%= hdnType.ClientID %>').value = selectedValue;
            document.getElementById('<%= frmSearch.ClientID %>').submit();
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


