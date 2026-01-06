<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="user-modify.aspx.cs" Inherits="AdminPanel_vendor_Modify" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register Src="~/AdminPanel/includes/vendoreditor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        .dayborder {
            padding: 10px;
            border: 1px solid #c3c0c0;
            margin-bottom: 10px;
        }

        .brand-highlight, .brand-highlight td {
            background: none !important;
        }

        .category {
            height: 340px;
            overflow-x: hidden;
            overflow-y: auto;
            display: block;
            border: 1px solid #DEDEDE;
        }

            .category td {
                padding: 10px 0px 0px 10px;
            }

        .form-control {
            border: 1px solid #efebeb !important;
        }

        .opac {
            opacity: 0.4;
        }

            .opac input {
                color: transparent !important;
            }

            .opac select {
                color: transparent !important;
            }

        .imgSelected {
            border: 3px solid red;
            padding: 5px;
        }

        .imgNonSelected {
            border: 1px solid gray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Sales Admin Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">

        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <%--<div class="col-lg-12">
                        <table width="100%" style="background-color: transparent; display:none" id="tblMobileAPPCOpyClipboard" >--%>
                            <%--<tr>
                                <td width="130">
                                    <h4>Android App Link:  </h4>
                                </td>
                                <td width="420">
                                    <input type="text" disabled="disabled" class="form-control" value="<%= Config.WebSiteUrl %>BMHBusinessDetails/<%=ID %>"/>
                                </td>
                                <td align="left">
                                    <button class="btnAndroidCopyClipBoard" id="btnAndroidCopyClipBoard<%=ID %>" data-clipboard-text="<%= Config.WebSiteUrl %>BMHBusinessDetails/<%=ID %>" style="border: 0px; background-color: transparent">
                                        <img src="images/CopyClipBoard.png" height="30" />
                                    </button>


                                </td>
                                 <td width="100">
                                    <h4>IOS App Link:  </h4>
                                </td>
                                <td width="420">
                                    <input type="text" disabled="disabled" class="form-control" value="com.app.BringMeHomeConsumer://BMHBusinessDetails/<%=ID %>"/>
                                </td>
                                <td align="left">
                                    <button class="btnIOSCopyClipBoard" id="btnIOSCopyClipBoard<%=ID %>"  data-clipboard-text="com.app.BringMeHomeConsumer://BMHBusinessDetails/<%=ID %>" style="border: 0px; background-color: transparent">
                                        <img src="images/CopyClipBoard.png" height="30" />
                                    </button>

                                </td>
                            </tr>--%>
                       <%-- </table>


                    </div>
                    <br />--%>
                    <%-- <form id="frmAdmin" action="vendor-modify.aspx" onsubmit="return ValidateForm();return false;">--%>
                    <form id="UserModify" action="user-modify.aspx" onsubmit="return ValidateForm();return false;">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>

                        <%--===========================================--%>
                        <div class="col-lg-6">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Update User Details</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                        <input type="hidden" id="hdnUserID" name="hdnUserID" value="<%= Request.QueryString["id"] %>" />

                                        <div class="form-group">
                                            <label>User Name</label><span class="red">*</span>
                                            <input id="txtUserName" name="txtUserName" type="text" class="form-control" value="<%= txtUserNameValue ?? "" %>" />
                                        </div>

                                        <div class="form-group">
                                            <label>Password</label><span class="red">*</span>
                                            <input id="txtPassword" name="txtPassword" type="text" class="form-control" value="<%= txtPasswordValue%>"/>
                                        </div>

                                        <div class="form-group">
                                            <label>Email</label><span class="red">*</span>
                                            <input id="txtEmail" name="txtEmail" type="text" class="form-control" value="<%= txtEmailValue %>" />
                                        </div>

                                        <div class="form-group">
                                            <label>Post Code</label><span class="red">*</span>
                                            <input id="txtPostCode" name="txtPostCode" type="text" class="form-control" value="<%= txtPostCodeValue %>" />
                                        </div>

                                        <div class="form-group">
                                            <label>Mobile</label><span class="red">*</span>
                                            <input id="txtMobile" name="txtMobile" type="text" class="form-control" value="<%= txtMobileValue %>" />
                                        </div>

                                        <div class="pull-right">
                                            <button type="button" class="btn btn-primary" onclick="ValidateForm();">Save Information</button>
                                            <button type="button" class="btn btn-default" onclick="window.location='user-list.aspx';">Cancel</button>
                                        </div>
                                </div>
                            </div>
                        </div>
                        </form>

                        <%--===========================================--%>

                        <%--===========================================--%>
                        <div style="display:none">
                        <div style="display: none">
                            <div class="col-lg-6">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <div class="pull-left">
                                            <h4><i class="icon-th-large"></i>Personal Details - Step 2</h4>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="panel-body well-lg">

                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="<%=txtFullName.ClientID%>">First Name</label><span class="red">*</span>
                                                <input type="text" class="form-control" id="txtFullName" runat="server" name="txtFullName" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="<%=txtLastName.ClientID%>">Last Name</label><span class="red">*</span>
                                                <input type="text" class="form-control" id="txtLastName" runat="server" name="txtLastName" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=txtphone.ClientID%>">Mobile</label>
                                            <input id="txtphone" name="txtphone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                        </div>




                                    </div>
                                </div>

                            </div>
                            <div class="clearfix"></div>
                            <div class="col-lg-12">

                                <div class="panel">
                                    <div class="panel-heading">
                                        <div class="pull-left">
                                            <h4><i class="icon-th-large"></i>Business Details - Step 3</h4>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="panel-body well-lg">

                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="<%=txtBusinessName.ClientID%>">Business Name</label><span class="red">*</span>
                                                <input type="text" class="form-control" id="txtBusinessName" runat="server" name="txtBusinessName" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="<%=txtLocation.ClientID%>">Location</label>
                                                <input type="text" class="form-control" id="txtLocation" runat="server" name="txtLocation" maxlength="500" />
                                                <input type="hidden" class="form-control" id="hdnLocation" runat="server" name="hdnLocation" maxlength="500" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="ddlState">State</label>
                                                <select id="ddlState" name="ddlState" class="form-control" runat="server">
                                                </select>

                                            </div>
                                        </div>

                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="<%=txtBusinessPhone.ClientID%>">Business Phone</label>
                                                <input id="txtBusinessPhone" name="txtBusinessPhone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="<%=txtLatitude.ClientID%>">Latitude</label>
                                                <input id="txtLatitude" name="txtLatitude" type="text" size="30" maxlength="100" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="<%=txtLongitude.ClientID%>">Longitude</label>
                                                <input id="txtLongitude" name="txtLongitude" type="text" size="30" maxlength="100" runat="server" class="form-control" />
                                            </div>
                                        </div>



                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="<%=txtPersonIncharge.ClientID%>">Store Manager Name</label>
                                                <input type="text" class="form-control" id="txtPersonIncharge" runat="server" name="txtPersonIncharge" maxlength="100" />
                                            </div>

                                        </div>

                                        <div class="col-lg-6">

                                            <div class="form-group">
                                                <label for="<%=txtABN.ClientID%>">ABN</label><span class="red">*</span>
                                                <input type="text" class="form-control" id="txtABN" runat="server" name="txtABN" maxlength="100" />
                                            </div>

                                        </div>


                                        <div class="col-lg-6" style="padding-left: 0px">
                                            <div class="form-group">
                                                <label for="<%=txtComission.ClientID%>">Note</label>
                                                <input id="txtComission" runat="server" name="txtComission" type="text" size="30" maxlength="15" class="form-control" />

                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="<%=ddlStatus.ClientID%>">Status</label><span class="red">*</span>
                                                <select id="ddlStatus" name="ddlStatus" runat="server" class="form-control">
                                                    <option value="1">Active</option>
                                                    <option value="0">Inactive</option>
                                                    <option value="2">Cancelled</option>
                                                    <option value="3">Pending</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-3" style="padding-left: 0px">
                                            <div class="form-group">
                                                <label for="<%=txtComission.ClientID%>">BMH Commission Rate(%)</label><span class="red">*</span>
                                                <input id="txtBMHComission" runat="server" name="txtBMHComission" type="text" size="30" maxlength="6" class="form-control discount" />
                                            </div>
                                        </div>


                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="<%=ddlMultipleStore.ClientID%>">Multiple Store</label>
                                                <select id="ddlMultipleStore" name="ddlMultipleStore" runat="server" class="form-control">
                                                    <option value="0">No</option>
                                                    <option value="1">Yes</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="<%=ddlMultipleStore.ClientID%>">Encourage BYO Containers?</label>
                                                <select id="ddlBYOContainers" name="ddlBYOContainers" runat="server" class="form-control">
                                                    <option value="1">Yes</option>
                                                    <option value="0">No</option>
                                                    <option value="2">Unsure</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="form-group hide">
                                            <label for="<%=txtStreetAddress.ClientID%>">Street Address</label><span class="red">*</span>
                                            <input type="text" class="form-control" id="txtStreetAddress" runat="server" name="txtStreetAddress" maxlength="500" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                        <%--======================--%>
                        <div style="display:none">
                        <div class="clearfix"></div>
                        <div class="col-lg-6">

                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Restaurant Types & Food Types - Step 4 & 5</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-6">
                                        <asp:Repeater ID="rptRestaurantTypes" runat="server">
                                            <HeaderTemplate>
                                                <label>Restaurant Types</label>
                                                <table width="90%" class="category">
                                                    <tr>
                                                        <td style="border-bottom: 1px dashed #ced1d7">
                                                            <input type="checkbox" class="chkRestaurantTypesALL" />
                                                        </td>
                                                        <td style="border-bottom: 1px dashed #ced1d7; width: 100%"><b>ALL</b></td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="20">
                                                        <input type="checkbox" class="clsRestaurantTypes" id="chkRestaurantTypes<%#Eval("ID") %>" value="<%#Eval("ID") %>" /></td>
                                                    <td><%# "<b>" + Eval("Name") + "</b>" %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Repeater ID="rptFoodItems" runat="server">
                                            <HeaderTemplate>
                                                <label>Food Items</label>
                                                <table width="90%" class="category">
                                                    <tr>
                                                        <td style="border-bottom: 1px dashed #ced1d7">
                                                            <input type="checkbox" class="chkFoodItemsALL" />
                                                        </td>
                                                        <td style="border-bottom: 1px dashed #ced1d7; width: 100%"><b>ALL</b></td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="20">
                                                        <input type="checkbox" class="clsFoodItems" id="chk<%#Eval("ID") %>" value="<%#Eval("ID") %>" /></td>
                                                    <td><%# "<b>" + Eval("Name") + "</b>" %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-lg-2 hide">
                                        <asp:Repeater ID="rptCategory" runat="server">
                                            <HeaderTemplate>
                                                <label>Category</label>
                                                <table width="90%" class="category">
                                                    <tr>
                                                        <td style="border-bottom: 1px dashed #ced1d7">
                                                            <input type="checkbox" class="chkCategoryALL" />
                                                        </td>
                                                        <td style="border-bottom: 1px dashed #ced1d7; width: 100%"><b>ALL</b></td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="20">
                                                        <input type="checkbox" class="clsCategory" id="chkCategory<%#Eval("ID") %>" value="<%#Eval("ID") %>" /></td>
                                                    <td><%# "<b>" + Eval("Name") + "</b>" %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>

                                </div>
                            </div>
                        </div>
                            </div>
                        <div style="display:none">
                        <div class="col-lg-6">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Bank Details</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="form-group">
                                        <label for="<%=txtBSBNo.ClientID%>">BSB No</label>
                                        <input type="text" class="form-control" id="txtBSBNo" runat="server" name="txtBSBNo" maxlength="500" />
                                    </div>

                                    <div class="form-group">
                                        <label for="<%=txtAccountNo.ClientID%>">Account Number</label>
                                        <input type="text" class="form-control" id="txtAccountNo" runat="server" name="txtAccountNo" maxlength="500" />
                                    </div>
                                    <div class="form-group">
                                        <label for="<%=txtBankName.ClientID%>">Bank Name</label>
                                        <input type="text" class="form-control" id="txtBankName" runat="server" name="txtBankName" maxlength="500" />
                                    </div>

                                    <div class="form-group">
                                        <label for="<%=txtAccountName.ClientID%>">Account Name</label>
                                        <input type="text" class="form-control" id="txtAccountName" runat="server" name="txtAccountName" maxlength="500" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        </div>
                        <div style="display:none">
                        <div class="clearfix"></div>



                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Description</label><span class="red">*</span>
                                <%--<textarea class="form-control" name="tareaDescription" id="tareaDescription" runat="server" style="resize: none;" />--%>
                                <uctrl:uctrlContent ID="tareaDescription" runat="server" />
                            </div>
                        </div>
                        <div class="clearfix"></div>
                            </div>


                        <%--======================--%>
                        <div style="display:none">
                        <div class="col-lg-12">

                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Profile Photo</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg" style="height: 250px; overflow: auto; overflow-x: hidden">
                                    <asp:Repeater ID="rptProfilePhotos" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-2">
                                                <%--<img src="<%#Config.VirtualDir + Config.CMSFiles %><%#Eval("ImageName") %>" id="img<%#Eval("ID") %>" onclick="SelectProfilePhoto(this)" width="150" height="83" style="margin: 10px;" class="imgNonSelected clsProfilePhoto" />--%>
                                                <img class="lazy imgNonSelected clsProfilePhoto" src="images/grey.gif" data-src="<%#Config.VirtualDir %>thumb.aspx?path=<%#Config.CMSFiles  %><%#Eval("ImageName") %>&width=150&height=83" id="img<%#Eval("ID") %>" style="margin: 10px;" onclick="SelectProfilePhoto(this)" alt="" title="" />
                                            </div>
                                            <%#(Container.ItemIndex +1) == 6 ? "<div class='clearfix'></div>" : "" %>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <div class="col-lg-12">

                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Business Schedule - Step 6</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Monday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnMondayOn" name="hdnMondayOn" class="hdnDay" rel="Monday" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="MondayOn" name="MondayOn" class="MondayOn rdoDayOnOff" runat="server" rel="Monday" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="MondayOff" name="MondayOn" value="0" class="MondayOff rdoDayOnOff" rel="Monday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityMonday">
                                                <div class="form-group">
                                                    <label for="<%=MondayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="MondayNoOfItems" runat="server" name="MondayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=MondayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="MondayOriginalPrice" runat="server" name="MondayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=MondayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="MondayDiscount" runat="server" name="MondayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=MondayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="MondayFromTime" runat="server" name="MondayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=MondayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="MondayToTime" runat="server" name="MondayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Monday  Pickup Time 2</legend>
                                            <input type="hidden" id="hdnMonday2On" name="hdnMonday2On" class="hdnDay" rel="Monday2" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Monday2On" name="Monday2On" class="Monday2On rdoDayOnOff" runat="server" rel="Monday2" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Monday2Off" name="Monday2On" value="0" class="Monday2Off rdoDayOnOff" rel="Monday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityMonday2">
                                                <div class="form-group">
                                                    <label for="<%=Monday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Monday2NoOfItems" runat="server" name="Monday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Monday2OriginalPrice" runat="server" name="Monday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Monday2Discount" runat="server" name="Monday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Monday2FromTime" runat="server" name="Monday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Monday2ToTime" runat="server" name="Monday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Monday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnMonday3On" name="hdnMonday3On" class="hdnDay" rel="Monday3" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Monday3On" name="Monday3On" class="Monday3On rdoDayOnOff" runat="server" rel="Monday3" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Monday3Off" name="Monday3On" value="0" class="Monday3Off rdoDayOnOff" rel="Monday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityMonday3">
                                                <div class="form-group">
                                                    <label for="<%=Monday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Monday3NoOfItems" runat="server" name="Monday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Monday3OriginalPrice" runat="server" name="Monday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Monday3Discount" runat="server" name="Monday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Monday3FromTime" runat="server" name="Monday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Monday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Monday3ToTime" runat="server" name="Monday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Tuesday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnTuesdayOn" name="hdnTuesdayOn" class="hdnDay" rel="Tuesday" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="TuesdayOn" name="TuesdayOn" class="TuesdayOn rdoDayOnOff" rel="Tuesday" runat="server" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="TuesdayOff" name="TuesdayOn" value="0" class="TuesdayOff rdoDayOnOff" rel="Tuesday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityTuesday">
                                                <div class="form-group">
                                                    <label for="<%=TuesdayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="TuesdayNoOfItems" runat="server" name="TuesdayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=TuesdayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="TuesdayOriginalPrice" runat="server" name="TuesdayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=TuesdayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="TuesdayDiscount" runat="server" name="TuesdayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=TuesdayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="TuesdayFromTime" runat="server" name="TuesdayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=TuesdayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="TuesdayToTime" runat="server" name="TuesdayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Tuesday Pickup Time 2</legend>
                                            <input type="hidden" id="hdnTuesday2On" name="hdnTuesday2On" class="hdnDay" rel="Tuesday2" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Tuesday2On" name="Tuesday2On" class="Tuesday2On rdoDayOnOff" rel="Tuesday2" runat="server" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Tuesday2Off" name="Tuesday2On" value="0" class="Tuesday2Off rdoDayOnOff" rel="Tuesday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityTuesday2">
                                                <div class="form-group">
                                                    <label for="<%=Tuesday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Tuesday2NoOfItems" runat="server" name="Tuesday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Tuesday2OriginalPrice" runat="server" name="Tuesday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Tuesday2Discount" runat="server" name="Tuesday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Tuesday2FromTime" runat="server" name="Tuesday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Tuesday2ToTime" runat="server" name="Tuesday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Tuesday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnTuesday3On" name="hdnTuesday3On" class="hdnDay" rel="Tuesday3" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Tuesday3On" name="Tuesday3On" class="Tuesday3On rdoDayOnOff" rel="Tuesday3" runat="server" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Tuesday3Off" name="Tuesday3On" value="0" class="Tuesday3Off rdoDayOnOff" rel="Tuesday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityTuesday3">
                                                <div class="form-group">
                                                    <label for="<%=Tuesday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Tuesday3NoOfItems" runat="server" name="Tuesday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Tuesday3OriginalPrice" runat="server" name="Tuesday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Tuesday3Discount" runat="server" name="Tuesday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Tuesday3FromTime" runat="server" name="Tuesday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Tuesday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Tuesday3ToTime" runat="server" name="Tuesday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Wednesday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnWednesdayOn" name="hdnWednesdayOn" class="hdnDay" rel="Wednesday" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="WednesdayOn" name="WednesdayOn" class="WednesdayOn rdoDayOnOff" runat="server" rel="Wednesday" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="WednesdayOff" name="WednesdayOn" value="0" class="WednesdayOff rdoDayOnOff" rel="Wednesday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityWednesday">
                                                <div class="form-group">
                                                    <label for="<%=WednesdayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="WednesdayNoOfItems" runat="server" name="WednesdayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=WednesdayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="WednesdayOriginalPrice" runat="server" name="WednesdayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=WednesdayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="WednesdayDiscount" runat="server" name="WednesdayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=WednesdayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="WednesdayFromTime" runat="server" name="WednesdayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=WednesdayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="WednesdayToTime" runat="server" name="WednesdayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Wednesday Pickup Time 2</legend>
                                            <input type="hidden" id="hdnWednesday2On" name="hdnWednesday2On" class="hdnDay" rel="Wednesday2" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Wednesday2On" name="Wednesday2On" class="Wednesday2On rdoDayOnOff" runat="server" rel="Wednesday2" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Wednesday2Off" name="Wednesday2On" value="0" class="Wednesday2Off rdoDayOnOff" rel="Wednesday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityWednesday2">
                                                <div class="form-group">
                                                    <label for="<%=Wednesday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Wednesday2NoOfItems" runat="server" name="Wednesday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Wednesday2OriginalPrice" runat="server" name="Wednesday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Wednesday2Discount" runat="server" name="Wednesday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Wednesday2FromTime" runat="server" name="Wednesday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Wednesday2ToTime" runat="server" name="Wednesday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Wednesday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnWednesday3On" name="hdnWednesday3On" class="hdnDay" rel="Wednesday3" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Wednesday3On" name="Wednesday3On" class="Wednesday3On rdoDayOnOff" runat="server" rel="Wednesday3" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Wednesday3Off" name="Wednesday3On" value="0" class="Wednesday3Off rdoDayOnOff" rel="Wednesday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityWednesday3">
                                                <div class="form-group">
                                                    <label for="<%=Wednesday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Wednesday3NoOfItems" runat="server" name="Wednesday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Wednesday3OriginalPrice" runat="server" name="Wednesday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Wednesday3Discount" runat="server" name="Wednesday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Wednesday3FromTime" runat="server" name="Wednesday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Wednesday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Wednesday3ToTime" runat="server" name="Wednesday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Thursday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnThursdayOn" name="hdnThursdayOn" class="hdnDay" rel="Thursday" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="ThirsdayOn" name="ThirsdayOn" class="ThursdayOn rdoDayOnOff" runat="server" rel="Thursday" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="ThirsdayOff" name="ThirsdayOn" value="0" class="ThursdayOff rdoDayOnOff" rel="Thursday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityThursday">
                                                <div class="form-group">
                                                    <label for="<%=ThirsdayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="ThirsdayNoOfItems" runat="server" name="ThirsdayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=ThirsdayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="ThirsdayOriginalPrice" runat="server" name="ThirsdayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=ThirsdayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="ThirsdayDiscount" runat="server" name="ThirsdayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=ThirsdayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="ThirsdayFromTime" runat="server" name="ThirsdayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=ThirsdayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="ThirsdayToTime" runat="server" name="ThirsdayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Thursday Pickup Time 2</legend>
                                            <input type="hidden" id="hdnThursday2On" name="hdnThursday2On" class="hdnDay" rel="Thursday2" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Thirsday2On" name="Thirsday2On" class="Thursday2On rdoDayOnOff" runat="server" rel="Thursday2" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Thirsday2Off" name="Thirsday2On" value="0" class="Thursday2Off rdoDayOnOff" rel="Thursday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityThursday2">
                                                <div class="form-group">
                                                    <label for="<%=Thirsday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Thirsday2NoOfItems" runat="server" name="Thirsday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Thirsday2OriginalPrice" runat="server" name="Thirsday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Thirsday2Discount" runat="server" name="Thirsday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Thirsday2FromTime" runat="server" name="Thirsday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Thirsday2ToTime" runat="server" name="Thirsday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Thursday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnThursday3On" name="hdnThursday3On" class="hdnDay" rel="Thursday3" runat="server" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Thirsday3On" name="Thirsday3On" class="Thursday3On rdoDayOnOff" runat="server" rel="Thursday3" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Thirsday3Off" name="Thirsday3On" value="0" class="Thursday3Off rdoDayOnOff" rel="Thursday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityThursday3">
                                                <div class="form-group">
                                                    <label for="<%=Thirsday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Thirsday3NoOfItems" runat="server" name="Thirsday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Thirsday3OriginalPrice" runat="server" name="Thirsday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Thirsday3Discount" runat="server" name="Thirsday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Thirsday3FromTime" runat="server" name="Thirsday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Thirsday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Thirsday3ToTime" runat="server" name="Thirsday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Friday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnFridayOn" name="hdnFridayOn" runat="server" class="hdnDay" rel="Friday" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="FridayOn" name="FridayOn" class="FridayOn rdoDayOnOff" runat="server" rel="Friday" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="FridayOff" name="FridayOn" value="0" class="FridayOff rdoDayOnOff" rel="Friday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityFriday">
                                                <div class="form-group">
                                                    <label for="<%=FridayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="FridayNoOfItems" runat="server" name="FridayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=FridayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="FridayOriginalPrice" runat="server" name="FridayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=FridayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="FridayDiscount" runat="server" name="FridayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=FridayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="FridayFromTime" runat="server" name="FridayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=FridayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="FridayToTime" runat="server" name="FridayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Friday Pickup Time 2</legend>
                                            <input type="hidden" id="hdnFriday2On" name="hdnFriday2On" runat="server" class="hdnDay" rel="Friday2" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Friday2On" name="Friday2On" class="Friday2On rdoDayOnOff" runat="server" rel="Friday2" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Friday2Off" name="Friday2On" value="0" class="Friday2Off rdoDayOnOff" rel="Friday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityFriday2">
                                                <div class="form-group">
                                                    <label for="<%=Friday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Friday2NoOfItems" runat="server" name="Friday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Friday2OriginalPrice" runat="server" name="Friday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Friday2Discount" runat="server" name="Friday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Friday2FromTime" runat="server" name="Friday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Friday2ToTime" runat="server" name="Friday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Friday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnFriday3On" name="hdnFriday3On" runat="server" class="hdnDay" rel="Friday3" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Friday3On" name="Friday3On" class="Friday3On rdoDayOnOff" runat="server" rel="Friday3" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Friday3Off" name="Friday3On" value="0" class="Friday3Off rdoDayOnOff" rel="Friday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacityFriday3">
                                                <div class="form-group">
                                                    <label for="<%=Friday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Friday3NoOfItems" runat="server" name="Friday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Friday3OriginalPrice" runat="server" name="Friday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Friday3Discount" runat="server" name="Friday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Friday3FromTime" runat="server" name="Friday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Friday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Friday3ToTime" runat="server" name="Friday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Saturday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnSaturdayOn" name="hdnSaturdayOn" runat="server" class="hdnDay" rel="Saturday" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="SaturdayOn" name="SaturdayOn" class="SaturdayOn rdoDayOnOff" runat="server" rel="Saturday" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="SaturdayOff" name="SaturdayOn" value="0" class="SaturdayOff rdoDayOnOff" rel="Saturday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacitySaturday">
                                                <div class="form-group">
                                                    <label for="<%=SaturdayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="SaturdayNoOfItems" runat="server" name="SaturdayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SaturdayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="SaturdayOriginalPrice" runat="server" name="SaturdayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SaturdayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="SaturdayDiscount" runat="server" name="SaturdayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SaturdayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="SaturdayFromTime" runat="server" name="SaturdayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SaturdayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="SaturdayToTime" runat="server" name="SaturdayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Saturday Pickup Time 2</legend>
                                            <input type="hidden" id="hdnSaturday2On" name="hdnSaturday2On" runat="server" class="hdnDay" rel="Saturday2" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Saturday2On" name="Saturday2On" class="Saturday2On rdoDayOnOff" runat="server" rel="Saturday2" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Saturday2Off" name="Saturday2On" value="0" class="Saturday2Off rdoDayOnOff" rel="Saturday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacitySaturday2">
                                                <div class="form-group">
                                                    <label for="<%=Saturday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Saturday2NoOfItems" runat="server" name="Saturday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Saturday2OriginalPrice" runat="server" name="Saturday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Saturday2Discount" runat="server" name="Saturday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Saturday2FromTime" runat="server" name="Saturday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Saturday2ToTime" runat="server" name="Saturday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Saturday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnSaturday3On" name="hdnSaturday3On" runat="server" class="hdnDay" rel="Saturday3" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Saturday3On" name="Saturday3On" class="Saturday3On rdoDayOnOff" runat="server" rel="Saturday3" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Saturday3Off" name="Saturday3On" value="0" class="Saturday3Off rdoDayOnOff" rel="Saturday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacitySaturday3">
                                                <div class="form-group">
                                                    <label for="<%=Saturday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Saturday3NoOfItems" runat="server" name="Saturday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Saturday3OriginalPrice" runat="server" name="Saturday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Saturday3Discount" runat="server" name="Saturday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Saturday3FromTime" runat="server" name="Saturday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Saturday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Saturday3ToTime" runat="server" name="Saturday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Sunday Pickup Time 1</legend>
                                            <input type="hidden" id="hdnSundayOn" name="hdnSundayOn" runat="server" class="hdnDay" rel="Sunday" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="SundayOn" name="SundayOn" class="SundayOn rdoDayOnOff" runat="server" rel="Sunday" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="SundayOff" name="SundayOn" value="0" class="SundayOff rdoDayOnOff" rel="Sunday" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacitySunday">
                                                <div class="form-group">
                                                    <label for="<%=SundayNoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="SundayNoOfItems" runat="server" name="SundayNoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SundayOriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="SundayOriginalPrice" runat="server" name="SundayOriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SundayDiscount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="SundayDiscount" runat="server" name="SundayDiscount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SundayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="SundayFromTime" runat="server" name="SundayFromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=SundayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="SundayToTime" runat="server" name="SundayToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Sunday Pickup Time 2</legend>
                                            <input type="hidden" id="hdnSunday2On" name="hdnSunday2On" runat="server" class="hdnDay" rel="Sunday2" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Sunday2On" name="Sunday2On" class="Sunday2On rdoDayOnOff" runat="server" rel="Sunday2" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Sunday2Off" name="Sunday2On" value="0" class="Sunday2Off rdoDayOnOff" rel="Sunday2" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacitySunday2">
                                                <div class="form-group">
                                                    <label for="<%=Sunday2NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Sunday2NoOfItems" runat="server" name="Sunday2NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday2OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Sunday2OriginalPrice" runat="server" name="Sunday2OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday2Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Sunday2Discount" runat="server" name="Sunday2Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday2FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Sunday2FromTime" runat="server" name="Sunday2FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday2ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Sunday2ToTime" runat="server" name="Sunday2ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="dayborder">
                                            <legend>Sunday Pickup Time 3</legend>
                                            <input type="hidden" id="hdnSunday3On" name="hdnSunday3On" runat="server" class="hdnDay" rel="Sunday3" value="0" />
                                            <div class="form-group">
                                                <table width="70%" style="background: none;">
                                                    <tr>
                                                        <td valign="top" width="50">
                                                            <input type="radio" id="Sunday3On" name="Sunday3On" class="Sunday3On rdoDayOnOff" runat="server" rel="Sunday3" value="1" />&nbsp;<span>ON</span></td>
                                                        <td valign="top">
                                                            <input type="radio" id="Sunday3Off" name="Sunday3On" value="0" class="Sunday3Off rdoDayOnOff" rel="Sunday3" runat="server" />&nbsp;<span>OFF</span></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="divOpacitySunday3">
                                                <div class="form-group">
                                                    <label for="<%=Sunday3NoOfItems.ClientID%>">No Of Items</label>
                                                    <input type="text" class="form-control" id="Sunday3NoOfItems" runat="server" name="Sunday3NoOfItems" maxlength="5" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday3OriginalPrice.ClientID%>">Original Price</label>
                                                    <input type="text" class="form-control" id="Sunday3OriginalPrice" runat="server" name="Sunday3OriginalPrice" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday3Discount.ClientID%>">Discounted Price</label><br />
                                                    <input type="text" class="form-control discount" id="Sunday3Discount" runat="server" name="Sunday3Discount" maxlength="10" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday3FromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Sunday3FromTime" runat="server" name="Sunday3FromTime" maxlength="500" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="<%=Sunday3ToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                                    <input type="text" class="form-control txtTime" id="Sunday3ToTime" runat="server" name="Sunday3ToTime" maxlength="500" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>

                        </div>

                        <%--======================--%>


                        <div class="clearfix"></div>
                        <div class="pull-right">
                            <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
                            <input type="hidden" id="hdnCategory" runat="server" name="hdnCategory" class="hdnCategory" value="" />
                            <input type="hidden" id="hdnFoodItems" runat="server" name="hdnFoodItems" class="hdnFoodItems" value="" />
                            <input type="hidden" id="hdnRestaurantTypes" runat="server" name="hdnRestaurantTypes" class="hdnRestaurantTypes" value="" />
                            <input type="hidden" id="hdnProfilePhotoID" runat="server" name="hdnProfilePhotoID" class="hdnProfilePhotoID" value="0" />
                            <button type="button" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm()">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='user-list.aspx';">Cancel</button>
                        </div>
                    <%--</form>--%>
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
            
            if (parseInt('<%=ID%>') == 0) {
                $('#tblMobileAPPCOpyClipboard').hide();
            }
            else {
                $('#tblMobileAPPCOpyClipboard').show();
            }

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
        var divMsg = '<%= divMsg.ClientID %>';
        $(document).ready(function () {
            //$('#<%=txtFullName.ClientID %>').focus();

            <%--var SelectedProfilePhotoID = '<%=SelectedProfilePhotoID%>';
            $('.hdnProfilePhotoID').val(SelectedProfilePhotoID);
            $('#img' + SelectedProfilePhotoID).removeClass("imgNonSelected")
            $('#img' + SelectedProfilePhotoID).addClass("imgSelected");--%>

            $('.chkFoodItemsALL').change(function () {
                if ($(this).prop('checked')) {
                    $('.clsFoodItems').prop('checked', true);
                }
                else {
                    $('.clsFoodItems').prop('checked', false);
                }
            });
            $('.chkCategoryALL').change(function () {
                if ($(this).prop('checked')) {
                    $('.clsCategory').prop('checked', true);
                }
                else {
                    $('.clsCategory').prop('checked', false);
                }
            });
            $('.chkRestaurantTypesALL').change(function () {
                if ($(this).prop('checked')) {
                    $('.clsRestaurantTypes').prop('checked', true);
                }
                else {
                    $('.clsRestaurantTypes').prop('checked', false);
                }
            });

            $('.hdnDay').each(function () {
                if ($(this).val() == "0") {
                    $('.divOpacity' + $(this).attr('rel')).addClass('opac');
                    $('.' + $(this).attr('rel') + 'Off').attr('checked', true);
                }
                else {
                    $('.divOpacity' + $(this).attr('rel')).removeClass('opac');
                    $('.' + $(this).attr('rel') + 'On').attr('checked', true);
                }
            });

            $('.rdoDayOnOff').click(function () {
                if ($(this).val() == "0") {
                    $('.divOpacity' + $(this).attr('rel')).addClass('opac');
                }
                else {
                    $('.divOpacity' + $(this).attr('rel')).removeClass('opac');

                }
            });

            if (parseInt('<%=ID%>') > 0) {
                //Category
                var Category = $('#<%=hdnCategory.ClientID%>').val();
                var CategorySplit = Category.split(',');

                $('.clsCategory').each(function () {

                    for (var i = 0; i < CategorySplit.length ; i++) {
                        if (jQuery.trim($(this).val()) == CategorySplit[i]) {
                            $(this).prop('checked', true);
                        }
                    }
                });
                //Category


                //Food Items
                var FoodItems = $('#<%=hdnFoodItems.ClientID%>').val();
                var FoodItemsSplit = FoodItems.split(',');

                $('.clsFoodItems').each(function () {

                    for (var i = 0; i < FoodItemsSplit.length ; i++) {
                        if (jQuery.trim($(this).val()) == FoodItemsSplit[i]) {
                            $(this).prop('checked', true);
                        }
                    }
                });
                //Food Items

                //Restaurant Types
                var RestaurantTypes = $('#<%=hdnRestaurantTypes.ClientID%>').val();
                var RestaurantTypesSplit = RestaurantTypes.split(',');

                $('.clsRestaurantTypes').each(function () {

                    for (var i = 0; i < RestaurantTypesSplit.length ; i++) {
                        if (jQuery.trim($(this).val()) == RestaurantTypesSplit[i]) {
                            $(this).prop('checked', true);
                        }
                    }
                });
                //Restaurant Types


            }
        });
        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
        function ValidateForm() {

            var ErrMsg = '';

            if ($.trim($('#txtUserName').val()) == '') {
                ErrMsg += '<br/> - User Name is required';
                console.warn("Validation: Username empty");
            }
            if ($.trim($('#txtPassword').val()) == '') {
                ErrMsg += '<br/> - Password is required';
                console.warn("Validation: Password empty");
            }
            else if ($.trim($('#txtPassword').val()).length < 6) {
                ErrMsg += '<br/> - Password must be at least 6 characters long.';
                console.warn("Validation: Password too short");
            }
            if ($.trim($('#txtEmail').val()) == '') {
                ErrMsg += '<br/> - Email Address is required';
                console.warn("Validation: Email empty");
            }
            else if (!IsEmail($.trim($('#txtEmail').val()))) {
                ErrMsg += '<br/> - Email invalid';
                console.warn("Validation: Invalid email format");
            }
            if ($.trim($('#txtPostCode').val()) == '') {
                ErrMsg += '<br/> - Post Code is required';
                console.warn("Validation: PostCode empty");
            }
            if ($.trim($('#txtMobile').val()) == '') {
                ErrMsg += '<br/> - Mobile Number is required';
                console.warn("Validation: Mobile empty");
            }

            if (ErrMsg.length != 0) {
                console.error("Validation failed with errors:", ErrMsg);
                $('#divMsg').html('<div class="alert alert-danger">Please correct the following error(s):' + ErrMsg + '</div>');
                $('html, body').animate({ scrollTop: 0 }, 'fast');
                return false;
            }

            $('#divMsg').hide();

            if ($.showprogress) {
                $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
            } else {
                console.warn("$.showprogress() not found");
            }

            console.log("Form serialized:", $('#UserModify').serialize());

            $.ajax({
                url: 'user-modify.aspx',
                data: $('#UserModify').serialize(),
                type: 'POST',
                headers: { 'X-Requested-With': 'XMLHttpRequest' },

                success: function (resp) {
                    console.log("AJAX success triggered");
                    console.log("Raw response:", resp);

                    var trimmed = $.trim(resp);
                    console.log("Trimmed response:", trimmed);

                    if (trimmed === 'duplicate') {
                        console.warn("Server returned: duplicate");
                        $('#divMsg').html('<div class="alert alert-danger">This email address already exists. Please try another.</div>');
                    }
                    else if (trimmed === 'success') {
                        console.log("Server returned: success");
                        $('#divMsg').html('<div class="alert alert-success">User information has been updated successfully.</div>');
                        setTimeout(function () {
                            console.log("Redirecting to user-list.aspx");
                            window.location.href = 'user-list.aspx';
                        }, 2000);
                    }
                    else {
                        console.warn("Unexpected server response:", trimmed);
                        $('#divMsg').html('<div class="alert alert-warning">Unexpected response: ' + trimmed + '</div>');
                    }

                    if ($.hideprogress) {
                        $.hideprogress();
                    }
                    $('html, body').animate({ scrollTop: 0 }, 'fast');
                },

                error: function (xhr, status, error) {
                    console.error("AJAX error:", status, error);
                    console.error("Response text:", xhr.responseText);
                    $('#divMsg').html('<div class="alert alert-danger">AJAX error: ' + error + '</div>');
                    if ($.hideprogress) $.hideprogress();
                }
            });

            return false;
        }



        $(".txtTime").keydown(function (e) {
            console.log(e.keyCode);
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 186, 59]) !== -1 ||
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }

            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
        $(".discount").keydown(function (e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }

            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        function SelectProfilePhoto(ele) {
            $('.clsProfilePhoto').removeClass("imgNonSelected").removeClass("imgSelected");
            $('.clsProfilePhoto').addClass("imgNonSelected");
            $(ele).removeClass("imgNonSelected")
            $(ele).addClass("imgSelected");
            $('.hdnProfilePhotoID').val($('.imgSelected').attr("ID").replace("img", ""));
        }
    </script>

    <script type="text/javascript">
        //google.maps.event.addDomListener(window, 'load', function () {
        //    //var places = new google.maps.places.Autocomplete(document.getElementById('<%=txtLocation.ClientID %>'));
            //google.maps.event.addListener(places, 'place_changed', function () {
            //    var place = places.getPlace();

            //    //document.getElementById('<%=txtLocation.ClientID %>').value = place.formatted_address;
                ////document.getElementById('<%=txtLatitude.ClientID %>').value = place.geometry.location.lat();
                ////document.getElementById('<%=txtLongitude.ClientID %>').value = place.geometry.location.lng();



                //for (var ac = 0; ac < place.address_components.length; ac++) {
                //    debugger;
                //    var component = place.address_components[ac];

                //    switch (component.types[0]) {
                //        case 'administrative_area_level_1':
        //      //              document.getElementById('<%=ddlState.ClientID %>').value = component.short_name;
        //                    break;
        //            }
        //        };


        //    });
        //});
    </script>

</asp:Content>

