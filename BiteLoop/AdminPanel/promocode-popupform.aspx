<%@ Page Language="C#" AutoEventWireup="true" CodeFile="promocode-popupform.aspx.cs" Inherits="controlpanel_popupform_Promocode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <style>
        .modal-dialog td {
            padding: 0px 8px !important;
        }
        #divContactUs td {
            background-color:#fff !important;
        }
    </style>
</head>
<body>
    <div class="modal-dialog" id="divContactUs" runat="server" visible="false">
        <div class="modal-content">
            <div class="modal-header" style="padding-bottom: 0">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&#215;</button>
                <h4 class="modal-title" id="h4head" runat="server" style="text-align: left">Users</h4>
                <hr />
            </div>
            <div class="modal-body table-responsive table-no-border">
                <table class="table table-hover table-content popup">
                    <tr>
                            <th width="20">#</th>
                        <th>Name</th>
                        <th width="120">Used Date</th>
                        <th width="120">Order ID</th>
                    </tr>
                    <asp:Repeater ID="rptContactUs" runat="server" Visible="false">
                        <ItemTemplate>

                            <tr>
                                <td align="center"><%#Container.ItemIndex + 1 %></td>
                                <td valign="top"  align="left">
                                    <%#Eval("UserName") %>   
                                </td>
                                <td valign="top"  align="left">
                                    <%# Convert.ToDateTime(Eval("UsedDate")).ToString("dd/MMM/yyyy") %>   
                                </td>
                                <td valign="top"  align="left">
                                    ORD-<%#Eval("OrderID") %>   
                                </td>
                            </tr>


                        </ItemTemplate>
                    </asp:Repeater>
                    <tr id="tyrNoRecords" runat="server" visible="false">
                        <td colspan="6">No records found</td>
                    </tr>
                </table>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>

</body>
</html>

