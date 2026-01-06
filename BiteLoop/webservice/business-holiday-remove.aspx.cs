using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using BiteLoop.Common;

public partial class webservice_Business_Holiday_remove : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            RemoveBusinessHoliday();
        }
    }
    #endregion

    #region Business Holiday
    private void RemoveBusinessHoliday()
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
            
            CommonBAL objCommonBAL = new CommonBAL();
            long result = objCommonBAL.BusinessHolidayRemove_Webservice(Convert.ToInt64(Request["BusinessID"]), Convert.ToDateTime(Request["HolidayDate"]));


            if (result >= 0)
            {
                objResponse.success = "true";
                objResponse.message = "Holiday removed successfully.";
            }
            else
            {
                objResponse.success = "false";
                objResponse.message = "Holiday information not removed. Please try after some time.";
            }
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        }

        Response.End();
    }
    #endregion

    #region Json Response
    public string SetResponse(string strResponse, int Status)
    {
        Response objResponse = new Response();
        if (Status == 0)
        {
            objResponse.success = "false";
        }
        else
        {
            objResponse.success = "true";
        }

        objResponse.message = strResponse;

        return JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    }
    #endregion
}