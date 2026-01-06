using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using DAL;
using System.Data;
using Stripe;
using System.Configuration;
using BiteLoop.Common;

public partial class webservice_user_add_Promocode_Validate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserPromoCodeValidate();
        }
    }

    private void UserPromoCodeValidate()
    {
        Response objResponse = new Response();
        bool IsValidated = false;
               

        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"]), ""))
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
            try
            {
                long UserID = 0;
                if (Request["UserID"] != null)
                {
                    UserID = Convert.ToInt64(Request["UserID"]);
                }

                PromotionalCodeBAL objPromotionalCodeBAL = new PromotionalCodeBAL();

                long result = objPromotionalCodeBAL.UserPromoCodeSave(Convert.ToString(Request["Code"]), UserID);

                

                switch (result)
                {
                    case -1:
                        objResponse.success = "false";
                        objResponse.message = "Invalid promotional code.";
                        break;
                    case -2:
                        objResponse.success = "false";
                        objResponse.message = "This code is expired.";
                        break;                   
                    case 0:
                        objResponse.success = "false";
                        objResponse.message = "Please try after some time.";
                        break;
                    default:                        
                        objResponse.success = "true";
                        objResponse.message = "Valid";
                        break;
                }

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
         

            }
            catch (Exception ex)
            {
                objResponse.success = "false";
                objResponse.message = ex.Message.ToString();
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
              
            }
            HttpContext.Current.Response.End();
        }
        
    }
  
}