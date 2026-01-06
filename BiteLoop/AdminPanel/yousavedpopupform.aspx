<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yousavedpopupform.aspx.cs" Inherits="controlpanel_YouSavedPopupform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <style>
        .modal-dialog td {
            padding: 0px 8px !important;
        }

        .tblRewards td {
            border: none !important;
        }
    </style>
</head>
<body>
    <div class="modal-dialog" id="divContactUs" runat="server" visible="false">
        <div class="modal-content">
            <div class="modal-header" style="padding-bottom: 0">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&#215;</button>
                <h4 class="modal-title" id="h4head" runat="server" style="text-align: left">Modify Rewards Points</h4>
                <hr />
                  <div id="divRewards" class="red" runat="server">
                    </div>
            </div>
            
            <div class="modal-body table-responsive table-no-border">
                <asp:Repeater ID="rptContactUs" runat="server" Visible="false">
                    <ItemTemplate>
                        <table class="table table-hover table-content popup">
                            <tr>
                                <td valign="top" align="left" width="80">
                                    <b>User</b>
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top"><strong><%#Convert.ToString(Eval("Name")).Trim() != String.Empty ? Convert.ToString(Eval("Name")).Trim() + "( "+ Convert.ToString(Eval("Email")).Trim()+" )" : " - "%></strong></td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table class="tblRewards">
                            <tr>
                                <td>
                                    <input type="text" class="form-control discount txtRewards" id="txtRewards" name="txtRewards" maxlength="10" style="border: 1px solid #bdbdbd !important" value='<%#Convert.ToString(Eval("YouSaved")).Replace(".00","") %>' /></td>
                                <td>
                                    <button type="button" class="btn btn-primary" onclick="UpdateRewards('<%#Eval("ID") %>')">Update</button></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>


            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default btnRewardsPopupClose" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <script type="text/javascript">
        var divRewards = '<%= divRewards.ClientID %>'
        $('#divRewards').hide();
        $(".discount").keydown(function (e) {            
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }

            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        function UpdateRewards(id) {           

            $.ajax({
                url: 'yousavedpopupform.aspx?updateyousaved=y',
                type: 'POST',
                data: { 'id': id, 'meals': jQuery.trim($('.txtRewards').val()) },
                success: function (resp) {
                    DisplMsg(divRewards, '$ Saved has been updated successfully.', 'alert-message success');
                    setTimeout('HidePopup();', '3000');
                    $('.yousaved' + id).text(jQuery.trim($('.txtRewards').val()));
                }
            });
        }

        function HidePopup() {
            $('.btnRewardsPopupClose').click();
            $('#divRewards').hide();
        }
    </script>
</body>
</html>

