<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="profilephoto-list.aspx.cs" Inherits="AdminPanel_profilephoto_List" %>

<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div id="divProfilePhotoContainer" style="display:none">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Logo & Store Image</a></li>
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
                <form id="frmSearch" onsubmit="SubmitForm();return false;" action="profilephoto-list.aspx">
                    <div class="form-group">
                        <label for="txtTitle">Title</label>
                        <input type="text" class="form-control" name="txtTitle" id="txtTitle" maxlength="50" placeholder="Enter Title" />
                    </div>
                    <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                    <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />
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
                <h4><i class="icon-table"></i>Logo & Store Image</h4>
            </div>
            <div class="pull-right padding-right"><a class="btn btn-info" href="profilephoto-modify.aspx">Add New</a></div>
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
                                            <input class="checked" type="checkbox" onclick="CbxAll(this); GetSelRecord();" id="cbxAll" /></span></th>
                                        <th width="120">Photo</th>
                                        <th width="400">Vendor</th>
                                        <th>Title</th>
                                        <th style="text-align:center">Status</th>
                                        <th style="text-align:center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <span class="check">
                                                        <input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td>
                                                    <%--<img src="<%# Utility.Config.VirtualDir + Utility.Config.CMSFiles + Eval("ImageName") %>" width="100" />--%>
                                                     <img class="lazy " src="images/grey.gif" data-src="<%#Utility.Config.VirtualDir %>thumb.aspx?path=<%#Utility.Config.CMSFiles  %><%#Eval("ImageName") %>&width=100"  alt="" title="" />
                                                </td>
                                                <td>
                                                    <%#Convert.ToString(Eval("Vendor")).ToUpper()=="AVAILABLE" ? "<span class='text-primary'><strong>"+ Eval("Vendor") +"</strong></span>" : Eval("Vendor") %>
                                                </td>                                                
                                                <td><span class="text-primary1"><%#((System.Data.DataRowView)Container.DataItem)["Name"]%></span> </td>
                                                <td class="actions">
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'status','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                        <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i>
                                                    </a>
                                                </td>
                                                <td class="actions">
                                                    <a href="profilephoto-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a></td>
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
                            <Ctrl:Paging runat="server" ID="CtrlPage1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var lazyloadImages;

            if ("IntersectionObserver" in window) {
                lazyloadImages = document.querySelectorAll(".lazy");
                var imageObserver = new IntersectionObserver(function (entries, observer) {
                    entries.forEach(function (entry) {
                        if (entry.isIntersecting) {
                            var image = entry.target;
                            image.src = image.dataset.src;
                            image.classList.remove("lazy");
                            imageObserver.unobserve(image);
                        }
                    });
                });

                lazyloadImages.forEach(function (image) {
                    imageObserver.observe(image);
                });
            } else {
                var lazyloadThrottleTimeout;
                lazyloadImages = document.querySelectorAll(".lazy");

                function lazyload() {
                    if (lazyloadThrottleTimeout) {
                        clearTimeout(lazyloadThrottleTimeout);
                    }

                    lazyloadThrottleTimeout = setTimeout(function () {
                        var scrollTop = window.pageYOffset;
                        lazyloadImages.forEach(function (img) {
                            if (img.offsetTop < (window.innerHeight + scrollTop)) {
                                img.src = img.dataset.src;
                                img.classList.remove('lazy');
                            }
                        });
                        if (lazyloadImages.length == 0) {
                            document.removeEventListener("scroll", lazyload);
                            window.removeEventListener("resize", lazyload);
                            window.removeEventListener("orientationChange", lazyload);
                        }
                    }, 20);
                }

                document.addEventListener("scroll", lazyload);
                window.addEventListener("resize", lazyload);
                window.addEventListener("orientationChange", lazyload);
            }
        })
    </script>
    <script type="text/javascript">
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'profilephoto-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = "<%= divMsg.ClientID %>";
        var SearchControl = 'txtTitle::Title';

        $(window).load(function () {
            $('#divProfilePhotoContainer').show();
        });
    </script>
</asp:Content>

