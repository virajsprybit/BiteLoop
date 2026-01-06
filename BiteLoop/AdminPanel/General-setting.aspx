<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="General-setting.aspx.cs" Inherits="General_setting" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        #frmGeneralSetting .form-control {
            border: 1px solid #e0e0e0 !important;
        }

        .datepicker td.day.disabled {
            color: #c5bfbf !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <form id="frmGeneralSetting" action="general-setting.aspx" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
        <div class="row">
            <div class="alert-message hide" id="divMsg" runat="server"></div>
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4><i class="icon-th-large"></i>Site Settings</h4>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body well-lg">
                        <div class="form-group" style="display: none">
                            <label for="<%= txtSiteUrl.ClientID %>">Site URL</label><span class="red">*</span>
                            <input type="text" class="form-control" id="txtSiteUrl" runat="server" name="tbxTitle" maxlength="50" />
                        </div>
                        <div class="form-group col-lg-12">
                            <label for="<%= txtInfoEmail.ClientID %>">
                                User Enquiry/Signup Email</label><span class="red">*</span>
                            <input id="txtInfoEmail" runat="server" name="txtInfoEmail" type="text" maxlength="250" class="form-control" />
                        </div>
                        <%--For Email--%>
                        <div class="form-group col-lg-12">
                            <label for="<%= txtabnno.ClientID %>">Vendor Enquiry/Signup Email</label>
                            <input id="txtabnno" runat="server" type="text" maxlength="50" class="form-control" />
                        </div>


                        <div class="form-group col-lg-12">
                            <label for="<%= txtMailto.ClientID %>">Support & Tech Issue Email</label>
                            <input id="txtMailto" runat="server" name="txtMailto" type="text" maxlength="250" class="form-control" />
                        </div>

                        <%--For Email--%>
                        <div class="form-group" style="display: none">
                            <label for="<%= txtWebsiteName.ClientID %>">
                                Website Name</label><span class="red">*</span>
                            <input id="txtWebsiteName" runat="server" name="txtWebsiteName" type="text" maxlength="250" class="form-control" />
                        </div>

                        <div class="clearfix"></div>

                        <div class="form-group col-lg-12">
                            <label for="<%= txtExternalLink.ClientID %>">Total Distance Covered in search(KM)</label><span class="red">*</span>
                            <input id="txtExternalLink" runat="server" name="txtExternalLink" type="text" maxlength="10" class="form-control" />
                        </div>



                        <div class="clearfix"></div>
                        <%--<div class="form-group col-lg-12">
                            <label for="<%= txttwittername.ClientID %>">Flat Transaction Fee($)</label>
                            <input id="txttwittername" runat="server" name="txttwittername" type="text" maxlength="50" class="form-control" />
                        </div>--%>


                        <div class="form-group col-lg-12">
                            <label for="<%= txttwitterfeedurl.ClientID %>">Term & Conditions</label>
                            <input id="txttwitterfeedurl" runat="server" name="txttwitterfeedurl" type="text" maxlength="100" class="form-control" />
                        </div>
                        <div class="form-group col-lg-12">
                            <label for="<%= txttelephoneno.ClientID %>">Privay Policy URL</label>
                            <input id="txttelephoneno" runat="server" name="txttelephoneno" type="text" maxlength="50" class="form-control" />
                        </div>
                        <div class="form-group hide">
                            <label for="<%= txtTradingHrs.ClientID %>">Reedeem Popup text</label><span></span>
                            <textarea name="txtTradingHrs" id="txtTradingHrs" runat="server" style="resize: none;" class="form-control" rows="3"></textarea>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-6">
                                <label for="<%= txtYoutube.ClientID %>">Together Saved Value (Impact & Rewards)</label>
                                <input id="txtYoutube" runat="server" name="txtYoutube" type="text" maxlength="10" class="form-control" />
                            </div>


                            <div class="form-group col-lg-6">

                                <label for="<%= txtSite.ClientID %>">Biteloop Fee(%)</label>
                                <input id="txtSite" runat="server" name="txtSite" type="text" maxlength="1000" class="form-control" />

                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-6">
                                <label for="<%= txtContactUs.ClientID %>">Business Registration Fee</label><span class="red">*</span>
                                <input type="number" id="txtContactUs" runat="server" name="txtContactUs" maxlength="5" class="form-control" />
                            </div>

                            <div class="form-group col-lg-6">
                                <label for="<%= txtMetaTitle.ClientID %>">GST %</label><span class="red">*</span>
                                <input id="txtMetaTitle" runat="server" name="txtMetaTitle" type="number" maxlength="250" class="form-control" />
                            </div>
                        </div>




                        <div class="col-lg-12">
                            <div class="form-group">
                                <label for="<%= txtAnalyticCode.ClientID %>">Vendor Report Summary</label>
                                <textarea name="txtAnalyticCode" id="txtAnalyticCode" runat="server" style="resize: none; height: 150px;" class="form-control"></textarea>
                            </div>

                        </div>


                        <%-- <div class="form-group">
                            <label for="<%= txtYoutube.ClientID %>">Refer Friend Amount($)</label>
                            <input id="txtReferFriendAmount" runat="server" name="txtReferFriendAmount" type="text" maxlength="10" class="form-control" />
                        </div--%>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="col-lg-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <h4><i class="icon-th-large"></i>Email Settings</h4>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body well-lg">
                            <div class="form-group">
                                <label for="<%= txtEmailaddress.ClientID %>">Email Address</label><span class="red">*</span>
                                <input id="txtEmailaddress" runat="server" name="txtEmailaddress" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtPassword.ClientID %>">
                                    Password</label><span class="red">*</span>
                                <input id="txtPassword" runat="server" name="txtPassword" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtHostName.ClientID %>">
                                    Host Name</label><span class="red">*</span>
                                <input id="txtHostName" runat="server" name="txtHostName" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group custom-check">
                                <label for="<%= chkssl.ClientID %>">Enable SSL</label>
                                <input type="checkbox" runat="server" id="chkssl" />
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-12" runat="server" visible="false">
                    <div class="panel">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <h4><i class="icon-th-large"></i>Rewards Points Settings</h4>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body well-lg">
                            <div class="col-lg-2">
                                <label for="<%= txtPinterest.ClientID %>" style="font-size: 14px">$1 Spent = </label>
                            </div>
                            <div class="col-lg-2">
                                <input id="txtPinterest" runat="server" name="txtPinterest" type="text" maxlength="5" class="form-control" />
                            </div>
                            <div class="col-lg-2">
                                <label style="font-size: 16px">Point</label>
                            </div>


                            <div class="clearfix"></div>
                            <br />
                            <div class="col-lg-2">
                                <label for="<%= txtin.ClientID %>" style="font-size: 14px">5000 Points = </label>
                            </div>

                            <div class="col-lg-2">
                                <input id="txtin" runat="server" name="txtin" type="text" maxlength="5" class="form-control discount txtin" />
                            </div>
                            <div class="col-lg-2">
                                <label style="font-size: 16px">$ Cashback</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12" runat="server" visible="false">
                    <div class="panel">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <h4><i class="icon-th-large"></i>Signup Rewards Points Settings</h4>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body well-lg">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Enable</label><span class="red">*</span><br />
                                    <input type="checkbox" id="chkEnable" name="chkEnable" class="chkEnable" runat="server" onclick="ShowRewards()" />
                                </div>
                            </div>
                            <div class="divSignupRewards" style="display: none">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label for="<%=txtStartDate.ClientID%>">Start Date</label><span class="red">*</span>
                                        <input id="txtStartDate" runat="server" name="txtStartDate" type="text" autocomplete="off" size="30" class="form-control txtStartDate" data-date-format="dd/M/yyyy" />
                                    </div>
                                </div>
                                <div class="col-lg-3">

                                    <div class="form-group">
                                        <label for="<%=txtCompletionDate.ClientID%>">End Date</label><span class="red">*</span>
                                        <input id="txtCompletionDate" runat="server" name="txtCompletionDate" type="text" size="30" autocomplete="off" class="form-control txtCompletionDate" data-date-format="dd/M/yyyy" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label for="<%= txtRewardsPoints.ClientID %>">Rewards Points</label><span class="red">*</span>
                                        <input type="text" class="form-control discount" id="txtRewardsPoints" runat="server" name="txtRewardsPoints" maxlength="100" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-lg-6" runat="server" visible="false">
                <div class="panel">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4><i class="icon-th-large"></i>Android Version & Priority</h4>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body well-lg">
                        <div class="clearfix"></div>
                        <b>Consumer APP</b>
                        <hr />
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtExternalVedio.ClientID %>">Android Current Version</label>
                                <input id="txtExternalVedio" runat="server" name="txtExternalVedio" type="text" maxlength="100" class="form-control" />
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtaddressfirstContact.ClientID %>">Android App Update Priority</label>
                                <select id="txtaddressfirstContact" name="txtaddressfirstContact" runat="server" class="form-control">
                                    <option value="">-- Select --</option>
                                    <option value="Low">Low</option>
                                    <option value="Medium">Medium</option>
                                    <option value="High">High</option>
                                </select>
                            </div>
                            <%--<textarea id="txtaddressfirstContact" runat="server" name="txtaddressfirstContact" style="resize: none;" class="form-control"></textarea>--%>
                        </div>
                        <div class="clearfix"></div>
                        <b>Business APP</b>
                        <hr />
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtTwitter.ClientID %>">Android Current Version</label>
                                <input id="txtTwitter" runat="server" name="txtTwitter" type="text" maxlength="250" class="form-control" />
                            </div>

                        </div>

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtaddresssecondContact.ClientID %>">Android App Update Priority</label>
                                <%--<textarea id="txtaddresssecondContact" runat="server" name="txtaddresssecondContact" style="resize: none;" class="form-control"></textarea>--%>
                                <select id="txtaddresssecondContact" name="txtaddresssecondContact" runat="server" class="form-control">
                                    <option value="">-- Select --</option>
                                    <option value="Low">Low</option>
                                    <option value="Medium">Medium</option>
                                    <option value="High">High</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6" runat="server" visible="false">
                <div class="panel">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4><i class="icon-th-large"></i>IOS Version & Priority</h4>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body well-lg">
                        <div class="clearfix"></div>
                        <b>Consumer APP</b>
                        <hr />
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtcopyright.ClientID %>">
                                    IOS Current Version</label><span></span>
                                <input id="txtcopyright" runat="server" type="text" maxlength="100" class="form-control" />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtMetaDesc.ClientID %>">IOS App Update Priority</label><span class="red">*</span>
                                <select id="txtMetaDesc" name="txtMetaDesc" runat="server" class="form-control">
                                    <option value="">-- Select --</option>
                                    <option value="Low">Low</option>
                                    <option value="Medium">Medium</option>
                                    <option value="High">High</option>
                                </select>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <b>Business APP</b>
                        <hr />
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="<%= txtaddressfirst.ClientID %>">
                                    IOS Current Version</label>
                                <input id="txtaddressfirst" name="txtaddressfirst" runat="server" type="text" maxlength="100" class="form-control" />
                                <%--<textarea id="txtaddressfirst" runat="server" rows="3" cols="50" name="txtaddressfirst" style="resize: none;" class="form-control"></textarea>--%>
                            </div>
                        </div>
                        <div class="col-lg-4">

                            <div class="form-group">
                                <label for="<%= txtaddresssecond.ClientID %>">
                                    IOS App Update Priority</label>
                                <select id="txtaddresssecond" name="txtaddresssecond" runat="server" class="form-control">
                                    <option value="">-- Select --</option>
                                    <option value="Low">Low</option>
                                    <option value="Medium">Medium</option>
                                    <option value="High">High</option>
                                </select>
                                <%--  <textarea id="txtaddresssecond" runat="server" name="txtaddresssecond" style="resize: none;" class="form-control"></textarea>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <div class="clearfix"></div>
            <div class="col-lg-12" style="display: none">
                <div class="panel">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4><i class="icon-th-large"></i>Site Settings</h4>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body well-lg">


                        <div class="form-group hide">
                            <label for="<%= txtFacebook.ClientID %>">Facebook Link</label>
                            <input id="txtFacebook" runat="server" name="txtFacebook" type="text" maxlength="250" class="form-control" />
                        </div>

                        <div class="form-group hide hide">

                            <label for="<%= txtFooterText.ClientID %>"></label>
                            <input id="txtFooterText" runat="server" name="txtFooterText" type="text" maxlength="250" class="form-control" />


                        </div>


                        <div class="col-lg-6">

                            <div class="form-group" style="display: none">
                                <label for="<%= txtMetaKeywords.ClientID %>">Meta Keywords</label><span class="red">*</span>
                                <input name="txtMetaKeywords" id="txtMetaKeywords" runat="server" type="text" class="form-control" maxlength="250" />
                            </div>



                        </div>

                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="panel-body well-lg">
                <div class="pull-right">
                    <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
                    <button type="submit" class="btn btn-info" runat="server" id="btnSave" onclick="return ValidateForm(this);">Save Information</button>
                    <%--<button type="button" class="btn btn-default" onclick="window.location='cms-list.aspx';">Cancel</button>--%>
                </div>
            </div>

        </div>
        <div class="row" style="display: none">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4><i class="icon-th-large"></i>General Settings</h4>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body well-lg">
                    <div class="col-lg-6">
                    </div>

                    <div class="col-lg-6">

                        <div class="form-group hide">
                            <label>Default CMS Image</label><span class="red">*</span>
                            <input id="fupdImage" runat="server" name="fupdImage" type="file" maxlength="500" class="input-short" />
                            <b>Note:</b>Image size approx.(152px Width x 112px Height)
                        </div>
                        <div class="form-group hide">
                            <img id="imgImage" src="" alt="" runat="server" visible="false" />
                            <img src="images/delete.png" alt="Remove" id="imgFreackle" title="Remove" onclick="RemoveFreackleImage(this)" style="cursor: pointer; display: none;" />
                            <input type="hidden" runat="server" id="hdnCMSFile" name="hdnCMSFile" />
                            <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                        </div>



                    </div>

                </div>
            </div>
        </div>
    </form>
    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            if ($('#<%=hdnImage.ClientID%>').val() == '') {
                $('#imgFreackle').hide();
            }
            else {
                $('#imgFreackle').show();
            }

        });
        var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {

            var ErrMsg = '';
        //ErrMsg += DirValidateCtrl();

           <%-- var fupdImage = '<%= fupdImage.ClientID %>';
            if (document.getElementById(fupdImage).value != '') {
                if (!isImage(document.getElementById(fupdImage))) {
                    ErrMsg = ErrMsg + '<br> - ' + 'Please upload only JPG, .JPEG, .PNG, .BMP, .GIF image files.';
                    document.getElementById(fupdImage).value = '';
                }
            }
            else {
                if ($("#<%= hdnImage.ClientID %>").val() == '') {
                    ErrMsg = ErrMsg + '<br> - Upload Image';

                }
            }--%>

            if ($('.txtin').val() != '') {
                debugger;
                if (parseFloat($('.txtin').val()) <= 0) {
                    ErrMsg = ErrMsg + '<br> - Rewards points cashback must be greater than 0';
                }
            }
            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
                return false;
            }
            else {

                $('#ctl00_CPHContent_divMsg').hide();
                return true;
            }
            return true;
        }
        function ScrollTop() {
            $('body,html').animate({
                scrollTop: 0
            }, 800);

        }
        function RemoveFreackleImage(imgFreackle) {
            if (confirm('Are you sure you want to remove advertisement image?')) {
                $(imgFreackle).hide();
                $('#<%=imgImage.ClientID %>').hide();
                $('#<%=hdnImage.ClientID %>').val('');
            }
        }
        function DirValidateCtrl(concatStr) {
            if (concatStr == undefined)
                concatStr = '<br/> - ';
            var lbl;
            var inctrl;
            var ErrMsg = '';
            var IsFirst = 0;
            $('span[class="red"]').each(function () {
                lbl = $(this).prev();
                if ($(this).prev().length > 0) {
                    inctrl = $(lbl).attr('for');
                    console.log(lbl)
                    if (inctrl.length > 0 && $(lbl).attr('for').length > 0) {
                        switch ($('#' + inctrl).attr('type')) {
                            case "textarea":
                            case "text":
                            case "file":
                            case "password":
                                if ($('#' + inctrl).val() == '' && $('#' + inctrl).is(':visible') == true) {
                                    $('#' + inctrl).addClass('valerror');
                                    ErrMsg = ErrMsg + concatStr + stripHTML($(lbl).html());
                                    if (IsFirst == 0)
                                        $('#' + inctrl).focus();
                                    IsFirst = 1;
                                } else
                                    $('#' + inctrl).removeClass('valerror');
                                $('#' + inctrl).attr('style', '');
                                break;
                            case "select":
                                if ($('#' + inctrl).val() == '')
                                    ErrMsg = ErrMsg + concatStr + stripHTML($(lbl).html());
                                else
                                    $('#' + inctrl).removeClass('valerror');
                                break;
                        }
                    }
                }
            });
            return trim(ErrMsg);
        }
    </script>
    <link href="js/datepicker1/css/datepicker.css" rel="stylesheet" />
    <script src="js/datepicker1/js/bootstrap-datepicker.js"></script>


    <script type="text/javascript">
        $(function () {
            //$(".txtStartDate").datepicker().on('changeDate', function (e) {
            //    $(this).datepicker('hide');
            //});
            //$(".txtCompletionDate").datepicker().on('changeDate', function (e) {
            //    $(this).datepicker('hide');
            //});


            $('.txtStartDate').datepicker().on('changeDate', function () {
                $(this).datepicker('hide');
                $('.txtCompletionDate').datepicker('setStartDate', new Date($(this).val()));

                if ($('.txtCompletionDate').val() != '') {

                    if (new Date($('.txtCompletionDate').val()) < new Date($('.txtStartDate').val())) {
                        $('.txtCompletionDate').val('');
                    }
                }

            });

            $('.txtCompletionDate').datepicker().on('changeDate', function () {
                $(this).datepicker('hide');
                //$('.txtStartDate').datepicker('setEndDate', new Date($(this).val()));
            });

            if ('<%= strEditStartDate%>' != '') {
                $('.txtStartDate').datepicker('setDate', new Date('<%= strEditStartDate%>'));
            }

            if ('<%= strEditEndDate%>' != '') {
                $('.txtCompletionDate').datepicker('setDate', new Date('<%= strEditEndDate%>'));
            }

            ShowRewards();
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

        function ShowRewards() {
            if ($('.chkEnable').prop('checked')) {
                $('.divSignupRewards').show();
            }
            else {
                $('.divSignupRewards').hide();
            }
        }
    </script>

</asp:Content>

