using System;
using System.Web;
using System.Web.UI;
using BAL;
using Utility;
using Newtonsoft.Json;
using BiteLoop.Common;
using System.Configuration;


public partial class webservice_user_profile_update : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }
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

        bool isValidated = ValidateRequestBAL.UserValidateClientRequest(
            Convert.ToInt64(Request["UserID"]),
            Convert.ToString(Request["SecretKey"]),
            Convert.ToString(Request["AuthToken"]),
            Convert.ToString(Request["Suburb"])
        );

        if (!isValidated)
        {
            new CommonAPI().Unauthorized();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return;
        }

        long userId = Convert.ToInt64(Request["UserID"]);
        string newEmail = Convert.ToString(Request["EmailAddress"]) ?? "";
        string otp = Convert.ToString(Request["OTP"]);
        string profilePhoto = Convert.ToString(Request["ProfilePhoto"]);

        string oldEmail = UserAPIBAL.GetEmailByID(userId) ?? "";
        bool emailChanged = !string.Equals(newEmail, oldEmail, StringComparison.OrdinalIgnoreCase);

        if (emailChanged)
        {
            string expectedEmail = Convert.ToString(HttpRuntime.Cache.Get("EMAIL_CHANGE_" + userId));

            if (expectedEmail == null)
            {
                objResponse.success = "false";
                objResponse.message = "No email change request found. Please request OTP again.";
                WriteResponse(objResponse);
                return;
            }

            if (!string.Equals(expectedEmail, newEmail, StringComparison.OrdinalIgnoreCase))
            {
                objResponse.success = "false";
                objResponse.message = "OTP was sent for a different email.";
                WriteResponse(objResponse);
                return;
            }
            if (string.IsNullOrEmpty(otp))
            {
                objResponse.success = "false";
                objResponse.message = "OTP is required when changing email.";
                WriteResponse(objResponse);
                return;
            }

            if (!UserAPIBAL.IsValidOTP(userId, otp))
            {
                objResponse.success = "false";
                objResponse.message = "Incorrect OTP.";
                WriteResponse(objResponse);
                return;
            }
        }

        string profilePhotoFileName = null;

        if (Request.Files.Count > 0 && Request.Files["ProfilePhoto"] != null)
        {
            HttpPostedFile file = Request.Files["ProfilePhoto"];

            if (file.ContentLength > 0)
            {
                string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                string newFileName = "PROFILE_" + userId + "_" + DateTime.Now.Ticks + ext;

                string savePath = Server.MapPath("~/source/CMSFiles/");

                if (!System.IO.Directory.Exists(savePath))
                    System.IO.Directory.CreateDirectory(savePath);

                string fullPath = System.IO.Path.Combine(savePath, newFileName);
                file.SaveAs(fullPath);

                profilePhotoFileName = newFileName;  
            }
        }

        int loginType = UserAPIBAL.GetLoginType(userId);

        string password = null;

        if (loginType == 0) 
        {
            password = Convert.ToString(Request["Password"]);
        }

        UserAPIBAL user = new UserAPIBAL
        {
            ID = userId,
            Name = Convert.ToString(Request["FullName"]),
            LastName = Convert.ToString(Request["LastName"]),
            Email = newEmail,
            Mobile = Convert.ToString(Request["Mobile"]),
            PostCode = Convert.ToString(Request["PostCode"]),
            Password = password,
            ProfilePhoto = profilePhotoFileName
        };

        long result = user.UserProfileUpdate();

        if (result == -1)
        {
            objResponse.success = "false";
            objResponse.message = "Duplicate Email Address found.";
            WriteResponse(objResponse);
            return;
        }
       
        objResponse.success = "true";
        objResponse.message = "Profile updated successfully.";
        objResponse.StatusCode = "200";


        string baseUrl = ConfigurationManager.AppSettings["WebSiteUrl"] + "source/CMSFiles/";

        objResponse.UserLogin = new[]
        {
            new UserLogin
            {
                UserID = userId,
                FullName = Convert.ToString(Request["FullName"]),
                LastName = Convert.ToString(Request["LastName"]),
                EmailAddress = newEmail,
                Mobile = Convert.ToString(Request["Mobile"]),
                SecretKey = Convert.ToString(Request["SecretKey"]),
                AuthToken = Convert.ToString(Request["AuthToken"]),
                PostCode = Convert.ToString(Request["PostCode"]),
            
                ProfilePhoto = string.IsNullOrEmpty(profilePhotoFileName)
                    ? null
                    : baseUrl + profilePhotoFileName,
            
                StateID = 0,
                IsSignupByFacebook = loginType
            }
        };

        WriteResponse(objResponse);
    }

    private void WriteResponse(object obj)
    {
        string json = JsonConvert.SerializeObject(
            obj,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
        );

        json = json.Replace("\"UserLogin\"", "\"data\"");

        Response.ContentType = "application/json";
        Response.Write(json);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}
