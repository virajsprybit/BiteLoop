using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_business_restaurant_save : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessRestaurantUpdate();
        }
    }

    private void BusinessRestaurantUpdate()
    {
        Response objResponse = new Response();
        bool IsValidated = false;
        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"])))
            {
                IsValidated = true;
            }
        }
        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(Request["BusinessID"]);
            if (Convert.ToString(Request["RestaurantTypes"]) != null)
            {
                objBusinessBAL.RestaurantTypes = Convert.ToString(Request["RestaurantTypes"]);
            }
            else
            {
                objBusinessBAL.RestaurantTypes = "";
            }

            objBusinessBAL.BusinessRestaurantUpdate();
            objResponse.success = "true";
            objResponse.message = "Restaurant Details has been updated successfully.";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        } Response.End();
    }
}