<%@ Page Language="C#" AutoEventWireup="true" CodeFile="refundpopupform.aspx.cs" Inherits="controlpanel_Refundpopupform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <style>
        .modal-dialog td {
            padding: 0px 8px !important;
        }

        .tblRefund td {
            border: none !important;
        }

        .popup {
            font-size: 15px;
        }
    </style>
</head>
<body>
    <div class="modal-dialog" id="divContactUs" runat="server" visible="false">
        <div class="modal-content">
            <div class="modal-header" style="padding-bottom: 0">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&#215;</button>
                <h4 class="modal-title" id="h4head" runat="server" style="text-align: left; font-family: Verdana">Refund - ###OrderID###</h4>
                <hr />
                <div id="divRefund" class="red" runat="server">
                </div>
            </div>

            <div class="modal-body table-responsive table-no-border">
                <asp:Repeater ID="rptRefundDetails" runat="server" Visible="false">
                    <ItemTemplate>
                        <table class="table table-hover table-content popup">
                            <tr>
                                 <td valign="top" align="left" width="120">Order ID
                                </td>
                                 <td valign="top" width="10">
                                    <label>:</label></td>
                                <td colspan="3"><%#Eval("OrderID") %>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="120">No of Serving(s)
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top"><%# Eval("Qty") %></td>
                                <td valign="top" align="left" width="100">Unit Price
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Convert.ToDecimal( Eval("OriginalPrice")) - Convert.ToDecimal( Eval("DiscountValue"))).Replace(".00","") %>
                                    <input type="hidden" id="hdnUnitPrice<%#Eval("OrderID") %>" class="hdnUnitPrice<%#Eval("OrderID") %>" value="<%# Convert.ToString(Convert.ToDecimal( Eval("OriginalPrice")) - Convert.ToDecimal( Eval("DiscountValue"))).Replace(".00","") %>" />
                                </td>
                            </tr>

                            <tr>
                                <td valign="top" align="left" width="120">Redeem
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("RewardsAmount")).Replace(".00","") %></td>
                                <td valign="top" align="left" width="120">Promo Discount
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("APPPromocodeDiscountAmount")).Replace(".00","") %></td>
                            </tr>
                              <tr>
                                
                                <td valign="top" align="left" width="100">Grand Total
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("GrandTotal")).Replace(".00","") %></td>
                                  <td></td>
                                  <td></td>
                                  <td></td>
                            </tr>
                        </table>
                        <br />
                        <table class="tblRefund" width="100%">
                            <tr>
                                <td>
                                    <input type="radio" checked="checked" value="1" name="refund" onclick="ShowPartial('1')" id="FullRefund<%=OrderID %>" />&nbsp;&nbsp;<h4 style="display: inline-block">Full Refund</h4>
                                    <br />
                                    <input type="radio" value="0" name="refund" onclick="ShowPartial('0')" id="PartialRefund<%=OrderID %>" />&nbsp;&nbsp;<h4 style="display: inline-block">Partial Refund </h4>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="padding: 20px 20px 0px 20px; display: none" class="divPartitalRefund divPartitalRefund<%#Eval("OrderID") %>" id="<%#Eval("OrderID") %>">
                                        <label>Select Qty to Refund: </label>
                                        &nbsp;&nbsp;
                                        <asp:Repeater ID="rptQty" runat="server" DataSource='<%#GetQty( Convert.ToInt32(Eval("Qty"))) %>'>
                                            <HeaderTemplate>
                                                <select id="ddlQty<%=OrderID %>" name="ddlQty<%=OrderID %>" style="width: 50px;" class="ddlQty<%=OrderID %>" rel="<%=OrderID %>" onchange="SetRefundAmount(this)">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <option value="<%#Eval("Qty") %>"><%#Eval("Qty") %></option>
                                            </ItemTemplate>
                                            <FooterTemplate></select></FooterTemplate>
                                        </asp:Repeater>
                                        &nbsp;&nbsp;&nbsp;<b>Refund Amount <span id="spnRefundAmount<%#Eval("OrderID") %>">$<%# Convert.ToString(Convert.ToDecimal( Eval("OriginalPrice")) - Convert.ToDecimal( Eval("DiscountValue")) - Convert.ToDecimal( Eval("APPPromocodeDiscountAmount"))).Replace(".00","") %></span></b>
                                        <br />
                                        <br />
                                        <label style="text-transform: uppercase; border: 1px dashed red; padding: 10px; font-weight: 500; font-family: Verdana;">
                                            <b>Note:</b> Maximum <b>$<%# Convert.ToString(Eval("GrandTotal")).Replace(".00","") %></b> can be refund from Stripe. Other amount needs to be managed manually.</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <div style="padding-left: 18px;">
                                        <h4 style="line-height: 25px">Reason: </h4>
                                        <textarea id="RefundReason" name="RefundReason" class="RefundReason" rows="3" cols="10" style="width: 100%" maxlength="150"></textarea>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />

                        <table class="tblRefund">
                            <tr>
                                <td>
                                    <button type="button" class="btn btn-primary btnRefund" onclick="Refund('<%#Eval("OrderID") %>','<%# Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString( ((System.Data.DataRowView) Container.DataItem)["OrderID"] ))%>')">Refund</button>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>


            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default btnRefundPopupClose" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <script type="text/javascript">
        var divRefund = '<%= divRefund.ClientID %>'
        $('#divRefund').hide();

        function SetRefundAmount(ele) {
            var OID = $(ele).attr('rel');
            //var OID = $(ele).parents().parents().find('.divPartitalRefund').attr('id')
            $('#spnRefundAmount' + OID).html('$' + parseFloat($('.hdnUnitPrice' + OID).val()) * parseFloat($(ele).val()));
        }
        function HidePopup() {
            $('.btnRefundPopupClose').click();
            $('#divRefund').hide();
            RefreshOrderReport();
        }
        //=========================

        function Refund(OID, ENID) {

            if (confirm("Are you sure you want to refund selected order?")) {

                $('.btnRefund').html('Processing...');
                $('.btnRefund').attr('disabled', true);
                var RefundType = 'F';
                if ($('#PartialRefund' + OID).prop('checked')) {
                    RefundType = 'P';
                }

                var Qty = $('.ddlQty' + OID).val();


                $.ajax({
                    url: 'refundpopupform.aspx',
                    type: 'POST',
                    data: { 'processrefund': 'y', 'ENID': ENID, 'RefundType': RefundType, 'RefundReason': $('.RefundReason').val(), 'Qty': Qty },
                    success: function (resp) {
                        if (resp == 'succeeded') {
                            DisplMsg(divRefund, 'Refund completed successfully.', 'alert-message success');
                            setTimeout('HidePopup();', '3000');
                            $('.btnRefund').html('Refund');
                            $('.btnRefund').attr('disabled', false);

                        }
                        else {
                            DisplMsg(divRefund, resp, 'alert-message error');
                        }
                    }
                });
            }


        }
        function ShowPartial(type) {
            if (type == '1') {
                $('.divPartitalRefund').hide();
            }
            else {
                $('.divPartitalRefund').show();
            }
        }
    </script>
</body>
</html>

