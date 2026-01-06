<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="business-lat-long-update.aspx.cs" Inherits="AdminPanel_business_lat_long_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        .wronglatlong td
        {
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-6">
            <div class="panel">
                <div class="panel-body well-lg">
                    <div class="form-group">
                        <label>Business ID</label>
                        <select id="ddlBusiness" name="ddlBusiness" class="drp-border form-control ddlBusiness" runat="server" style="width: 185px"></select>
                    </div>


                    <div class="form-group">
                        <label>Latitude</label>
                        <input type="text" id="txtLatitude" name="txtLatitude" class="form-control txtLatitude" />
                    </div>
                    <div class="form-group">
                        <label>Longitude</label>
                        <input type="text" id="txtLongitude" name="txtLongitude" class="txtLongitude form-control" />
                    </div>
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm();">Update Lat Long</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div id="DivRender">
                <div id="divList" runat="server">
                    <table class="dataTable table table-striped table-hover table-bordered custom-check">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Vendor Name</th>
                                <th>Email Address</th>
                                <th>Full Name</th>                                
                                <th>Business Phone</th>
                                <th>Registered Date</th>
                                <th>Last Active Date Time</th>
                                <th>Latitude</th>
                                <th>Longitude</th>
                                
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptRecord">
                                <ItemTemplate>
                                    <tr class="<%# ( Convert.ToString(Eval("Longitude")).IndexOf(".") < 0  &&  Convert.ToString(Eval("Longitude")) != "" ) ||  (Convert.ToString(Eval("Latitude")).IndexOf(".") < 0 &&  Convert.ToString(Eval("Latitude")) != "" )  ? "wronglatlong" : ""%>">
                                          <td><%# ((System.Data.DataRowView)Container.DataItem)["ID"]%></td>
                                        <td><%# ((System.Data.DataRowView)Container.DataItem)["Name"]%></td>
                                        <td><%# ((System.Data.DataRowView)Container.DataItem)["EmailAddress"]%></td>
                                        <td><%# ((System.Data.DataRowView)Container.DataItem)["FullName"]%></td>                                        
                                        <td><%# ((System.Data.DataRowView)Container.DataItem)["BusinessPhone"]%></td>
                                        <td><%#Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedOn"]).ToString("dd/MMM/yyyy")%></td>
                                        <td><%#Convert.ToString(Eval("LastLoginDateTime")) != string.Empty ? Convert.ToDateTime(Eval("LastLoginDateTime")).ToString("dd/MMM/yyyy hh:mm tt") : "" %></td>                                        
                                        <td><%# ((System.Data.DataRowView)Container.DataItem)["Latitude"]%></td>
                                        <td><%# ((System.Data.DataRowView)Container.DataItem)["Longitude"]%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr runat="server" id="trNoRecords" visible="false">
                                <td colspan="6" align="center" style="text-align: center;">
                                    <b>No Records Exists.</b>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </div>


            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">

        function ValidateForm() {
            if (jQuery.trim($('.ddlBusiness').val()) > 0 && jQuery.trim($('.txtLatitude').val()) != '' && jQuery.trim($('.txtLongitude').val()) != '') {

                $.ajax({
                    url: 'business-lat-long-update.aspx',
                    type: 'POST',
                    data: { 'updatelatlong': 'y', 'id': jQuery.trim($('.ddlBusiness').val()), 'lat': jQuery.trim($('.txtLatitude').val()), 'long': jQuery.trim($('.txtLongitude').val()) },
                    success: function (resp) {
                        $('.ddlBusiness').val('0');
                        $('.txtLatitude').val('');
                        $('.txtLongitude').val('');
                        alert('Updated Successfully.');
                    }
                });
            }
            else {

                alert("Please select all values");
            }
        }
    </script>
</asp:Content>

