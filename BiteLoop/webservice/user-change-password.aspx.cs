using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_user_change_password : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserChangePassword();
        }
    }
    #endregion

    #region Change Password
    private void UserChangePassword()
    {
        Response objResponse = new Response();
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================


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
            string oldpassword = Convert.ToString(Request["OldPassword"]);
            string Password = string.Empty;
            if (!string.IsNullOrEmpty(Request["NewPassword"]))
                Password = Request["NewPassword"];

            UsersBAL objUserBAL = new UsersBAL();
 //objUserBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Password);
            objUserBAL.Password = Password;
            objUserBAL.ID = Convert.ToInt64(Request["UserID"]);

//            int result = objUserBAL.Changepassword(Utility.Security.EncryptDescrypt.EncryptString(oldpassword));
int result = objUserBAL.Changepassword(oldpassword);
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