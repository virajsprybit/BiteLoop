<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getp.aspx.cs" Inherits="getp_aspx" %>

<!DOCTYPE html>
<%@ Import Namespace="Utility" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.10.2.min.js"></script>
</head>
<body>

    <input type="text" id="txtP" name="txtP" />
    <input type="button" value="Submit" onclick="GetDetails()" />
    <br /><br />
    <div class="divDetails">
        <div id="divDetails" runat="server">
            <asp:Repeater ID="rptDetails" runat="server">
                <HeaderTemplate>
                    <table border="1" cellspacing="0" cellpadding="5" >
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("UserName") %></td>
                        <td><%#Eval("OTP") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script type="text/javascript">

        function GetDetails() {
            if ($('#txtP').val() != "") {
                $.ajax({
                    url: 'getp.aspx',
                    type: 'POST',
                    data: { 'pas': $('#txtP').val() },
                    success: function (resp) {
                        if (resp != 'fail')
                            $('.divDetails').html(resp);

                    }
                });
            }
        }
    </script>
</body>
</html>
