<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>


<%@ Import Namespace="Utility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title><%= new BAL.GeneralSettings().getConfigValue("WebsiteName").ToString()%> | Login</title>
    <!-- Bootstrap core CSS -->
    <link href="dist/css/bootstrap.css" rel="stylesheet" />
    <!-- Boostrap Theme -->
    <link href="css/skin.css" rel="stylesheet" />
    <link href="css/boostrap-overrides.css" rel="stylesheet" />
    <link href="css/theme.css" rel="stylesheet" />
    <!-- Ez mark-->
    <link rel="stylesheet" href="assets/css/ezmark.css" />
    <!-- Font Awesome-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <!-- Animate-->
    <link href="assets/css/animate-custom.css" rel="stylesheet" />
    <link href="js/jquery-alerts/jquery.alerts.css" rel="stylesheet" />

    <!-- Bootstrap core JavaScript -->
    <script src="assets/js/jquery.js"></script>
    <script src="dist/js/bootstrap.js"></script>
    <script type="text/javascript" src="assets/js/general.js"></script>
    <script src="assets/js/jquery.alerts.js"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
    <script src="assets/js/html5shiv.js"></script>
    <script src="assets/js/respond.min.js"></script>
    <![endif]-->
</head>
<body class="bg-med" style="background-color: #dce1eb">

    <div id="login" class="container">
        <div class="row">
            <div class="center-panel" style="margin-top: 10%">
                <div class="col-md-3 col-sm-2 col-xs-1"></div>
                <div class="col-md-6 col-sm-8 col-xs-10 text-center">

                    <div class="custom-check animated fadeInDown">
                        <span class="padding-bottom avatar avatar-md">
                            <img src="images/BiteLooplogo.png" alt="logo" width="300" style="margin-bottom: 20px;" class="img-circle1" /></span>
                        <div class="notification n-success" id="divMsg" runat="server" style="display: none;">Please log in.</div>
                        <div id="divUserLogin">
                            <form class="form-signin form-group" id="frmUser" onsubmit="PostForm(); return false;" method="post">
                            
                                <div id="divUserNameDetails">
                                    <div class="input-stacked input-group-lg">
                                        <input type="text" class="form-control" style="border-radius: 0px; margin-bottom: 13px;" placeholder="Email Address or User Name" id="tbxUsername" name="tbxUsername" autofocus autocomplete="off" />
                                        <input type="password" class="form-control" style="border-radius: 0px;" id="tbxPassword" name="tbxPassword" placeholder="Password" autocomplete="off"/>
                                    </div>
                                    <button class="btn btn-lg btn-primary btn-block" type="submit" id="btnLogin" onclick="t='login'">Next</button>
                                </div>
                                <div id="divOTPDetails" style="display: none">
                                    <label style="text-align:left;width:100%;padding-bottom:5px;">We have sent OTP in your registered email address</label>
                                    <div class="input-stacked input-group-lg">
                                        <input type="text" class="form-control" style="border-radius: 0px; margin-bottom: 13px;" placeholder="Enter OTP" id="tbxOTP" name="tbxOTP" autofocus autocomplete="off" onkeypress="return runScript(event)" />
                                        <input type="hidden" class="form-control" style="border-radius: 0px;display:none" id="tbxP" name="tbxP" autocomplete="off" />
                                    </div>
                                    <button class="btn btn-lg btn-primary btn-block" type="submit" id="btnLogin1" onclick="t='otp'">Submit</button>
                                </div>
                                <img src="images/login-loader.gif"  style="display:none" id="imgLoading"/>
                            </form>
                        </div>
                        <div id="divUserForgetPwd" style="display: none;">
                            <form id="frmUserForgetPwd" onsubmit="t='forgetpwd';PostForm();return false;">
                                <div class="input-stacked input-group-lg">
                                    <input type="text" class="form-control" placeholder="Email Address" style="border-radius: 0px;" id="tbxEmailAddress" name="tbxEmailAddress" maxlength="100" />
                                </div>
                                <button type="submit" class="btn btn-lg btn-primary btn-block" id="btnForgotPwd" onclick="t='forgetpwd'" value="Submit">Submit</button>
                                <div id="progressbar"></div>
                            </form>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-2 col-xs-1"></div>
                    <div class="clearfix"></div>
                    <br />
                    <a id="lnkLogin" class="text-transparent" style="display: none;" href="javascript:;" onclick="ShowHideCtrl('divUserForgetPwd','divUserLogin');ShowHideCtrl('lnkforgotPass','lnkLogin');">Forgot Password? </a>
                    <a style="display: none;" id="lnkforgotPass" class="text-transparent" href="javascript:;" onclick="ShowHideCtrl('divUserLogin','divUserForgetPwd');ShowHideCtrl('lnkLogin','lnkforgotPass');"><< Back to login </a>
                    <span class="text-transparent">&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </div>
            </div>
        </div>
    </div>

    <!-- Plugins & Custom -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!-- EzMark -->
    <script src="assets/js/jquery.ezmark.js"></script>
    <script type="text/javascript">
        $(function () { $('.custom-check input').ezMark(); });
    </script>

    <!-- Custom -->
    <script src="assets/js/script.js"></script>

    <script src="dist/js/bootstrap.js"></script>


    <script type="text/javascript">
        var divMsg = "<%= divMsg.ClientID %>";
        $('#tbxUsername').focus();
        var t = '';
        function PostForm() {            
            //$('#progressbar').html('<div class="progress"><div class="progress-bar progress-bar-success" aria-valuetransitiongoal="100"></div></div>');
            //$('.progress .progress-bar').progressbar(); 
            $('#<%=divMsg.ClientID %>').css('display', 'none');
            if (t == 'login') {
                var err = '';

                if (jQuery.trim($('#tbxUsername').val()) == '' || jQuery.trim($('#tbxUsername').val()) == 'Email Address') {
                    err = '<br />- Email Address is required.';
                }
                else {
                    var a = jQuery.trim($('#tbxUsername').val().replace('<', ''));

                }

                if (jQuery.trim($('#tbxPassword').val()) == '' || jQuery.trim($('#tbxPassword').val()) == 'Password') err += '<br />- Password is required.';

                if (err != '') {
                    if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }
                    msgbox(HeaderText + err, 'Error Message');
                    //DisplMsg(divMsg, HeaderText + err, 'alert-message error')
                    return false;
                }
                $('#btnLogin').attr('disabled', true);
                $('#imgLoading').show();
                $.ajax({

                    url: 'login.aspx?type=' + t,
                    data: $('#frmUser').serialize(),
                    type: 'POST',
                    success: function (resp) {                        
                        //$('#divMsg').html(resp);
                        $('#btnLogin').attr('disabled', false);
                        $('#imgLoading').hide();
                        if (resp == 'invalid') {
                            msgbox('<b>Invalid User Name or Password</b>', 'LOGIN'); return false;
                        }
                        else if (resp == 'success') {
                            $('#tbxP').val(jQuery.trim($('#tbxPassword').val()));
                            $('#divUserNameDetails').hide();
                            $('#divOTPDetails').slideToggle('slow');
                        }
                    }

                });

            }
            else if (t == 'otp') {
                var err = '';

                if (jQuery.trim($('#tbxOTP').val()) == '' || jQuery.trim($('#tbxOTP').val()) == 'Enter OTP') err += '<br />- OTP is required.';

                if (err != '') {
                    if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }
                    msgbox(HeaderText + err, 'Error Message');                    
                    return false;
                }
                $.ajax({

                    url: 'login.aspx?type=' + t,
                    data: $('#frmUser').serialize(),
                    type: 'POST',
                    success: function (resp) {                        
                        if (resp == 'wrongotp') {
                            msgbox('<b>You have entered Invalid OTP. <br>The maximum retry attempts allowed are 3.</b>', 'LOGIN'); return false;
                        }
                        else if (resp == 'locked') {
                            msgbox('<b>Your account is locked. Kindly contact Administrator.</b>', 'LOGIN');
                            window.setTimeout('RedirectToLoginPage()', 3000);
                            return false;
                        }
                        else{
                            $('#divMsg').html(resp);
                        }
                    }

                });

            }
            else {

                var err = '';
                if (jQuery.trim($('#tbxEmailAddress').val()) == '' || jQuery.trim($('#tbxEmailAddress').val()) == 'Email') {
                    err = '<br />- Email';
                }
                else {
                    if (!isValidEmailAddress(jQuery.trim($('#tbxEmailAddress').val()))) {
                        err += '<br/> - Invalid Email';
                    }
                }

                if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }

                $.ajax({
                    url: 'login.aspx?type=' + t,
                    data: $('#frmUserForgetPwd').serialize(),
                    type: 'POST',
                    success: function (resp) { $('#divMsg').html(resp); $('#tbxEmailAddress').val(''); }
                });

            }

            return false;
        }
        function RedirectToLoginPage() {
            window.location.href = 'login.aspx'
        }

        function runScript(e) {
            //See notes about 'which' and 'key'
            if (e.keyCode == 13) {
                t = 'otp';
                return false;
            }
        }
    </script>

</body>
</html>
