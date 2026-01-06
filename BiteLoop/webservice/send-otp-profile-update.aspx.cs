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
using System.Net;
using System.IO;
using BiteLoop.Common;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Configuration;

public partial class webservice_Send_OTP_ProfileUpdate : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            SendOTP();
        }
    }
    private void SendOTP()
    {
        if (Request["Mobile"] != null)
        {
            SendOTPToMobile(Convert.ToString(Request["Mobile"]));
        }
        else
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        HttpContext.Current.Response.End();
    }

    #region SendOTP
    public string SendSMS(string ToMobileNo, string Message)
    {
        string strResponse = "success";
        try
        {
            if (ToMobileNo.Trim() != string.Empty)
            {
                string accountSid = "AC934ac048716d104d44ba10ce6382e312";
                string authToken = "269e29fd63ea6abe925f1c8f091f1be1";

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                TwilioClient.Init(accountSid, authToken);
                string FromNo = Convert.ToString(ConfigurationManager.AppSettings["TwillioFromNo"]);
                var message = MessageResource.Create(
                    body: Message,
                    from: new Twilio.Types.PhoneNumber(FromNo),
                    to: new Twilio.Types.PhoneNumber(ToMobileNo)
                );
                strResponse = "success";
            }
        }
        catch (Exception ex)
        {
        }

        return strResponse;
    }


    private void SendOTPToMobile(string strMobile)
    {
        try
        {
            Response objResponse = new Response();
            int Code = GenerateOTP(strMobile, "-1");

            if (Code == -1)
            {
                objResponse.success = "false";
                objResponse.message = "Invalid Mobile.";
            }
            else
            {
                string strMessage = "Bring Me Home OTP is " + Code + ".";

                string Result = SendSMS(strMobile, strMessage);

                if (Result == "success")
                {
                    objResponse.success = "true";
                    objResponse.message = "OTP Sent successfully.";
                }
                else
                {
                    objResponse.success = "false";
                    objResponse.message = Result;
                }
            }
            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            HttpContext.Current.Response.Write(strResponseName);

        }
        catch (Exception ex)
        {


        }

    }

    #endregion

    public int GenerateOTP(string Mobile, string Email)
    {
        int returnVal = 0;
        try
        {
            UserAPIBAL objUserAPIBAL = new UserAPIBAL();
            returnVal = objUserAPIBAL.GenerateOTP(Mobile, Email);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return returnVal;
    }


}