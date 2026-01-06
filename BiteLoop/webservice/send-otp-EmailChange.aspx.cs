using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using BAL;
using Utility;
using BiteLoop.Common;
using System.Web;
using Newtonsoft.Json;

public partial class webservice_Send_OTP_EmailChange : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer) { }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod == "POST")
        {
            SendOTP();
        }
        else
        {
            Response.StatusCode = 405;
            Response.End();
        }
    }

    private void SendOTP()
    {
        Response.ContentType = "application/json";

        long userId = Convert.ToInt64(Request["UserID"]);
        string email = Convert.ToString(Request["Email"]);
        string secretKey = Convert.ToString(Request["SecretKey"]);
        string authToken = Convert.ToString(Request["AuthToken"]);
        

        bool isValidated = ValidateRequestBAL.UserValidateClientRequest(
            userId,
            secretKey,
            authToken
        
        );

        if (!isValidated)
        {
            new CommonAPI().Unauthorized();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return;
        }

        if (string.IsNullOrEmpty(email))
        {
            WriteResponse(new
            {
                success = "false",
                message = "Email is required.",
                StatusCode = "400",
                data = new object[] { }
            }, 400);
            return;
        }

        if (UserAPIBAL.IsDuplicateEmailForEmailChange(email, userId))
        {
            WriteResponse(new
            {
                success = "false",
                message = "Email already exists.",
                StatusCode = "409",
                data = new object[] { }
            }, 409);
            return;
        }

        string otp = new Random().Next(100000, 999999).ToString();

        UserAPIBAL.SaveOTPForEmailChange(userId, otp);

        HttpRuntime.Cache.Insert(
            "EMAIL_CHANGE_" + userId,
                               email,
                                null,
          DateTime.Now.AddMinutes(10),
          System.Web.Caching.Cache.NoSlidingExpiration
        );

        SendEmail(email, otp);

        WriteResponse(new
        {
            success = "true",
            message = "OTP sent to your new email.",
            StatusCode = "200",
            data = new[]
            {
                new { OTP = otp }
            }
        }, 200);
    }

    private void SendEmail(string email, string otp)
    {
        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = objEmail.GetByID(3, 1);

        if (dt.Rows.Count > 0)
        {
            string subject = Convert.ToString(dt.Rows[0]["Subject"]);
            string template = Convert.ToString(dt.Rows[0]["Template"]);

            template = template.Replace("###Password###", otp);
            template = template.Replace("###SiteName###", Config.WebsiteName);

            GeneralSettings.SendEmail(
                email,
                new GeneralSettings().getConfigValue("abnno"),
                subject,
                template
            );
        }
    }

    private void WriteResponse(object obj, int statusCode = 200)
    {
        Response.StatusCode = statusCode;
        Response.Write(
            JsonConvert.SerializeObject(obj,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
        );
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}