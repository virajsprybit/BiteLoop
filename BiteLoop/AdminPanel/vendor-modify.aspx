<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="vendor-modify.aspx.cs" Inherits="AdminPanel_vendor_Modify" ValidateRequest="false" %>

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

        .equal-height {
            display: flex;
        }

        .equal-height > [class*="col-"] {
            display: flex;
        }

        .panel-equal {
            display: flex;
            flex-direction: column;
            width: 100%;
        }

        .panel-equal .panel-body {
            flex: 1;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Vendor Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">

        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <br />
                    <form id="frmAdmin" runat="server">
                        <div class="alert-message hide" id="divMsg" runat="server"></div>

                        <div class="col-lg-12">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Business Details</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">

                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtABN.ClientID%>">ABN</label><span class="red">*</span>
                                                <input type="text" class="form-control" id="txtABN" runat="server" name="txtABN" maxlength="100" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtBusinessName.ClientID%>">Business Name</label><span class="red">*</span>
                                                <input type="text" class="form-control" id="txtBusinessName" runat="server" name="txtBusinessName" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtBusinessMobile.ClientID%>">Mobile</label>
                                                <input id="txtBusinessMobile" name="txtBusinessMobile" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                        <%--<div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtLocation.ClientID%>">Street Address</label>
                                                <input type="text" class="form-control" id="txtAddress" runat="server" name="txtLocation" maxlength="500" />--%>
                                                <%--<input type="hidden" class="form-control" id="hdnLocation" runat="server" name="hdnLocation" maxlength="500" />--%>
                                            <%--</div>
                                        </div>--%>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtLocation.ClientID%>">Location</label>
                                                <input type="text" class="form-control" id="txtLocation" runat="server" name="txtLocation" maxlength="500" />
                                                <%--<input type="hidden" class="form-control" id="hdnLocation" runat="server" name="hdnLocation" maxlength="500" />--%>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtBusinessPhone.ClientID%>">Business Phone</label>
                                                <input id="txtBusinessPhone" name="txtBusinessPhone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="<%=txtBusinessEmail.ClientID%>">Email</label>
                                                <input id="txtBusinessEmail" name="txtBusinessEmail" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                        
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="ddlState">State</label>
                                                <select id="ddlState" name="ddlState" class="form-control" runat="server"></select>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>


                        <%--======================--%>
                        <div class="clearfix">
                        </div>
                        <div class="row equal-height">
                        <div class="col-lg-3">
                            <div class="panel panel-equal">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Select Business Type</h4>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-12">
                                        <asp:Repeater ID="rptRestaurantTypes" runat="server">
                                            <HeaderTemplate>
                                                <table width="90%" class="category">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="20">
                                                        <input type="checkbox" class="clsBusinessTypes" id="clsBusinessTypes" runat="server" value='<%# Eval("ID") %>' />
                                                    </td>
                                                    <td><%# "<b>" + Eval("Name") + "</b>" %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="panel panel-equal">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Description</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label>Description</label><span class="red">*</span>
                                            <%--<textarea class="form-control" name="tareaDescription" id="tareaDescription" runat="server" style="resize: none;" />--%>
                                            <uctrl:uctrlContent ID="tareaDescription" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="panel panel-equal">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Select Diet Type</h4>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-12">
                                        <asp:Repeater ID="DietaryType" runat="server">
                                            <HeaderTemplate>
                                                <table width="90%" class="category">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="20">
                                                        <input type="checkbox" class="clsDietTypes" id="clsDietTypes" runat="server" value='<%# Eval("Name") %>' />
                                                    </td>
                                                    <td><%# "<b>" + Eval("Name") + "</b>" %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                            </div>

                        <div class="col-lg-6">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>About Us</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label>About Us</label><span class="red">*</span>
                                            <%--<textarea class="form-control" name="tareaDescription" id="tareaDescription" runat="server" style="resize: none;" />--%>
                                            <uctrl:uctrlContent ID="tareaAboutUs" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="panel">
                                <div class="panel-heading">
                                    <div class="pull-left">
                                        <h4><i class="icon-th-large"></i>Bank Details</h4>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body well-lg">
                                    <div class="col-lg-12">
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
                                        <div class="form-group">
                                            <label for="<%=txtABN.ClientID%>">ABN</label><span class="red">*</span>
                                            <input type="text" class="form-control" id="txtABNBank" runat="server" name="txtABN" maxlength="100" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        

                        <div class="clearfix"></div>

                        <%--======================--%>

                        <div class="col-lg-12">
                            <div class="row">
                                <!-- Profile Photo Panel -->
                                <div class="col-lg-6">
                                    <div class="panel">
                                        <div class="panel-heading">
                                            <h4><i class="icon-th-large"></i>Profile Photo</h4>
                                        </div>

                                        <div class="panel-body well-lg">
                                            <asp:Image ID="imgProfile" runat="server" Width="200" Height="200"
                                                CssClass="img-thumbnail" />
                                            <asp:HiddenField ID="hdnOldPhoto" runat="server" />

                                            <div class="form-group mt-3">
                                                <label for="fupdImage">Upload New Photo</label>
                                                <input id="fupdImage" type="file" runat="server" class="form-control" />
                                                <asp:HiddenField ID="hdnProfilePhoto" runat="server" />
                                                <b>Note:</b> Image size approx. (200px x 200px)
                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <!-- Store Photo Panel -->
                                <div class="col-lg-6">
                                    <div class="panel">
                                        <div class="panel-heading">
                                            <div>
                                                <h4><i class="icon-th-large"></i>Store Photo</h4>
                                            </div>
                                            <%--<div class="clearfix"></div>--%>
                                        </div>
                                        <div class="panel-body well-lg">
                                            <asp:Image ID="imgStore" runat="server" Width="200" Height="200"
                                                CssClass="img-thumbnail" />
                                            <asp:HiddenField ID="hdnStorePhoto" runat="server" />

                                            <div class="form-group mt-3">
                                                <label for="StoreImage">Upload New Photo</label>
                                                <input id="StoreImage" type="file" runat="server" class="form-control" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                <b>Note:</b> Image size approx. (200px x 200px)
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>



                        <%--</div>--%>

                        <div class="clearfix"></div>

                        <div class="col-lg-3">
                            <div class="dayborder">
                                <legend>Current Day Schedule </legend>
                                <input type="hidden" id="hdnCurrentDayOn" name="hdnCurrentDayOn" class="hdnDay" rel="CurrentDay" runat="server" value="0" />
                                <div class="form-group">
                                    <table width="70%" style="background: none;">
                                        <tr>
                                            <td valign="top" width="50">
                                                <input type="radio" id="CurrentDayOn" name="CurrentDayOn" class="CurrentDayOn rdoDayOnOff" runat="server" rel="CurrentDay" value="1" /><span>Today</span></td>
                                            <td valign="top">
                                                <input type="radio" id="CurrentDayOff" name="CurrentDayOn" class="CurrentDayOff rdoDayOnOff" runat="server" rel="CurrentDay" value="0" /><span>Tomorrow</span></td>
                                        </tr>
                                    </table>
                                </div>
                                <%--<div class="divOpacityCurrentDay">--%>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="CurrentDayOriginalPrice1" runat="server"
                                                    name="CurrentDayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="CurrentDayDiscount1" runat="server"
                                                    name="CurrentDayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="CurrentDayNoOfItems1" runat="server"
                                                    name="CurrentDayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="CurrentDayOriginalPrice2" runat="server"
                                                    name="CurrentDayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="CurrentDayDiscount2" runat="server"
                                                    name="CurrentDayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="CurrentDayNoOfItems2" runat="server"
                                                    name="CurrentDayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="CurrentDayOriginalPrice3" runat="server"
                                                    name="CurrentDayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="CurrentDayDiscount3" runat="server"
                                                    name="CurrentDayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=CurrentDayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="CurrentDayNoOfItems3" runat="server"
                                                    name="CurrentDayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="<%=CurrentDayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                        <input type="text" class="form-control txtTime" id="CurrentDayFromTime" runat="server" name="CurrentDayFromTime" maxlength="500" />
                                    </div>
                                    <div class="form-group">
                                        <label for="<%=CurrentDayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                        <input type="text" class="form-control txtTime" id="CurrentDayToTime" runat="server" name="CurrentDayToTime" maxlength="500" />
                                    </div>
                                <%--</div>--%>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="dayborder">
                                <legend>Monday Schedule </legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="MondayOriginalPrice1" runat="server"
                                                    name="MondayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="MondayDiscount1" runat="server"
                                                    name="MondayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="MondayNoOfItems1" runat="server"
                                                    name="MondayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="MondayOriginalPrice2" runat="server"
                                                    name="MondayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="MondayDiscount2" runat="server"
                                                    name="MondayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="MondayNoOfItems2" runat="server"
                                                    name="MondayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="MondayOriginalPrice3" runat="server"
                                                    name="MondayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="MondayDiscount3" runat="server"
                                                    name="MondayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=MondayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="MondayNoOfItems3" runat="server"
                                                    name="MondayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
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
                                <legend>Tuesday Schedule </legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="TuesdayOriginalPrice1" runat="server"
                                                    name="TuesdayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="TuesdayDiscount1" runat="server"
                                                    name="TuesdayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="TuesdayNoOfItems1" runat="server"
                                                    name="TuesdayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="TuesdayOriginalPrice2" runat="server"
                                                    name="TuesdayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="TuesdayDiscount2" runat="server"
                                                    name="TuesdayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="TuesdayNoOfItems2" runat="server"
                                                    name="TuesdayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="TuesdayOriginalPrice3" runat="server"
                                                    name="TuesdayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="TuesdayDiscount3" runat="server"
                                                    name="TuesdayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=TuesdayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="TuesdayNoOfItems3" runat="server"
                                                    name="TuesdayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
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
                                <legend>Wednesday Schedule </legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="WednesdayOriginalPrice1" runat="server"
                                                    name="WednesdayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="WednesdayDiscount1" runat="server"
                                                    name="WednesdayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="WednesdayNoOfItems1" runat="server"
                                                    name="WednesdayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="WednesdayOriginalPrice2" runat="server"
                                                    name="WednesdayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="WednesdayDiscount2" runat="server"
                                                    name="WednesdayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="WednesdayNoOfItems2" runat="server"
                                                    name="WednesdayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="WednesdayOriginalPrice3" runat="server"
                                                    name="WednesdayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="WednesdayDiscount3" runat="server"
                                                    name="WednesdayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=WednesdayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="WednesdayNoOfItems3" runat="server"
                                                    name="WednesdayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
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
                                <legend>Thursday Schedule </legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="ThursdayOriginalPrice1" runat="server"
                                                    name="ThursdayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="ThursdayDiscount1" runat="server"
                                                    name="ThursdayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="ThursdayNoOfItems1" runat="server"
                                                    name="ThursdayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="ThursdayOriginalPrice2" runat="server"
                                                    name="ThursdayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="ThursdayDiscount2" runat="server"
                                                    name="ThursdayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="ThursdayNoOfItems2" runat="server"
                                                    name="ThursdayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="ThursdayOriginalPrice3" runat="server"
                                                    name="ThursdayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="ThursdayDiscount3" runat="server"
                                                    name="ThursdayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=ThursdayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="ThursdayNoOfItems3" runat="server"
                                                    name="ThursdayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label for="<%=ThursdayFromTime.ClientID%>">Pickup From Time(24 Hours Format HH:MM)</label>
                                        <input type="text" class="form-control txtTime" id="ThursdayFromTime" runat="server" name="ThirsdayFromTime" maxlength="500" />
                                    </div>
                                    <div class="form-group">
                                        <label for="<%=ThursdayToTime.ClientID%>">Pickup To Time(24 Hours Format HH:MM)</label>
                                        <input type="text" class="form-control txtTime" id="ThursdayToTime" runat="server" name="ThirsdayToTime" maxlength="500" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="dayborder">
                                <legend>Friday Schedule</legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="FridayOriginalPrice1" runat="server"
                                                    clientidmode="Static" disabled="disabled" />
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="FridayDiscount1" runat="server"
                                                    name="FridayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="FridayNoOfItems1" runat="server"
                                                    name="FridayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="FridayOriginalPrice2" runat="server"
                                                    name="FridayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="FridayDiscount2" runat="server"
                                                    name="FridayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="FridayNoOfItems2" runat="server"
                                                    name="FridayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="FridayOriginalPrice3" runat="server"
                                                    name="FridayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="FridayDiscount3" runat="server"
                                                    name="FridayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=FridayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="FridayNoOfItems3" runat="server"
                                                    name="FridayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
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
                                <legend>Saturday Schedule </legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="SaturdayOriginalPrice1" runat="server"
                                                    name="SaturdayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="SaturdayDiscount1" runat="server"
                                                    name="SaturdayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="SaturdayNoOfItems1" runat="server"
                                                    name="SaturdayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="SaturdayOriginalPrice2" runat="server"
                                                    name="SaturdayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="SaturdayDiscount2" runat="server"
                                                    name="SaturdayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="SaturdayNoOfItems2" runat="server"
                                                    name="SaturdayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="SaturdayOriginalPrice3" runat="server"
                                                    name="SaturdayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="SaturdayDiscount3" runat="server"
                                                    name="SaturdayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SaturdayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="SaturdayNoOfItems3" runat="server"
                                                    name="SaturdayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
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
                                <legend>Sunday Schedule</legend>
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
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayOriginalPrice1.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="SundayOriginalPrice1" runat="server"
                                                    name="SundayOriginalPrice1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayDiscount1.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="SundayDiscount1" runat="server"
                                                    name="SundayDiscount1" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayNoOfItems1.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="SundayNoOfItems1" runat="server"
                                                    name="SundayNoOfItems1" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayOriginalPrice2.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="SundayOriginalPrice2" runat="server"
                                                    name="SundayOriginalPrice2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayDiscount2.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="SundayDiscount2" runat="server"
                                                    name="SundayDiscount2" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayNoOfItems2.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="SundayNoOfItems2" runat="server"
                                                    name="SundayNoOfItems2" maxlength="5" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayOriginalPrice3.ClientID%>">Original Price</label>
                                                <input type="text" class="form-control" id="SundayOriginalPrice3" runat="server"
                                                    name="SundayOriginalPrice3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayDiscount3.ClientID%>">Discounted Price</label>
                                                <input type="text" class="form-control discount" id="SundayDiscount3" runat="server"
                                                    name="SundayDiscount3" maxlength="10" disabled="disabled"/>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="<%=SundayNoOfItems3.ClientID%>">No Of Items</label>
                                                <input type="text" class="form-control" id="SundayNoOfItems3" runat="server"
                                                    name="SundayNoOfItems3" maxlength="5" />
                                            </div>
                                        </div>
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


                        <%--<div class="col-lg-3">
                            <asp:Panel ID="pnlToday" runat="server">
                                <div class="dayborder" data-day="CurrentDay">
                                    <label>Current Day</label>
                                    <input type="hidden" class="hdnDay" rel="CurrentDay" value="0" />

                                    <div class="form-group">
                                        <div>
                                            <input type ="radio" ID="rdoTodayOn" runat="server" GroupName="CurrentDayOn" Text="ON" />
                                            &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoTodayOff" runat="server" GroupName="CurrentDayOn" Text="OFF" Checked="true" />
                                        </div>
                                        <div>
                                            <input type ="radio" ID="rdoTodaySelectToday" runat="server" GroupName="DaySelect" Text="Today" />
                                            &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoTodaySelectTomorrow" runat="server" GroupName="DaySelect" Text="Tomorrow" Checked="true" />
                                        </div>
                                    </div>

                                    <div class="divOpacityCurrentDay" style="pointer-events: auto;">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <label>No Of Items</label><br>
                                                    <asp:TextBox ID="txtTodayNoOfItems0" runat="server" CssClass="form-control" MaxLength="5" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Original Price</label><br>
                                                    <asp:TextBox ID="txtTodayOriginalPrice0" runat="server" CssClass="form-control" MaxLength="10" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Discounted Price</label><br>
                                                    <asp:TextBox ID="txtTodayDiscountedPrice0" runat="server" CssClass="form-control" MaxLength="10" Enabled="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>No Of Items</label><br>
                                                    <asp:TextBox ID="txtTodayNoOfItems1" runat="server" CssClass="form-control" MaxLength="5" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Original Price</label><br>
                                                    <asp:TextBox ID="txtTodayOriginalPrice1" runat="server" CssClass="form-control" MaxLength="10" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Discounted Price</label><br>
                                                    <asp:TextBox ID="txtTodayDiscountedPrice1" runat="server" CssClass="form-control" MaxLength="10" Enabled="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>No Of Items</label><br>
                                                    <asp:TextBox ID="txtTodayNoOfItems2" runat="server" CssClass="form-control" MaxLength="5" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Original Price</label><br>
                                                    <asp:TextBox ID="txtTodayOriginalPrice2" runat="server" CssClass="form-control" MaxLength="10" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Discounted Price</label><br>
                                                    <asp:TextBox ID="txtTodayDiscountedPrice2" runat="server" CssClass="form-control" MaxLength="10" Enabled="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Pickup From (HH:MM)</label><br>
                                                    <asp:TextBox ID="txtTodayFromTime" runat="server" CssClass="form-control" MaxLength="5" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Pickup To (HH:MM)</label><br>
                                                    <asp:TextBox ID="txtTodayToTime" runat="server" CssClass="form-control" MaxLength="5" Enabled="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>


                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Monday">
                                <label>Monday</label>

                                <input type="hidden" class="hdnDay" rel="Monday" value="0" />


                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="MondayOn" class="rdoDayOnOff" rel="Monday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="MondayOn" class="rdoDayOnOff" rel="Monday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>


                                <div class="divOpacityMonday">
                                    <table width="100%" border="0">

                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="MondayNoOfItems[]" class="form-control" maxlength="5">
                                            </td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="MondayOriginalPrice[]" class="form-control" maxlength="10">
                                            </td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="MondayDiscount[]" class="form-control" maxlength="10">
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="MondayNoOfItems[]" class="form-control" maxlength="5">
                                            </td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="MondayOriginalPrice[]" class="form-control" maxlength="10">
                                            </td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="MondayDiscount[]" class="form-control" maxlength="10">
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="MondayNoOfItems[]" class="form-control" maxlength="5">
                                            </td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="MondayOriginalPrice[]" class="form-control" maxlength="10">
                                            </td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="MondayDiscount[]" class="form-control" maxlength="10">
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <br>


                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="MondayFromTime" maxlength="5">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="MondayToTime" maxlength="5">
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Tuesday">
                                <label>Tuesday</label>

                                <input type="hidden" class="hdnDay" rel="Tuesday" value="0" />

                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="TuesdayOn" class="rdoDayOnOff" rel="Tuesday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="TuesdayOn" class="rdoDayOnOff" rel="Tuesday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>

                                <div class="divOpacityTuesday">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="TuesdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="TuesdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="TuesdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="TuesdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="TuesdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="TuesdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="TuesdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="TuesdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="TuesdayDiscount[]" class="form-control"></td>
                                        </tr>
                                    </table>
                                </div>

                                <br>

                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="TuesdayFromTime">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="TuesdayToTime">
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Wednesday">
                                <label>Wednesday</label>

                                <input type="hidden" class="hdnDay" rel="Wednesday" value="0" />

                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="WednesdayOn" class="rdoDayOnOff" rel="Wednesday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="WednesdayOn" class="rdoDayOnOff" rel="Wednesday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>

                                <div class="divOpacityWednesday">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="WednesdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="WednesdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="WednesdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="WednesdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="WednesdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="WednesdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="WednesdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="WednesdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="WednesdayDiscount[]" class="form-control"></td>
                                        </tr>
                                    </table>
                                </div>

                                <br>

                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="WednesdayFromTime">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="WednesdayToTime">
                                </div>

                            </div>
                        </div>
                        <div class="clearfix"></div>



                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Thursday">
                                <label>Thursday</label>

                                <input type="hidden" class="hdnDay" rel="Thursday" value="0" />

                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="ThursdayOn" class="rdoDayOnOff" rel="Thursday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="ThursdayOn" class="rdoDayOnOff" rel="Thursday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>

                                <div class="divOpacityThursday">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="ThursdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="ThursdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="ThursdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="ThursdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="ThursdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="ThursdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="ThursdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="ThursdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="ThursdayDiscount[]" class="form-control"></td>
                                        </tr>
                                    </table>
                                </div>

                                <br>

                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="ThursdayFromTime">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="ThursdayToTime">
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Friday">
                                <label>Friday</label>

                                <input type="hidden" class="hdnDay" rel="Friday" value="0" />

                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="FridayOn" class="rdoDayOnOff" rel="Friday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="FridayOn" class="rdoDayOnOff" rel="Friday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>

                                <div class="divOpacityFriday">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="FridayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="FridayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="FridayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="FridayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="FridayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="FridayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="FridayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="FridayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="FridayDiscount[]" class="form-control"></td>
                                        </tr>
                                    </table>
                                </div>

                                <br>

                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="FridayFromTime">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="FridayToTime">
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Saturday">
                                <label>Saturday</label>

                                <input type="hidden" class="hdnDay" rel="Saturday" value="0" />

                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="SaturdayOn" class="rdoDayOnOff" rel="Saturday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="SaturdayOn" class="rdoDayOnOff" rel="Saturday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>

                                <div class="divOpacitySaturday">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="SaturdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="SaturdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="SaturdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="SaturdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="SaturdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="SaturdayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="SaturdayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="SaturdayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="SaturdayDiscount[]" class="form-control"></td>
                                        </tr>
                                    </table>
                                </div>

                                <br>

                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="SaturdayFromTime">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="SaturdayToTime">
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dayborder" data-day="Sunday">
                                <label>Sunday</label>

                                <input type="hidden" class="hdnDay" rel="Sunday" value="0" />

                                <div class="form-group">
                                    <div>
                                        <input type="radio" name="SundayOn" class="rdoDayOnOff" rel="Sunday" value="1">
                                        <span>ON</span>
                                        &nbsp;&nbsp;&nbsp;
                    <input type="radio" name="SundayOn" class="rdoDayOnOff" rel="Sunday" value="0" checked>
                                        <span>OFF</span>
                                    </div>
                                </div>

                                <div class="divOpacitySunday">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="SundayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="SundayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="SundayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="SundayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="SundayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="SundayDiscount[]" class="form-control"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>No Of Items</label><br>
                                                <input type="text" name="SundayNoOfItems[]" class="form-control"></td>
                                            <td>
                                                <label>Original Price</label><br>
                                                <input type="text" name="SundayOriginalPrice[]" class="form-control"></td>
                                            <td>
                                                <label>Discounted Price</label><br>
                                                <input type="text" name="SundayDiscount[]" class="form-control"></td>
                                        </tr>
                                    </table>
                                </div>

                                <br>

                                <div class="form-group">
                                    <label>Pickup From Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="SundayFromTime">
                                </div>

                                <div class="form-group">
                                    <label>Pickup To Time (HH:MM)</label>
                                    <input type="text" class="form-control txtTime" name="SundayToTime">
                                </div>

                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="clearfix"></div>
                </div>
            </div>--%>
                </div>
                </form>
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
                <button type="button" class="btn btn-default" onclick="window.location='vendor-list.aspx';">Cancel</button>
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
            /*            $('#txtFullName.ClientID').focus();*/

            var SelectedProfilePhotoID = '<%=SelectedProfilePhotoID%>';
            $('.hdnProfilePhotoID').val(SelectedProfilePhotoID);
            $('#img' + SelectedProfilePhotoID).removeClass("imgNonSelected")
            $('#img' + SelectedProfilePhotoID).addClass("imgSelected");

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
            //$('.chkRestaurantTypesALL').change(function () {
            //    if ($(this).prop('checked')) {
            //        $('.clsRestaurantTypes').prop('checked', true);
            //    }
            //    else {
            //        $('.clsRestaurantTypes').prop('checked', false);
            //    }
            //});

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

                    for (var i = 0; i < CategorySplit.length; i++) {
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

                    for (var i = 0; i < FoodItemsSplit.length; i++) {
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

                    for (var i = 0; i < RestaurantTypesSplit.length; i++) {
                        if (jQuery.trim($(this).val()) == RestaurantTypesSplit[i]) {
                            $(this).prop('checked', true);
                        }
                    }
                });
                //Restaurant Types


            }
        });
        $(document).ready(function () {

            $('.rdoDayOnOff').change(function () {
                var day = $(this).attr('rel');
                var block = $('.dayborder[data-day="' + day + '"]');

                if ($(this).val() === '1') {
                    block.find('.divOpacity' + day).show();
                } else {
                    block.find('.divOpacity' + day).hide();
                }
            });

            // Set default to OFF
            $('#CurrentDayOff').trigger('change');

        });

        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
        $(document).ready(function () {
            // Force only one restaurant type to be selected
            $(document).on('change', '.clsRestaurantTypes', function () {
                // Uncheck all other checkboxes
                $('.clsRestaurantTypes').not(this).prop('checked', false);
            });
        });
        function ValidateForm() {

            var ErrMsg = '';
            $('#<%= hdnContent.ClientID %>').val(FillTextArea());
            // ErrMsg += DirValCtrl();

            if (jQuery.trim($('#<%=txtBusinessName.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Business Name';
            }
            <%--if (jQuery.trim($('#<%=txtFullName.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Full Name';
            }--%>
            if (jQuery.trim($('#<%=txtABN.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'ABN';
            }
            <%--if (jQuery.trim($('#<%=txtEmail.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Email Address';
            }
            if (jQuery.trim($('#<%=txtEmail.ClientID %>').val()) != '') {
                if (!IsEmail(jQuery.trim($('#<%=txtEmail.ClientID %>').val()))) {
                    ErrMsg += '<br/> - ' + 'Email address is not valid.';
                }
            }--%>
            <%--if (jQuery.trim($('#<%=txtPassword.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Password';
            }--%>
            <%--if (jQuery.trim($('#<%=txtStreetAddress.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Street Address';
            }--%>
            if (jQuery.trim($('#<%= hdnContent.ClientID %>').val()) == '') {
                ErrMsg += '<br />- Description';
            }

            <%--if (jQuery.trim($('#<%=txtPassword.ClientID %>').val()) != '') {
                if (jQuery.trim($('#<%=txtPassword.ClientID %>').val()).length < 6) {
                    ErrMsg += '<br/> - ' + 'Password must be at least 6 characters long.';
                }
            }
            if (jQuery.trim($('#<%=txtPassword.ClientID %>').val()) != '' && jQuery.trim($('#<%=txtConfPassword.ClientID %>').val()) != '') {
                if (jQuery.trim($('#<%=txtPassword.ClientID %>').val()) != jQuery.trim($('#<%=txtConfPassword.ClientID %>').val())) {
                    ErrMsg += '<br/> - ' + 'Password and Confirm Password must be same.';
                }
            }--%>


            <%--if (jQuery.trim($('#<%= txtBMHComission.ClientID %>').val()) == '') {
                ErrMsg += '<br />- BMH Commission Rate';
            }--%>

         <%--   if (jQuery.trim($('#<%=tareaDescription.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - ' + 'Description';
            }--%>


            //Category
            var Category = '';

            $('.clsCategory').each(function () {
                if ($(this).prop('checked')) {
                    Category = Category + jQuery.trim($(this).val()) + ',';
                }
            });
            if (Category.length > 0) {
                Category = Category.substring(0, Category.length - 1);
            }
            $('.hdnCategory').val(Category);
            //Category

            //Food Items
            var FoodItems = '';

            $('.clsFoodItems').each(function () {
                if ($(this).prop('checked')) {
                    FoodItems = FoodItems + jQuery.trim($(this).val()) + ',';
                }
            });
            if (FoodItems.length > 0) {
                FoodItems = FoodItems.substring(0, FoodItems.length - 1);
            }
            $('.hdnFoodItems').val(FoodItems);
            //Food Items

            //Restaurant Types
            var RestaurantTypes = '';

            $('.clsRestaurantTypes').each(function () {
                if ($(this).prop('checked')) {
                    RestaurantTypes = RestaurantTypes + jQuery.trim($(this).val()) + ',';
                }
            });
            if (RestaurantTypes.length > 0) {
                RestaurantTypes = RestaurantTypes.substring(0, RestaurantTypes.length - 1);
            }
            $('.hdnRestaurantTypes').val(RestaurantTypes);


            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
            }
            else {
                $('#' + divMsg).hide();
                $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');

                var formData = new FormData();

                formData.append('businessName', $('#<%= txtBusinessName.ClientID %>').val());
                formData.append('location', $('#<%= txtLocation.ClientID %>').val());
                formData.append('accountName', $('#<%= txtAccountName.ClientID %>').val());
                formData.append('state', $('#<%= ddlState.ClientID %>').val());
                formData.append('businessPhone', $('#<%= txtBusinessPhone.ClientID %>').val());
                formData.append('businessMobile', $('#<%= txtBusinessMobile.ClientID %>').val());
                formData.append('emailAddress', $('#<%= txtBusinessEmail.ClientID %>').val());
                formData.append('abn', $('#<%= txtABN.ClientID %>').val());
                formData.append('bsbno', $('#<%= txtBSBNo.ClientID %>').val());
                formData.append('aboutUs', $('#<%= tareaAboutUs.ClientID %>').val());
                //formData.append('aboutUs', $('#<%= tareaAboutUs.ClientID %>').html());
                //formData.append('description', $('#<%= tareaDescription.ClientID %>').val());
                //formData.append('description', $('#<%= tareaDescription.ClientID %>').val());
                //formData.append('description', $('#<%= tareaDescription.ClientID %>').text());
                //formData.append('description', $('#<%= tareaDescription.ClientID %>').html());
                formData.append('description', $('#<%= tareaDescription.ClientID %> div[contenteditable]').html() || "");
                formData.append('selectedBusiness', $('.clsBusinessTypes:checked').first().val() || '');
                formData.append('selectedDietTypes', $('.clsDietTypes:checked').map(function () {
                    return $(this).val();
                }).get().join(','));
                formData.append('currentDate',(new Date(
                        ($('#<%= CurrentDayOff.ClientID %>').is(':checked') ?
                        new Date().setDate(new Date().getDate() + 1) : new Date())
                )).toISOString().split('T')[0]
                );
                formData.append('accountNumber', $('#<%= txtAccountNo.ClientID %>').val());
                formData.append('bankName', $('#<%= txtBankName.ClientID %>').val());
                if ($('#<%= fupdImage.ClientID %>')[0].files.length > 0) {
                    formData.append('profilePhoto', $('#<%= fupdImage.ClientID %>')[0].files[0]);
                }
                if ($('#<%= StoreImage.ClientID %>')[0].files.length > 0) {
                    formData.append('storePhoto', $('#<%= StoreImage.ClientID %>')[0].files[0]);
                }

                formData.append('currentDayFromTime', $('#<%= CurrentDayFromTime.ClientID %>').val());
                formData.append('currentDayToTime', $('#<%= CurrentDayToTime.ClientID %>').val());
                formData.append('currentDayNoOfItems1', $('#<%= CurrentDayNoOfItems1.ClientID %>').val());
                formData.append('currentDayOriginalPrice1', $('#<%= CurrentDayOriginalPrice1.ClientID %>').val());
                formData.append('currentDayDiscount1', $('#<%= CurrentDayDiscount1.ClientID %>').val());
                formData.append('currentDayNoOfItems2', $('#<%= CurrentDayNoOfItems2.ClientID %>').val());
                formData.append('currentDayOriginalPrice2', $('#<%= CurrentDayOriginalPrice2.ClientID %>').val());
                formData.append('currentDayDiscount2', $('#<%= CurrentDayDiscount2.ClientID %>').val());
                formData.append('currentDayNoOfItems3', $('#<%= CurrentDayNoOfItems3.ClientID %>').val());
                formData.append('currentDayOriginalPrice3', $('#<%= CurrentDayOriginalPrice3.ClientID %>').val());
                formData.append('currentDayDiscount3', $('#<%= CurrentDayDiscount3.ClientID %>').val());

                formData.append('mondayFromTime', $('#<%= MondayFromTime.ClientID %>').val());
                formData.append('mondayToTime', $('#<%= MondayToTime.ClientID %>').val());
                formData.append('mondayNoOfItems1', $('#<%= MondayNoOfItems1.ClientID %>').val());
                formData.append('mondayOriginalPrice1', $('#<%= MondayOriginalPrice1.ClientID %>').val());
                formData.append('mondayDiscount1', $('#<%= MondayDiscount1.ClientID %>').val());
                formData.append('mondayNoOfItems2', $('#<%= MondayNoOfItems2.ClientID %>').val());
                formData.append('mondayOriginalPrice2', $('#<%= MondayOriginalPrice2.ClientID %>').val());
                formData.append('mondayDiscount2', $('#<%= MondayDiscount2.ClientID %>').val());
                formData.append('mondayNoOfItems3', $('#<%= MondayNoOfItems3.ClientID %>').val());
                formData.append('mondayOriginalPrice3', $('#<%= MondayOriginalPrice3.ClientID %>').val());
                formData.append('mondayDiscount3', $('#<%= MondayDiscount3.ClientID %>').val());

                formData.append('tuesdayFromTime', $('#<%= TuesdayFromTime.ClientID %>').val());
                formData.append('tuesdayToTime', $('#<%= TuesdayToTime.ClientID %>').val());
                formData.append('tuesdayNoOfItems1', $('#<%= TuesdayNoOfItems1.ClientID %>').val());
                formData.append('tuesdayOriginalPrice1', $('#<%= TuesdayOriginalPrice1.ClientID %>').val());
                formData.append('tuesdayDiscount1', $('#<%= TuesdayDiscount1.ClientID %>').val());
                formData.append('tuesdayNoOfItems2', $('#<%= TuesdayNoOfItems2.ClientID %>').val());
                formData.append('tuesdayOriginalPrice2', $('#<%= TuesdayOriginalPrice2.ClientID %>').val());
                formData.append('tuesdayDiscount2', $('#<%= TuesdayDiscount2.ClientID %>').val());
                formData.append('tuesdayNoOfItems3', $('#<%= TuesdayNoOfItems3.ClientID %>').val());
                formData.append('tuesdayOriginalPrice3', $('#<%= TuesdayOriginalPrice3.ClientID %>').val());
                formData.append('tuesdayDiscount3', $('#<%= TuesdayDiscount3.ClientID %>').val());

                formData.append('wednesdayFromTime', $('#<%= WednesdayFromTime.ClientID %>').val());
                formData.append('wednesdayToTime', $('#<%= WednesdayToTime.ClientID %>').val());
                formData.append('wednesdayNoOfItems1', $('#<%= WednesdayNoOfItems1.ClientID %>').val());
                formData.append('wednesdayOriginalPrice1', $('#<%= WednesdayOriginalPrice1.ClientID %>').val());
                formData.append('wednesdayDiscount1', $('#<%= WednesdayDiscount1.ClientID %>').val());
                formData.append('wednesdayNoOfItems2', $('#<%= WednesdayNoOfItems2.ClientID %>').val());
                formData.append('wednesdayOriginalPrice2', $('#<%= WednesdayOriginalPrice2.ClientID %>').val());
                formData.append('wednesdayDiscount2', $('#<%= WednesdayDiscount2.ClientID %>').val());
                formData.append('wednesdayNoOfItems3', $('#<%= WednesdayNoOfItems3.ClientID %>').val());
                formData.append('wednesdayOriginalPrice3', $('#<%= WednesdayOriginalPrice3.ClientID %>').val());
                formData.append('wednesdayDiscount3', $('#<%= WednesdayDiscount3.ClientID %>').val());

                formData.append('thursdayFromTime', $('#<%= ThursdayFromTime.ClientID %>').val());
                formData.append('thursdayToTime', $('#<%= ThursdayToTime.ClientID %>').val());
                formData.append('thursdayNoOfItems1', $('#<%= ThursdayNoOfItems1.ClientID %>').val());
                formData.append('thursdayOriginalPrice1', $('#<%= ThursdayOriginalPrice1.ClientID %>').val());
                formData.append('thursdayDiscount1', $('#<%= ThursdayDiscount1.ClientID %>').val());
                formData.append('thursdayNoOfItems2', $('#<%= ThursdayNoOfItems2.ClientID %>').val());
                formData.append('thursdayOriginalPrice2', $('#<%= ThursdayOriginalPrice2.ClientID %>').val());
                formData.append('thursdayDiscount2', $('#<%= ThursdayDiscount2.ClientID %>').val());
                formData.append('thursdayNoOfItems3', $('#<%= ThursdayNoOfItems3.ClientID %>').val());
                formData.append('thursdayOriginalPrice3', $('#<%= ThursdayOriginalPrice3.ClientID %>').val());
                formData.append('thursdayDiscount3', $('#<%= ThursdayDiscount3.ClientID %>').val());

                formData.append('fridayFromTime', $('#<%= FridayFromTime.ClientID %>').val());
                formData.append('fridayToTime', $('#<%= FridayToTime.ClientID %>').val());
                formData.append('fridayNoOfItems1', $('#<%= FridayNoOfItems1.ClientID %>').val());
                formData.append('fridayOriginalPrice1', $('#<%= FridayOriginalPrice1.ClientID %>').val());
                formData.append('fridayDiscount1', $('#<%= FridayDiscount1.ClientID %>').val());
                formData.append('fridayNoOfItems2', $('#<%= FridayNoOfItems2.ClientID %>').val());
                formData.append('fridayOriginalPrice2', $('#<%= FridayOriginalPrice2.ClientID %>').val());
                formData.append('fridayDiscount2', $('#<%= FridayDiscount2.ClientID %>').val());
                formData.append('fridayNoOfItems3', $('#<%= FridayNoOfItems3.ClientID %>').val());
                formData.append('fridayOriginalPrice3', $('#<%= FridayOriginalPrice3.ClientID %>').val());
                formData.append('fridayDiscount3', $('#<%= FridayDiscount3.ClientID %>').val());

                formData.append('saturdayFromTime', $('#<%= SaturdayFromTime.ClientID %>').val());
                formData.append('saturdayToTime', $('#<%= SaturdayToTime.ClientID %>').val());
                formData.append('saturdayNoOfItems1', $('#<%= SaturdayNoOfItems1.ClientID %>').val());
                formData.append('saturdayOriginalPrice1', $('#<%= SaturdayOriginalPrice1.ClientID %>').val());
                formData.append('saturdayDiscount1', $('#<%= SaturdayDiscount1.ClientID %>').val());
                formData.append('saturdayNoOfItems2', $('#<%= SaturdayNoOfItems2.ClientID %>').val());
                formData.append('saturdayOriginalPrice2', $('#<%= SaturdayOriginalPrice2.ClientID %>').val());
                formData.append('saturdayDiscount2', $('#<%= SaturdayDiscount2.ClientID %>').val());
                formData.append('saturdayNoOfItems3', $('#<%= SaturdayNoOfItems3.ClientID %>').val());
                formData.append('saturdayOriginalPrice3', $('#<%= SaturdayOriginalPrice3.ClientID %>').val());
                formData.append('saturdayDiscount3', $('#<%= SaturdayDiscount3.ClientID %>').val());

                formData.append('sundayFromTime', $('#<%= SundayFromTime.ClientID %>').val());
                formData.append('sundayToTime', $('#<%= SundayToTime.ClientID %>').val());
                formData.append('sundayNoOfItems1', $('#<%= SundayNoOfItems1.ClientID %>').val());
                formData.append('sundayOriginalPrice1', $('#<%= SundayOriginalPrice1.ClientID %>').val());
                formData.append('sundayDiscount1', $('#<%= SundayDiscount1.ClientID %>').val());
                formData.append('sundayNoOfItems2', $('#<%= SundayNoOfItems2.ClientID %>').val());
                formData.append('sundayOriginalPrice2', $('#<%= SundayOriginalPrice2.ClientID %>').val());
                formData.append('sundayDiscount2', $('#<%= SundayDiscount2.ClientID %>').val());
                formData.append('sundayNoOfItems3', $('#<%= SundayNoOfItems3.ClientID %>').val());
                formData.append('sundayOriginalPrice3', $('#<%= SundayOriginalPrice3.ClientID %>').val());
                formData.append('sundayDiscount3', $('#<%= SundayDiscount3.ClientID %>').val());

                $.ajax({
                    url: 'vendor-modify.aspx?id=<%= ID %>',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (resp) {
                        DisplMsg(divMsg, 'Business information has been saved successfully.', 'alert-message success');
                        window.setTimeout(function () { window.location.href = 'vendor-list.aspx'; }, 2000);
                    },
                    error: function (xhr, status, error) {
                        DisplMsg(divMsg, 'Error saving business information.', 'alert-message error');
                    }
                });
            }

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

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places&key=<%=GoogleMapApiKey %>"></script>
    <script type="text/javascript">
        google.maps.event.addDomListener(window, 'load', function () {
            var places = new google.maps.places.Autocomplete(document.getElementById('<%=txtLocation.ClientID %>'));
            google.maps.event.addListener(places, 'place_changed', function () {
                var place = places.getPlace();

               <%-- document.getElementById('<%=txtLocation.ClientID %>').value = place.formatted_address;
                document.getElementById('<%=txtLatitude.ClientID %>').value = place.geometry.location.lat();
                document.getElementById('<%=txtLongitude.ClientID %>').value = place.geometry.location.lng();--%>



                for (var ac = 0; ac < place.address_components.length; ac++) {
                    debugger;
                    var component = place.address_components[ac];

                    switch (component.types[0]) {
                        case 'administrative_area_level_1':
                            document.getElementById('<%=ddlState.ClientID %>').value = component.short_name;
                            break;
                    }
                };


            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Only allow one Business Type to be checked at a time
            $(document).on('change', '.clsBusinessTypes', function () {
                if ($(this).is(':checked')) {
                    $('.clsBusinessTypes').not(this).prop('checked', false);
                }
            });

        });
    </script>

</asp:Content>

