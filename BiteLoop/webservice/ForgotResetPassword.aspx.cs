using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using System.Net;
using System.Data;
using System.Net.Mail;
using System.Threading;
using BiteLoop.Common;

public partial class webservice_forgot_ResetPassword : System.Web.UI.Page
{
    #region Parameters
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserForgotPassword();
        }
    }
    #endregion

    #region Send Mobile Code

    private void UserForgotPassword()
    {
        Response objResponse = new Response();
        string Email = "";
        string TempPassword = "";
        string NewPassword = "";

        if (Request["Email"] != null)
        {
            Email = Convert.ToString(Request["Email"]);
        }
        if (Request["TempPassword"] != null)
        {
            TempPassword = Convert.ToString(Request["TempPassword"]);
        }

        if (Request["NewPassword"] != null)
        {
            NewPassword = Convert.ToString(Request["NewPassword"]);
        }


        CommonBAL objCommonBAL = new CommonBAL();

        if (Email == "" || TempPassword == "" || NewPassword == "")
        {
            objResponse.success = "false";
            objResponse.message = "Please enter require fields.";
        }
        else
        {
            long result = objCommonBAL.UserForgotResetPassword(Email, TempPassword, NewPassword);

            switch (result)
            {
                case -1:
                    objResponse.success = "false";
                    objResponse.message = "Invalid Temporary Password.";
                    break;
                default:
                    objResponse.success = "true";
                    objResponse.message = "Password has been changed successfully.";
                    break;
            }
        }
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        HttpContext.Current.Response.End();


    }

    #endregion

}