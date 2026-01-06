using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using System.Data;
using Newtonsoft.Json;

public partial class webservice_user_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessLogout();
        }
    }

    private void BusinessLogout()
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
            UsersBAL objUsersBAL = new UsersBAL();
            objUsersBAL.ID = Convert.ToInt64(Request["UserID"]);
            objUsersBAL.UserLogout();
            objResponse.success = "true";
            objResponse.message = "You are logged out successfully.";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));         
        }
        HttpContext.Current.Response.End();
    }

}