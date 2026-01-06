<%@ Page Language="C#" AutoEventWireup="true" CodeFile="refunddetailspopupform.aspx.cs" Inherits="controlpanel_RefundDetailsPopupform" %>

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
                        <h4 style="padding-bottom: 10px; text-align: left">Order Details</h4>
                        <table class="table table-hover table-content popup">
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
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("DiscountValue")).Replace(".00","") %>
                                    <input type="hidden" id="hdnUnitPrice<%#Eval("OrderID") %>" class="hdnUnitPrice<%#Eval("OrderID") %>" value="<%# Convert.ToString(Eval("DiscountValue")).Replace(".00","") %>" />
                                </td>
                            </tr>

                            <tr>
                                <td valign="top" align="left" width="120">Redeem
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("RewardsAmount")).Replace(".00","") %></td>
                                <td valign="top" align="left" width="100">Grand Total
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("GrandTotal")).Replace(".00","") %></td>
                            </tr>

                        </table>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptRefund" runat="server" Visible="false">
                    <ItemTemplate>
                        <br />
                        <h4 style="padding-bottom: 10px; text-align: left">Refund Details</h4>
                        <table class="table table-hover table-content popup">
                            <tr>
                                <td valign="top" align="left" width="120">Stripe Refund ID
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top"><%# Eval("StripeRefundID") %></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="120">Status
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top"><%# RefundStatus%></td>

                            </tr>

                            <tr>
                                <td valign="top" align="left" width="120">Refund Qty
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top"><%# Eval("Qty") %></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="100">Refund Amount
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top">$<%# Convert.ToString(Eval("Amount")).Replace(".00","") %></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="120">Reason
                                </td>
                                <td valign="top" width="10">
                                    <label>:</label></td>
                                <td class="text" align="left" valign="top"><%# RefundReason%></td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default btnRefundPopupClose" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
</body>
</html>

