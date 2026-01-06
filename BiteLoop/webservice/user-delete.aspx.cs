using System;
using System.Web;
using System.Web.UI;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_user_delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            DeleteUser();
        }
    }

    private void DeleteUser()
    {
        Response objResponse = new Response();
        bool IsValidated = false;

        string strSuburb = Convert.ToString(Request["Suburb"] ?? "");

        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            long userId;

            if (long.TryParse(Request["UserID"], out userId))
            {
                if (ValidateRequestBAL.UserValidateClientRequest(
                        userId,
                        Convert.ToString(Request["SecretKey"]),
                        Convert.ToString(Request["AuthToken"]),
                        strSuburb))
                {
                    IsValidated = true;
                }
            }
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
            HttpContext.Current.Response.End();
            return;
        }

        UsersBAL objUsersBAL = new UsersBAL();
        objUsersBAL.ID = Convert.ToInt64(Request["UserID"]);

        objUsersBAL.Email = "xxxxxxxxxx";
        objUsersBAL.Mobile = "xxxxxxxxxx";
        objUsersBAL.SocialMediaKey = "xxxxxxxxxx";

        objUsersBAL.UpdateUserForDelete();   

        objResponse.success = "true";
        objResponse.message = "User deleted successfully.";

        HttpContext.Current.Response.Write(
            JsonConvert.SerializeObject(
                objResponse,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            )
        );

        HttpContext.Current.Response.End();
    }
}
