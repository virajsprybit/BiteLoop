<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetAddress.aspx.cs" Inherits="GetAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="https://maps.google.com/maps/api/js?key=AIzaSyChTix-cmWXRaTRwlwLI92WRfBqlo2xaOU&sensor=false"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset style="width:435px">
            <legend>Fill city,state and country Example</legend>
            <table>  
                <tr>
                    <td>ZipCode</td>
                    <td>
                        <asp:TextBox ID="txtZipCode" runat="server" onblur="getLocation();"></asp:TextBox>
                        <asp:Button ID="btnSubmit" runat="server" Text="Fill city,state,country" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td>City</td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>State</td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Country</td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>                       
                        <asp:HiddenField ID="hdCity" runat="server" Value="" />
                        <asp:HiddenField ID="hdState" runat="server" Value="" />
                        <asp:HiddenField ID="hdCountry" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
          <script language="javascript">
        function getLocation() {
            getAddressInfoByZip(document.forms[0].txtZipCode.value);
        }

        function response(obj) {
            console.log(obj);
        }
        function getAddressInfoByZip(zip)
        {
            debugger;
            if (zip.length >= 3 && typeof google != 'undefined') {
                var addr = {};
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'address': zip }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results.length >= 1) {
                            for (var ii = 0; ii < results[0].address_components.length; ii++) {
                                var street_number = route = street = city = state = zipcode = country = formatted_address = '';
                                var types = results[0].address_components[ii].types.join(",");
                                if (types == "street_number") {
                                    addr.street_number = results[0].address_components[ii].long_name;
                                }
                                if (types == "route" || types == "point_of_interest,establishment") {
                                    addr.route = results[0].address_components[ii].long_name;
                                }
                                if (types == "sublocality,political" || types == "locality,political" || types == "neighborhood,political" || types == "administrative_area_level_3,political") {
                                    addr.city = (city == '' || types == "locality,political") ? results[0].address_components[ii].long_name : city;
                                   
                                    document.getElementById("<%= hdCity.ClientID %>").value = addr.city;
                                }
                                if (types == "administrative_area_level_1,political") {
                                    addr.state = results[0].address_components[ii].short_name;                                           
                                 
                                    document.getElementById("<%= hdState.ClientID %>").value = addr.state;                                    
                                }
                                if (types == "postal_code" || types == "postal_code_prefix,postal_code") {
                                    addr.zipcode = results[0].address_components[ii].long_name;                                  
                                }
                                if (types == "country,political") {
                                    addr.country = results[0].address_components[ii].long_name;

                                    document.getElementById("<%= hdCountry.ClientID %>").value = addr.country;
                                }
                            }
                            addr.success = true;
                            for (name in addr) {
                                console.log('### google maps api ### ' + name + ': ' + addr[name]);
                            }
                            response(addr);
                    
                        } else {
                            response({ success: false });
                        }
                    } else {
                        response({ success: false });
                    }
                });
            } else {
                response({ success: false });
            }       
          }
</script>
    </form>

</body>
</html>
