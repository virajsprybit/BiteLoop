using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_business_change_password : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessChangePassword();
        }
    }
    #endregion

    #region Change Password
    private void BusinessChangePassword()
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
            string oldpassword = Convert.ToString(Request["OldPassword"]);
            string Password = string.Empty;
            if (!string.IsNullOrEmpty(Request["NewPassword"]))
                Password = Request["NewPassword"];

            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Password);
            objBusinessBAL.ID = Convert.ToInt64(Request["UserID"]);

            int result =  objBusinessBAL.Changepassword(Utility.Security.EncryptDescrypt.EncryptString(oldpassword));
            switch (result)
            {
                case 0:
                    objResponse.success = "true";
                    objResponse.message = "Your password has been changed successfully.";                    
                    break;
                default:
                    objResponse.success = "false";
                    objResponse.message = "Current Password you have entered is invalid.";
                    break;
            }
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));        
        }
        
        Response.End();
    }
    #endregion

    
}