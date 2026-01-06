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

public partial class webservice_user_mobile_update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserProfileUpdate();
        }
    }

    private void UserProfileUpdate()
    {
        Response objResponse = new Response();
        bool IsValidated = false;        
        string strSuburb = string.Empty;       
        

        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"]), strSuburb))
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
            UserAPIBAL objUserAPIBAL = new UserAPIBAL();
            objUserAPIBAL.ID = Convert.ToInt64(Request["UserID"]);                        
            objUserAPIBAL.Mobile = Convert.ToString(Request["Mobile"]);

            long result = objUserAPIBAL.UserMobileUpdate(Convert.ToString(Request["OTP"]));

            switch (result)
            {              
                case -2 :
                    objResponse.success = "false";
                    objResponse.message = "Invalid OTP.";
                    break;
                default:
                    objResponse.success = "true";
                    objResponse.message = "Mobile has been updated successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
           
        }
        Response.End();
    }
}