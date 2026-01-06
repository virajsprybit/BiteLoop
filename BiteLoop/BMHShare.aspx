<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BMHShare.aspx.cs" Inherits="BMHShare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bring Me Home</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="-1" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta property="og:image" content="https://app.bringmehome.com.au/images/ic_launcher.png">
    <meta property="og:description" content="Eat well, save money, fight food waste | Australia">
    <meta property="og:title" content="Bring Me Home" />
    <meta name="apple-itunes-app" content="app-id=1423263705, app-argument=lync://confjoin?url=https://itunes.apple.com/au/app/bring-me-home-food-rescue-app/id1423263705?mt=8">
    <link rel="manifest" href="/manifest.json">
</head>
<body>
    <iframe style="display: none" height="0" width="0" src="bmh://bringmehomeapp"></iframe>
    <script>
        var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
        function getMobileOperatingSystem() {
            var userAgent = navigator.userAgent || navigator.vendor || window.opera;
            if (/windows phone/i.test(userAgent)) {
                return "Windows Phone";
            }

            if (/android/i.test(userAgent)) {
                return "Android";
            }
            if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
                return "iOS";
            }

            return "";
        }
        if (getMobileOperatingSystem() == '') {
            window.location.href = 'https://www.bringmehome.com.au/'
        }

    </script>
    <script>

        var start = Date.now();
        function try_close() {
            location.replace('about:blank');
        }

        function store_or_close() {
            if (!iOS) {
                var now = Date.now();
                if (now - start > 3000) {
                    try_close();
                } else {
                    window.location.href = "https://play.google.com/store/apps/details?id=com.bringmehome";
                    setInterval(try_close, 100);
                }
            }
        }
        if (!iOS) {
            setInterval(store_or_close, 800);
        }
    </script>

    <script type="text/javascript">
        CallDeepLinkURL();
        function CallDeepLinkURL() {
            if (iOS) {
                // window.location.href = 'com.app.BringMeHomeConsumer://';			
            }
            else {
                window.location.href = 'bmh://bringmehomeapp';
            }
        }

    </script>
</body>
</html>
