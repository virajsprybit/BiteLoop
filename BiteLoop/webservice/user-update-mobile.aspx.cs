using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_user_update_Mobile : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserUpdateMobile();
        }
    }
    #endregion

    #region User UpdateMobile
    private void UserUpdateMobile()
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
            string strMobile = Convert.ToString(Request["Mobile"]);
            
            UsersBAL objUserBAL = new UsersBAL();             
            objUserBAL.ID = Convert.ToInt64(Request["UserID"]);

            objUserBAL.UpdateMobile(strMobile);
            objResponse.success = "true";
            objResponse.message = "Mobile has been updated successfully.";           
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