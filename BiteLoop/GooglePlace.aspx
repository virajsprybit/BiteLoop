<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GooglePlace.aspx.cs" Inherits="GooglePlace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td>Location</td>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server" Width="200" /></td>
            </tr>
            <tr>
                <td>Address</td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Width="200" /></td>
            </tr>
            <tr>
                <td>Latitude</td>
                <td>
                    <asp:TextBox ID="txtLatitude" runat="server" Width="200" /></td>
            </tr>
            <tr>
                <td>Longitude</td>
                <td>
                    <asp:TextBox ID="txtLongitude" runat="server" Width="200" /></td>
            </tr>
             <tr>
                <td>State</td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" Width="200" /></td>
            </tr>
        </table>
        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places&key=<%=GoogleMapApiKey %>"></script>
        <script type="text/javascript">
            google.maps.event.addDomListener(window, 'load', function () {
                var places = new google.maps.places.Autocomplete(document.getElementById('<%=txtLocation.ClientID %>'));
                google.maps.event.addListener(places, 'place_changed', function () {
                    var place = places.getPlace();
                    console.log(place.address_components);
                    console.log(location);
                    document.getElementById('<%=txtAddress.ClientID %>').value = place.formatted_address;
                    document.getElementById('<%=txtLatitude.ClientID %>').value = place.geometry.location.lat();
                    document.getElementById('<%=txtLongitude.ClientID %>').value = place.geometry.location.lng();



                    for (var ac = 0; ac < place.address_components.length; ac++) {
                        debugger;
                        var component = place.address_components[ac];

                        switch (component.types[0]) {                            
                            case 'administrative_area_level_1':
                                document.getElementById('<%=txtState.ClientID %>').value = component.short_name;
                                break;                            
                        }
                    };


                });
            });
        </script>
    </form>
</body>
</html>
